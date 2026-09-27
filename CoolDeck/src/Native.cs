// CoolDeck — native DCHU protocol layer.
//
// Binds InsydeDCHU.dll dynamically (LoadLibrary + GetProcAddress) so the app can
// pick the richest available copy of the vendor DLL at runtime and report a clean
// diagnostic when none is present, instead of failing with a bare DllNotFoundException.
//
// Everything below was reverse-engineered from the stock CLEVO "Fan Speed Setting"
// app (CLEVOCO.504814C03D814) and verified live against AcpiBridge.sys / EC firmware
// 07.02 on a DEVIL RAYS DR521 (Clevo P955ET1 board). No admin rights required.

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace CoolDeck
{
    internal static class Native
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibraryW(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("psapi.dll", SetLastError = true)]
        private static extern bool EmptyWorkingSet(IntPtr hProcess);

        /// <summary>Drop the process working set after moving to the tray: page out
        /// everything not actively touched so Task Manager stops reporting 80+ MB for a
        /// hidden monitor. The pages fault straight back on the next Show — this trades
        /// a few ms of re-fault latency for a small resident footprint, which is the
        /// right trade exactly when nobody can see the window. Never call on a timer:
        /// periodic trimming fights the working-set manager and only manufactures
        /// soft page faults.</summary>
        public static void TrimWorkingSet()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                using (var p = System.Diagnostics.Process.GetCurrentProcess())
                    EmptyWorkingSet(p.Handle);
                Trace.W("working set trimmed");
            }
            catch { }
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int D_GetInt(int cmd, ref int val);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int D_SetData(int cmd, byte[] buf, int len);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int D_GetBuf(int cmd, ref byte buf);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int D_Read(int hi, int lo, int len, ref byte buf);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int D_Write(int hi, int lo, int len, ref byte buf);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int D_SetEx(int cmd, byte[] inb, int len, ref byte outb);

        private static IntPtr _lib;
        private static D_GetInt _getInt;
        private static D_SetData _setData;
        private static D_GetBuf _getBuf;
        private static D_Read _read;
        private static D_Write _write;
        private static D_SetEx _setEx;

        public static bool Ready { get { return _lib != IntPtr.Zero; } }
        public static string LoadedFrom { get; private set; }
        public static bool HasEx { get { return _setEx != null; } }

        // Candidate locations, richest copy first: the App build exports
        // SetDCHU_DataEx, the service build does not.
        public static string[] Candidates()
        {
            var local = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "CoolDeck", "lib");
            var list = new System.Collections.Generic.List<string>();

            string beside = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InsydeDCHU.dll");
            if (File.Exists(beside)) list.Add(beside);
            list.Add(Path.Combine(local, "InsydeDCHU.dll"));

            // Installed CLEVO control-centre packages.
            string[] roots = {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "WindowsApps"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "ControlCenter"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "ControlCenter3.0"),
            };
            foreach (var r in roots)
            {
                try
                {
                    if (!Directory.Exists(r)) continue;
                    foreach (var f in Directory.GetFiles(r, "InsydeDCHU.dll", SearchOption.AllDirectories))
                        if (new FileInfo(f).Length > 3 * 1024 * 1024) list.Add(f);
                }
                catch { /* WindowsApps is ACL-locked for enumeration; skip */ }
            }
            return list.ToArray();
        }

        public static string Init()
        {
            foreach (var path in Candidates())
            {
                IntPtr h = LoadLibraryW(path);
                if (h == IntPtr.Zero) continue;

                _getInt = Bind<D_GetInt>(h, "GetDCHU_Data_Integer");
                _setData = Bind<D_SetData>(h, "SetDCHU_Data");
                _getBuf = Bind<D_GetBuf>(h, "GetDCHU_Data_Buffer");
                _read = Bind<D_Read>(h, "ReadAppSettings");
                _write = Bind<D_Write>(h, "WriteAppSettings");
                _setEx = Bind<D_SetEx>(h, "SetDCHU_DataEx");

                if (_getBuf != null && _setData != null && _read != null && _write != null)
                {
                    _lib = h;
                    LoadedFrom = path;
                    return path;
                }
                FreeLibrary(h);
                _getInt = null; _setData = null; _getBuf = null;
                _read = null; _write = null; _setEx = null;
            }
            return null;
        }

        private static T Bind<T>(IntPtr h, string name) where T : class
        {
            IntPtr p = GetProcAddress(h, name);
            if (p == IntPtr.Zero) return null;
            return Marshal.GetDelegateForFunctionPointer(p, typeof(T)) as T;
        }

        // ---------- thin typed wrappers ----------

        public static int GetInt(int cmd)
        {
            int v = 0;
            return _getInt == null ? -1 : _getInt(cmd, ref v);
        }

        public static int GetInt(int cmd, out int val)
        {
            int v = 0;
            int r = _getInt == null ? -1 : _getInt(cmd, ref v);
            val = v;
            return r;
        }

        public static byte[] GetBuffer(int cmd)
        {
            byte[] b = new byte[256];
            if (_getBuf == null) return b;
            _getBuf(cmd, ref b[0]);
            return b;
        }

        /// <summary>DCHU command: 4-byte payload = (sub &lt;&lt; 24) | value.</summary>
        public static int SetDchu(int cmd, int sub, int value)
        {
            if (_setData == null) return -1;
            uint p = ((uint)sub << 24) | (uint)value;
            byte[] b = BitConverter.GetBytes(p);
            return _setData(cmd, b, 4);
        }

        public static int SetDchuRaw(int cmd, byte[] payload)
        {
            return _setData == null ? -1 : _setData(cmd, payload, payload.Length);
        }

        public static int ReadStore(int hi, int lo, int len, byte[] buf)
        {
            if (_read == null || buf == null || buf.Length == 0) return -1;
            return _read(hi, lo, len, ref buf[0]);
        }

        public static byte[] ReadStore(int hi, int lo, int len)
        {
            byte[] b = new byte[len];
            ReadStore(hi, lo, len, b);
            return b;
        }

        public static int WriteStore(int hi, int lo, byte[] buf)
        {
            if (_write == null || buf == null || buf.Length == 0) return -1;
            return _write(hi, lo, buf.Length, ref buf[0]);
        }

        public static byte[] SetEx(int sub, byte magic, int len = 256)
        {
            byte[] o = new byte[256];
            if (_setEx == null) return o;
            byte[] i = new byte[256];
            i[0] = 1; i[1] = (byte)sub; i[6] = magic;
            _setEx(4, i, len, ref o[0]);
            return o;
        }

        public static string Ansi(byte[] b, int off, int max)
        {
            int n = 0;
            while (n < max && off + n < b.Length && b[off + n] != 0) n++;
            return Encoding.ASCII.GetString(b, off, n);
        }
    }
}
