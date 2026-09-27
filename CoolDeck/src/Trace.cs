// CoolDeck — crash tracing. Writes an append-only log next to the exe so a hard
// crash (access violation on the render thread) still leaves a breadcrumb.

using System;
using System.IO;

namespace CoolDeck
{
    public static class Trace
    {
        static readonly object Gate = new object();
        static string _path;

        public static void Init()
        {
            try
            {
                _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cooldeck.log");
                lock (Gate) File.AppendAllText(_path,
                    "\n==== " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ====\n");
            }
            catch { _path = null; }
        }

        public static void W(string fmt, params object[] a)
        {
            if (_path == null) return;
            try
            {
                string s = a == null || a.Length == 0 ? fmt : string.Format(fmt, a);
                lock (Gate) File.AppendAllText(_path, DateTime.Now.ToString("HH:mm:ss.fff") + "  " + s + "\n");
            }
            catch { }
        }
    }
}
