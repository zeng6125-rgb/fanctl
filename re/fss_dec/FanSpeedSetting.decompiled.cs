using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using Microsoft.Win32;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: AssemblyTitle("Fan Speed Setting")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Fan Speed Setting")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("7.87.0.0")]
[assembly: AssemblyAssociatedContentFile("insydedchu.dll")]
[assembly: TargetFramework(".NETFramework,Version=v4.7.2", FrameworkDisplayName = ".NET Framework 4.7.2")]
[assembly: SuppressIldasm]
[assembly: AssemblyVersion("7.87.0.0")]
internal interface \u0002
{
	bool \u0002\u2009\u2005\u2006\u0003();

	object \u0002\u2009\u2005\u2006\u0003();

	void \u0002\u2009\u2005\u2006\u0003();
}
internal static class \u0002\u0005
{
	private enum \u0003
	{

	}

	private sealed class \u0006
	{
		private Stream m_\u0003;

		private byte[] m_\u0006;

		public \u0006(Stream \u0003)
		{
			this.m_\u0003 = \u0003;
			m_\u0006 = new byte[4];
		}

		public Stream \u0003()
		{
			return this.m_\u0003;
		}

		public short \u0003()
		{
			\u0003(2);
			return (short)(m_\u0006[0] | (m_\u0006[1] << 8));
		}

		public int \u0003()
		{
			\u0003(4);
			return m_\u0006[0] | (m_\u0006[1] << 8) | (m_\u0006[2] << 16) | (m_\u0006[3] << 24);
		}

		private static void \u0003()
		{
			throw new EndOfStreamException();
		}

		private void \u0003(int \u0003)
		{
			int num = 0;
			int num2 = 0;
			if (\u0003 == 1)
			{
				num2 = this.m_\u0003.ReadByte();
				if (num2 == -1)
				{
					\u0002\u0005.\u0006.\u0003();
				}
				m_\u0006[0] = (byte)num2;
				return;
			}
			do
			{
				num2 = this.m_\u0003.Read(m_\u0006, num, \u0003 - num);
				if (num2 == 0)
				{
					\u0002\u0005.\u0006.\u0003();
				}
				num += num2;
			}
			while (num < \u0003);
		}

		public void \u0003()
		{
			Stream stream = this.m_\u0003;
			this.m_\u0003 = null;
			stream?.Close();
			m_\u0006 = null;
		}

		public byte[] \u0003(int \u0003)
		{
			if (\u0003 < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			byte[] array = new byte[\u0003];
			int num = 0;
			do
			{
				int num2 = this.m_\u0003.Read(array, num, \u0003);
				if (num2 == 0)
				{
					break;
				}
				num += num2;
				\u0003 -= num2;
			}
			while (\u0003 > 0);
			if (num != array.Length)
			{
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}
	}

	private static byte[] \u0005;

	private static \u0006 m_\u0006;

	private static byte[] \u000f;

	private static int \u000e;

	private static int \u0008;

	private static ConcurrentDictionary<int, string> m_\u0003;

	private static int \u0003\u2002;

	private static short \u0002;

	private static \u0003 \u0006\u2002;

	[MethodImpl(MethodImplOptions.NoInlining)]
	static \u0002\u0005()
	{
		int num = -994358595;
		int num2 = num ^ 0x6A6E26D3;
		\u0002\u0005.m_\u0003 = new ConcurrentDictionary<int, string>();
		int num3 = 2;
		StackTrace stackTrace = new StackTrace(num3, fNeedFileInfo: false);
		num3 -= 2;
		StackFrame frame = stackTrace.GetFrame(num3);
		int num4 = num3;
		if (frame == null)
		{
			stackTrace = new StackTrace();
			num4 = 1;
			frame = stackTrace.GetFrame(num4);
		}
		int num5 = -(~(~(-(~(-(~(-(~((-1623119531 + num) ^ num2))))))))) ^ -(~(-(~(~(-(-(~(~((num ^ 0x58A04436) - num2)))))))));
		MethodBase methodBase = frame?.GetMethod();
		if (frame != null)
		{
			num5 ^= ~(-(-(~(~(-(-(~(~(-(~(53206605 - num + num2)))))))))));
		}
		Type type = methodBase?.DeclaringType;
		if (type == typeof(RuntimeMethodHandle))
		{
			\u0006\u2002 = (\u0003)4 | \u0006\u2002;
			num5 ^= (0x6A6E25F4 ^ num ^ num2) + num3;
		}
		else if (type == null)
		{
			if (\u0003(stackTrace, num4))
			{
				num5 ^= -(~(~(-(~(-(~(-(-(~(~(367362153 - num + num2))))))))))) - num3;
				\u0006\u2002 |= (\u0003)16;
			}
			else
			{
				num5 ^= -(~(-(~(-(~(~(-(~((0x6A6F9104 ^ num) - num2)))))))));
				\u0006\u2002 = (\u0003)1 | \u0006\u2002;
			}
		}
		else
		{
			num5 ^= ~(-(-(~(-(~(~(-(~(-(~(-1938887953 + num + num2))))))))))) - num3;
			\u0006\u2002 = (\u0003)16 | \u0006\u2002;
		}
		\u0003\u2002 += num5;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	internal static string \u0003(int \u0003)
	{
		if (\u0002\u0005.m_\u0003.TryGetValue(\u0003, out var value))
		{
			return value;
		}
		return \u0002\u0005.\u0003(\u0003, \u0006: true);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private static string \u0003(int \u0003, bool \u0006)
	{
		int num = 1602640953;
		int num2 = num ^ -24332008;
		string value = null;
		byte[] array;
		int num19;
		int num20;
		int num21;
		int num22;
		byte[] array4;
		byte[] array3;
		int num23;
		while (true)
		{
			lock (\u0002\u0005.m_\u0003)
			{
				int num7;
				if (\u0002\u0005.m_\u0006 == null)
				{
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					Assembly callingAssembly;
					try
					{
						callingAssembly = Assembly.GetCallingAssembly();
					}
					catch (PlatformNotSupportedException)
					{
						callingAssembly = executingAssembly;
					}
					\u0008 |= -1097594470 - num + num2;
					StringBuilder stringBuilder = new StringBuilder();
					int num3 = (-1634903779 ^ num) - num2;
					stringBuilder.Append((char)(num3 >> 16)).Append((char)num3);
					num3 = (-527425186 - num) ^ num2;
					stringBuilder.Append((char)(num3 >> 16)).Append((char)num3);
					num3 = 527883947 + num + num2;
					stringBuilder.Append((char)(num3 >> 16)).Append((char)num3);
					num3 = (-527949474 - num) ^ num2;
					stringBuilder.Append((char)num3).Append((char)(num3 >> 16));
					num3 = 1636477164 + num - num2;
					stringBuilder.Append((char)(num3 >> 16)).Append((char)num3);
					num3 = (0x217966E6 ^ num) + num2;
					stringBuilder.Append((char)num3).Append((char)(num3 >> 16));
					Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(stringBuilder.ToString());
					int num4 = 2;
					StackTrace stackTrace = new StackTrace(num4, fNeedFileInfo: false);
					\u0008 ^= ((9525408 - num) ^ num2) | num4;
					num4 -= 2;
					StackFrame frame = stackTrace.GetFrame(num4);
					int num5 = num4;
					if (frame == null)
					{
						stackTrace = new StackTrace();
						num5 = 1;
						frame = stackTrace.GetFrame(num5);
					}
					MethodBase methodBase = frame?.GetMethod();
					\u0008 ^= num4 + ((-24331880 ^ num) - num2);
					Type type = methodBase?.DeclaringType;
					if (frame == null)
					{
						\u0008 ^= -1098985525 - num + num2;
					}
					bool flag = type == typeof(RuntimeMethodHandle);
					\u0008 ^= -24331848 ^ num ^ num2;
					if (!flag)
					{
						flag = type == null;
						if (flag)
						{
							if (\u0002\u0005.\u0003(stackTrace, num5))
							{
								flag = false;
							}
							else
							{
								\u0008 ^= -1098985557 - num + num2;
							}
						}
					}
					if (flag == (stackTrace != null))
					{
						\u0008 = 0x20 ^ \u0008;
					}
					\u0008 ^= (-24338306 ^ num ^ num2) | (1 + num4);
					\u0002\u0005.m_\u0006 = new \u0006(manifestResourceStream);
					short num6 = (short)(\u0002\u0005.m_\u0006.\u0003() ^ (short)(~(-(-(~(~(-(~(-(~(-1099176761 - num + num2)))))))))));
					if (num6 == 0)
					{
						\u0002 = (short)(\u0002\u0005.m_\u0006.\u0003() ^ (short)(-(~(-(~(~(-(-(~(~(-(~(num + 1099183731 - num2)))))))))))));
					}
					else
					{
						\u000f = \u0002\u0005.m_\u0006.\u0003(num6);
					}
					callingAssembly = executingAssembly;
					AssemblyName assemblyName = \u0002\u0005.\u0003(callingAssembly);
					\u0005 = \u0002\u0005.\u0003(assemblyName);
					num7 = \u0003\u2002;
					\u0003\u2002 = 0;
					num7 ^= (num ^ -135169230) + num2;
					long num8 = \u0005\u2001.\u0003();
					num7 ^= (int)num8;
					num7 ^= (1208408051 - num) ^ num2;
					num7 ^= 1747691746 + num + num2;
					int num9 = 0;
					int num10 = num7;
					global::\u0005<int> obj = null;
					int num11 = 0;
					int num12 = 0;
					int num13 = 0;
					int num14 = 0;
					int num15 = 0;
					num14 = num10;
					num13 = num14 ^ (0x229D2211 ^ num ^ num2);
					num11 = 0;
					num15 = num13 * (num + -9514157 + num2) % ((num ^ -46895305) - num2);
					obj = null;
					num9 = 0;
					num11 = -1099204723 - num + num2;
					num12 = num15;
					obj = ((global::\u000f<int>)new \u000e.\u000f(0x17346E6 ^ num ^ num2)
					{
						\u0008 = num12
					}).GetEnumerator();
					try
					{
						while (((\u0002)obj).\u0002\u2009\u2005\u2006\u0003())
						{
							num9 = obj.\u0002\u2009\u2005\u2006\u0003();
							num15 ^= num9 - num11;
							num11 -= 3 + num15 >> 8;
						}
					}
					finally
					{
						obj?.\u0008\u2009\u2005\u2006\u0003();
					}
					int num16 = num15;
					num7 ^= 10234508 - num - num2 + ~(-(~(-(-(~(~(-(~((0x1734593 ^ num) + num2)))))))));
					num7 ^= -(~(-(~(-(~(~(-(~(-17503449 - num - num2)))))))));
					num7 = (\u000e = num16 + num7);
					\u0008 = (\u0008 & ((num + 1332657690) ^ num2)) ^ ((-24337508 ^ num) - num2);
					if (((uint)\u0006\u2002 & (uint)(-(~(~(-(~(-(~(-(~((num + -9519469) | num2))))))))))) == 0)
					{
						\u0008 = 9563412 - num - num2;
					}
				}
				else
				{
					num7 = \u000e;
				}
				if (\u0008 == num + -9475488 + num2)
				{
					value = new string(new char[3]
					{
						(char)((9519538 - num) ^ num2),
						'0',
						(char)(num + 1099204928 - num2)
					});
					return value;
				}
				int num17 = \u0003 ^ ((1029172097 - num) ^ num2) ^ num7;
				num17 ^= num + 186868247 + num2;
				\u0002\u0005.m_\u0006.\u0003().Position = num17;
				if (\u000f != null)
				{
					array = \u000f;
				}
				else
				{
					short num18 = ((\u0002 != -1) ? \u0002 : ((short)(\u0002\u0005.m_\u0006.\u0003() ^ (9516933 - num - num2) ^ num17)));
					if (num18 == 0)
					{
						array = null;
					}
					else
					{
						array = \u0002\u0005.m_\u0006.\u0003(num18);
						for (int i = 0; i != array.Length; i++)
						{
							array[i] ^= (byte)(\u000e >> ((3 & i) << 3));
						}
					}
				}
				num19 = \u0002\u0005.m_\u0006.\u0003() ^ num17 ^ ~(-(~(-(-(~(~(-(~((0x3EF920D4 ^ num) + num2))))))))) ^ num7;
				if (num19 == ((-1099204840 - num) ^ num2))
				{
					byte[] array2 = \u0002\u0005.m_\u0006.\u0003(4);
					\u0003 = ((-1056514263 ^ num) - num2) ^ num7;
					\u0003 = (array2[2] | (array2[3] << 16) | (array2[0] << 8) | (array2[1] << 24)) ^ -\u0003;
					goto IL_0013;
				}
				num20 = (10013152 - num) ^ num2;
				num21 = \u0008;
				num22 = num19;
				num23 = num21 - 12;
				num19 &= (-292767463 ^ num) - num2;
				array3 = \u0002\u0005.m_\u0006.\u0003(num19);
				array4 = \u0005;
			}
			break;
			IL_0013:
			if (\u0002\u0005.m_\u0003.TryGetValue(\u0003, out value))
			{
				return value;
			}
		}
		bool flag2 = (num22 & ((562333928 + num) ^ num2)) != 0;
		bool flag3 = (num22 & ((-1098073832 ^ num) - num2)) != 0;
		bool flag4 = (num22 & ((num + -1048278808) ^ num2)) != 0;
		byte[] array5 = array;
		byte[] array6 = array3;
		byte[] array7 = array5;
		int num24 = 0;
		byte b = 0;
		int num25 = 0;
		byte b2 = 0;
		ushort num26 = 0;
		byte b3 = 0;
		byte b4 = 0;
		uint num27 = 0u;
		b = array7[1];
		num24 = array6.Length;
		b2 = (byte)((num24 + 11) ^ (b + 7));
		num27 = (uint)((array7[0] | (array7[2] << 8)) + (b2 << 3));
		num25 = 0;
		num26 = 0;
		for (; num25 < num24; num25++)
		{
			if ((1 & num25) == 0)
			{
				num27 = (uint)((int)num27 * (-1098990827 - num + num2) + ((num + 1101406121) ^ num2));
				num26 = (ushort)(num27 >> 16);
			}
			b3 = (byte)num26;
			num26 >>= 8;
			b4 = array6[num25];
			array6[num25] = (byte)(b4 ^ b ^ (b2 + 3) ^ b3);
			b2 = b4;
		}
		array3 = array6;
		if (array4 != null != (num20 != num21))
		{
			for (int num28 = 0; num28 < num19; num28 = 1 + num28)
			{
				byte b5 = array4[num28 & 7];
				b5 = (byte)((b5 << 3) | (b5 >> 5));
				array3[num28] ^= b5;
			}
		}
		byte[] array8;
		int num29;
		if (!flag3)
		{
			array8 = array3;
			num29 = num19;
		}
		else
		{
			num29 = array3[2] | (array3[0] << 16) | (array3[3] << 8) | (array3[1] << 24);
			array8 = new byte[num29];
			\u0002\u0005.\u0003(array3, 4, array8);
		}
		if (flag2 && num23 == num20 - 12)
		{
			char[] array9 = new char[num29];
			for (int num30 = 0; num30 < num29; num30 = 1 + num30)
			{
				array9[num30] = (char)array8[num30];
			}
			value = new string(array9);
		}
		else
		{
			char[] array10 = new char[num29 / 2];
			int num31 = 0;
			for (int num32 = 0; num32 < num29; num32 = 2 + num32)
			{
				array10[num31++] = (char)(array8[num32] | (array8[num32 + 1] << 8));
			}
			value = new string(array10);
		}
		num23 += -1099204713 - num + num2 + (3 & num23) << 5;
		if (num23 != num20 - 12 + ((num ^ -24331879) - num2 + ((num20 - 12) & 3) << 5))
		{
			int num33 = (\u0003 + num19) ^ ((9112466 - num) ^ num2) ^ (num23 & (num + 1099206133 - num2));
			StringBuilder stringBuilder = new StringBuilder();
			int num3 = (9519538 - num) ^ num2;
			stringBuilder.Append((char)(byte)num3);
			value = num33.ToString(stringBuilder.ToString());
		}
		if (!flag4 && \u0006)
		{
			value = string.Intern(value);
			\u0002\u0005.m_\u0003[\u0003] = value;
			if (\u0002\u0005.m_\u0003.Count == (0x173405E ^ num) + num2)
			{
				bool lockTaken = false;
				ConcurrentDictionary<int, string> obj2 = \u0002\u0005.m_\u0003;
				try
				{
					Monitor.Enter(obj2, ref lockTaken);
					if (\u0002\u0005.m_\u0006 != null)
					{
						\u0002\u0005.m_\u0006.\u0003();
						\u0002\u0005.m_\u0006 = null;
						\u000f = null;
						\u0005 = null;
					}
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(obj2);
					}
				}
			}
		}
		return value;
	}

	private static AssemblyName \u0003(Assembly \u0003)
	{
		try
		{
			return \u0003.GetName();
		}
		catch
		{
			return new AssemblyName(\u0003.FullName);
		}
	}

	private static byte[] \u0003(AssemblyName \u0003)
	{
		byte[] array = \u0003.GetPublicKeyToken();
		if (array != null && array.Length == 0)
		{
			array = null;
		}
		return array;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private static bool \u0003(StackTrace \u0003, int \u0006)
	{
		Assembly assembly = \u0003.GetFrame(++\u0006)?.GetMethod()?.DeclaringType?.Assembly;
		if (assembly != null)
		{
			AssemblyName assemblyName = \u0002\u0005.\u0003(assembly);
			byte[] array = \u0002\u0005.\u0003(assemblyName);
			if (array != null && array.Length == 8 && array[0] == 183 && array[7] == 137)
			{
				return true;
			}
		}
		while (true)
		{
			StackFrame frame = \u0003.GetFrame(++\u0006);
			if (frame == null)
			{
				break;
			}
			assembly = frame.GetMethod()?.DeclaringType?.Assembly;
			if (assembly != null && assembly == typeof(\u0002\u0005).Assembly)
			{
				return true;
			}
		}
		return false;
	}

	private static void \u0003(byte[] \u0003, int \u0006, byte[] \u000f)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 128;
		int num4 = \u000f.Length;
		while (num < num4)
		{
			if ((num3 <<= 1) == 256)
			{
				num3 = 1;
				num2 = \u0003[\u0006++];
			}
			if ((num2 & num3) != 0)
			{
				int num5 = (\u0003[\u0006] >> 2) + 3;
				int num6 = ((\u0003[\u0006] << 8) | \u0003[\u0006 + 1]) & 0x3FF;
				\u0006 += 2;
				int num7 = num - num6;
				if (num7 < 0)
				{
					break;
				}
				while (--num5 >= 0 && num < num4)
				{
					\u000f[num++] = \u000f[num7++];
				}
			}
			else
			{
				\u000f[num++] = \u0003[\u0006++];
			}
		}
	}
}
public sealed class \u0002\u2001
{
	private string m_\u0003;

	private StringBuilder \u0006 = new StringBuilder(255);

	private string \u000f = \u0002\u0005.\u0003(1031809548);

	private int \u0002 = 255;

	private string \u0008 = \u0002\u0005.\u0003(1031809543);

	public \u0002\u2001()
	{
		this.m_\u0003 = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + \u0002\u0005.\u0003(1031810035);
	}

	[DllImport("kernel32", CharSet = CharSet.Unicode, EntryPoint = "GetPrivateProfileString")]
	public static extern int \u0003(string \u0003, string \u0006, string \u000f, StringBuilder \u0002, int \u0008, string \u0005);

	[DllImport("kernel32", EntryPoint = "WritePrivateProfileString")]
	private static extern long \u0003(string \u0003, string \u0006, string \u000f, string \u0002);

	public string \u0003(string \u0003)
	{
		\u0002\u2001.\u0003(\u0008, \u0003, \u000f, \u0006, \u0002, this.m_\u0003);
		return \u0006.ToString();
	}

	public void \u0003()
	{
		\u0008 = this.\u0003();
	}

	public string \u0003()
	{
		string empty = string.Empty;
		byte[] array = \u0008\u2002.\u0002.\u0003(1, 2, 1);
		if (array[0] == 1)
		{
			return \u0002\u0005.\u0003(1031809440);
		}
		if (array[0] == 3)
		{
			return \u0002\u0005.\u0003(1031809436);
		}
		if (array[0] == 4)
		{
			return \u0002\u0005.\u0003(1031809429);
		}
		if (array[0] == 5)
		{
			return \u0002\u0005.\u0003(1031809422);
		}
		if (array[0] == 6)
		{
			if (\u0008\u2002.\u0006.\u0008\u2001 == 5)
			{
				return \u0002\u0005.\u0003(1031805907);
			}
			return \u0002\u0005.\u0003(1031809415);
		}
		if (array[0] == 7)
		{
			return \u0002\u0005.\u0003(1031809392);
		}
		if (array[0] == 8)
		{
			return \u0002\u0005.\u0003(1031809385);
		}
		if (array[0] == 9)
		{
			return \u0002\u0005.\u0003(1031809381);
		}
		if (array[0] == 10)
		{
			return \u0002\u0005.\u0003(1031809361);
		}
		if (array[0] == 11)
		{
			return \u0002\u0005.\u0003(1031809337);
		}
		if (array[0] == 12)
		{
			return \u0002\u0005.\u0003(1031809313);
		}
		if (array[0] == 13)
		{
			return \u0002\u0005.\u0003(1031809289);
		}
		if (array[0] == 14)
		{
			return \u0002\u0005.\u0003(1031810801);
		}
		if (array[0] == 15)
		{
			return \u0002\u0005.\u0003(1031810777);
		}
		if (array[0] == 16)
		{
			return \u0002\u0005.\u0003(1031810753);
		}
		if (array[0] == 17)
		{
			return \u0002\u0005.\u0003(1031810729);
		}
		if (array[0] == 18)
		{
			return \u0002\u0005.\u0003(1031805907);
		}
		return \u0002\u0005.\u0003(1031809440);
	}
}
public sealed class \u0002\u2002
{
	public \u0008\u2002.\u0003 \u0003 = new \u0008\u2002.\u0003();

	public \u0008\u2002.\u0003 \u0006 = new \u0008\u2002.\u0003();

	public \u0008\u2002.\u0003 \u000f = new \u0008\u2002.\u0003();

	public \u0008\u2002.\u0003 \u0002 = new \u0008\u2002.\u0003();

	public int \u0008 = 0;

	public int \u0005 = 0;

	public int \u000e = 0;

	public int \u0003\u2002 = 0;

	public byte \u0006\u2002 = 0;

	public bool \u000f\u2002 = false;

	public bool \u0002\u2002 = false;

	public bool \u0008\u2002 = false;

	public bool \u0005\u2002 = false;

	public int \u000e\u2002 = 0;

	public int \u0003\u2001 = 0;

	public int \u0006\u2001 = 0;

	public int \u000f\u2001 = 0;

	public int \u0002\u2001 = 0;

	public int \u0008\u2001 = 0;

	public int \u0005\u2001 = 0;

	public int \u000e\u2001 = 255;

	public int \u0003\u2009 = 1;

	public int \u0006\u2009 = 0;

	public int \u000f\u2009 = 3;

	public bool \u0002\u2009 = false;

	public bool \u0008\u2009 = false;

	public bool \u0005\u2009 = true;

	public bool \u000e\u2009 = true;

	public void \u0003()
	{
		byte[] array = new byte[256];
		array = global::\u0008\u2002.\u0002.\u0003(12);
		this.\u0003.\u0006\u2001 = array[3] + (array[2] << 8);
		this.\u0003.\u0008\u2001 = array[16];
		this.\u0006.\u0006\u2001 = array[5] + (array[4] << 8);
		this.\u0006.\u0008\u2001 = array[19];
		this.\u000f.\u0006\u2001 = array[7] + (array[6] << 8);
		this.\u000f.\u0008\u2001 = array[22];
		this.\u0002.\u0006\u2001 = array[37] + (array[36] << 8);
		this.\u0002.\u0008\u2001 = array[38];
		this.\u0003.\u0002\u2001 = \u0003(this.\u0003(), array[18]);
		this.\u0006.\u0002\u2001 = array[21];
		this.\u000f.\u0002\u2001 = array[24];
		this.\u0002.\u0002\u2001 = array[40];
	}

	public void \u0006()
	{
		byte[] array = new byte[256];
		array = global::\u0008\u2002.\u0002.\u0003(13);
		this.\u0008 = array[12];
		this.\u0005 = array[14];
		if (this.\u0005 == 5)
		{
			global::\u0008\u2002.\u0006.\u0002\u2001 = true;
		}
		byte b = array[43];
		if ((this.\u0008 <= 1 && !global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001) || ((b >> 1) & 1) == 1)
		{
			\u0005\u2009 = false;
		}
		if (\u0005\u2009 && !global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
		{
			this.\u0003.\u0003 = array[16];
			this.\u0003.\u000e = (byte)Math.Round((double)(int)array[17] / 255.0 * 100.0, 0);
			this.\u0003.\u0006 = (this.\u0003.\u0008 = array[18]);
			this.\u0003.\u0003\u2002 = (this.\u0003.\u0002\u2002 = (byte)Math.Round((double)(int)array[19] / 255.0 * 100.0, 0));
			this.\u0003.\u000f = (this.\u0003.\u0005 = array[20]);
			this.\u0003.\u0006\u2002 = (this.\u0003.\u0008\u2002 = (byte)Math.Round((double)(int)array[21] / 255.0 * 100.0, 0));
			this.\u0003.\u0002 = 100;
			this.\u0003.\u000f\u2002 = 100;
			if (this.\u0003.\u000e >= this.\u0003.\u0003\u2002)
			{
				this.\u0003.\u0003\u2002 = (byte)(this.\u0003.\u000e + 1);
				if (this.\u0003.\u0003\u2002 >= this.\u0003.\u0006\u2002)
				{
					this.\u0003.\u0006\u2002 = (byte)(this.\u0003.\u0003\u2002 + 1);
				}
				if (this.\u0003.\u0006\u2002 >= this.\u0003.\u000f\u2002)
				{
					this.\u0003.\u0006\u2002 = (byte)(this.\u0003.\u000f\u2002 - 1);
				}
			}
			if (this.\u0003.\u0003\u2002 >= this.\u0003.\u0006\u2002)
			{
				this.\u0003.\u0006\u2002 = (byte)(this.\u0003.\u0003\u2002 + 1);
				if (this.\u0003.\u0006\u2002 >= this.\u0003.\u000f\u2002)
				{
					this.\u0003.\u0006\u2002 = (byte)(this.\u0003.\u000f\u2002 - 1);
				}
				if (this.\u0003.\u0003\u2002 >= this.\u0003.\u0006\u2002)
				{
					this.\u0003.\u0003\u2002 = (byte)(this.\u0003.\u0006\u2002 - 1);
				}
			}
			if (this.\u0003.\u0006\u2002 >= this.\u0003.\u000f\u2002)
			{
				this.\u0003.\u0006\u2002--;
				if (this.\u0003.\u0003\u2002 >= this.\u0003.\u0006\u2002)
				{
					this.\u0003.\u0003\u2002 = (byte)(this.\u0003.\u0006\u2002 - 1);
				}
			}
			this.\u0003.\u0002\u2002 = this.\u0003.\u0003\u2002;
			this.\u0003.\u0008\u2002 = this.\u0003.\u0006\u2002;
			this.\u0006.\u0003 = array[24];
			this.\u0006.\u000e = (byte)Math.Round((double)(int)array[25] / 255.0 * 100.0, 0);
			this.\u0006.\u0006 = (this.\u0006.\u0008 = array[26]);
			this.\u0006.\u0003\u2002 = (this.\u0006.\u0002\u2002 = (byte)Math.Round((double)(int)array[27] / 255.0 * 100.0, 0));
			this.\u0006.\u000f = (this.\u0006.\u0005 = array[28]);
			this.\u0006.\u0006\u2002 = (this.\u0006.\u0008\u2002 = (byte)Math.Round((double)(int)array[29] / 255.0 * 100.0, 0));
			this.\u0006.\u0002 = 100;
			this.\u0006.\u000f\u2002 = 100;
			if (this.\u0006.\u000e >= this.\u0006.\u0003\u2002)
			{
				this.\u0006.\u0003\u2002 = (byte)(this.\u0006.\u000e + 1);
				if (this.\u0006.\u0003\u2002 >= this.\u0006.\u0006\u2002)
				{
					this.\u0006.\u0006\u2002 = (byte)(this.\u0006.\u0003\u2002 + 1);
				}
				if (this.\u0006.\u0006\u2002 >= this.\u0006.\u000f\u2002)
				{
					this.\u0006.\u0006\u2002 = (byte)(this.\u0006.\u000f\u2002 - 1);
				}
			}
			if (this.\u0006.\u0003\u2002 >= this.\u0006.\u0006\u2002)
			{
				this.\u0006.\u0006\u2002 = (byte)(this.\u0006.\u0003\u2002 + 1);
				if (this.\u0006.\u0006\u2002 >= this.\u0006.\u000f\u2002)
				{
					this.\u0006.\u0006\u2002 = (byte)(this.\u0006.\u000f\u2002 - 1);
				}
				if (this.\u0006.\u0003\u2002 >= this.\u0006.\u0006\u2002)
				{
					this.\u0006.\u0003\u2002 = (byte)(this.\u0006.\u0006\u2002 - 1);
				}
			}
			if (this.\u0006.\u0006\u2002 >= this.\u0006.\u000f\u2002)
			{
				this.\u0006.\u0006\u2002--;
				if (this.\u0006.\u0003\u2002 >= this.\u0006.\u0006\u2002)
				{
					this.\u0006.\u0003\u2002 = (byte)(this.\u0006.\u0006\u2002 - 1);
				}
			}
			this.\u0006.\u0002\u2002 = this.\u0006.\u0003\u2002;
			this.\u0006.\u0008\u2002 = this.\u0006.\u0006\u2002;
			this.\u000f.\u0003 = array[32];
			this.\u000f.\u000e = (byte)Math.Round((double)(int)array[33] / 255.0 * 100.0, 0);
			this.\u000f.\u0006 = (this.\u000f.\u0008 = array[34]);
			this.\u000f.\u0003\u2002 = (this.\u000f.\u0002\u2002 = (byte)Math.Round((double)(int)array[35] / 255.0 * 100.0, 0));
			this.\u000f.\u000f = (this.\u000f.\u0005 = array[36]);
			this.\u000f.\u0006\u2002 = (this.\u000f.\u0008\u2002 = (byte)Math.Round((double)(int)array[37] / 255.0 * 100.0, 0));
			this.\u000f.\u0002 = 100;
			this.\u000f.\u000f\u2002 = 100;
			if (this.\u000f.\u000e >= this.\u000f.\u0003\u2002)
			{
				this.\u000f.\u0003\u2002 = (byte)(this.\u000f.\u000e + 1);
				if (this.\u000f.\u0003\u2002 >= this.\u000f.\u0006\u2002)
				{
					this.\u000f.\u0006\u2002 = (byte)(this.\u000f.\u0003\u2002 + 1);
				}
				if (this.\u000f.\u0006\u2002 >= this.\u000f.\u000f\u2002)
				{
					this.\u000f.\u0006\u2002 = (byte)(this.\u000f.\u000f\u2002 - 1);
				}
			}
			if (this.\u000f.\u0003\u2002 >= this.\u000f.\u0006\u2002)
			{
				this.\u000f.\u0006\u2002 = (byte)(this.\u000f.\u0003\u2002 + 1);
				if (this.\u000f.\u0006\u2002 >= this.\u000f.\u000f\u2002)
				{
					this.\u000f.\u0006\u2002 = (byte)(this.\u000f.\u000f\u2002 - 1);
				}
				if (this.\u000f.\u0003\u2002 >= this.\u000f.\u0006\u2002)
				{
					this.\u000f.\u0003\u2002 = (byte)(this.\u000f.\u0006\u2002 - 1);
				}
			}
			if (this.\u000f.\u0006\u2002 >= this.\u000f.\u000f\u2002)
			{
				this.\u000f.\u0006\u2002--;
				if (this.\u000f.\u0003\u2002 >= this.\u000f.\u0006\u2002)
				{
					this.\u000f.\u0003\u2002 = (byte)(this.\u000f.\u0006\u2002 - 1);
				}
			}
			this.\u000f.\u0002\u2002 = this.\u000f.\u0003\u2002;
			this.\u000f.\u0008\u2002 = this.\u000f.\u0006\u2002;
			this.\u0003.\u0005\u2001 = 1;
			this.\u0006.\u0005\u2001 = 2;
			this.\u000f.\u0005\u2001 = 3;
			global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031814133) + this.\u0003.\u0003 + \u0002\u0005.\u0003(1031814099) + this.\u0003.\u0006 + \u0002\u0005.\u0003(1031814086) + this.\u0003.\u000f + \u0002\u0005.\u0003(1031814057) + this.\u0003.\u0002 + \u0002\u0005.\u0003(1031814044) + this.\u0003.\u0008 + \u0002\u0005.\u0003(1031814023) + this.\u0003.\u0005 + \u0002\u0005.\u0003(1031813986) + this.\u0003.\u000e + \u0002\u0005.\u0003(1031813973) + this.\u0003.\u0003\u2002 + \u0002\u0005.\u0003(1031813944) + this.\u0003.\u0006\u2002 + \u0002\u0005.\u0003(1031813931) + this.\u0003.\u000f\u2002 + \u0002\u0005.\u0003(1031813918) + this.\u0003.\u0002\u2002 + \u0002\u0005.\u0003(1031813369) + this.\u0003.\u0008\u2002 + \u0002\u0005.\u0003(1031813348) + this.\u0006.\u0003 + \u0002\u0005.\u0003(1031813274) + this.\u0006.\u0006 + \u0002\u0005.\u0003(1031813262) + this.\u0006.\u000f + \u0002\u0005.\u0003(1031813234) + this.\u0006.\u0002 + \u0002\u0005.\u0003(1031813222) + this.\u0006.\u0008 + \u0002\u0005.\u0003(1031813186) + this.\u0006.\u0005 + \u0002\u0005.\u0003(1031813166) + this.\u0006.\u000e + \u0002\u0005.\u0003(1031813138) + this.\u0006.\u0003\u2002 + \u0002\u0005.\u0003(1031813126) + this.\u0006.\u0006\u2002 + \u0002\u0005.\u0003(1031813610) + this.\u0006.\u000f\u2002 + \u0002\u0005.\u0003(1031813598) + this.\u0006.\u0002\u2002 + \u0002\u0005.\u0003(1031813562) + this.\u0006.\u0008\u2002 + \u0002\u0005.\u0003(1031813543) + this.\u000f.\u0003 + \u0002\u0005.\u0003(1031813469) + this.\u000f.\u0006 + \u0002\u0005.\u0003(1031813441) + this.\u000f.\u000f + \u0002\u0005.\u0003(1031813429) + this.\u000f.\u0002 + \u0002\u0005.\u0003(1031813401) + this.\u000f.\u0008 + \u0002\u0005.\u0003(1031813381) + this.\u000f.\u0005 + \u0002\u0005.\u0003(1031814881) + this.\u000f.\u000e + \u0002\u0005.\u0003(1031814869) + this.\u000f.\u0003\u2002 + \u0002\u0005.\u0003(1031814841) + this.\u000f.\u0006\u2002 + \u0002\u0005.\u0003(1031814829) + this.\u000f.\u000f\u2002 + \u0002\u0005.\u0003(1031814801) + this.\u000f.\u0002\u2002 + \u0002\u0005.\u0003(1031814781) + this.\u000f.\u0008\u2002);
		}
		this.\u0006\u2002 = array[50];
		if ((this.\u0006\u2002 & 1) == 1)
		{
			this.\u000f\u2002 = true;
		}
		if (((this.\u0006\u2002 >> 1) & 1) == 1)
		{
			this.\u0002\u2002 = true;
		}
		if (((this.\u0006\u2002 >> 2) & 1) == 1)
		{
			this.\u0008\u2002 = true;
		}
		if (((this.\u0006\u2002 >> 3) & 1) == 1)
		{
			this.\u0005\u2002 = true;
		}
	}

	public void \u000f()
	{
		\u0008();
		byte[] array = new byte[256];
		array[2] = this.\u0003.\u0006;
		array[3] = (byte)Math.Round((double)(int)this.\u0003.\u0003\u2002 / 100.0 * 255.0, 0);
		array[4] = this.\u0003.\u000f;
		array[5] = (byte)Math.Round((double)(int)this.\u0003.\u0006\u2002 / 100.0 * 255.0, 0);
		array[6] = this.\u0006.\u0006;
		array[7] = (byte)Math.Round((double)(int)this.\u0006.\u0003\u2002 / 100.0 * 255.0, 0);
		array[8] = this.\u0006.\u000f;
		array[9] = (byte)Math.Round((double)(int)this.\u0006.\u0006\u2002 / 100.0 * 255.0, 0);
		array[10] = this.\u000f.\u0006;
		array[11] = (byte)Math.Round((double)(int)this.\u000f.\u0003\u2002 / 100.0 * 255.0, 0);
		array[12] = this.\u000f.\u000f;
		array[13] = (byte)Math.Round((double)(int)this.\u000f.\u0006\u2002 / 100.0 * 255.0, 0);
		array[14] = (byte)(this.\u0003.\u0005\u2002 >> 8);
		array[15] = (byte)this.\u0003.\u0005\u2002;
		array[16] = (byte)(this.\u0003.\u000e\u2002 >> 8);
		array[17] = (byte)this.\u0003.\u000e\u2002;
		array[18] = (byte)(this.\u0003.\u0003\u2001 >> 8);
		array[19] = (byte)this.\u0003.\u0003\u2001;
		array[20] = (byte)(this.\u0006.\u0005\u2002 >> 8);
		array[21] = (byte)this.\u0006.\u0005\u2002;
		array[22] = (byte)(this.\u0006.\u000e\u2002 >> 8);
		array[23] = (byte)this.\u0006.\u000e\u2002;
		array[24] = (byte)(this.\u0006.\u0003\u2001 >> 8);
		array[25] = (byte)this.\u0006.\u0003\u2001;
		array[26] = (byte)(this.\u000f.\u0005\u2002 >> 8);
		array[27] = (byte)this.\u000f.\u0005\u2002;
		array[28] = (byte)(this.\u000f.\u000e\u2002 >> 8);
		array[29] = (byte)this.\u000f.\u000e\u2002;
		array[30] = (byte)(this.\u000f.\u0003\u2001 >> 8);
		array[31] = (byte)this.\u000f.\u0003\u2001;
		global::\u0008\u2002.\u0002.\u0003(14, array);
		global::\u0008\u2002.\u0008.\u000e = 6;
		\u0005();
	}

	public void \u0002()
	{
		byte[] array = new byte[256];
		array = global::\u0008\u2002.\u0002.\u0003(4, 0, 256);
		this.\u0005 = array[4];
		if (this.\u0005 == 5)
		{
			global::\u0008\u2002.\u0006.\u0002\u2001 = true;
		}
		this.\u000e = array[5];
		this.\u0003\u2002 = array[7];
		byte[] array2 = new byte[256];
		array2 = global::\u0008\u2002.\u0002.\u0003(13);
		this.\u0008 = array2[12];
		byte b = array2[43];
		if ((this.\u0008 <= 1 && !global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001) || ((b >> 1) & 1) == 1)
		{
			\u0005\u2009 = false;
		}
		if (\u0005\u2009 && !global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
		{
			this.\u0003.\u000e = array[16];
			this.\u0003.\u0003\u2002 = array[17];
			this.\u0003.\u0006\u2002 = array[18];
			this.\u0003.\u000f\u2002 = 100;
			this.\u0003.\u0002\u2002 = array[20];
			this.\u0003.\u0008\u2002 = array[21];
			this.\u0003.\u0003 = array[22];
			this.\u0003.\u0006 = array[23];
			this.\u0003.\u000f = array[24];
			this.\u0003.\u0002 = 100;
			this.\u0003.\u0008 = array[26];
			this.\u0003.\u0005 = array[27];
			this.\u0003.\u0005\u2002 = (array[29] << 8) + array[28];
			this.\u0003.\u000e\u2002 = (array[31] << 8) + array[30];
			this.\u0003.\u0003\u2001 = (array[33] << 8) + array[32];
			this.\u0006.\u000e = array[34];
			this.\u0006.\u0003\u2002 = array[35];
			this.\u0006.\u0006\u2002 = array[36];
			this.\u0006.\u000f\u2002 = 100;
			this.\u0006.\u0002\u2002 = array[38];
			this.\u0006.\u0008\u2002 = array[39];
			this.\u0006.\u0003 = array[40];
			this.\u0006.\u0006 = array[41];
			this.\u0006.\u000f = array[42];
			this.\u0006.\u0002 = 100;
			this.\u0006.\u0008 = array[44];
			this.\u0006.\u0005 = array[45];
			this.\u0006.\u0005\u2002 = (array[47] << 8) + array[46];
			this.\u0006.\u000e\u2002 = (array[49] << 8) + array[48];
			this.\u0006.\u0003\u2001 = (array[51] << 8) + array[50];
			this.\u000f.\u000e = array[52];
			this.\u000f.\u0003\u2002 = array[53];
			this.\u000f.\u0006\u2002 = array[54];
			this.\u000f.\u000f\u2002 = 100;
			this.\u000f.\u0002\u2002 = array[56];
			this.\u000f.\u0008\u2002 = array[57];
			this.\u000f.\u0003 = array[58];
			this.\u000f.\u0006 = array[59];
			this.\u000f.\u000f = array[60];
			this.\u000f.\u0002 = 100;
			this.\u000f.\u0008 = array[62];
			this.\u000f.\u0005 = array[63];
			this.\u000f.\u0005\u2002 = (array[65] << 8) + array[64];
			this.\u000f.\u000e\u2002 = (array[67] << 8) + array[66];
			this.\u000f.\u0003\u2001 = (array[69] << 8) + array[68];
			this.\u0003.\u0005\u2001 = 1;
			this.\u0006.\u0005\u2001 = 2;
			this.\u000f.\u0005\u2001 = 3;
			global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031814746) + this.\u0003.\u0003 + \u0002\u0005.\u0003(1031814099) + this.\u0003.\u0006 + \u0002\u0005.\u0003(1031814086) + this.\u0003.\u000f + \u0002\u0005.\u0003(1031814057) + this.\u0003.\u0002 + \u0002\u0005.\u0003(1031814044) + this.\u0003.\u0008 + \u0002\u0005.\u0003(1031814023) + this.\u0003.\u0005 + \u0002\u0005.\u0003(1031813986) + this.\u0003.\u000e + \u0002\u0005.\u0003(1031813973) + this.\u0003.\u0003\u2002 + \u0002\u0005.\u0003(1031813944) + this.\u0003.\u0006\u2002 + \u0002\u0005.\u0003(1031813931) + this.\u0003.\u000f\u2002 + \u0002\u0005.\u0003(1031813918) + this.\u0003.\u0002\u2002 + \u0002\u0005.\u0003(1031813369) + this.\u0003.\u0008\u2002 + \u0002\u0005.\u0003(1031813348) + this.\u0006.\u0003 + \u0002\u0005.\u0003(1031813274) + this.\u0006.\u0006 + \u0002\u0005.\u0003(1031813262) + this.\u0006.\u000f + \u0002\u0005.\u0003(1031813234) + this.\u0006.\u0002 + \u0002\u0005.\u0003(1031813222) + this.\u0006.\u0008 + \u0002\u0005.\u0003(1031813186) + this.\u0006.\u0005 + \u0002\u0005.\u0003(1031813166) + this.\u0006.\u000e + \u0002\u0005.\u0003(1031813138) + this.\u0006.\u0003\u2002 + \u0002\u0005.\u0003(1031813126) + this.\u0006.\u0006\u2002 + \u0002\u0005.\u0003(1031813610) + this.\u0006.\u000f\u2002 + \u0002\u0005.\u0003(1031813598) + this.\u0006.\u0002\u2002 + \u0002\u0005.\u0003(1031813562) + this.\u0006.\u0008\u2002);
		}
		if (global::\u0008\u2002.\u0006.\u0005\u2001)
		{
			\u0008\u2002();
			\u000e\u2002();
			\u0005\u2001 = array[81];
			if (\u0005\u2001 == 0)
			{
				\u0003(1, 127, 0);
			}
			else
			{
				\u000e\u2001 = array[82] & 0x7F;
				\u0003\u2009 = array[82] >> 7;
				\u0006\u2009 = array[83];
			}
		}
		this.\u0006\u2002 = array2[50];
		if ((this.\u0006\u2002 & 1) == 1)
		{
			this.\u000f\u2002 = true;
		}
		if (((this.\u0006\u2002 >> 1) & 1) == 1)
		{
			this.\u0002\u2002 = true;
		}
		if (((this.\u0006\u2002 >> 2) & 1) == 1)
		{
			this.\u0008\u2002 = true;
		}
		if (((this.\u0006\u2002 >> 3) & 1) == 1)
		{
			this.\u0005\u2002 = true;
		}
		if (global::\u0008\u2002.\u0006.\u0003\u2009 || global::\u0008\u2002.\u0006.\u0006\u2009)
		{
			global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031814718));
			global::\u0008\u2002.\u0008.\u000e();
			global::\u0008\u2002.\u0008.\u0006();
			if (global::\u0008\u2002.\u0008.\u0005\u2009 && !global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
			{
				global::\u0008\u2002.\u0008.\u000f();
			}
			global::\u0008\u2002.\u0005.\u0003(\u0006\u0005.\u0003.Auto);
			global::\u0008\u2002.\u0008.\u0006(0);
		}
	}

	public void \u0008()
	{
		this.\u0003.\u0005\u2002 = (int)Math.Round((double)(this.\u0003.\u0003\u2002 - this.\u0003.\u000e) / (double)(this.\u0003.\u0006 - this.\u0003.\u0003) * 2.55 * 16.0, 0);
		this.\u0003.\u000e\u2002 = (int)Math.Round((double)(this.\u0003.\u0006\u2002 - this.\u0003.\u0003\u2002) / (double)(this.\u0003.\u000f - this.\u0003.\u0006) * 2.55 * 16.0, 0);
		this.\u0003.\u0003\u2001 = (int)Math.Round((double)(this.\u0003.\u000f\u2002 - this.\u0003.\u0006\u2002) / (double)(this.\u0003.\u0002 - this.\u0003.\u000f) * 2.55 * 16.0, 0);
		this.\u0006.\u0005\u2002 = (int)Math.Round((double)(this.\u0006.\u0003\u2002 - this.\u0006.\u000e) / (double)(this.\u0006.\u0006 - this.\u0006.\u0003) * 2.55 * 16.0, 0);
		this.\u0006.\u000e\u2002 = (int)Math.Round((double)(this.\u0006.\u0006\u2002 - this.\u0006.\u0003\u2002) / (double)(this.\u0006.\u000f - this.\u0006.\u0006) * 2.55 * 16.0, 0);
		this.\u0006.\u0003\u2001 = (int)Math.Round((double)(this.\u0006.\u000f\u2002 - this.\u0006.\u0006\u2002) / (double)(this.\u0006.\u0002 - this.\u0006.\u000f) * 2.55 * 16.0, 0);
		this.\u000f.\u0005\u2002 = (int)Math.Round((double)(this.\u000f.\u0003\u2002 - this.\u000f.\u000e) / (double)(this.\u000f.\u0006 - this.\u000f.\u0003) * 2.55 * 16.0, 0);
		this.\u000f.\u000e\u2002 = (int)Math.Round((double)(this.\u000f.\u0006\u2002 - this.\u000f.\u0003\u2002) / (double)(this.\u000f.\u000f - this.\u000f.\u0006) * 2.55 * 16.0, 0);
		this.\u000f.\u0003\u2001 = (int)Math.Round((double)(this.\u000f.\u000f\u2002 - this.\u000f.\u0006\u2002) / (double)(this.\u000f.\u0002 - this.\u000f.\u000f) * 2.55 * 16.0, 0);
	}

	public void \u0005()
	{
		byte[] array = new byte[256];
		array[0] = (byte)global::\u0008\u2002.\u0008.\u0003\u2001;
		array[1] = (byte)global::\u0008\u2002.\u0008.\u0006\u2001;
		array[2] = (byte)global::\u0008\u2002.\u0008.\u0002\u2001;
		array[3] = (byte)global::\u0008\u2002.\u0008.\u000f\u2001;
		array[4] = (byte)this.\u0005;
		array[5] = (byte)this.\u000e;
		array[6] = (byte)this.\u0008;
		array[7] = (byte)this.\u0003\u2002;
		array[16] = this.\u0003.\u000e;
		array[17] = this.\u0003.\u0003\u2002;
		array[18] = this.\u0003.\u0006\u2002;
		array[19] = 100;
		array[20] = this.\u0003.\u0002\u2002;
		array[21] = this.\u0003.\u0008\u2002;
		array[22] = this.\u0003.\u0003;
		array[23] = this.\u0003.\u0006;
		array[24] = this.\u0003.\u000f;
		array[25] = 100;
		array[26] = this.\u0003.\u0008;
		array[27] = this.\u0003.\u0005;
		array[28] = (byte)this.\u0003.\u0005\u2002;
		array[29] = (byte)(this.\u0003.\u0005\u2002 >> 8);
		array[30] = (byte)this.\u0003.\u000e\u2002;
		array[31] = (byte)(this.\u0003.\u000e\u2002 >> 8);
		array[32] = (byte)this.\u0003.\u0003\u2001;
		array[33] = (byte)(this.\u0003.\u0003\u2001 >> 8);
		array[34] = this.\u0006.\u000e;
		array[35] = this.\u0006.\u0003\u2002;
		array[36] = this.\u0006.\u0006\u2002;
		array[37] = 100;
		array[38] = this.\u0006.\u0002\u2002;
		array[39] = this.\u0006.\u0008\u2002;
		array[40] = this.\u0006.\u0003;
		array[41] = this.\u0006.\u0006;
		array[42] = this.\u0006.\u000f;
		array[43] = 100;
		array[44] = this.\u0006.\u0008;
		array[45] = this.\u0006.\u0005;
		array[46] = (byte)this.\u0006.\u0005\u2002;
		array[47] = (byte)(this.\u0006.\u0005\u2002 >> 8);
		array[48] = (byte)this.\u0006.\u000e\u2002;
		array[49] = (byte)(this.\u0006.\u000e\u2002 >> 8);
		array[50] = (byte)this.\u0006.\u0003\u2001;
		array[51] = (byte)(this.\u0006.\u0003\u2001 >> 8);
		array[52] = this.\u000f.\u000e;
		array[53] = this.\u000f.\u0003\u2002;
		array[54] = this.\u000f.\u0006\u2002;
		array[55] = 100;
		array[56] = this.\u000f.\u0002\u2002;
		array[57] = this.\u000f.\u0008\u2002;
		array[58] = this.\u000f.\u0003;
		array[59] = this.\u000f.\u0006;
		array[60] = this.\u000f.\u000f;
		array[61] = 100;
		array[62] = this.\u000f.\u0008;
		array[63] = this.\u000f.\u0005;
		array[64] = (byte)this.\u000f.\u0005\u2002;
		array[65] = (byte)(this.\u000f.\u0005\u2002 >> 8);
		array[66] = (byte)this.\u000f.\u000e\u2002;
		array[67] = (byte)(this.\u000f.\u000e\u2002 >> 8);
		array[68] = (byte)this.\u000f.\u0003\u2001;
		array[69] = (byte)(this.\u000f.\u0003\u2001 >> 8);
		global::\u0008\u2002.\u0002.\u0003(4, 0, 256, array);
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031814702) + this.\u0003.\u0003 + \u0002\u0005.\u0003(1031814099) + this.\u0003.\u0006 + \u0002\u0005.\u0003(1031814086) + this.\u0003.\u000f + \u0002\u0005.\u0003(1031814057) + this.\u0003.\u0002 + \u0002\u0005.\u0003(1031814044) + this.\u0003.\u0008 + \u0002\u0005.\u0003(1031814023) + this.\u0003.\u0005 + \u0002\u0005.\u0003(1031813986) + this.\u0003.\u000e + \u0002\u0005.\u0003(1031813973) + this.\u0003.\u0003\u2002 + \u0002\u0005.\u0003(1031813944) + this.\u0003.\u0006\u2002 + \u0002\u0005.\u0003(1031813931) + this.\u0003.\u000f\u2002 + \u0002\u0005.\u0003(1031813918) + this.\u0003.\u0002\u2002 + \u0002\u0005.\u0003(1031813369) + this.\u0003.\u0008\u2002 + \u0002\u0005.\u0003(1031813348) + this.\u0006.\u0003 + \u0002\u0005.\u0003(1031813274) + this.\u0006.\u0006 + \u0002\u0005.\u0003(1031813262) + this.\u0006.\u000f + \u0002\u0005.\u0003(1031813234) + this.\u0006.\u0002 + \u0002\u0005.\u0003(1031813222) + this.\u0006.\u0008 + \u0002\u0005.\u0003(1031813186) + this.\u0006.\u0005 + \u0002\u0005.\u0003(1031813166) + this.\u0006.\u000e + \u0002\u0005.\u0003(1031813138) + this.\u0006.\u0003\u2002 + \u0002\u0005.\u0003(1031813126) + this.\u0006.\u0006\u2002 + \u0002\u0005.\u0003(1031813610) + this.\u0006.\u000f\u2002 + \u0002\u0005.\u0003(1031813598) + this.\u0006.\u0002\u2002 + \u0002\u0005.\u0003(1031813562) + this.\u0006.\u0008\u2002 + \u0002\u0005.\u0003(1031813543) + this.\u000f.\u0003 + \u0002\u0005.\u0003(1031813469) + this.\u000f.\u0006 + \u0002\u0005.\u0003(1031813441) + this.\u000f.\u000f + \u0002\u0005.\u0003(1031813429) + this.\u000f.\u0002 + \u0002\u0005.\u0003(1031813401) + this.\u000f.\u0008 + \u0002\u0005.\u0003(1031813381) + this.\u000f.\u0005 + \u0002\u0005.\u0003(1031814881) + this.\u000f.\u000e + \u0002\u0005.\u0003(1031814869) + this.\u000f.\u0003\u2002 + \u0002\u0005.\u0003(1031814841) + this.\u000f.\u0006\u2002 + \u0002\u0005.\u0003(1031814829) + this.\u000f.\u000f\u2002 + \u0002\u0005.\u0003(1031814801) + this.\u000f.\u0002\u2002 + \u0002\u0005.\u0003(1031814781) + this.\u000f.\u0008\u2002);
	}

	public void \u0003(byte \u0003)
	{
		global::\u0008\u2002.\u0002.\u0003(121, 1, (uint)\u0003);
		global::\u000f\u2002.\u0006(\u0002\u0005.\u0003(1031814670) + \u0003);
	}

	public void \u0006(byte \u0003)
	{
		int num = (int)Math.Round(255.0 * ((double)(int)\u0003 / 100.0), 0);
		global::\u0008\u2002.\u0002.\u0003(121, 14, (uint)num);
		byte[] array = new byte[1] { \u0003 };
		global::\u0008\u2002.\u0002.\u0003(4, 7, 1, array);
		if (global::\u0008\u2002.\u0006.\u0005\u2009)
		{
			global::\u0008\u2002.\u0008.\u0003\u2002();
		}
	}

	public void \u000e()
	{
		global::\u0008\u2002.\u0002.\u0003(121, 34, 1u);
	}

	public void \u0003\u2002()
	{
		byte[] array = new byte[1];
		byte[] array2 = new byte[1];
		byte[] array3 = new byte[256];
		array = global::\u0008\u2002.\u0002.\u0003(1029, 1);
		if (array[0] == 1)
		{
			array[0] = 0;
		}
		array2 = global::\u0008\u2002.\u0002.\u0003(1031, 1);
		array3 = global::\u0008\u2002.\u0002.\u0003(257, 1);
		switch (array3[0])
		{
		case 0:
		case 4:
			global::\u0008\u2002.\u0002.\u0003(1094, 1, array);
			global::\u0008\u2002.\u0002.\u0003(1098, 1, array2);
			break;
		case 1:
			global::\u0008\u2002.\u0002.\u0003(1095, 1, array);
			global::\u0008\u2002.\u0002.\u0003(1099, 1, array2);
			break;
		case 2:
			global::\u0008\u2002.\u0002.\u0003(1096, 1, array);
			global::\u0008\u2002.\u0002.\u0003(1100, 1, array2);
			break;
		case 3:
			global::\u0008\u2002.\u0002.\u0003(1097, 1, array);
			global::\u0008\u2002.\u0002.\u0003(1101, 1, array2);
			break;
		}
	}

	public void \u0006\u2002()
	{
		global::\u0008\u2002.\u0002.\u0003(121, 41, 1u);
		\u000f\u2002();
	}

	public void \u000f\u2002()
	{
		\u0008\u2001 = 1;
		byte[] array = new byte[1] { 1 };
		global::\u0008\u2002.\u0002.\u0003(4, 80, 1, array);
	}

	public void \u0002\u2002()
	{
		\u0008\u2001 = 0;
		byte[] array = new byte[1] { 0 };
		global::\u0008\u2002.\u0002.\u0003(4, 80, 1, array);
	}

	public void \u0003(int \u0003, int \u0006, int \u000f)
	{
		int num = \u0006 + (\u0003 << 7) + (\u000f << 8);
		global::\u0008\u2002.\u0002.\u0003(121, 40, (uint)num);
		\u000e\u2001 = \u0006;
		\u0003\u2009 = \u0003;
		\u0006\u2009 = \u000f;
		byte[] array = new byte[5]
		{
			1,
			(byte)(\u0006 + (\u0003 << 7)),
			(byte)\u000f,
			0,
			0
		};
		global::\u0008\u2002.\u0002.\u0003(4, 81, 5, array);
	}

	public void \u0008\u2002()
	{
		int dayOfWeek = (int)DateTime.Now.DayOfWeek;
		int num = DateTime.Now.Second + (DateTime.Now.Minute << 6) + (DateTime.Now.Hour << 12) + (dayOfWeek << 17);
		global::\u0008\u2002.\u0002.\u0003(118, 1, (uint)num);
	}

	public void \u0005\u2002()
	{
		int num = 0;
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		array[0] = 1;
		array[1] = 9;
		array[2] = 0;
		array[3] = 0;
		array[4] = 0;
		array[5] = 0;
		array[6] = 192;
		global::\u0008\u2002.\u0002.\u0003(4, array, ref array2);
		num = array2[2];
		if (((num >> 4) & 1) == 1)
		{
			\u000e\u2009 = true;
		}
		else
		{
			\u000e\u2009 = false;
		}
	}

	public void \u000e\u2002()
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		array[0] = 1;
		array[1] = 163;
		array[2] = 0;
		array[3] = 0;
		array[4] = 0;
		array[5] = 0;
		array[6] = 184;
		global::\u0008\u2002.\u0002.\u0003(4, array, ref array2);
		\u0008\u2001 = array2[1];
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031815161) + \u0008\u2001 + \u0002\u0005.\u0003(1031815139) + array2[0] + \u0002\u0005.\u0003(1031815121) + array2[2] + \u0002\u0005.\u0003(1031815119) + array2[3]);
		byte[] array3 = new byte[1];
		array[0] = (byte)\u0008\u2001;
		global::\u0008\u2002.\u0002.\u0003(4, 80, 1, array3);
	}

	public int \u0003(string \u0003, int \u0006)
	{
		double num = 0.0;
		num = ((\u0003 == \u0002\u0005.\u0003(1031815101) || \u0003 == \u0002\u0005.\u0003(1031815095)) ? ((\u0006 <= 60) ? ((double)(\u0006 - 1)) : ((double)(\u0006 - 12) * 0.33 + 44.0)) : ((\u0003 == \u0002\u0005.\u0003(1031815073)) ? ((\u0006 <= 50) ? ((double)(\u0006 - 1)) : ((double)(\u0006 - 35) * 0.41 + 43.7)) : ((\u0003 == \u0002\u0005.\u0003(1031815067)) ? ((\u0006 <= 50) ? ((double)(\u0006 - 5)) : ((double)(\u0006 - 9) * 0.22 + 43.7)) : ((\u0003 == \u0002\u0005.\u0003(1031815061)) ? ((\u0006 <= 32) ? ((double)\u0006) : ((double)\u0006 * 0.5 + 16.0)) : ((!(\u0003 == \u0002\u0005.\u0003(1031815055))) ? ((double)\u0006) : ((\u0006 <= 26) ? ((double)\u0006) : ((double)\u0006 * 0.5 + 13.0)))))));
		return (int)Math.Round(num, 0);
	}

	public string \u0003()
	{
		return \u0002\u0005.\u0003(1031815033);
	}
}
internal static class \u0003
{
}
internal sealed class \u0003\u0005
{
	public enum \u0002
	{

	}

	public enum \u0002\u0005
	{

	}

	public enum \u0003
	{

	}

	public enum \u0003\u0005
	{

	}

	public enum \u0005
	{

	}

	public enum \u0006
	{

	}

	public enum \u0006\u0005
	{

	}

	public enum \u0008
	{

	}

	public enum \u0008\u0005
	{

	}

	public enum \u000e
	{

	}

	public enum \u000f
	{

	}

	public enum \u000f\u0005
	{

	}
}
public sealed class \u0003\u2001
{
	private string m_\u0003;

	private StringBuilder m_\u0006 = new StringBuilder(255);

	private string m_\u000f = \u0002\u0005.\u0003(1031809548);

	private int \u0002 = 255;

	private string \u0008 = \u0002\u0005.\u0003(1031809543);

	public \u0003\u2001()
	{
		this.m_\u0003 = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
		\u0008 = \u0008\u2002.\u0003.\u0006();
	}

	[DllImport("kernel32", CharSet = CharSet.Unicode, EntryPoint = "GetPrivateProfileString")]
	public static extern int \u0003(string \u0003, string \u0006, string \u000f, StringBuilder \u0002, int \u0008, string \u0005);

	[DllImport("kernel32", EntryPoint = "WritePrivateProfileString")]
	private static extern long \u0003(string \u0003, string \u0006, string \u000f, string \u0002);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetPrivateProfileSection")]
	private static extern int \u0003(string \u0003, byte[] \u0006, int \u000f, string \u0002);

	public string \u0003(string \u0003)
	{
		\u0003\u2001.\u0003(\u0008, \u0003, this.m_\u000f, this.m_\u0006, \u0002, this.m_\u0003 + \u0002\u0005.\u0003(1031810035));
		return this.m_\u0006.ToString();
	}

	public string \u0003(string \u0003, string \u0006)
	{
		\u0003\u2001.\u0003(\u0003, \u0006, this.m_\u000f, this.m_\u0006, \u0002, this.m_\u0003 + \u0002\u0005.\u0003(1031810009));
		return this.m_\u0006.ToString();
	}

	public string \u0006(string \u0003, string \u0006)
	{
		\u0003\u2001.\u0003(\u0003, \u0006, this.m_\u000f, this.m_\u0006, \u0002, this.m_\u0003 + \u0002\u0005.\u0003(1031809992));
		return this.m_\u0006.ToString();
	}

	public string \u000f(string \u0003, string \u0006)
	{
		\u0003\u2001.\u0003(\u0003, \u0006, this.m_\u000f, this.m_\u0006, \u0002, this.m_\u0003 + \u0002\u0005.\u0003(1031809978));
		return this.m_\u0006.ToString();
	}

	public List<string> \u0003(string \u0003)
	{
		byte[] array = new byte[128];
		\u0003\u2001.\u0003(\u0003, array, 128, this.m_\u0003 + \u0002\u0005.\u0003(1031810009));
		string[] array2 = Encoding.Unicode.GetString(array).Trim(default(char)).Split(default(char));
		if (array2.Length == 0)
		{
			return null;
		}
		if (array2.Length == 1 && array2[0] == string.Empty)
		{
			return null;
		}
		List<string> list = new List<string>();
		string[] array3 = array2;
		foreach (string text in array3)
		{
			list.Add(text.Substring(0, text.IndexOf(\u0002\u0005.\u0003(1031809965))));
		}
		return list;
	}
}
internal sealed class \u0003\u2002
{
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 10)]
	private struct \u0003
	{
	}

	internal static readonly \u0003 \u0003/* Not supported: data(00 1E 28 2D 32 3C 46 50 5A 64) */;

	internal static readonly \u0003 \u0006/* Not supported: data(23 28 2D 32 3C 41 46 50 5A 64) */;
}
internal interface \u0005<\u0003> : \u0002, \u0008
{
	[SpecialName]
	new \u0003 \u0002\u2009\u2005\u2006\u0003();
}
internal static class \u0005\u2001
{
	private sealed class \u0002
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static int \u0003()
		{
			return \u0005\u2001.\u0006.\u000f(\u0005\u2001.\u0006.\u0003(\u0008.\u0003() ^ -(~(~(-(-(~(~(-(-(~(~-527758446)))))))))), \u0005\u2001.\u0003(typeof(\u0005))), \u0005\u2001.\u0006.\u0006(\u0005\u2001.\u0003(typeof(\u000f)) ^ \u0005\u2001.\u0003(typeof(\u0003\u2002)), -(~(~(-(~(-(~(-(~-915151888))))))))));
		}
	}

	private sealed class \u0003
	{
		private int m_\u0003;

		private int \u0006;

		internal \u0003()
		{
			\u0003(0L);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		internal long \u0003()
		{
			if ((object)Assembly.GetCallingAssembly() != typeof(\u0003).Assembly)
			{
				return 2918384L;
			}
			if (!\u0005\u2001.\u0003())
			{
				return 2918384L;
			}
			int[] array = new int[4];
			array[3] = ~(-(~(-(-(~(~(-(~546425339))))))));
			array[1] = -(~(-(~(-(~(~(-(~-418352871))))))));
			array[2] = -(~(~(-(~(-(~(-(~-897921266))))))));
			array[0] = -(~(~(-(-(~(-(~(~1880361339))))))));
			int num = this.m_\u0003;
			int num2 = \u0006;
			int num3 = ~(-(~(-(-(~(~(-(~(-(~1640531523))))))))));
			int num4 = -(~(-(~(-(~(~(-(~957401313))))))));
			for (int i = 0; i != 32; i++)
			{
				num2 -= (((num << 4) ^ (num >> 5)) + num) ^ (num4 + array[(num4 >> 11) & 3]);
				num4 -= num3;
				num -= (((num2 << 4) ^ (num2 >> 5)) + num2) ^ (num4 + array[num4 & 3]);
			}
			for (int j = 0; j != 4; j++)
			{
				array[j] = 0;
			}
			ulong num5 = (ulong)((long)num2 << 32);
			return (long)(num5 | (uint)num);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void \u0003(long \u0003)
		{
			if ((object)Assembly.GetCallingAssembly() == typeof(\u0003).Assembly && \u0005\u2001.\u0003())
			{
				int[] array = new int[4];
				array[1] = -(~(~(-(-(~(-(~(~(-(~-418352872))))))))));
				array[0] = -(~(-(~(~(-(-(~(~1880361339))))))));
				array[2] = -(~(~(-(-(~(-(~(-(~(~-897921261))))))))));
				array[3] = ~(-(-(~(~(-(~(-(-(~(~546425340))))))))));
				int num = -(~(-(~(~(-(-(~(~1640531528))))))));
				int num2 = (int)\u0003;
				int num3 = (int)(\u0003 >> 32);
				int num4 = 0;
				for (int i = 0; i != 32; i++)
				{
					num2 += (((num3 << 4) ^ (num3 >> 5)) + num3) ^ (num4 + array[num4 & 3]);
					num4 += num;
					num3 += (((num2 << 4) ^ (num2 >> 5)) + num2) ^ (num4 + array[(num4 >> 11) & 3]);
				}
				for (int j = 0; j != 4; j++)
				{
					array[j] = 0;
				}
				this.m_\u0003 = num2;
				\u0006 = num3;
			}
		}
	}

	private sealed class \u0003\u2002
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static int \u0003()
		{
			return \u0005\u2001.\u0006.\u0003(\u0005\u2001.\u0003(typeof(\u0003\u2002)), \u0005\u2001.\u0006.\u000f(\u0005\u2001.\u0006.\u0006(\u0005\u2001.\u0003(typeof(\u000e)), \u0005\u2001.\u0003(typeof(\u000f))), \u0005\u2001.\u0006.\u000f(\u0005\u2001.\u0003(typeof(\u0002)) ^ -(~(~(-(-(~(-(~(~-256353473)))))))), \u000e.\u0003())));
		}
	}

	private sealed class \u0005
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static int \u0003()
		{
			return \u0005\u2001.\u0006.\u000f(\u0005\u2001.\u0003(typeof(\u0005)), \u0005\u2001.\u0006.\u0003(\u0005\u2001.\u0003(typeof(\u000f)), \u0005\u2001.\u0006.\u0006(\u0005\u2001.\u0003(typeof(\u0008)), \u0005\u2001.\u0006.\u000f(\u0005\u2001.\u0003(typeof(\u0002)), \u0005\u2001.\u0006.\u0003(\u0005\u2001.\u0003(typeof(\u000e)), \u0005\u2001.\u0003(typeof(\u0003\u2002)))))));
		}
	}

	private static class \u0006
	{
		internal static int \u0003(int \u0003, int \u0006)
		{
			return \u0003 ^ (\u0006 - -(~(~(-(~(-(~(-(~-1179895243)))))))));
		}

		internal static int \u0006(int \u0003, int \u0006)
		{
			return (\u0003 - -(~(~(-(-(~(-(~(~1721078672))))))))) ^ (\u0006 + -(~(~(-(~(-(~(-(~1503732673)))))))));
		}

		internal static int \u000f(int \u0003, int \u0006)
		{
			return \u0003 ^ ((\u0006 - -(~(-(~(~(-(-(~(-(~(~-2116206608))))))))))) ^ (\u0003 - \u0006));
		}
	}

	private sealed class \u0008
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static int \u0003()
		{
			return \u0005\u2001.\u0006.\u0003(\u0005\u2001.\u0003(typeof(\u0002)), \u0005\u2001.\u0003(typeof(\u0005)) ^ \u0005\u2001.\u0006.\u0006(\u0005\u2001.\u0003(typeof(\u0008)), \u0005\u2001.\u0006.\u000f(\u0005\u2001.\u0003(typeof(\u0003\u2002)), \u0005.\u0003())));
		}
	}

	private sealed class \u000e
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static int \u0003()
		{
			return \u0005\u2001.\u0006.\u0006(\u0005\u2001.\u0006.\u0006(\u0002.\u0003(), \u0005\u2001.\u0006.\u0003(\u0005\u2001.\u0003(typeof(\u000e)), \u0008.\u0003())), \u0005\u2001.\u0003(typeof(\u0003\u2002)));
		}
	}

	private sealed class \u000f
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static int \u0003()
		{
			return \u0005\u2001.\u0006.\u000f(\u0005\u2001.\u0006.\u0006(\u0005\u2001.\u0003(typeof(\u0008)), \u0005\u2001.\u0006.\u000f(\u0005\u2001.\u0003(typeof(\u000f)), \u0005\u2001.\u0003(typeof(\u000e)))), \u0003\u2002.\u0003());
		}
	}

	private static \u0003 m_\u0003 = new \u0003();

	[MethodImpl(MethodImplOptions.NoInlining)]
	internal static long \u0003()
	{
		if ((object)Assembly.GetCallingAssembly() != typeof(\u0005\u2001).Assembly || !\u0003())
		{
			return 0L;
		}
		lock (\u0005\u2001.m_\u0003)
		{
			long num = \u0005\u2001.m_\u0003.\u0003();
			if (num == 0)
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				List<byte> list = new List<byte>();
				AssemblyName assemblyName;
				try
				{
					assemblyName = executingAssembly.GetName();
				}
				catch
				{
					assemblyName = new AssemblyName(executingAssembly.FullName);
				}
				byte[] array = assemblyName.GetPublicKeyToken();
				if (array != null && array.Length == 0)
				{
					array = null;
				}
				if (array != null)
				{
					list.AddRange(array);
				}
				list.AddRange(Encoding.Unicode.GetBytes(assemblyName.Name));
				int num2 = \u0003(typeof(\u0005\u2001));
				int num3 = \u000f.\u0003();
				list.Add((byte)num2);
				list.Add((byte)(num3 >> 8));
				list.Add((byte)(num2 >> 24));
				list.Add((byte)(num3 >> 16));
				list.Add((byte)(num2 >> 8));
				list.Add((byte)(num3 >> 24));
				list.Add((byte)(num2 >> 16));
				list.Add((byte)num3);
				int count = list.Count;
				ulong num4 = 0uL;
				for (int i = 0; i != count; i++)
				{
					num4 += list[i];
					num4 += num4 << 20;
					num4 ^= num4 >> 12;
					list[i] = 0;
				}
				num4 += num4 << 6;
				num4 ^= num4 >> 22;
				num4 += num4 << 30;
				num = (long)num4;
				num ^= 0x1500E0F3553F5CD2L;
				\u0005\u2001.m_\u0003.\u0003(num);
			}
			return num;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private static bool \u0003()
	{
		if (!\u0006())
		{
			return false;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private static bool \u0006()
	{
		StackTrace stackTrace = new StackTrace();
		Type type = (stackTrace.GetFrame(3)?.GetMethod())?.DeclaringType;
		if ((object)type == typeof(RuntimeMethodHandle))
		{
			return false;
		}
		if ((object)type == null)
		{
			return false;
		}
		if ((object)type.Assembly != typeof(\u0005\u2001).Assembly)
		{
			return false;
		}
		return true;
	}

	private static int \u0003(Type \u0003)
	{
		return \u0003.MetadataToken;
	}
}
public sealed class \u0005\u2002
{
	[DllImport("InsydeDCHU.dll", EntryPoint = "GetDCHU_Data_Integer")]
	public static extern int \u0003(int \u0003, ref int \u0006);

	[DllImport("InsydeDCHU.dll", EntryPoint = "SetDCHU_Data")]
	public static extern int \u0003(int \u0003, byte[] \u0006, int \u000f);

	[DllImport("InsydeDCHU.dll", EntryPoint = "SetDCHU_DataEx")]
	public static extern int \u0003(int \u0003, byte[] \u0006, int \u000f, ref byte \u0002);

	[DllImport("InsydeDCHU.dll", EntryPoint = "GetDCHU_Data_Buffer")]
	public static extern int \u0003(int \u0003, ref byte \u0006);

	[DllImport("InsydeDCHU.dll", EntryPoint = "ReadAppSettings")]
	public static extern int \u0003(int \u0003, int \u0006, int \u000f, ref byte \u0002);

	[DllImport("InsydeDCHU.dll", EntryPoint = "WriteAppSettings")]
	public static extern int \u0006(int \u0003, int \u0006, int \u000f, ref byte \u0002);

	public int \u0003(int \u0003, uint \u0006)
	{
		byte[] array = new byte[4];
		array = BitConverter.GetBytes(\u0006);
		int result = \u0005\u2002.\u0003(\u0003, array, 4);
		\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031815025) + \u0003 + \u0002\u0005.\u0003(1031813714) + \u0006);
		return result;
	}

	public int \u0003(int \u0003, int \u0006, uint \u000f)
	{
		byte[] array = new byte[4];
		array = BitConverter.GetBytes(\u000f);
		array[3] = Convert.ToByte(\u0006);
		int result = \u0005\u2002.\u0003(\u0003, array, 4);
		\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031815025) + \u0003 + \u0002\u0005.\u0003(1031815011) + \u0006 + \u0002\u0005.\u0003(1031813714) + \u000f);
		return result;
	}

	public int \u0003(int \u0003, byte[] \u0006)
	{
		int result = \u0005\u2002.\u0003(\u0003, \u0006, 256);
		\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031814994) + \u0003);
		return result;
	}

	public int \u0003(int \u0003, byte[] \u0006, ref byte[] \u000f)
	{
		int num = 0;
		try
		{
			num = \u0005\u2002.\u0003(\u0003, \u0006, 256, ref \u000f[0]);
		}
		catch
		{
			return 0;
		}
		return num;
	}

	public int \u0003(int \u0003)
	{
		int result = 0;
		\u0005\u2002.\u0003(\u0003, ref result);
		\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031814972) + \u0003 + \u0002\u0005.\u0003(1031813714) + result);
		return result;
	}

	public byte[] \u0003(int \u0003)
	{
		byte[] array = new byte[256];
		\u0005\u2002.\u0003(\u0003, ref array[0]);
		return array;
	}

	public byte[] \u0003(int \u0003, int \u0006, int \u000f)
	{
		byte[] array = new byte[\u000f];
		\u0005\u2002.\u0003(\u0003, \u0006, \u000f, ref array[0]);
		return array;
	}

	public int \u0003(int \u0003, int \u0006, int \u000f, byte[] \u0002)
	{
		return \u0005\u2002.\u0006(\u0003, \u0006, \u000f, ref \u0002[0]);
	}

	public byte[] \u0003(int \u0003, int \u0006)
	{
		byte[] array = new byte[\u0006];
		try
		{
			\u0005\u2002.\u0003(\u0003 / 256, \u0003 % 256, \u0006, ref array[0]);
		}
		catch
		{
		}
		return array;
	}

	public int \u0003(int \u0003, int \u0006, byte[] \u000f)
	{
		int result;
		try
		{
			result = \u0005\u2002.\u0006(\u0003 / 256, \u0003 % 256, \u0006, ref \u000f[0]);
		}
		catch
		{
			return 0;
		}
		return result;
	}

	public void \u0003()
	{
		this.\u0003(70);
		byte[] array = new byte[256];
		array = \u0008\u2002.\u0002.\u0003(7, 0, 256);
		switch ((array[0] << 8) + array[1])
		{
		case 0:
			\u0003(array);
			break;
		case 256:
			\u0006(array);
			break;
		default:
			\u0003(array);
			break;
		}
	}

	private void \u0003(byte[] \u0003)
	{
		int num = this.\u0003(16);
		if ((num & 1) == 1)
		{
			\u0008\u2002.\u0006.\u000f\u2002 = true;
		}
		if ((num & 2) == 2)
		{
			\u0008\u2002.\u0006.\u0002\u2002 = true;
		}
		if ((num & 4) == 4)
		{
			\u0008\u2002.\u0006.\u0008\u2002 = true;
		}
		if ((num & 8) == 8)
		{
			\u0008\u2002.\u0006.\u0005\u2002 = true;
		}
		if ((num & 0x10) == 16)
		{
			\u0008\u2002.\u0006.\u000e\u2002 = true;
		}
		if ((num & 0x20) == 32)
		{
			\u0008\u2002.\u0006.\u0003\u2001 = true;
		}
		if ((num & 0x40) == 64)
		{
			\u0008\u2002.\u0006.\u0006\u2001 = true;
		}
		if ((num & 0x80) == 128)
		{
			\u0008\u2002.\u0006.\u000f\u2001 = true;
		}
		int num2 = this.\u0003(122);
		if (((num2 >> 15) & 1) == 1)
		{
			\u0008\u2002.\u0006.\u0006\u2002 = true;
		}
		int num3 = this.\u0003(96);
		if (((num3 >> 7) & 1) == 1)
		{
			\u0008\u2002.\u0006.\u0005\u2001 = true;
		}
		if (((num3 >> 10) & 1) == 1)
		{
			\u0008\u2002.\u0006.\u000e\u2001 = false;
		}
		if ((\u0003[17] & 0xFu) != 0)
		{
			\u0008\u2002.\u0006.\u000e\u2009.\u0006\u2002 = true;
			if ((\u0003[17] & 1) == 1)
			{
				\u0008\u2002.\u0006.\u000e\u2009.\u000f\u2002 = true;
			}
			if (((\u0003[17] >> 1) & 1) == 1)
			{
				\u0008\u2002.\u0006.\u000e\u2009.\u0002\u2002 = true;
			}
			if (((\u0003[17] >> 2) & 1) == 1)
			{
				\u0008\u2002.\u0006.\u000e\u2009.\u0008\u2002 = true;
			}
			if (((\u0003[17] >> 3) & 1) == 1)
			{
				\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2002 = true;
			}
		}
		\u0008\u2002.\u0006.\u000e\u2009.\u000e\u2002 = (\u0003[17] >> 4) & 0xF;
	}

	private void \u0006(byte[] \u0003)
	{
		_ = \u0003[5];
		_ = \u0003[4];
		_ = \u0003[3];
		_ = \u0003[2];
		int num = (\u0003[9] << 24) + (\u0003[8] << 16) + (\u0003[7] << 8) + \u0003[6];
		int num2 = (\u0003[11] << 8) + \u0003[10];
		int num3 = (\u0003[14] << 8) + \u0003[13];
		if ((num3 & 1) == 1)
		{
			\u0008\u2002.\u0006.\u000f\u2002 = true;
		}
		if ((num3 & 2) == 2)
		{
			\u0008\u2002.\u0006.\u0002\u2002 = true;
		}
		if ((num3 & 4) == 4)
		{
			\u0008\u2002.\u0006.\u0008\u2002 = true;
		}
		if ((num3 & 8) == 8)
		{
			\u0008\u2002.\u0006.\u0005\u2002 = true;
		}
		if ((num3 & 0x10) == 16)
		{
			\u0008\u2002.\u0006.\u000e\u2002 = true;
		}
		if ((num3 & 0x20) == 32)
		{
			\u0008\u2002.\u0006.\u0003\u2001 = true;
		}
		if ((num3 & 0x40) == 64)
		{
			\u0008\u2002.\u0006.\u0006\u2001 = true;
		}
		if ((num3 & 0x80) == 128)
		{
			\u0008\u2002.\u0006.\u000f\u2001 = true;
		}
		if (((num >> 15) & 1) == 1)
		{
			\u0008\u2002.\u0006.\u0006\u2002 = true;
		}
		if (((num2 >> 7) & 1) == 1)
		{
			\u0008\u2002.\u0006.\u0005\u2001 = true;
		}
		if (((num2 >> 10) & 1) == 1)
		{
			\u0008\u2002.\u0006.\u000e\u2001 = false;
		}
		if (((num2 >> 12) & 1) == 1)
		{
			\u0008\u2002.\u0006.\u0002\u2009 = true;
		}
		if (((\u0003[16] >> 4) & 1) == 1)
		{
			\u0008\u2002.\u0006.\u000e\u2009.\u0003\u2002 = true;
		}
		if ((\u0003[17] & 0xFu) != 0)
		{
			\u0008\u2002.\u0006.\u000e\u2009.\u0006\u2002 = true;
			if ((\u0003[17] & 1) == 1)
			{
				\u0008\u2002.\u0006.\u000e\u2009.\u000f\u2002 = true;
			}
			else
			{
				\u0008\u2002.\u0006.\u000e\u2009.\u000f\u2002 = false;
			}
			if (((\u0003[17] >> 1) & 1) == 1)
			{
				\u0008\u2002.\u0006.\u000e\u2009.\u0002\u2002 = true;
			}
			else
			{
				\u0008\u2002.\u0006.\u000e\u2009.\u0002\u2002 = false;
			}
			if (((\u0003[17] >> 2) & 1) == 1)
			{
				\u0008\u2002.\u0006.\u000e\u2009.\u0008\u2002 = true;
			}
			else
			{
				\u0008\u2002.\u0006.\u000e\u2009.\u0008\u2002 = false;
			}
			if (((\u0003[17] >> 3) & 1) == 1)
			{
				\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2002 = true;
			}
			else
			{
				\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2002 = false;
			}
			if (((\u0003[17] >> 7) & 1) == 1)
			{
				\u0008\u2002.\u0006.\u000e\u2009.\u0003\u2001 = true;
				\u0008\u2002.\u0006.\u000e\u2009.\u000f\u2002 = false;
			}
			else
			{
				\u0008\u2002.\u0006.\u000e\u2009.\u0003\u2001 = false;
			}
		}
		\u0008\u2002.\u0006.\u000e\u2009.\u000e\u2002 = (\u0003[17] >> 4) & 0xF;
		if ((\u0003[20] & 1) == 1)
		{
			\u0008\u2002.\u0006.\u000e\u2009.\u0002\u2001 = true;
		}
		if (((\u0003[20] >> 3) & 1) == 1)
		{
			\u0008\u2002.\u0006.\u000e\u2009.\u0008\u2001 = true;
		}
		if (((\u0003[20] >> 4) & 1) == 1)
		{
			\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001 = true;
		}
		if (((\u0003[20] >> 5) & 1) == 1)
		{
			\u0008\u2002.\u0006.\u000e\u2009.\u000e\u2001 = true;
		}
		if (((\u0003[20] >> 6) & 1) == 1)
		{
			\u0008\u2002.\u0006.\u000e\u2009.\u0003\u2009 = true;
		}
		if (((\u0003[20] >> 7) & 1) == 1)
		{
			\u0008\u2002.\u0006.\u000e\u2009.\u0006\u2009 = true;
		}
		if ((\u0003[21] & 1) == 1)
		{
			\u0008\u2002.\u0006.\u000e\u2009.\u000f\u2009 = true;
		}
		if (((\u0003[21] >> 1) & 1) == 1)
		{
			\u0008\u2002.\u0006.\u000e\u2009.\u0002\u2009 = true;
		}
		\u0006();
	}

	private void \u0006()
	{
		if (\u0008\u2002.\u0006.\u000e\u2009.\u000e\u2001 || \u0008\u2002.\u0006.\u000e\u2009.\u0003\u2009 || \u0008\u2002.\u0006.\u000e\u2009.\u0006\u2009 || \u0008\u2002.\u0006.\u000e\u2009.\u000f\u2009 || \u0008\u2002.\u0006.\u000e\u2009.\u0002\u2009 || \u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
		{
			\u0008\u2002.\u0006.\u0005\u2009 = true;
		}
	}

	public string \u0003()
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		byte[] array3 = new byte[24];
		string text = \u0002\u0005.\u0003(1031814958);
		array[0] = 1;
		array[1] = 0;
		array[2] = 0;
		array[3] = 0;
		array[4] = 0;
		array[5] = 0;
		array[6] = 222;
		\u0003(4, array, ref array2);
		array[0] = 1;
		array[1] = 1;
		array[2] = 0;
		array[3] = 0;
		array[4] = 0;
		array[5] = 0;
		array[6] = 222;
		\u0003(4, array, ref array2);
		array3[0] = array2[1];
		array3[1] = array2[2];
		array3[2] = array2[3];
		array3[3] = array2[4];
		array3[4] = array2[5];
		array[0] = 1;
		array[1] = 2;
		array[2] = 0;
		array[3] = 0;
		array[4] = 0;
		array[5] = 0;
		array[6] = 222;
		\u0003(4, array, ref array2);
		array3[5] = array2[1];
		array3[6] = array2[2];
		array3[7] = array2[3];
		array3[8] = array2[4];
		array3[9] = array2[5];
		array[0] = 1;
		array[1] = 3;
		array[2] = 0;
		array[3] = 0;
		array[4] = 0;
		array[5] = 0;
		array[6] = 222;
		\u0003(4, array, ref array2);
		array3[10] = array2[1];
		array3[11] = array2[2];
		array3[12] = array2[3];
		array3[13] = array2[4];
		array3[14] = array2[5];
		text += Encoding.ASCII.GetString(array3);
		string[] array4 = text.Split('$');
		return array4[0].Trim(default(char));
	}
}
internal interface \u0006
{
	\u0002 \u0006\u2009\u2005\u2006\u0003();
}
public sealed class \u0006\u0005
{
	public enum \u0003
	{
		Auto,
		Max,
		silen,
		x3,
		x4,
		Maxq,
		Custom,
		x7,
		Noissles,
		IFSC
	}

	public double \u0003 = 255.0;

	public bool \u0006 = false;

	public bool \u000f = false;

	private List<string> m_\u0002 = new List<string>();

	private List<string> m_\u0008 = new List<string>
	{
		\u0002\u0005.\u0003(1031814951),
		\u0002\u0005.\u0003(1031814369)
	};

	public \u0003 \u0003()
	{
		byte[] array = \u0008\u2002.\u0002.\u0003(4, 5, 1);
		\u0008\u2002.\u0008.\u000e = array[0];
		return \u0006\u0005.\u0003.Auto;
	}

	public void \u0003(\u0003 \u0003)
	{
		\u0008\u2002.\u0008.\u000e = (int)\u0003;
		\u0008\u2002.\u0008.\u0003((byte)\u0003);
		byte[] array = new byte[1] { (byte)\u0003 };
		\u0008\u2002.\u0002.\u0003(4, 5, 1, array);
		if (\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001 && \u0003 == \u0003.Custom)
		{
			\u0008\u2002.\u000e.\u000e();
		}
		if (\u0008\u2002.\u0006.\u0005\u2009)
		{
			\u0008\u2002.\u0008.\u0003\u2002();
		}
		\u000f\u2002.\u0006(\u0002\u0005.\u0003(1031814670) + \u0003);
	}

	public void \u0003(bool \u0003)
	{
		byte[] array = new byte[1];
		if (\u0003)
		{
			\u0008\u2002.\u0008.\u0003(8);
			array[0] = 1;
		}
		else
		{
			\u0008\u2002.\u0008.\u0003(0);
			array[0] = 0;
		}
		\u0008\u2002.\u0002.\u0003(4, 9, 1, array);
		byte[] array2 = new byte[1] { 0 };
		\u0008\u2002.\u0002.\u0003(1029, 1, array2);
		if (\u0008\u2002.\u0006.\u0005\u2009)
		{
			\u0008\u2002.\u0008.\u0003\u2002();
		}
	}

	public int \u0003()
	{
		byte[] array = \u0008\u2002.\u0002.\u0003(4, 9, 1);
		return array[0];
	}

	public void \u0003()
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		array[0] = 1;
		array[1] = 11;
		array[2] = 0;
		array[3] = 0;
		array[4] = 0;
		array[5] = 0;
		array[6] = 192;
		\u0008\u2002.\u0002.\u0003(4, array, ref array2);
		\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031814311) + array2[2]);
		this.\u0003 = (int)array2[2];
	}

	public void \u0006()
	{
		if (\u0008\u2002.\u0006.\u000e\u2009.\u0003\u2002)
		{
			this.\u0006 = true;
		}
	}

	public void \u000f()
	{
		if (!this.\u000f)
		{
			\u0002();
		}
	}

	public void \u0006(bool \u0003)
	{
		byte[] array = new byte[1];
		byte[] array2 = new byte[1];
		if (\u0003)
		{
			array[0] = 1;
		}
		else
		{
			array[0] = 0;
			array2[0] = 0;
		}
		\u0008\u2002.\u0008.\u000e\u2002 = array[0];
		\u0008\u2002.\u0002.\u0003(4, 8, 1, array);
		\u0008\u2002.\u0002.\u0003(1, 32, 1, array2);
		int num = 2 | (array[0] << 6) | (array2[0] << 7);
		\u0008\u2002.\u0002.\u0003(121, 25, (uint)num);
		byte[] array3 = new byte[1] { 0 };
		\u0008\u2002.\u0002.\u0003(4, 5, 1, array3);
		\u0008\u2002.\u0002.\u0003(1096, 1, array3);
		if (\u0003)
		{
			\u0008\u2002.\u0002.\u0003(121, 1, (uint)array3[0]);
		}
	}

	private void \u0002()
	{
		if (!this.\u000f)
		{
			\u0003(RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64), this.m_\u0008, this.m_\u0002);
		}
		if (!this.\u000f)
		{
			\u0003(RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64), this.m_\u0008, this.m_\u0002);
		}
		if (!this.\u000f)
		{
			\u0008();
		}
	}

	private void \u0008()
	{
		try
		{
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(\u0002\u0005.\u0003(1031814284), \u0002\u0005.\u0003(1031814269));
			foreach (ManagementObject item in managementObjectSearcher.Get())
			{
				string text = (string)item[\u0002\u0005.\u0003(1031814231)];
				string text2 = (string)item[\u0002\u0005.\u0003(1031814201)];
				_ = (string)item[\u0002\u0005.\u0003(1031814185)];
				if (text2 != null && text.Contains(\u0002\u0005.\u0003(1031814173)))
				{
					this.\u000f = true;
					break;
				}
			}
		}
		catch
		{
		}
	}

	private void \u0003(RegistryKey \u0003, List<string> \u0006, List<string> \u000f)
	{
		string empty = string.Empty;
		foreach (string item in \u0006)
		{
			using RegistryKey registryKey = \u0003.OpenSubKey(item);
			if (registryKey == null)
			{
				continue;
			}
			string[] subKeyNames = registryKey.GetSubKeyNames();
			foreach (string name in subKeyNames)
			{
				using RegistryKey registryKey2 = registryKey.OpenSubKey(name);
				try
				{
					if (registryKey2.GetValue(\u0002\u0005.\u0003(1031814146)) != null)
					{
						empty = registryKey2.GetValue(\u0002\u0005.\u0003(1031814146)).ToString();
						if (empty.Contains(\u0002\u0005.\u0003(1031814173)))
						{
							this.\u000f = true;
							break;
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}
	}
}
public sealed class \u0006\u2001
{
	public string \u0003 = \u0002\u0005.\u0003(1031809543);

	private int m_\u0006;

	private int m_\u000f;

	private int m_\u0002;

	public double \u0008;

	public double \u0005;

	public double \u000e;

	public double \u0003\u2002;

	public string \u0006\u2002;

	public string \u000f\u2002;

	public int \u0002\u2002;

	public int \u0008\u2002;

	public int \u0005\u2002;

	public int \u000e\u2002;

	public int \u0003\u2001;

	public int \u0006\u2001;

	public int \u000f\u2001;

	public bool \u0002\u2001 = false;

	[DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSection")]
	private static extern int \u0003(string \u0003, byte[] \u0006, int \u000f, string \u0002);

	public void \u0003()
	{
	}

	public int \u0003()
	{
		string text = Convert.ToString(Registry.GetValue(\u0002\u0005.\u0003(1031809957), \u0002\u0005.\u0003(1031809900), \u0002\u0005.\u0003(1031809548)));
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 1;
		}
		return Convert.ToInt16(text);
	}

	public void \u0003(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031809957), \u0002\u0005.\u0003(1031809900), \u0003.ToString());
	}

	public int \u0006()
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031809876), \u0002\u0005.\u0003(1031809818), \u0002\u0005.\u0003(1031809548)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 10617087;
		}
		return Convert.ToInt32(text);
	}

	public void \u0006(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031809876), \u0002\u0005.\u0003(1031809818), \u0003.ToString());
	}

	public int \u000f()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031809957), \u0002\u0005.\u0003(1031814718), 0));
	}

	public void \u000f(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031809957), \u0002\u0005.\u0003(1031814718), \u0003, RegistryValueKind.DWord);
	}

	public string \u0003()
	{
		return Convert.ToString(Registry.GetValue(\u0002\u0005.\u0003(1031809805), \u0002\u0005.\u0003(1031809232), \u0002\u0005.\u0003(1031814958)));
	}

	public void \u0003(string \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031809805), \u0002\u0005.\u0003(1031809232), \u0003, RegistryValueKind.String);
	}

	public int \u0002()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031809957), \u0002\u0005.\u0003(1031809216), 0));
	}

	public void \u0002(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031809957), \u0002\u0005.\u0003(1031809216), \u0003, RegistryValueKind.DWord);
	}

	public int \u0008()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031809957), \u0002\u0005.\u0003(1031809203), 0));
	}

	public void \u0008(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031809957), \u0002\u0005.\u0003(1031809203), \u0003, RegistryValueKind.DWord);
	}

	public double \u0003()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031809957), \u0002\u0005.\u0003(1031809191), 0));
	}

	public void \u0003(double \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031809957), \u0002\u0005.\u0003(1031809191), \u0003, RegistryValueKind.DWord);
	}

	public double \u0006()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031809957), \u0002\u0005.\u0003(1031809162), 0));
	}

	public void \u0006(double \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031809957), \u0002\u0005.\u0003(1031809162), \u0003, RegistryValueKind.DWord);
	}

	public int \u0005()
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031809150), \u0002\u0005.\u0003(1031809057), \u0002\u0005.\u0003(1031809026)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 5;
		}
		return Convert.ToInt32(text);
	}

	public void \u0005(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031809150), \u0002\u0005.\u0003(1031809057), \u0003, RegistryValueKind.DWord);
	}

	public int \u000e()
	{
		return Convert.ToInt32(Registry.GetValue(\u0002\u0005.\u0003(1031809876), \u0002\u0005.\u0003(1031809530), \u0002\u0005.\u0003(1031815033)));
	}

	public int \u0003\u2002()
	{
		return Convert.ToInt32(Registry.GetValue(\u0002\u0005.\u0003(1031809876), \u0002\u0005.\u0003(1031809499), \u0002\u0005.\u0003(1031815033)));
	}

	public string \u0006()
	{
		string empty = string.Empty;
		empty = Convert.ToString(Registry.GetValue(\u0002\u0005.\u0003(1031809957), \u0002\u0005.\u0003(1031809464), string.Empty));
		if (empty == string.Empty)
		{
			empty = CultureInfo.CurrentCulture.Name.ToLower();
			\u0006(empty);
		}
		if (empty.Contains(\u0002\u0005.\u0003(1031809463)))
		{
			if (empty == \u0002\u0005.\u0003(1031809440))
			{
				global::\u0008\u2002.\u0008\u2002 = 0;
			}
			else
			{
				global::\u0008\u2002.\u0008\u2002 = 1;
			}
			empty = \u0002\u0005.\u0003(1031809440);
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031809436)))
		{
			empty = \u0002\u0005.\u0003(1031809436);
			global::\u0008\u2002.\u0008\u2002 = 1;
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031809429)))
		{
			empty = \u0002\u0005.\u0003(1031809429);
			global::\u0008\u2002.\u0008\u2002 = 1;
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031809422)))
		{
			empty = \u0002\u0005.\u0003(1031809422);
			global::\u0008\u2002.\u0008\u2002 = 1;
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031809415)))
		{
			empty = \u0002\u0005.\u0003(1031809415);
			global::\u0008\u2002.\u0008\u2002 = 2;
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031809392)))
		{
			empty = \u0002\u0005.\u0003(1031809392);
			global::\u0008\u2002.\u0008\u2002 = 0;
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031809385)))
		{
			empty = \u0002\u0005.\u0003(1031809385);
			global::\u0008\u2002.\u0008\u2002 = 0;
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031809381)))
		{
			empty = \u0002\u0005.\u0003(1031809381);
			global::\u0008\u2002.\u0008\u2002 = 0;
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031809361)) || empty.Contains(\u0002\u0005.\u0003(1031809357)))
		{
			empty = \u0002\u0005.\u0003(1031809361);
			global::\u0008\u2002.\u0008\u2002 = 1;
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031809337)) || empty.Contains(\u0002\u0005.\u0003(1031809333)))
		{
			empty = \u0002\u0005.\u0003(1031809337);
			global::\u0008\u2002.\u0008\u2002 = 1;
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031809313)) || empty.Contains(\u0002\u0005.\u0003(1031809309)))
		{
			empty = \u0002\u0005.\u0003(1031809313);
			global::\u0008\u2002.\u0008\u2002 = 3;
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031809289)) || empty.Contains(\u0002\u0005.\u0003(1031809285)))
		{
			empty = \u0002\u0005.\u0003(1031809289);
			global::\u0008\u2002.\u0008\u2002 = 1;
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031810801)) || empty.Contains(\u0002\u0005.\u0003(1031810797)))
		{
			empty = \u0002\u0005.\u0003(1031810801);
			global::\u0008\u2002.\u0008\u2002 = 1;
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031810777)) || empty.Contains(\u0002\u0005.\u0003(1031810773)))
		{
			empty = \u0002\u0005.\u0003(1031810777);
			global::\u0008\u2002.\u0008\u2002 = 0;
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031810753)) || empty.Contains(\u0002\u0005.\u0003(1031810749)))
		{
			empty = \u0002\u0005.\u0003(1031810753);
			global::\u0008\u2002.\u0008\u2002 = 0;
		}
		else if (empty.Contains(\u0002\u0005.\u0003(1031810729)))
		{
			empty = \u0002\u0005.\u0003(1031810729);
			global::\u0008\u2002.\u0008\u2002 = 1;
		}
		else
		{
			empty = \u0002\u0005.\u0003(1031809440);
			global::\u0008\u2002.\u0008\u2002 = 0;
		}
		return empty;
	}

	public int \u0006\u2002()
	{
		return Convert.ToInt32(Registry.GetValue(\u0002\u0005.\u0003(1031809876), \u0002\u0005.\u0003(1031810722), \u0002\u0005.\u0003(1031815033)));
	}

	private void \u0006(string \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031809957), \u0002\u0005.\u0003(1031809464), \u0003);
	}

	public int \u000f\u2002()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031810708), \u0002\u0005.\u0003(1031810650), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u000e(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031810708), \u0002\u0005.\u0003(1031810650), \u0003, RegistryValueKind.DWord);
	}

	public int \u0002\u2002()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031810708), \u0002\u0005.\u0003(1031810635), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0003\u2002(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031810708), \u0002\u0005.\u0003(1031810635), \u0003, RegistryValueKind.DWord);
	}

	public int \u0008\u2002()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031810708), \u0002\u0005.\u0003(1031810621), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0006\u2002(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031810708), \u0002\u0005.\u0003(1031810621), \u0003, RegistryValueKind.DWord);
	}

	public int \u0005\u2002()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031810708), \u0002\u0005.\u0003(1031810607), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u000f\u2002(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031810708), \u0002\u0005.\u0003(1031810607), \u0003, RegistryValueKind.DWord);
	}

	public int \u000e\u2002()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031810708), \u0002\u0005.\u0003(1031810578), \u0002\u0005.\u0003(1031815033)));
	}

	public int \u0003\u2001()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031810708), \u0002\u0005.\u0003(1031810561), \u0002\u0005.\u0003(1031815033)));
	}

	public int \u0006\u2001()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031810708), \u0002\u0005.\u0003(1031811056), \u0002\u0005.\u0003(1031815033)));
	}

	public int \u000f\u2001()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031810708), \u0002\u0005.\u0003(1031811055), \u0002\u0005.\u0003(1031815033)));
	}

	public int \u0002\u2001()
	{
		string value = Registry.GetValue(\u0002\u0005.\u0003(1031811038), \u0002\u0005.\u0003(1031810981), \u0002\u0005.\u0003(1031815033)).ToString();
		return Convert.ToInt32(value);
	}

	public int \u0008\u2001()
	{
		string value = Registry.GetValue(\u0002\u0005.\u0003(1031811038), \u0002\u0005.\u0003(1031810961), \u0002\u0005.\u0003(1031815033)).ToString();
		return Convert.ToInt32(value);
	}

	public int \u0005\u2001()
	{
		string value = Registry.GetValue(\u0002\u0005.\u0003(1031811038), \u0002\u0005.\u0003(1031810957), \u0002\u0005.\u0003(1031815033)).ToString();
		return Convert.ToInt32(value);
	}

	public int \u000e\u2001()
	{
		string value = Registry.GetValue(\u0002\u0005.\u0003(1031811038), \u0002\u0005.\u0003(1031810938), \u0002\u0005.\u0003(1031815033)).ToString();
		return Convert.ToInt32(value);
	}

	public int \u0003\u2009()
	{
		string value = Registry.GetValue(\u0002\u0005.\u0003(1031811038), \u0002\u0005.\u0003(1031810935), \u0002\u0005.\u0003(1031815033)).ToString();
		return Convert.ToInt32(value);
	}

	public int \u0006\u2009()
	{
		string value = Registry.GetValue(\u0002\u0005.\u0003(1031811038), \u0002\u0005.\u0003(1031810917), \u0002\u0005.\u0003(1031815033)).ToString();
		return Convert.ToInt16(value);
	}

	public int \u000f\u2009()
	{
		string value = Registry.GetValue(\u0002\u0005.\u0003(1031811038), \u0002\u0005.\u0003(1031810898), \u0002\u0005.\u0003(1031815033)).ToString();
		return Convert.ToInt16(value);
	}

	public int \u0002\u2009()
	{
		string value = Registry.GetValue(\u0002\u0005.\u0003(1031811038), \u0002\u0005.\u0003(1031810895), \u0002\u0005.\u0003(1031815033)).ToString();
		return Convert.ToInt16(value);
	}

	public int \u0008\u2009()
	{
		string value = Registry.GetValue(\u0002\u0005.\u0003(1031810876), \u0002\u0005.\u0003(1031810830), \u0002\u0005.\u0003(1031811328)).ToString();
		return Convert.ToInt32(value);
	}

	public bool \u0003()
	{
		return Convert.ToBoolean(Registry.GetValue(\u0002\u0005.\u0003(1031810876), \u0002\u0005.\u0003(1031810303), null));
	}

	public bool \u0006()
	{
		return Convert.ToBoolean(Registry.GetValue(\u0002\u0005.\u0003(1031810876), \u0002\u0005.\u0003(1031810278), null));
	}

	public int \u0005\u2009()
	{
		string value = Registry.GetValue(\u0002\u0005.\u0003(1031810876), \u0002\u0005.\u0003(1031810251), \u0002\u0005.\u0003(1031815033)).ToString();
		return Convert.ToInt32(value);
	}

	public int \u0003(int \u0003, int \u0006)
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031810239) + \u0003, \u0002\u0005.\u0003(1031810168) + \u0006, \u0002\u0005.\u0003(1031809548)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 0;
		}
		return Convert.ToInt32(text);
	}

	public void \u0003(int \u0003, int \u0006, int \u000f)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031810239) + \u0003, \u0002\u0005.\u0003(1031810168) + \u0006, \u000f, RegistryValueKind.DWord);
	}

	public int \u0006(int \u0003, int \u0006)
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031810239) + \u0003, \u0002\u0005.\u0003(1031810160) + \u0006, \u0002\u0005.\u0003(1031809548)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 0;
		}
		return Convert.ToInt32(text);
	}

	public void \u0006(int \u0003, int \u0006, int \u000f)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031810239) + \u0003, \u0002\u0005.\u0003(1031810160) + \u0006, \u000f, RegistryValueKind.DWord);
	}

	public int \u000f(int \u0003, int \u0006)
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031810239) + \u0003, \u0002\u0005.\u0003(1031810152) + \u0006, \u0002\u0005.\u0003(1031809548)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 0;
		}
		return Convert.ToInt32(text);
	}

	public int \u0002(int \u0003, int \u0006)
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031810239) + \u0003, \u0002\u0005.\u0003(1031810136) + \u0006, \u0002\u0005.\u0003(1031809548)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 0;
		}
		return Convert.ToInt32(text);
	}

	public int \u000e\u2009()
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031810120), \u0002\u0005.\u0003(1031811354), \u0002\u0005.\u0003(1031815033)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 0;
		}
		return Convert.ToInt32(text);
	}

	public int \u0003\u2003()
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031810066), \u0002\u0005.\u0003(1031811354), \u0002\u0005.\u0003(1031815033)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 0;
		}
		return Convert.ToInt32(text);
	}

	public int \u0006\u2003()
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031810524), \u0002\u0005.\u0003(1031811354), \u0002\u0005.\u0003(1031815033)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 0;
		}
		return Convert.ToInt32(text);
	}

	public int \u000f\u2003()
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031810120), \u0002\u0005.\u0003(1031810470), \u0002\u0005.\u0003(1031815033)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 0;
		}
		return Convert.ToInt32(text);
	}

	public int \u0002\u2003()
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031810066), \u0002\u0005.\u0003(1031810470), \u0002\u0005.\u0003(1031815033)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 0;
		}
		return Convert.ToInt32(text);
	}

	public int \u0008\u2003()
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031810524), \u0002\u0005.\u0003(1031810470), \u0002\u0005.\u0003(1031815033)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 0;
		}
		return Convert.ToInt32(text);
	}

	public int \u0005\u2003()
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031810448), \u0002\u0005.\u0003(1031810405), \u0002\u0005.\u0003(1031815033)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 0;
		}
		return Convert.ToInt32(text);
	}

	public int \u000e\u2003()
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031810448), \u0002\u0005.\u0003(1031810387), \u0002\u0005.\u0003(1031815033)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 0;
		}
		return Convert.ToInt32(text);
	}

	public int \u0003\u2004()
	{
		string text = Registry.GetValue(\u0002\u0005.\u0003(1031810448), \u0002\u0005.\u0003(1031810371), \u0002\u0005.\u0003(1031815033)).ToString();
		if (text == \u0002\u0005.\u0003(1031809548))
		{
			return 0;
		}
		return Convert.ToInt32(text);
	}

	public byte \u0003()
	{
		return Convert.ToByte(Registry.GetValue(\u0002\u0005.\u0003(1031810448), \u0002\u0005.\u0003(1031810354), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0003(byte \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031810448), \u0002\u0005.\u0003(1031810354), \u0003, RegistryValueKind.DWord);
	}

	public bool \u000f()
	{
		return Convert.ToBoolean(Registry.GetValue(\u0002\u0005.\u0003(1031810876), \u0002\u0005.\u0003(1031810338), null));
	}

	public void \u0003(bool \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031810876), \u0002\u0005.\u0003(1031810338), \u0003);
	}

	public bool \u0002()
	{
		return Convert.ToBoolean(Registry.GetValue(\u0002\u0005.\u0003(1031810327), \u0002\u0005.\u0003(1031807709), null));
	}

	public void \u0006()
	{
		int num = (int)Registry.GetValue(\u0002\u0005.\u0003(1031807683), \u0002\u0005.\u0003(1031807625), null);
		if (((num >> 2) & 1) == 1)
		{
			this.\u0002\u2001 = true;
		}
		else
		{
			this.\u0002\u2001 = false;
		}
		if (this.\u0002\u2001)
		{
			this.\u000f\u2002 = Registry.GetValue(\u0002\u0005.\u0003(1031807622), \u0002\u0005.\u0003(1031807561), \u0002\u0005.\u0003(1031811144)).ToString();
		}
		this.\u0006\u2002 = Registry.GetValue(\u0002\u0005.\u0003(1031807622), \u0002\u0005.\u0003(1031807548), \u0002\u0005.\u0003(1031811144)).ToString();
		RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(\u0002\u0005.\u0003(1031807534) + this.\u0006\u2002, writable: false);
		this.\u0005\u2002 = (int)registryKey.GetValue(\u0002\u0005.\u0003(1031807503));
		this.\u0008\u2002 = (int)Registry.GetValue(\u0002\u0005.\u0003(1031810876), \u0002\u0005.\u0003(1031807993), null);
		this.\u0002\u2002 = (int)registryKey.GetValue(\u0002\u0005.\u0003(1031807976));
		this.\u0003\u2001 = (int)registryKey.GetValue(\u0002\u0005.\u0003(1031807963));
		this.\u0006\u2001 = (int)registryKey.GetValue(\u0002\u0005.\u0003(1031807947));
		this.\u000f\u2001 = ((this.\u0006\u2001 & 0x80000000u).Equals(0L) ? 1 : 0);
		this.\u0006\u2001 &= 268435455;
	}

	public int \u0006\u2004()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031807934), \u0002\u0005.\u0003(1031807876), \u0002\u0005.\u0003(1031815033)));
	}

	public int \u000f\u2004()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031807934), \u0002\u0005.\u0003(1031807857), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0002\u2002(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807934), \u0002\u0005.\u0003(1031807857), \u0003, RegistryValueKind.DWord);
	}

	public int \u0002\u2004()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031807934), \u0002\u0005.\u0003(1031807855), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0008\u2002(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807934), \u0002\u0005.\u0003(1031807855), \u0003, RegistryValueKind.DWord);
	}

	public int \u0008\u2004()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031807934), \u0002\u0005.\u0003(1031807838), \u0002\u0005.\u0003(1031812848)));
	}

	public void \u0005\u2002(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807934), \u0002\u0005.\u0003(1031807838), \u0003, RegistryValueKind.DWord);
	}

	public bool \u0008()
	{
		if (Convert.ToInt32(Registry.GetValue(\u0002\u0005.\u0003(1031807809), \u0002\u0005.\u0003(1031807752), \u0002\u0005.\u0003(1031815033))) == 0)
		{
			return false;
		}
		return true;
	}

	public int \u0005\u2004()
	{
		return Convert.ToInt32(Registry.GetValue(\u0002\u0005.\u0003(1031807809), \u0002\u0005.\u0003(1031807208), \u0002\u0005.\u0003(1031815033)));
	}

	public int \u000e\u2004()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031807934), \u0002\u0005.\u0003(1031807185), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u000e\u2002(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807934), \u0002\u0005.\u0003(1031807185), \u0003, RegistryValueKind.DWord);
	}

	public void \u0003(string \u0003, int \u0006, int \u000f, int \u0002)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807809) + \u0003, \u0002\u0005.\u0003(1031807171), \u0006, RegistryValueKind.DWord);
		Registry.SetValue(\u0002\u0005.\u0003(1031807809) + \u0003, \u0002\u0005.\u0003(1031807163), \u000f, RegistryValueKind.DWord);
		Registry.SetValue(\u0002\u0005.\u0003(1031807809) + \u0003, \u0002\u0005.\u0003(1031807155), \u0002, RegistryValueKind.DWord);
	}

	public int \u0003(string \u0003)
	{
		string value = Registry.GetValue(\u0002\u0005.\u0003(1031807934) + \u0003, \u0002\u0005.\u0003(1031807171), \u0002\u0005.\u0003(1031815033)).ToString();
		return Convert.ToInt32(value);
	}

	public int \u0006(string \u0003)
	{
		string value = Registry.GetValue(\u0002\u0005.\u0003(1031807934) + \u0003, \u0002\u0005.\u0003(1031807163), \u0002\u0005.\u0003(1031815033)).ToString();
		return Convert.ToInt32(value);
	}

	public int \u000f(string \u0003)
	{
		string value = Registry.GetValue(\u0002\u0005.\u0003(1031807934) + \u0003, \u0002\u0005.\u0003(1031807155), \u0002\u0005.\u0003(1031815033)).ToString();
		return Convert.ToInt32(value);
	}

	public void \u0003(string \u0003, byte \u0006, byte \u000f, byte \u0002)
	{
		int num = (\u0006 << 16) + (\u000f << 8) + \u0002;
		Registry.SetValue(\u0002\u0005.\u0003(1031807147), \u0003, num, RegistryValueKind.DWord);
	}

	public int \u0002(string \u0003)
	{
		string value = Registry.GetValue(\u0002\u0005.\u0003(1031807147), \u0003, \u0002\u0005.\u0003(1031815033)).ToString();
		return Convert.ToInt32(value);
	}

	public int \u0003\u2000()
	{
		return Convert.ToInt32(Registry.GetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807031), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0003\u2001(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807031), \u0003, RegistryValueKind.DWord);
	}

	public int \u0006\u2000()
	{
		return Convert.ToInt32(Registry.GetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807000), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0006\u2001(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807000), \u0003, RegistryValueKind.DWord);
	}

	public byte \u0006()
	{
		return Convert.ToByte(Registry.GetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031806986), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0006(byte \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031806986), \u0003, RegistryValueKind.DWord);
	}

	public byte \u000f()
	{
		return Convert.ToByte(Registry.GetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807480), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u000f(byte \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807480), \u0003, RegistryValueKind.DWord);
	}

	public byte \u0002()
	{
		return Convert.ToByte(Registry.GetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807478), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0002(byte \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807478), \u0003, RegistryValueKind.DWord);
	}

	public int \u000f\u2000()
	{
		return Convert.ToInt32(Registry.GetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807460), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u000f\u2001(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807460), \u0003, RegistryValueKind.DWord);
	}

	public byte \u0008()
	{
		return Convert.ToByte(Registry.GetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807447), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0008(byte \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807447), \u0003, RegistryValueKind.DWord);
	}

	public byte \u0005()
	{
		return Convert.ToByte(Registry.GetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807430), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0005(byte \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807430), \u0003, RegistryValueKind.DWord);
	}

	public byte \u000e()
	{
		return Convert.ToByte(Registry.GetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807413), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u000e(byte \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807413), \u0003, RegistryValueKind.DWord);
	}

	public int \u0002\u2000()
	{
		return Convert.ToInt32(Registry.GetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807396), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0002\u2001(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807396), \u0003, RegistryValueKind.DWord);
	}

	public byte \u0003\u2002()
	{
		return Convert.ToByte(Registry.GetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807383), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0003\u2002(byte \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807383), \u0003, RegistryValueKind.DWord);
	}

	public byte \u0006\u2002()
	{
		return Convert.ToByte(Registry.GetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807366), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0006\u2002(byte \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807366), \u0003, RegistryValueKind.DWord);
	}

	public byte \u000f\u2002()
	{
		return Convert.ToByte(Registry.GetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807349), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u000f\u2002(byte \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807349), \u0003, RegistryValueKind.DWord);
	}

	public int \u0008\u2000()
	{
		return Convert.ToInt32(Registry.GetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807332), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0008\u2001(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031807084), \u0002\u0005.\u0003(1031807332), \u0003, RegistryValueKind.DWord);
	}

	public int \u0005\u2000()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031807934), \u0002\u0005.\u0003(1031807319), \u0002\u0005.\u0003(1031815033)));
	}

	public string \u000f()
	{
		return \u0002\u0005.\u0003(1031815033);
	}

	public int \u0003(string \u0003, int \u0006)
	{
		double num = 0.0;
		num = ((\u0003 == \u0002\u0005.\u0003(1031815101) || \u0003 == \u0002\u0005.\u0003(1031815095)) ? ((\u0006 <= 60) ? ((double)(\u0006 - 1)) : ((double)(\u0006 - 12) * 0.33 + 44.0)) : ((\u0003 == \u0002\u0005.\u0003(1031815073)) ? ((\u0006 <= 50) ? ((double)(\u0006 - 1)) : ((double)(\u0006 - 35) * 0.41 + 43.7)) : ((\u0003 == \u0002\u0005.\u0003(1031815067)) ? ((\u0006 <= 50) ? ((double)(\u0006 - 5)) : ((double)(\u0006 - 9) * 0.22 + 43.7)) : ((\u0003 == \u0002\u0005.\u0003(1031815061)) ? ((\u0006 <= 32) ? ((double)\u0006) : ((double)\u0006 * 0.5 + 16.0)) : ((!(\u0003 == \u0002\u0005.\u0003(1031815055))) ? ((double)\u0006) : ((\u0006 <= 26) ? ((double)\u0006) : ((double)\u0006 * 0.5 + 13.0)))))));
		return (int)Math.Round(num, 0);
	}

	public void \u000f()
	{
		object value = Registry.GetValue(\u0002\u0005.\u0003(1031807683), \u0002\u0005.\u0003(1031807289), string.Empty);
		byte[] array = (byte[])value;
		this.m_\u0006 = \u0003(this.\u000f(), array[18]);
		this.m_\u000f = array[21];
		this.m_\u0002 = array[24];
	}

	public int \u000e\u2000()
	{
		return this.m_\u0006;
	}

	public int \u0003(int \u0003)
	{
		return \u0003 switch
		{
			0 => this.m_\u000f, 
			1 => this.m_\u0002, 
			_ => 0, 
		};
	}

	public int \u0003\u2007()
	{
		return Convert.ToInt32(Registry.GetValue(\u0002\u0005.\u0003(1031807286), \u0002\u0005.\u0003(1031807233), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0005\u2001(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031808752), \u0002\u0005.\u0003(1031807233), \u0003.ToString());
	}

	public int \u0006\u2007()
	{
		return Convert.ToInt32(Registry.GetValue(\u0002\u0005.\u0003(1031807286), \u0002\u0005.\u0003(1031808714), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u000e\u2001(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031808752), \u0002\u0005.\u0003(1031808714), \u0003.ToString());
	}

	public int \u000f\u2007()
	{
		return Convert.ToInt32(Registry.GetValue(\u0002\u0005.\u0003(1031808700), \u0002\u0005.\u0003(1031808641), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0003\u2009(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031808700), \u0002\u0005.\u0003(1031808641), \u0003, RegistryValueKind.DWord);
	}

	public string \u0002()
	{
		return Convert.ToString(Registry.GetValue(\u0002\u0005.\u0003(1031808700), \u0002\u0005.\u0003(1031808617), string.Empty));
	}

	public void \u000f(string \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031808700), \u0002\u0005.\u0003(1031808617), \u0003, RegistryValueKind.String);
	}

	public string \u0008()
	{
		return Convert.ToString(Registry.GetValue(\u0002\u0005.\u0003(1031808700), \u0002\u0005.\u0003(1031808602), string.Empty));
	}

	public void \u0002(string \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031808700), \u0002\u0005.\u0003(1031808602), \u0003, RegistryValueKind.String);
	}

	public string \u0005()
	{
		return Convert.ToString(Registry.GetValue(\u0002\u0005.\u0003(1031808700), \u0002\u0005.\u0003(1031808597), string.Empty));
	}

	public void \u0008(string \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031808700), \u0002\u0005.\u0003(1031808597), \u0003, RegistryValueKind.String);
	}

	public string \u000e()
	{
		return Convert.ToString(Registry.GetValue(\u0002\u0005.\u0003(1031808590), \u0002\u0005.\u0003(1031808527), \u0002\u0005.\u0003(1031809548)));
	}

	public void \u0005(string \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031808590), \u0002\u0005.\u0003(1031808527), \u0003, RegistryValueKind.String);
	}

	public int \u0002\u2007()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031808590), \u0002\u0005.\u0003(1031809009), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0006\u2009(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031808590), \u0002\u0005.\u0003(1031809009), \u0003, RegistryValueKind.DWord);
	}

	public int \u0008\u2007()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031808590), \u0002\u0005.\u0003(1031808997), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u000f\u2009(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031808590), \u0002\u0005.\u0003(1031808997), \u0003, RegistryValueKind.DWord);
	}

	public int \u0005\u2007()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031808590), \u0002\u0005.\u0003(1031808962), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0002\u2009(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031808590), \u0002\u0005.\u0003(1031808962), \u0003, RegistryValueKind.DWord);
	}

	public int \u000e\u2007()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031808590), \u0002\u0005.\u0003(1031808943), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0008\u2009(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031808590), \u0002\u0005.\u0003(1031808943), \u0003, RegistryValueKind.DWord);
	}

	public int \u0003\u2005()
	{
		return Convert.ToInt16(Registry.GetValue(\u0002\u0005.\u0003(1031808590), \u0002\u0005.\u0003(1031808908), \u0002\u0005.\u0003(1031815033)));
	}

	public void \u0005\u2009(int \u0003)
	{
		Registry.SetValue(\u0002\u0005.\u0003(1031808590), \u0002\u0005.\u0003(1031808908), \u0003, RegistryValueKind.DWord);
	}
}
public sealed class \u0006\u2002
{
	public struct \u0003
	{
		public int \u0003;

		public int \u0006;

		public uint \u000f;
	}

	private int m_\u0003;

	private int m_\u0006;

	public \u0006\u2002()
	{
		\u0003();
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "RegisterWindowMessage", SetLastError = true)]
	private static extern int \u0003(string \u0003);

	[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SendNotifyMessage", SetLastError = true)]
	private static extern bool \u0003(int \u0003, int \u0006, int \u000f, int \u0002);

	[DllImport("User32.dll", EntryPoint = "SendMessage")]
	private static extern int \u0003(int \u0003, int \u0006, uint \u000f, uint \u0002);

	[DllImport("User32.dll", EntryPoint = "SendMessage")]
	private static extern int \u0003(int \u0003, int \u0006, int \u000f, ref \u0003 \u0002);

	[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "FindWindow", SetLastError = true)]
	private static extern IntPtr \u0003(string \u0003, string \u0006);

	[DllImport("DataAddress.dll", EntryPoint = "DataAddress")]
	private static extern uint \u0003(IntPtr \u0003);

	private void \u0003()
	{
		this.m_\u0003 = \u0003(\u0002\u0005.\u0003(1031813806));
		this.m_\u0006 = \u0003(\u0002\u0005.\u0003(1031813771));
	}

	public void \u0003(int \u0003, int \u0006)
	{
		\u0006\u2002.\u0003(65535, this.m_\u0003, \u0003, \u0006);
	}

	public void \u0006(int \u0003, int \u0006)
	{
		Console.WriteLine(\u0002\u0005.\u0003(1031813740) + \u0003 + \u0002\u0005.\u0003(1031813714) + \u0006);
		\u0003 obj = default(\u0003);
		int num = (int)\u0006\u2002.\u0003(null, \u0002\u0005.\u0003(1031813711));
		byte[] array = new byte[5]
		{
			Convert.ToByte(\u0003),
			Convert.ToByte(\u0006 & 0xFF),
			(byte)(\u0006 >> 8),
			(byte)(\u0006 >> 16),
			(byte)(\u0006 >> 24)
		};
		obj.\u0006 = array.GetUpperBound(0) + 1;
		obj.\u000f = \u0006\u2002.\u0003(Marshal.UnsafeAddrOfPinnedArrayElement(array, 0));
		\u0006\u2002.\u0003(num, 74, 2, ref obj);
	}

	public void \u0003(int \u0003)
	{
		\u0003 obj = default(\u0003);
		int num = (int)\u0006\u2002.\u0003(null, \u0002\u0005.\u0003(1031813711));
		byte[] array = new byte[1] { Convert.ToByte(\u0003) };
		obj.\u0006 = array.GetUpperBound(0) + 1;
		obj.\u000f = \u0006\u2002.\u0003(Marshal.UnsafeAddrOfPinnedArrayElement(array, 0));
		\u0006\u2002.\u0003(num, 74, 2, ref obj);
	}

	public void \u0006()
	{
		\u0003 obj = default(\u0003);
		obj.\u0006 = \u0008\u2002.\u0003\u2002.GetUpperBound(0) + 1;
		obj.\u000f = \u0003(Marshal.UnsafeAddrOfPinnedArrayElement(\u0008\u2002.\u0003\u2002, 0));
		int num = (int)\u0003(null, \u0002\u0005.\u0003(1031813711));
		\u0003(num, 74, 3, ref obj);
	}

	public void \u0006(int \u0003)
	{
		byte b = (byte)(\u0003 >> 8);
		\u0003 obj = default(\u0003);
		int num = (int)\u0006\u2002.\u0003(null, \u0002\u0005.\u0003(1031813711));
		byte[] array = new byte[5]
		{
			(byte)\u0003,
			0,
			0,
			0,
			0
		};
		obj.\u0003 = b;
		obj.\u0006 = array.GetUpperBound(0) + 1;
		obj.\u000f = \u0006\u2002.\u0003(Marshal.UnsafeAddrOfPinnedArrayElement(array, 0));
		\u0006\u2002.\u0003(num, 74, 2, ref obj);
	}

	public void \u0003(int \u0003, byte \u0006)
	{
		byte b = (byte)(\u0003 >> 8);
		\u0003 obj = default(\u0003);
		int num = (int)\u0006\u2002.\u0003(null, \u0002\u0005.\u0003(1031813711));
		byte[] array = new byte[5]
		{
			(byte)\u0003,
			\u0006,
			0,
			0,
			0
		};
		obj.\u0003 = b;
		obj.\u0006 = array.GetUpperBound(0) + 1;
		obj.\u000f = \u0006\u2002.\u0003(Marshal.UnsafeAddrOfPinnedArrayElement(array, 0));
		\u0006\u2002.\u0003(num, 74, 2, ref obj);
	}

	public void \u0003(int \u0003, byte \u0006, byte \u000f)
	{
		byte b = (byte)(\u0003 >> 8);
		\u0003 obj = default(\u0003);
		int num = (int)\u0006\u2002.\u0003(null, \u0002\u0005.\u0003(1031813711));
		byte[] array = new byte[5]
		{
			(byte)\u0003,
			\u0006,
			\u000f,
			0,
			0
		};
		obj.\u0003 = b;
		obj.\u0006 = array.GetUpperBound(0) + 1;
		obj.\u000f = \u0006\u2002.\u0003(Marshal.UnsafeAddrOfPinnedArrayElement(array, 0));
		\u0006\u2002.\u0003(num, 74, 2, ref obj);
	}

	public void \u0003(int \u0003, byte \u0006, byte \u000f, byte \u0002)
	{
		byte b = (byte)(\u0003 >> 8);
		\u0003 obj = default(\u0003);
		int num = (int)\u0006\u2002.\u0003(null, \u0002\u0005.\u0003(1031813711));
		byte[] array = new byte[5]
		{
			(byte)\u0003,
			\u0006,
			\u000f,
			\u0002,
			0
		};
		obj.\u0003 = b;
		obj.\u0006 = array.GetUpperBound(0) + 1;
		obj.\u000f = \u0006\u2002.\u0003(Marshal.UnsafeAddrOfPinnedArrayElement(array, 0));
		\u0006\u2002.\u0003(num, 74, 2, ref obj);
	}

	public void \u0003(int \u0003, byte \u0006, byte \u000f, byte \u0002, byte \u0008)
	{
		byte b = (byte)(\u0003 >> 8);
		\u0003 obj = default(\u0003);
		int num = (int)\u0006\u2002.\u0003(null, \u0002\u0005.\u0003(1031813711));
		byte[] array = new byte[5]
		{
			(byte)\u0003,
			\u0006,
			\u000f,
			\u0002,
			\u0008
		};
		obj.\u0003 = b;
		obj.\u0006 = array.GetUpperBound(0) + 1;
		obj.\u000f = \u0006\u2002.\u0003(Marshal.UnsafeAddrOfPinnedArrayElement(array, 0));
		\u0006\u2002.\u0003(num, 74, 2, ref obj);
	}
}
internal interface \u0008
{
	void \u0008\u2009\u2005\u2006\u0003();
}
internal static class \u0008\u2001
{
}
public sealed class \u0008\u2002
{
	public sealed class \u0003
	{
		public byte \u0003;

		public byte \u0006;

		public byte \u000f;

		public byte \u0002;

		public byte \u0008;

		public byte \u0005;

		public byte \u000e;

		public byte \u0003\u2002;

		public byte \u0006\u2002;

		public byte \u000f\u2002;

		public byte \u0002\u2002;

		public byte \u0008\u2002;

		public int \u0005\u2002;

		public int \u000e\u2002;

		public int \u0003\u2001;

		public int \u0006\u2001;

		public int \u000f\u2001;

		public int \u0002\u2001;

		public byte \u0008\u2001;

		public byte \u0005\u2001;
	}

	public sealed class \u0006
	{
		public Color \u0003 = default(Color);

		public Color \u0006 = default(Color);

		public Color \u000f = default(Color);

		public Color \u0002 = default(Color);

		public Color \u0008 = default(Color);

		public Color \u0005 = default(Color);

		public Color \u000e = default(Color);

		public int \u0003\u2002 = 0;

		public int \u0006\u2002 = 0;

		public int \u000f\u2002 = 0;

		public int \u0002\u2002 = 0;

		public int \u0008\u2002 = 0;

		public int \u0005\u2002 = 0;

		public int \u000e\u2002 = 0;

		public int \u0003\u2001 = 0;

		public int \u0006\u2001 = 0;

		public int \u000f\u2001 = 0;

		public int \u0002\u2001 = 0;
	}

	public static \u0006\u2001 \u0003;

	public static \u000f\u2001 \u0006;

	public static \u0006\u2002 \u000f;

	public static \u0005\u2002 \u0002;

	public static \u0002\u2002 \u0008;

	public static \u0006\u0005 \u0005;

	public static \u000f\u0005 \u000e;

	public static byte[] \u0003\u2002 = new byte[255];

	public static \u0006 \u0006\u2002 = new \u0006();

	public static \u0002\u2001 \u000f\u2002;

	public static \u0003\u2001 \u0002\u2002;

	public static int \u0008\u2002;
}
internal static class \u000e
{
	internal sealed class \u0003 : global::\u000f<int>, global::\u0006, global::\u0005<int>, \u0008, \u0002
	{
		private int m_\u0003;

		private int m_\u0006;

		private int \u000f;

		private int \u0002;

		public int \u0008;

		private int \u0005;

		private int \u000e;

		private global::\u0005<int> \u0003\u2002;

		private int \u0006\u2002;

		[DebuggerHidden]
		public \u0003(int \u0003)
		{
			this.m_\u0003 = \u0003;
			\u000f = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private void \u0003\u2009\u2005\u2006\u0003()
		{
			int num = m_\u0003;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					\u0006();
				}
			}
			\u0003\u2002 = null;
			m_\u0003 = -2;
		}

		void \u0008.\u0008\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			this.\u0003\u2009\u2005\u2006\u0003();
		}

		private bool \u0002\u2009\u2005\u2006\u0003()
		{
			bool result;
			try
			{
				switch (m_\u0003)
				{
				default:
					result = false;
					goto end_IL_0000;
				case 0:
					m_\u0003 = -1;
					\u0005 = 0;
					\u000e = 1;
					\u0003\u2002 = ((global::\u000f<int>)new \u0006(-2)).GetEnumerator();
					m_\u0003 = -3;
					break;
				case 1:
					m_\u0003 = -3;
					\u0002--;
					if (\u0002 != 0)
					{
						int num = \u000e;
						\u000e = (num + \u0005 + \u0002) ^ (-1358275320 + \u0006\u2002);
						\u0005 = num;
						break;
					}
					result = false;
					\u0006();
					goto end_IL_0000;
				}
				if (((\u0002)\u0003\u2002).\u0002\u2009\u2005\u2006\u0003())
				{
					\u0006\u2002 = \u0003\u2002.\u0002\u2009\u2005\u2006\u0003();
					this.m_\u0006 = \u000e;
					m_\u0003 = 1;
					result = true;
				}
				else
				{
					\u0006();
					\u0003\u2002 = null;
					result = false;
				}
				end_IL_0000:;
			}
			catch
			{
				//try-fault
				\u0003\u2009\u2005\u2006\u0003();
				throw;
			}
			return result;
		}

		bool \u0002.\u0002\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u0002\u2009\u2005\u2006\u0003();
		}

		private void \u0006()
		{
			m_\u0003 = -1;
			if (\u0003\u2002 != null)
			{
				\u0003\u2002.\u0008\u2009\u2005\u2006\u0003();
			}
		}

		[DebuggerHidden]
		private int \u0003\u2009\u2005\u2006\u0003()
		{
			return this.m_\u0006;
		}

		int global::\u0005<int>.\u0002\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u0003\u2009\u2005\u2006\u0003();
		}

		[DebuggerHidden]
		private void \u0003\u2009\u2005\u2006\u000f()
		{
			throw new NotSupportedException();
		}

		void \u0002.\u0002\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			this.\u0003\u2009\u2005\u2006\u000f();
		}

		[DebuggerHidden]
		private object \u0003\u2009\u2005\u2006\u0003()
		{
			return this.m_\u0006;
		}

		object \u0002.\u0002\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u0003\u2009\u2005\u2006\u0003();
		}

		[DebuggerHidden]
		private global::\u0005<int> \u0003\u2009\u2005\u2006\u0003()
		{
			\u0003 obj;
			if (m_\u0003 == -2 && \u000f == Thread.CurrentThread.ManagedThreadId)
			{
				m_\u0003 = 0;
				obj = this;
			}
			else
			{
				obj = new \u0003(0);
			}
			obj.\u0002 = \u0008;
			return obj;
		}

		global::\u0005<int> global::\u000f<int>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u0003\u2009\u2005\u2006\u0003();
		}

		[DebuggerHidden]
		private \u0002 \u0003\u2009\u2005\u2006\u0003()
		{
			return \u0003\u2009\u2005\u2006\u0003();
		}

		\u0002 global::\u0006.\u0006\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u0003\u2009\u2005\u2006\u0003();
		}
	}

	internal sealed class \u0006 : global::\u000f<int>, global::\u0006, global::\u0005<int>, \u0008, \u0002
	{
		private int \u0003;

		private int m_\u0006;

		private int \u000f;

		private int \u0002;

		[DebuggerHidden]
		public \u0006(int \u0003)
		{
			this.\u0003 = \u0003;
			\u000f = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private void \u0006\u2009\u2005\u2006\u0003()
		{
			\u0003 = -2;
		}

		void \u0008.\u0008\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			this.\u0006\u2009\u2005\u2006\u0003();
		}

		private bool \u0002\u2009\u2005\u2006\u0003()
		{
			int num = \u0003;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				\u0003 = -1;
				\u0002 += \u0002;
				if (\u0002 == 64)
				{
					\u0002 = 5;
				}
			}
			else
			{
				\u0003 = -1;
				\u0002 = 1;
			}
			m_\u0006 = \u0002;
			\u0003 = 1;
			return true;
		}

		bool \u0002.\u0002\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u0002\u2009\u2005\u2006\u0003();
		}

		[DebuggerHidden]
		private int \u0006\u2009\u2005\u2006\u0003()
		{
			return m_\u0006;
		}

		int global::\u0005<int>.\u0002\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u0006\u2009\u2005\u2006\u0003();
		}

		[DebuggerHidden]
		private void \u0006\u2009\u2005\u2006\u0006()
		{
			throw new NotSupportedException();
		}

		void \u0002.\u0002\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			this.\u0006\u2009\u2005\u2006\u0006();
		}

		[DebuggerHidden]
		private object \u0006\u2009\u2005\u2006\u0003()
		{
			return m_\u0006;
		}

		object \u0002.\u0002\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u0006\u2009\u2005\u2006\u0003();
		}

		[DebuggerHidden]
		private global::\u0005<int> \u0006\u2009\u2005\u2006\u0003()
		{
			if (\u0003 == -2 && \u000f == Thread.CurrentThread.ManagedThreadId)
			{
				\u0003 = 0;
				return this;
			}
			return new \u0006(0);
		}

		global::\u0005<int> global::\u000f<int>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u0006\u2009\u2005\u2006\u0003();
		}

		[DebuggerHidden]
		private \u0002 \u0006\u2009\u2005\u2006\u0003()
		{
			return \u0006\u2009\u2005\u2006\u0003();
		}

		\u0002 global::\u0006.\u0006\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u0006\u2009\u2005\u2006\u0003();
		}
	}

	internal sealed class \u000f : global::\u000f<int>, global::\u0006, global::\u0005<int>, \u0008, \u0002
	{
		private int \u0003;

		private int m_\u0006;

		private int m_\u000f;

		private int \u0002;

		public int \u0008;

		private int \u0005;

		private global::\u0005<int> \u000e;

		[DebuggerHidden]
		public \u000f(int \u0003)
		{
			this.\u0003 = \u0003;
			m_\u000f = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private void \u000f\u2009\u2005\u2006\u0003()
		{
			int num = \u0003;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					\u0006();
				}
			}
			\u000e = null;
			\u0003 = -2;
		}

		void \u0008.\u0008\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			this.\u000f\u2009\u2005\u2006\u0003();
		}

		private bool \u0002\u2009\u2005\u2006\u0003()
		{
			bool result;
			try
			{
				switch (\u0003)
				{
				default:
					result = false;
					goto end_IL_0000;
				case 0:
				{
					\u0003 = -1;
					\u0005 = 7;
					int num = \u0002;
					\u000e = ((global::\u000f<int>)new \u0003(-2)
					{
						\u0008 = num
					}).GetEnumerator();
					\u0003 = -3;
					break;
				}
				case 1:
					\u0003 = -3;
					if (\u0005 != 0)
					{
						break;
					}
					result = false;
					\u0006();
					goto end_IL_0000;
				}
				if (((\u0002)\u000e).\u0002\u2009\u2005\u2006\u0003())
				{
					int num2 = \u000e.\u0002\u2009\u2005\u2006\u0003() ^ \u0002;
					if ((num2 & 3) == 0)
					{
						num2 ^= 0x778BF18C;
					}
					int num3 = \u0005 - 1;
					\u0005 = num3;
					if ((num2 & 0xF) == 0)
					{
						num2 ^= -1189707964;
					}
					this.m_\u0006 = num2;
					\u0003 = 1;
					result = true;
				}
				else
				{
					\u0006();
					\u000e = null;
					result = false;
				}
				end_IL_0000:;
			}
			catch
			{
				//try-fault
				\u000f\u2009\u2005\u2006\u0003();
				throw;
			}
			return result;
		}

		bool \u0002.\u0002\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u0002\u2009\u2005\u2006\u0003();
		}

		private void \u0006()
		{
			\u0003 = -1;
			if (\u000e != null)
			{
				\u000e.\u0008\u2009\u2005\u2006\u0003();
			}
		}

		[DebuggerHidden]
		private int \u000f\u2009\u2005\u2006\u0003()
		{
			return this.m_\u0006;
		}

		int global::\u0005<int>.\u0002\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u000f\u2009\u2005\u2006\u0003();
		}

		[DebuggerHidden]
		private void \u000f\u2009\u2005\u2006\u000f()
		{
			throw new NotSupportedException();
		}

		void \u0002.\u0002\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			this.\u000f\u2009\u2005\u2006\u000f();
		}

		[DebuggerHidden]
		private object \u000f\u2009\u2005\u2006\u0003()
		{
			return this.m_\u0006;
		}

		object \u0002.\u0002\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u000f\u2009\u2005\u2006\u0003();
		}

		[DebuggerHidden]
		private global::\u0005<int> \u000f\u2009\u2005\u2006\u0003()
		{
			\u000f obj;
			if (\u0003 == -2 && m_\u000f == Thread.CurrentThread.ManagedThreadId)
			{
				\u0003 = 0;
				obj = this;
			}
			else
			{
				obj = new \u000f(0);
			}
			obj.\u0002 = \u0008;
			return obj;
		}

		global::\u0005<int> global::\u000f<int>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u000f\u2009\u2005\u2006\u0003();
		}

		[DebuggerHidden]
		private \u0002 \u000f\u2009\u2005\u2006\u0003()
		{
			return \u000f\u2009\u2005\u2006\u0003();
		}

		\u0002 global::\u0006.\u0006\u2009\u2005\u2006\u0003()
		{
			//ILSpy generated this explicit interface implementation from .override directive in    
			return this.\u000f\u2009\u2005\u2006\u0003();
		}
	}
}
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
[DebuggerNonUserCode]
internal sealed class \u000e\u2002
{
	private static ResourceManager m_\u0003;

	private static CultureInfo \u0006;

	internal \u000e\u2002()
	{
	}

	internal static ResourceManager \u0003()
	{
		if (\u000e\u2002.m_\u0003 == null)
		{
			ResourceManager resourceManager = new ResourceManager(\u0002\u0005.\u0003(1031809553), typeof(\u000e\u2002).Assembly);
			\u000e\u2002.m_\u0003 = resourceManager;
		}
		return \u000e\u2002.m_\u0003;
	}

	internal static CultureInfo \u0003()
	{
		return \u0006;
	}

	internal static void \u0003(CultureInfo \u0003)
	{
		\u0006 = \u0003;
	}
}
internal interface \u000f<\u0003> : \u0006
{
	global::\u0005<\u0003> GetEnumerator();
}
public sealed class \u000f\u0005
{
	public sealed class \u0003
	{
		public sealed class \u0003
		{
			public byte \u0003;

			public byte \u0006;

			public int \u000f;

			public int \u0002;

			public byte \u0008;

			public byte \u0005;

			public byte \u000e;

			public byte \u0003\u2002;

			public byte \u0006\u2002;

			public byte \u000f\u2002;
		}

		public byte \u0003;

		public byte \u0006;

		public byte \u000f;

		public byte \u0002;

		public byte \u0008;

		public byte \u0005;

		public byte \u000e;

		public byte \u0003\u2002;

		public byte \u0006\u2002;

		public int \u000f\u2002;

		public int \u0002\u2002;

		public int \u0008\u2002;

		public List<\u0003> \u0005\u2002 = new List<\u0003>();

		public \u0003()
		{
			for (int i = 0; i < 10; i++)
			{
				\u0003 item = new \u0003();
				\u0005\u2002.Add(item);
			}
		}
	}

	public enum \u0006
	{

	}

	public sealed class \u000f
	{
		public sealed class \u0003
		{
			public byte[] \u0003 = new byte[10] { 0, 30, 40, 45, 50, 60, 70, 80, 90, 100 };

			public byte[] \u0006 = new byte[10] { 35, 40, 45, 50, 60, 65, 70, 80, 90, 100 };
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int m_\u0003;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string m_\u0006;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int m_\u000f;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int \u0002;

		public \u0003 \u0008 = new \u0003();

		public \u0003 \u0005 = new \u0003();

		public \u0003 \u000e = new \u0003();

		public \u0003 \u0003\u2002 = new \u0003();

		public int \u0003()
		{
			return this.m_\u0003;
		}

		public void \u0003(int \u0003)
		{
			this.m_\u0003 = \u0003;
		}

		public string \u0003()
		{
			return this.m_\u0006;
		}

		public void \u0003(string \u0003)
		{
			this.m_\u0006 = \u0003;
		}

		public int \u0006()
		{
			return this.m_\u000f;
		}

		public void \u0006(int \u0003)
		{
			this.m_\u000f = \u0003;
		}

		public int \u000f()
		{
			return \u0002;
		}

		public void \u000f(int \u0003)
		{
			\u0002 = \u0003;
		}
	}

	public int \u0003 = 0;

	private byte[] m_\u0006 = new byte[256];

	private byte[] m_\u000f = new byte[256];

	private byte[] m_\u0002 = new byte[256];

	private byte[] m_\u0008 = new byte[256];

	public \u0003 \u0005 = new \u0003();

	public \u0003 \u000e = new \u0003();

	public \u0003 \u0003\u2002 = new \u0003();

	public \u0003 \u0006\u2002 = new \u0003();

	private string m_\u000f\u2002 = \u0002\u0005.\u0003(1031814644);

	private List<\u000f> \u0002\u2002;

	public void \u0003()
	{
		this.\u0003 = \u0008\u2002.\u0008.\u0008;
		if (\u0008\u2002.\u0008.\u000f\u2002 || \u0008\u2002.\u0008.\u0002\u2002)
		{
			\u0006();
		}
		if (\u0008\u2002.\u0008.\u0008\u2002 || \u0008\u2002.\u0008.\u0005\u2002)
		{
			\u000f();
		}
		\u0006\u2002();
	}

	private void \u0006()
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		array[0] = 40;
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031814618) + DateTime.Now.Second + \u0002\u0005.\u0003(1031814578) + DateTime.Now.Millisecond);
		\u0008\u2002.\u0002.\u0003(4, array, ref array2);
		string text = string.Empty;
		int num = 0;
		for (int i = 0; i < 256; i++)
		{
			text = text + \u0002\u0005.\u0003(1031814570) + Convert.ToString(array2[i], 16).PadLeft(2, '0');
			num++;
			if (num == 16)
			{
				text += \u0002\u0005.\u0003(1031814562);
				num = 0;
			}
		}
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031814554) + DateTime.Now.Second + \u0002\u0005.\u0003(1031814578) + DateTime.Now.Millisecond + \u0002\u0005.\u0003(1031814562) + text);
		this.\u0005 = \u0003(array2, (\u0006)0);
		this.\u000e = \u0003(array2, (\u0006)1);
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031814512) + this.\u0005.\u0008\u2002 + \u0002\u0005.\u0003(1031814482) + this.\u0005.\u0002\u2002 + \u0002\u0005.\u0003(1031814462) + this.\u000e.\u0008\u2002 + \u0002\u0005.\u0003(1031814416) + this.\u000e.\u0002\u2002);
	}

	private void \u000f()
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		array[0] = 42;
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031811836) + DateTime.Now.Second + \u0002\u0005.\u0003(1031814578) + DateTime.Now.Millisecond);
		\u0008\u2002.\u0002.\u0003(4, array, ref array2);
		string text = string.Empty;
		int num = 0;
		for (int i = 0; i < 256; i++)
		{
			text = text + \u0002\u0005.\u0003(1031814570) + Convert.ToString(array2[i], 16).PadLeft(2, '0');
			num++;
			if (num == 16)
			{
				text += \u0002\u0005.\u0003(1031814562);
				num = 0;
			}
		}
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031811807) + DateTime.Now.Second + \u0002\u0005.\u0003(1031814578) + DateTime.Now.Millisecond);
		this.\u0003\u2002 = \u0003(array2, (\u0006)2);
		this.\u0006\u2002 = \u0003(array2, (\u0006)3);
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031811760) + this.\u0003\u2002.\u0008\u2002 + \u0002\u0005.\u0003(1031811731) + this.\u0003\u2002.\u0002\u2002 + \u0002\u0005.\u0003(1031811696) + this.\u0006\u2002.\u0008\u2002 + \u0002\u0005.\u0003(1031811666) + this.\u0006\u2002.\u0002\u2002);
	}

	private \u0003 \u0003(byte[] \u0003, \u0006 \u0006)
	{
		\u0003 obj = new \u0003();
		int num = 0;
		for (int i = 0; i < 112; i++)
		{
			if (\u0006 == (\u0006)0)
			{
				this.m_\u0006[i] = \u0003[i];
			}
			if (\u0006 == (\u0006)1)
			{
				num = 128;
				this.m_\u000f[i] = \u0003[num + i];
			}
			if (\u0006 == (\u0006)2)
			{
				this.m_\u0002[i] = \u0003[i];
			}
			if (\u0006 == (\u0006)3)
			{
				num = 128;
				this.m_\u0008[i] = \u0003[num + i];
			}
		}
		obj.\u0006 = (byte)\u0006;
		obj.\u0003 = \u0003[num];
		obj.\u000f = \u0003[num + 1];
		obj.\u0002 = \u0003[num + 2];
		obj.\u0008 = \u0003[num + 3];
		obj.\u0005 = \u0003[num + 4];
		obj.\u000e = \u0003[num + 5];
		obj.\u0003\u2002 = \u0003[num + 6];
		obj.\u0006\u2002 = \u0003[num + 7];
		obj.\u000f\u2002 = (\u0003[num + 8] << 8) + \u0003[num + 9];
		obj.\u0002\u2002 = (\u0003[num + 10] << 8) + \u0003[num + 11];
		obj.\u0008\u2002 = (\u0003[num + 12] << 8) + \u0003[num + 13];
		for (int j = 0; j < 10; j++)
		{
			obj.\u0005\u2002[j].\u0003 = \u0003[num + 22 + j];
			obj.\u0005\u2002[j].\u0006 = \u0003[num + 32 + j];
			obj.\u0005\u2002[j].\u000f = (\u0003[num + 42 + j * 2] << 8) + \u0003[num + 43 + j * 2];
			obj.\u0005\u2002[j].\u0002 = (\u0003[num + 62 + j * 2] << 8) + \u0003[num + 63 + j * 2];
			obj.\u0005\u2002[j].\u0008 = \u0003[num + 92 + j];
			obj.\u0005\u2002[j].\u0005 = \u0003[num + 82 + j];
			obj.\u0005\u2002[j].\u000e = (byte)(\u0003[num + 102 + j] >> 4);
			obj.\u0005\u2002[j].\u0003\u2002 = (byte)(\u0003[num + 102 + j] & 0xFu);
		}
		return obj;
	}

	public void \u0002()
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		this.m_\u0006 = \u0003((\u0006)0);
		this.m_\u000f = \u0003((\u0006)1);
		array[0] = 41;
		for (byte b = 0; b < 112; b++)
		{
			array[16 + b] = this.m_\u0006[b];
			array[128 + b] = this.m_\u000f[b];
		}
		string text = string.Empty;
		int num = 0;
		for (int i = 0; i < 256; i++)
		{
			text = text + \u0002\u0005.\u0003(1031814570) + Convert.ToString(array[i], 16).PadLeft(2, '0');
			num++;
			if (num == 16)
			{
				text += \u0002\u0005.\u0003(1031814562);
				num = 0;
			}
		}
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031811646) + Convert.ToString(this.m_\u0006[0], 16) + \u0002\u0005.\u0003(1031811597) + Convert.ToString(this.m_\u000f[0], 16) + DateTime.Now.Second + \u0002\u0005.\u0003(1031814578) + DateTime.Now.Millisecond + \u0002\u0005.\u0003(1031814562) + text);
		\u0008\u2002.\u0002.\u0003(4, array, ref array2);
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812081) + DateTime.Now.Second + \u0002\u0005.\u0003(1031814578) + DateTime.Now.Millisecond);
	}

	public void \u0008()
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		this.m_\u0002 = \u0003((\u0006)2);
		this.m_\u0008 = \u0003((\u0006)3);
		array[0] = 43;
		for (byte b = 0; b < 112; b++)
		{
			array[16 + b] = this.m_\u0002[b];
			array[128 + b] = this.m_\u0008[b];
		}
		string text = string.Empty;
		int num = 0;
		for (int i = 0; i < 256; i++)
		{
			text = text + \u0002\u0005.\u0003(1031814570) + Convert.ToString(array[i], 16).PadLeft(2, '0');
			num++;
			if (num == 16)
			{
				text += \u0002\u0005.\u0003(1031814562);
				num = 0;
			}
		}
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812041) + Convert.ToString(this.m_\u0006[0], 16) + \u0002\u0005.\u0003(1031811597) + Convert.ToString(this.m_\u000f[0], 16) + DateTime.Now.Second + \u0002\u0005.\u0003(1031814578) + DateTime.Now.Millisecond + \u0002\u0005.\u0003(1031814562) + text);
		\u0008\u2002.\u0002.\u0003(4, array, ref array2);
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031811992) + DateTime.Now.Second + \u0002\u0005.\u0003(1031814578) + DateTime.Now.Millisecond);
	}

	private byte[] \u0003(\u0006 \u0003)
	{
		byte[] array = new byte[256];
		\u0003 obj = new \u0003();
		switch (\u0003)
		{
		case (\u0006)0:
			array = this.m_\u0006;
			obj = this.\u0005;
			break;
		case (\u0006)1:
			array = this.m_\u000f;
			obj = this.\u000e;
			break;
		case (\u0006)2:
			array = this.m_\u0002;
			obj = this.\u0003\u2002;
			break;
		case (\u0006)3:
			array = this.m_\u0008;
			obj = this.\u0006\u2002;
			break;
		}
		array[0] = obj.\u0003;
		array[1] = obj.\u000f;
		array[2] = obj.\u0002;
		array[3] = obj.\u0008;
		array[4] = obj.\u0005;
		array[5] = obj.\u000e;
		array[6] = obj.\u0003\u2002;
		array[7] = obj.\u0006\u2002;
		array[8] = (byte)(obj.\u000f\u2002 >> 8);
		array[9] = (byte)obj.\u000f\u2002;
		array[10] = (byte)(obj.\u0002\u2002 >> 8);
		array[11] = (byte)obj.\u0002\u2002;
		for (int i = 0; i < 10; i++)
		{
			array[22 + i] = obj.\u0005\u2002[i].\u0003;
			array[32 + i] = obj.\u0005\u2002[i].\u0006;
			array[42 + i * 2] = (byte)(obj.\u0005\u2002[i].\u000f >> 8);
			array[43 + i * 2] = (byte)obj.\u0005\u2002[i].\u000f;
			array[62 + i * 2] = (byte)(obj.\u0005\u2002[i].\u0002 >> 8);
			array[63 + i * 2] = (byte)obj.\u0005\u2002[i].\u0002;
			array[92 + i] = obj.\u0005\u2002[i].\u0008;
			array[82 + i] = obj.\u0005\u2002[i].\u0005;
			array[102 + i] = (byte)((obj.\u0005\u2002[i].\u000e << 4) + (obj.\u0005\u2002[i].\u0003\u2002 & 0xF));
		}
		return array;
	}

	public void \u0005()
	{
		if (\u0008\u2002.\u0008.\u000f\u2002)
		{
			this.\u0005 = \u0006(this.\u0005);
		}
		if (\u0008\u2002.\u0008.\u0002\u2002)
		{
			this.\u000e = \u0006(this.\u000e);
		}
		if (\u0008\u2002.\u0008.\u0008\u2002)
		{
			this.\u0003\u2002 = \u0006(this.\u0003\u2002);
		}
		if (\u0008\u2002.\u0008.\u0005\u2002)
		{
			this.\u0006\u2002 = \u0006(this.\u0006\u2002);
		}
	}

	public void \u000e()
	{
		\u0005();
		Thread.Sleep(50);
		if (\u0008\u2002.\u0008.\u000f\u2002 || \u0008\u2002.\u0008.\u0002\u2002)
		{
			\u0002();
		}
		if (\u0008\u2002.\u0008.\u0008\u2002 || \u0008\u2002.\u0008.\u0005\u2002)
		{
			Thread.Sleep(50);
			\u0008();
		}
	}

	public void \u0003\u2002()
	{
		\u0003(\u0002\u2002, this.m_\u000f\u2002);
		\u000e();
	}

	public \u0003 \u0003(\u0003 \u0003)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		byte[] array = new byte[10];
		byte[] array2 = new byte[10];
		for (int i = 0; i < 10; i++)
		{
			if (i == 0)
			{
				\u0003.\u0005\u2002[i].\u0003 = 0;
			}
			else
			{
				\u0003.\u0005\u2002[i].\u0003 = (byte)(\u0003.\u0005\u2002[i - 1].\u000f\u2002 + 1);
			}
			\u0003.\u0005\u2002[i].\u0006 = \u0003.\u0005\u2002[i].\u000f\u2002;
			array[i] = \u0003.\u0005\u2002[i].\u000f\u2002;
			array2[i] = \u0003.\u0005\u2002[i].\u0006\u2002;
			\u0003.\u0005\u2002[i].\u0002 = (int)Math.Round((double)(this.\u0005.\u0002\u2002 * array2[i]) / 100.0);
			\u0003.\u0005\u2002[i].\u000f = \u0003.\u0005\u2002[i].\u0002;
			\u0003.\u0005\u2002[i].\u0008 = 10;
			\u0003.\u0005\u2002[i].\u0005 = 26;
			\u0003.\u0005\u2002[i].\u000e = 6;
			\u0003.\u0005\u2002[i].\u0003\u2002 = 1;
			text = text + \u0002\u0005.\u0003(1031811952) + i + \u0002\u0005.\u0003(1031811944) + \u0003.\u0005\u2002[i].\u000f\u2002 + \u0002\u0005.\u0003(1031811937);
			text2 = text2 + \u0002\u0005.\u0003(1031811952) + i + \u0002\u0005.\u0003(1031811944) + \u0003.\u0005\u2002[i].\u0006\u2002 + \u0002\u0005.\u0003(1031811937);
		}
		int num = this.\u0003();
		foreach (\u000f item in \u0002\u2002)
		{
			if (item.\u000f() == num)
			{
				switch (\u0003.\u0006)
				{
				case 0:
					global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031811929) + item.\u000f());
					item.\u000f(item.\u000f());
					item.\u0008.\u0006 = array;
					item.\u0008.\u0003 = array2;
					break;
				case 1:
					global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031811912) + item.\u000f());
					item.\u000f(item.\u000f());
					item.\u0005.\u0006 = array;
					item.\u0005.\u0003 = array2;
					break;
				case 2:
					global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031811911) + item.\u000f());
					item.\u000f(item.\u000f());
					item.\u000e.\u0006 = array;
					item.\u000e.\u0003 = array2;
					break;
				case 3:
					global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031811894) + item.\u000f());
					item.\u000f(item.\u000f());
					item.\u0003\u2002.\u0006 = array;
					item.\u0003\u2002.\u0003 = array2;
					break;
				}
			}
		}
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031811877) + \u0003.\u0006 + \u0002\u0005.\u0003(1031811326) + Convert.ToString(\u0003.\u0003, 16) + \u0002\u0005.\u0003(1031811309) + text + \u0002\u0005.\u0003(1031811292) + text2);
		return \u0003;
	}

	public \u0003 \u0006(\u0003 \u0003)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		byte[] array = new byte[10];
		byte[] array2 = new byte[10];
		int num = this.\u0003();
		foreach (\u000f item in \u0002\u2002)
		{
			if (item.\u000f() == num)
			{
				switch (\u0003.\u0006)
				{
				case 0:
					\u0003.\u0003 = (byte)(128 + num);
					array = item.\u0008.\u0006;
					array2 = item.\u0008.\u0003;
					break;
				case 1:
					\u0003.\u0003 = (byte)(144 + num);
					array = item.\u0005.\u0006;
					array2 = item.\u0005.\u0003;
					break;
				case 2:
					\u0003.\u0003 = (byte)(160 + num);
					array = item.\u000e.\u0006;
					array2 = item.\u000e.\u0003;
					break;
				case 3:
					\u0003.\u0003 = (byte)(176 + num);
					array = item.\u0003\u2002.\u0006;
					array2 = item.\u0003\u2002.\u0003;
					break;
				}
			}
		}
		for (int i = 0; i < 10; i++)
		{
			if (i == 0)
			{
				\u0003.\u0005\u2002[i].\u0003 = 0;
			}
			else
			{
				\u0003.\u0005\u2002[i].\u0003 = (byte)(array[i - 1] + 1);
			}
			\u0003.\u0005\u2002[i].\u0006 = array[i];
			\u0003.\u0005\u2002[i].\u000f\u2002 = \u0003.\u0005\u2002[i].\u0006;
			\u0003.\u0005\u2002[i].\u0006\u2002 = array2[i];
			\u0003.\u0005\u2002[i].\u0002 = (int)Math.Round((double)(this.\u0005.\u0002\u2002 * array2[i]) / 100.0);
			\u0003.\u0005\u2002[i].\u000f = \u0003.\u0005\u2002[i].\u0002;
			\u0003.\u0005\u2002[i].\u0008 = 10;
			\u0003.\u0005\u2002[i].\u0005 = 26;
			\u0003.\u0005\u2002[i].\u000e = 6;
			\u0003.\u0005\u2002[i].\u0003\u2002 = 1;
			text = text + \u0002\u0005.\u0003(1031811952) + i + \u0002\u0005.\u0003(1031811944) + \u0003.\u0005\u2002[i].\u000f\u2002 + \u0002\u0005.\u0003(1031811937);
			text2 = text2 + \u0002\u0005.\u0003(1031811952) + i + \u0002\u0005.\u0003(1031811944) + \u0003.\u0005\u2002[i].\u0006\u2002 + \u0002\u0005.\u0003(1031811937);
		}
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031811277) + \u0003.\u0006 + \u0002\u0005.\u0003(1031811326) + Convert.ToString(\u0003.\u0003, 16) + \u0002\u0005.\u0003(1031811309) + text + \u0002\u0005.\u0003(1031811292) + text2);
		return \u0003;
	}

	public \u0003 \u000f(\u0003 \u0003)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		byte[] array = new byte[10] { 35, 40, 45, 50, 60, 65, 70, 80, 90, 100 };
		byte[] array2 = new byte[10] { 0, 30, 40, 45, 50, 60, 70, 80, 90, 100 };
		int num = this.\u0003();
		foreach (\u000f item in \u0002\u2002)
		{
			if (item.\u000f() == num)
			{
				switch (\u0003.\u0006)
				{
				case 0:
					\u0003.\u0003 = (byte)(128 + num);
					item.\u0008.\u0006 = array;
					item.\u0008.\u0003 = array2;
					break;
				case 1:
					\u0003.\u0003 = (byte)(144 + num);
					item.\u0005.\u0006 = array;
					item.\u0005.\u0003 = array2;
					break;
				case 2:
					\u0003.\u0003 = (byte)(160 + num);
					item.\u000e.\u0006 = array;
					item.\u000e.\u0003 = array2;
					break;
				case 3:
					\u0003.\u0003 = (byte)(176 + num);
					item.\u0003\u2002.\u0006 = array;
					item.\u0003\u2002.\u0003 = array2;
					break;
				}
			}
		}
		for (int i = 0; i < 10; i++)
		{
			if (i == 0)
			{
				\u0003.\u0005\u2002[i].\u0003 = 0;
			}
			else
			{
				\u0003.\u0005\u2002[i].\u0003 = (byte)(array[i - 1] + 1);
			}
			\u0003.\u0005\u2002[i].\u0006 = array[i];
			\u0003.\u0005\u2002[i].\u000f\u2002 = \u0003.\u0005\u2002[i].\u0006;
			\u0003.\u0005\u2002[i].\u0006\u2002 = array2[i];
			\u0003.\u0005\u2002[i].\u0002 = (int)Math.Round((double)(this.\u0005.\u0002\u2002 * array2[i]) / 100.0);
			\u0003.\u0005\u2002[i].\u000f = \u0003.\u0005\u2002[i].\u0002;
			\u0003.\u0005\u2002[i].\u0008 = 10;
			\u0003.\u0005\u2002[i].\u0005 = 26;
			\u0003.\u0005\u2002[i].\u000e = 6;
			\u0003.\u0005\u2002[i].\u0003\u2002 = 1;
			text = text + \u0002\u0005.\u0003(1031811952) + i + \u0002\u0005.\u0003(1031811944) + \u0003.\u0005\u2002[i].\u000f\u2002 + \u0002\u0005.\u0003(1031811937);
			text2 = text2 + \u0002\u0005.\u0003(1031811952) + i + \u0002\u0005.\u0003(1031811944) + \u0003.\u0005\u2002[i].\u0006\u2002 + \u0002\u0005.\u0003(1031811937);
		}
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031811236) + \u0003.\u0006 + \u0002\u0005.\u0003(1031811326) + Convert.ToString(\u0003.\u0003, 16) + \u0002\u0005.\u0003(1031811309) + text + \u0002\u0005.\u0003(1031811292) + text2);
		return \u0003;
	}

	private int \u0003()
	{
		byte[] array = \u0008\u2002.\u0002.\u0003(257, 1);
		_ = array[0];
		string text = string.Empty;
		int num = 0;
		switch (array[0])
		{
		case 0:
		case 4:
			array[0] = 0;
			text = \u0002\u0005.\u0003(1031811192);
			break;
		case 1:
			text = \u0002\u0005.\u0003(1031811188);
			num = 1;
			break;
		case 2:
			text = \u0002\u0005.\u0003(1031811174);
			num = 2;
			break;
		case 3:
			text = \u0002\u0005.\u0003(1031811144);
			num = 3;
			break;
		}
		bool flag = false;
		foreach (\u000f item in \u0002\u2002)
		{
			if (item.\u000f() == num)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			this.m_\u000f\u2002 = \u0002\u0005.\u0003(1031811132);
			if (File.Exists(this.m_\u000f\u2002))
			{
				File.Delete(this.m_\u000f\u2002);
			}
			if (!\u0003(this.m_\u000f\u2002))
			{
				\u000f\u2002();
			}
		}
		global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031811102) + text);
		return num;
	}

	private void \u0006\u2002()
	{
		string text = \u0002\u0005.\u0003(1031811577);
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		this.m_\u000f\u2002 = text + \u0002\u0005.\u0003(1031811563);
		\u0002\u2002 = new List<\u000f>();
		if (!\u0003(this.m_\u000f\u2002))
		{
			\u000f\u2002();
		}
	}

	private bool \u0003(string \u0003)
	{
		if (!File.Exists(\u0003))
		{
			return false;
		}
		\u0002\u2002.Clear();
		if (!this.\u0003(\u0003, out \u0002\u2002))
		{
			return false;
		}
		if (\u0002\u2002.Count == 0)
		{
			return false;
		}
		return true;
	}

	private void \u000f\u2002()
	{
		\u0002\u2002.Clear();
		if (\u0008\u2002.\u0006.\u000e\u2009.\u000f\u2002 || \u0008\u2002.\u0006.\u000e\u2009.\u0003\u2001)
		{
			\u000f obj = new \u000f();
			obj.\u0003(0);
			obj.\u0003(\u0002\u0005.\u0003(1031811192));
			obj.\u0006(0);
			obj.\u000f(obj.\u0003());
			\u0002\u2002.Add(obj);
		}
		if (\u0008\u2002.\u0006.\u000e\u2009.\u0002\u2002)
		{
			\u000f obj2 = new \u000f();
			obj2.\u0003(1);
			obj2.\u0003(\u0002\u0005.\u0003(1031811188));
			obj2.\u0006(1);
			obj2.\u000f(obj2.\u0003());
			\u0002\u2002.Add(obj2);
		}
		if (\u0008\u2002.\u0006.\u000e\u2009.\u0008\u2002)
		{
			\u000f obj3 = new \u000f();
			obj3.\u0003(2);
			obj3.\u0003(\u0002\u0005.\u0003(1031811174));
			obj3.\u0006(2);
			obj3.\u000f(obj3.\u0003());
			\u0002\u2002.Add(obj3);
		}
		if (\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2002)
		{
			\u000f obj4 = new \u000f();
			obj4.\u0003(3);
			obj4.\u0003(\u0002\u0005.\u0003(1031811144));
			obj4.\u0006(3);
			obj4.\u000f(obj4.\u0003());
			\u0002\u2002.Add(obj4);
		}
		\u0003(\u0002\u2002, this.m_\u000f\u2002);
	}

	private void \u0003(string \u0003, int \u0006)
	{
		\u000f obj = new \u000f();
		obj.\u0003(\u0006);
		obj.\u0003(\u0003);
		obj.\u0006(\u0006);
		obj.\u000f(obj.\u0003());
		\u0002\u2002.Add(obj);
		this.\u0003(\u0002\u2002, this.m_\u000f\u2002);
	}

	public void \u0003(List<\u000f> \u0003, string \u0006)
	{
		XmlDocument xmlDocument = new XmlDocument();
		XmlDeclaration newChild = xmlDocument.CreateXmlDeclaration(\u0002\u0005.\u0003(1031811538), \u0002\u0005.\u0003(1031811532), \u0002\u0005.\u0003(1031811512));
		xmlDocument.AppendChild(newChild);
		XmlElement xmlElement = xmlDocument.CreateElement(\u0002\u0005.\u0003(1031811506));
		xmlDocument.AppendChild(xmlElement);
		System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
		foreach (\u000f item in \u0003)
		{
			Console.WriteLine(\u0002\u0005.\u0003(1031811501));
			XmlElement xmlElement2 = xmlDocument.CreateElement(\u0002\u0005.\u0003(1031811476));
			xmlElement2.SetAttribute(\u0002\u0005.\u0003(1031811458), item.\u0003().ToString());
			xmlElement2.SetAttribute(\u0002\u0005.\u0003(1031811445), item.\u0003());
			xmlElement2.SetAttribute(\u0002\u0005.\u0003(1031811424), item.\u0006().ToString());
			xmlElement2.SetAttribute(\u0002\u0005.\u0003(1031811410), item.\u000f().ToString());
			xmlElement.AppendChild(xmlElement2);
			if (\u0008\u2002.\u0008.\u000f\u2002)
			{
				for (int i = 0; i < 10; i++)
				{
					XmlElement xmlElement3 = xmlDocument.CreateElement(\u0002\u0005.\u0003(1031811406));
					xmlElement3.SetAttribute(\u0002\u0005.\u0003(1031811390), \u0002\u0005.\u0003(1031815033));
					xmlElement3.SetAttribute(\u0002\u0005.\u0003(1031811374), i.ToString());
					xmlElement3.SetAttribute(\u0002\u0005.\u0003(1031811354), item.\u0008.\u0003[i].ToString());
					xmlElement3.SetAttribute(\u0002\u0005.\u0003(1031811349), item.\u0008.\u0006[i].ToString());
					xmlElement2.AppendChild(xmlElement3);
				}
			}
			if (\u0008\u2002.\u0008.\u0002\u2002)
			{
				for (int j = 0; j < 10; j++)
				{
					XmlElement xmlElement4 = xmlDocument.CreateElement(\u0002\u0005.\u0003(1031811406));
					xmlElement4.SetAttribute(\u0002\u0005.\u0003(1031811390), \u0002\u0005.\u0003(1031811328));
					xmlElement4.SetAttribute(\u0002\u0005.\u0003(1031811374), j.ToString());
					xmlElement4.SetAttribute(\u0002\u0005.\u0003(1031811354), item.\u0005.\u0003[j].ToString());
					xmlElement4.SetAttribute(\u0002\u0005.\u0003(1031811349), item.\u0005.\u0006[j].ToString());
					xmlElement2.AppendChild(xmlElement4);
				}
			}
			if (\u0008\u2002.\u0008.\u0008\u2002)
			{
				for (int k = 0; k < 10; k++)
				{
					XmlElement xmlElement5 = xmlDocument.CreateElement(\u0002\u0005.\u0003(1031811406));
					xmlElement5.SetAttribute(\u0002\u0005.\u0003(1031811390), \u0002\u0005.\u0003(1031812856));
					xmlElement5.SetAttribute(\u0002\u0005.\u0003(1031811374), k.ToString());
					xmlElement5.SetAttribute(\u0002\u0005.\u0003(1031811354), item.\u000e.\u0003[k].ToString());
					xmlElement5.SetAttribute(\u0002\u0005.\u0003(1031811349), item.\u000e.\u0006[k].ToString());
					xmlElement2.AppendChild(xmlElement5);
				}
			}
			if (\u0008\u2002.\u0008.\u0005\u2002)
			{
				for (int l = 0; l < 10; l++)
				{
					XmlElement xmlElement6 = xmlDocument.CreateElement(\u0002\u0005.\u0003(1031811406));
					xmlElement6.SetAttribute(\u0002\u0005.\u0003(1031811390), \u0002\u0005.\u0003(1031812848));
					xmlElement6.SetAttribute(\u0002\u0005.\u0003(1031811374), l.ToString());
					xmlElement6.SetAttribute(\u0002\u0005.\u0003(1031811354), item.\u0003\u2002.\u0003[l].ToString());
					xmlElement6.SetAttribute(\u0002\u0005.\u0003(1031811349), item.\u0003\u2002.\u0006[l].ToString());
					xmlElement2.AppendChild(xmlElement6);
				}
			}
		}
		xmlDocument.Save(\u0006);
	}

	public bool \u0003(string \u0003, out List<\u000f> \u0006)
	{
		\u0006 = new List<\u000f>();
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(\u0003);
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes(\u0002\u0005.\u0003(1031811506));
			System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			int count = xmlNodeList.Count;
			for (int i = 0; i < count; i++)
			{
				foreach (XmlNode childNode in xmlNodeList.Item(i).ChildNodes)
				{
					\u000f obj = new \u000f();
					obj.\u0003(Convert.ToInt32(childNode.Attributes[\u0002\u0005.\u0003(1031811458)].Value));
					obj.\u0003(childNode.Attributes[\u0002\u0005.\u0003(1031811445)].Value);
					obj.\u0006(Convert.ToInt32(childNode.Attributes[\u0002\u0005.\u0003(1031811424)].Value));
					obj.\u000f(Convert.ToInt32(childNode.Attributes[\u0002\u0005.\u0003(1031811410)].Value));
					foreach (XmlNode childNode2 in childNode.ChildNodes)
					{
						if (childNode2.Name == \u0002\u0005.\u0003(1031811406))
						{
							int num = Convert.ToInt32(childNode2.Attributes[\u0002\u0005.\u0003(1031811390)].Value);
							int num2 = Convert.ToInt32(childNode2.Attributes[\u0002\u0005.\u0003(1031811374)].Value);
							switch (num)
							{
							case 0:
								obj.\u0008.\u0003[num2] = Convert.ToByte(childNode2.Attributes[\u0002\u0005.\u0003(1031811354)].Value);
								obj.\u0008.\u0006[num2] = Convert.ToByte(childNode2.Attributes[\u0002\u0005.\u0003(1031811349)].Value);
								break;
							case 1:
								obj.\u0005.\u0003[num2] = Convert.ToByte(childNode2.Attributes[\u0002\u0005.\u0003(1031811354)].Value);
								obj.\u0005.\u0006[num2] = Convert.ToByte(childNode2.Attributes[\u0002\u0005.\u0003(1031811349)].Value);
								break;
							case 2:
								obj.\u000e.\u0003[num2] = Convert.ToByte(childNode2.Attributes[\u0002\u0005.\u0003(1031811354)].Value);
								obj.\u000e.\u0006[num2] = Convert.ToByte(childNode2.Attributes[\u0002\u0005.\u0003(1031811349)].Value);
								break;
							case 3:
								obj.\u0003\u2002.\u0003[num2] = Convert.ToByte(childNode2.Attributes[\u0002\u0005.\u0003(1031811354)].Value);
								obj.\u0003\u2002.\u0006[num2] = Convert.ToByte(childNode2.Attributes[\u0002\u0005.\u0003(1031811349)].Value);
								break;
							}
						}
					}
					\u0006.Add(obj);
				}
			}
		}
		catch
		{
			return false;
		}
		return true;
	}
}
public sealed class \u000f\u2001
{
	public sealed class \u0003
	{
		public int \u0003 = 0;

		public bool \u0006 = false;

		public bool \u000f = false;

		public bool \u0002 = false;

		public bool \u0008 = false;

		public bool \u0005 = false;

		public bool \u000e = false;

		public bool \u0003\u2002 = false;

		public bool \u0006\u2002 = false;

		public bool \u000f\u2002 = false;

		public bool \u0002\u2002 = false;

		public bool \u0008\u2002 = false;

		public bool \u0005\u2002 = false;

		public int \u000e\u2002 = 0;

		public bool \u0003\u2001 = false;

		public bool \u0006\u2001 = false;

		public bool \u000f\u2001 = false;

		public bool \u0002\u2001 = false;

		public bool \u0008\u2001 = false;

		public bool \u0005\u2001 = false;

		public bool \u000e\u2001 = false;

		public bool \u0003\u2009 = false;

		public bool \u0006\u2009 = false;

		public bool \u000f\u2009 = false;

		public bool \u0002\u2009 = false;
	}

	public bool \u0003 = false;

	public bool \u0006 = false;

	public bool \u000f = false;

	public bool \u0002 = false;

	public bool \u0008 = false;

	public bool \u0005 = false;

	public bool \u000e = false;

	public bool \u0003\u2002 = false;

	public bool \u0006\u2002 = false;

	public bool \u000f\u2002 = false;

	public bool \u0002\u2002 = false;

	public bool \u0008\u2002 = false;

	public bool \u0005\u2002 = false;

	public bool \u000e\u2002 = false;

	public bool \u0003\u2001 = false;

	public bool \u0006\u2001 = false;

	public bool \u000f\u2001 = false;

	public bool \u0002\u2001 = false;

	public byte \u0008\u2001 = 0;

	public bool \u0005\u2001 = false;

	public bool \u000e\u2001 = true;

	public bool \u0003\u2009 = false;

	public bool \u0006\u2009 = false;

	public bool \u000f\u2009 = false;

	public bool \u0002\u2009 = false;

	public bool \u0008\u2009 = false;

	public bool \u0005\u2009 = false;

	public \u0003 \u000e\u2009 = new \u0003();

	public void \u0003()
	{
	}
}
internal sealed class \u000f\u2002
{
	public static void \u0003(string \u0003)
	{
		try
		{
			EventLog eventLog = new EventLog();
			eventLog.Source = \u0002\u0005.\u0003(1031813694);
			eventLog.Log = \u0002\u0005.\u0003(1031813656);
			\u0003 = \u0002\u0005.\u0003(1031813653) + \u0003;
			eventLog.WriteEntry(\u0003);
		}
		catch
		{
		}
	}

	public static void \u0006(string \u0003)
	{
		try
		{
			EventLog eventLog = new EventLog();
			eventLog.Source = \u0002\u0005.\u0003(1031813694);
			eventLog.Log = \u0002\u0005.\u0003(1031813656);
			\u0003 = \u0002\u0005.\u0003(1031813637) + \u0003;
			eventLog.WriteEntry(\u0003);
		}
		catch
		{
		}
	}
}
namespace -
{
	public sealed class i11l1i1Ii1I1 : System.Windows.Controls.UserControl, IComponentConnector
	{
		private sealed class \u0003
		{
			public i11l1i1Ii1I1 \u0003;

			public int \u0006;

			internal void \u0003()
			{
				Thickness margin = default(Thickness);
				margin.Left = this.\u0003.\u0003\u2001[\u0006];
				margin.Top = this.\u0003.\u000e\u2002[\u0006];
				margin.Bottom = -20.0;
				margin.Right = -20.0;
				this.\u0003.\u0006\u2001[\u0006].ToolTip = \u0002\u0005.\u0003(1031808278) + (\u0006 + 1) + \u0002\u0005.\u0003(1031814562) + this.\u0003.\u0003\u200b\u2003.Text + \u0002\u0005.\u0003(1031808494) + this.\u0003.\u0008\u2002[\u0006] + \u0002\u0005.\u0003(1031808472) + this.\u0003.\u000f\u200b\u2003.Text + \u0002\u0005.\u0003(1031808494) + this.\u0003.\u0005\u2002[\u0006] + \u0002\u0005.\u0003(1031808465);
				this.\u0003.\u000f\u2001[\u0006].Margin = margin;
				if (\u0006 == 9)
				{
					this.\u0003.\u0002\u2001[\u0006].Width = 0.0;
					this.\u0003.\u0002\u2001[\u0006].Height = 0.0;
				}
				else
				{
					this.\u0003.\u0002\u2001[\u0006].Width = this.\u0003.\u0003\u2001[\u0006 + 1] - this.\u0003.\u0003\u2001[\u0006];
					this.\u0003.\u0002\u2001[\u0006].Height = this.\u0003.\u000e\u2002[\u0006 + 1] - this.\u0003.\u000e\u2002[\u0006];
				}
				if (\u0006 != 0)
				{
					this.\u0003.\u0002\u2001[\u0006 - 1].Width = this.\u0003.\u0003\u2001[\u0006] - this.\u0003.\u0003\u2001[\u0006 - 1];
					this.\u0003.\u0002\u2001[\u0006 - 1].Height = this.\u0003.\u000e\u2002[\u0006] - this.\u0003.\u000e\u2002[\u0006 - 1];
				}
			}
		}

		private sealed class \u0006
		{
			public i11l1i1Ii1I1 \u0003;

			public int \u0006;

			internal void \u0003()
			{
				this.\u0003.m_\u0002 = false;
				for (int i = 0; i < this.\u0003.\u0006\u2001.Count; i++)
				{
					this.\u0003.\u0006\u2001[i].StrokeThickness = 0.0;
				}
				Color color = Color.FromArgb(byte.MaxValue, byte.MaxValue, 0, 0);
				Brush stroke = new SolidColorBrush(color);
				this.\u0003.\u0006\u2001[\u0006].Stroke = stroke;
				this.\u0003.\u0006\u2001[\u0006].StrokeThickness = 24.0;
				this.\u0003.\u0006\u200b\u2003.Items.Clear();
				if (\u0006 == 0)
				{
					for (int j = 0; j < this.\u0003.\u0008\u2002[\u0006 + 1]; j++)
					{
						int num = j;
						this.\u0003.\u0006\u200b\u2003.Items.Add(num);
					}
				}
				else if (\u0006 == 9)
				{
					for (int k = 1; k <= 100 - this.\u0003.\u0008\u2002[\u0006 - 1]; k++)
					{
						int num2 = this.\u0003.\u0008\u2002[\u0006 - 1] + k;
						this.\u0003.\u0006\u200b\u2003.Items.Add(num2);
					}
				}
				else
				{
					for (int l = 1; l < this.\u0003.\u0008\u2002[\u0006 + 1] - this.\u0003.\u0008\u2002[\u0006 - 1]; l++)
					{
						int num3 = this.\u0003.\u0008\u2002[\u0006 - 1] + l;
						this.\u0003.\u0006\u200b\u2003.Items.Add(num3);
					}
				}
				this.\u0003.\u0006\u200b\u2003.SelectedValue = this.\u0003.\u0008\u2002[\u0006];
				this.\u0003.\u0002\u200b\u2003.Items.Clear();
				if (\u0006 == 0)
				{
					for (int m = 0; m < this.\u0003.\u0005\u2002[\u0006 + 1]; m++)
					{
						int num4 = m;
						this.\u0003.\u0002\u200b\u2003.Items.Add(num4);
					}
				}
				else if (\u0006 == 9)
				{
					for (int n = 1; n <= 100 - this.\u0003.\u0005\u2002[\u0006 - 1]; n++)
					{
						int num5 = this.\u0003.\u0005\u2002[\u0006 - 1] + n;
						this.\u0003.\u0002\u200b\u2003.Items.Add(num5);
					}
				}
				else
				{
					for (int num6 = 1; num6 < this.\u0003.\u0005\u2002[\u0006 + 1] - this.\u0003.\u0005\u2002[\u0006 - 1]; num6++)
					{
						int num7 = this.\u0003.\u0005\u2002[\u0006 - 1] + num6;
						this.\u0003.\u0002\u200b\u2003.Items.Add(num7);
					}
				}
				this.\u0003.\u0002\u200b\u2003.SelectedValue = this.\u0003.\u0005\u2002[\u0006];
				this.\u0003.m_\u0002 = true;
			}
		}

		public delegate void \u000f();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private \u000f m_\u0003;

		private bool m_\u0006 = false;

		private int m_\u000f = 0;

		private bool m_\u0002 = false;

		private \u000f\u0005.\u0006 \u0008;

		private \u000f\u0005.\u0003 \u0005;

		private bool \u000e = false;

		private bool \u0003\u2002 = false;

		private int \u0006\u2002 = 0;

		private double \u000f\u2002 = 11.2;

		private double \u0002\u2002 = 34.0;

		private List<int> \u0008\u2002 = new List<int>();

		private List<int> \u0005\u2002 = new List<int>();

		private List<double> \u000e\u2002 = new List<double>();

		private List<double> \u0003\u2001 = new List<double>();

		private List<Ellipse> \u0006\u2001 = new List<Ellipse>();

		private List<Grid> \u000f\u2001 = new List<Grid>();

		private List<System.Windows.Shapes.Path> \u0002\u2001 = new List<System.Windows.Shapes.Path>();

		internal i11l1i1Ii1I1 \u0008\u2001;

		internal Grid \u0005\u2001;

		internal StackPanel \u000e\u2001;

		internal TextBlock \u0003\u2009;

		internal TextBlock \u0006\u2009;

		internal TextBlock \u000f\u2009;

		internal TextBlock \u0002\u2009;

		internal TextBlock \u0008\u2009;

		internal TextBlock \u0005\u2009;

		internal TextBlock \u000e\u2009;

		internal TextBlock \u0003\u2003;

		internal TextBlock \u0006\u2003;

		internal TextBlock \u000f\u2003;

		internal TextBlock \u0002\u2003;

		internal TextBlock \u0008\u2003;

		internal StackPanel \u0005\u2003;

		internal TextBlock \u000e\u2003;

		internal TextBlock \u0003\u2004;

		internal TextBlock \u0006\u2004;

		internal TextBlock \u000f\u2004;

		internal TextBlock \u0002\u2004;

		internal TextBlock \u0008\u2004;

		internal TextBlock \u0005\u2004;

		internal TextBlock \u000e\u2004;

		internal TextBlock \u0003\u2000;

		internal StackPanel \u0006\u2000;

		internal TextBlock \u000f\u2000;

		internal TextBlock \u0002\u2000;

		internal TextBlock \u0008\u2000;

		internal TextBlock \u0005\u2000;

		internal TextBlock \u000e\u2000;

		internal TextBlock \u0003\u2007;

		internal TextBlock \u0006\u2007;

		internal TextBlock \u000f\u2007;

		internal TextBlock \u0002\u2007;

		internal Grid \u0008\u2007;

		internal StackPanel \u0005\u2007;

		internal StackPanel \u000e\u2007;

		internal Rectangle \u0003\u2005;

		internal Rectangle \u0006\u2005;

		internal Rectangle \u000f\u2005;

		internal Rectangle \u0002\u2005;

		internal Rectangle \u0008\u2005;

		internal Rectangle \u0005\u2005;

		internal Rectangle \u000e\u2005;

		internal Rectangle \u0003\u200b;

		internal Rectangle \u0006\u200b;

		internal Rectangle \u000f\u200b;

		internal Rectangle \u0002\u200b;

		internal Rectangle \u0008\u200b;

		internal Rectangle \u0005\u200b;

		internal Rectangle \u000e\u200b;

		internal Rectangle \u0003\u200a;

		internal Rectangle \u0006\u200a;

		internal Rectangle \u000f\u200a;

		internal Rectangle \u0002\u200a;

		internal Rectangle \u0008\u200a;

		internal Rectangle \u0005\u200a;

		internal Rectangle \u000e\u200a;

		internal Rectangle \u0003\u2006;

		internal Rectangle \u0006\u2006;

		internal Rectangle \u000f\u2006;

		internal Rectangle \u0002\u2006;

		internal Rectangle \u0008\u2006;

		internal Rectangle \u0005\u2006;

		internal Rectangle \u000e\u2006;

		internal Rectangle \u0003\u2008;

		internal Rectangle \u0006\u2008;

		internal Rectangle \u000f\u2008;

		internal Rectangle \u0002\u2008;

		internal Rectangle \u0008\u2008;

		internal Rectangle \u0005\u2008;

		internal Rectangle \u000e\u2008;

		internal Rectangle \u0003\u2002\u2009;

		internal Rectangle \u0006\u2002\u2009;

		internal Rectangle \u000f\u2002\u2009;

		internal Rectangle \u0002\u2002\u2009;

		internal Rectangle \u0008\u2002\u2009;

		internal Rectangle \u0005\u2002\u2009;

		internal Rectangle \u000e\u2002\u2009;

		internal Rectangle \u0003\u2001\u2009;

		internal Rectangle \u0006\u2001\u2009;

		internal Rectangle \u000f\u2001\u2009;

		internal Rectangle \u0002\u2001\u2009;

		internal Rectangle \u0008\u2001\u2009;

		internal Rectangle \u0005\u2001\u2009;

		internal Rectangle \u000e\u2001\u2009;

		internal Rectangle \u0003\u2009\u2009;

		internal Rectangle \u0006\u2009\u2009;

		internal Rectangle \u000f\u2009\u2009;

		internal Rectangle \u0002\u2009\u2009;

		internal Rectangle \u0008\u2009\u2009;

		internal Rectangle \u0005\u2009\u2009;

		internal Rectangle \u000e\u2009\u2009;

		internal Rectangle \u0003\u2003\u2009;

		internal Rectangle \u0006\u2003\u2009;

		internal Rectangle \u000f\u2003\u2009;

		internal Rectangle \u0002\u2003\u2009;

		internal Rectangle \u0008\u2003\u2009;

		internal Rectangle \u0005\u2003\u2009;

		internal Rectangle \u000e\u2003\u2009;

		internal Rectangle \u0003\u2004\u2009;

		internal Rectangle \u0006\u2004\u2009;

		internal Rectangle \u000f\u2004\u2009;

		internal StackPanel \u0002\u2004\u2009;

		internal Rectangle \u0008\u2004\u2009;

		internal Rectangle \u0005\u2004\u2009;

		internal Rectangle \u000e\u2004\u2009;

		internal Rectangle \u0003\u2000\u2009;

		internal Rectangle \u0006\u2000\u2009;

		internal Rectangle \u000f\u2000\u2009;

		internal Rectangle \u0002\u2000\u2009;

		internal Rectangle \u0008\u2000\u2009;

		internal Rectangle \u0005\u2000\u2009;

		internal Rectangle \u000e\u2000\u2009;

		internal Rectangle \u0003\u2007\u2009;

		internal Rectangle \u0006\u2007\u2009;

		internal Rectangle \u000f\u2007\u2009;

		internal Rectangle \u0002\u2007\u2009;

		internal Rectangle \u0008\u2007\u2009;

		internal Rectangle \u0005\u2007\u2009;

		internal Rectangle \u000e\u2007\u2009;

		internal Rectangle \u0003\u2005\u2009;

		internal Rectangle \u0006\u2005\u2009;

		internal Rectangle \u000f\u2005\u2009;

		internal Rectangle \u0002\u2005\u2009;

		internal Rectangle \u0008\u2005\u2009;

		internal Rectangle \u0005\u2005\u2009;

		internal Rectangle \u000e\u2005\u2009;

		internal Rectangle \u0003\u200b\u2009;

		internal Rectangle \u0006\u200b\u2009;

		internal Rectangle \u000f\u200b\u2009;

		internal Rectangle \u0002\u200b\u2009;

		internal Rectangle \u0008\u200b\u2009;

		internal Rectangle \u0005\u200b\u2009;

		internal Rectangle \u000e\u200b\u2009;

		internal Rectangle \u0003\u200a\u2009;

		internal Rectangle \u0006\u200a\u2009;

		internal Rectangle \u000f\u200a\u2009;

		internal Rectangle \u0002\u200a\u2009;

		internal Rectangle \u0008\u200a\u2009;

		internal Rectangle \u0005\u200a\u2009;

		internal Rectangle \u000e\u200a\u2009;

		internal Rectangle \u0003\u2006\u2009;

		internal Rectangle \u0006\u2006\u2009;

		internal Rectangle \u000f\u2006\u2009;

		internal Rectangle \u0002\u2006\u2009;

		internal Rectangle \u0008\u2006\u2009;

		internal Rectangle \u0005\u2006\u2009;

		internal Rectangle \u000e\u2006\u2009;

		internal Rectangle \u0003\u2008\u2009;

		internal Rectangle \u0006\u2008\u2009;

		internal Rectangle \u000f\u2008\u2009;

		internal Rectangle \u0002\u2008\u2009;

		internal Rectangle \u0008\u2008\u2009;

		internal Rectangle \u0005\u2008\u2009;

		internal Rectangle \u000e\u2008\u2009;

		internal Rectangle \u0003\u2002\u2003;

		internal Rectangle \u0006\u2002\u2003;

		internal Rectangle \u000f\u2002\u2003;

		internal Rectangle \u0002\u2002\u2003;

		internal Rectangle \u0008\u2002\u2003;

		internal Rectangle \u0005\u2002\u2003;

		internal Rectangle \u000e\u2002\u2003;

		internal Rectangle \u0003\u2001\u2003;

		internal Rectangle \u0006\u2001\u2003;

		internal Rectangle \u000f\u2001\u2003;

		internal Rectangle \u0002\u2001\u2003;

		internal Rectangle \u0008\u2001\u2003;

		internal Rectangle \u0005\u2001\u2003;

		internal Rectangle \u000e\u2001\u2003;

		internal StackPanel \u0003\u2009\u2003;

		internal StackPanel \u0006\u2009\u2003;

		internal System.Windows.Shapes.Path \u000f\u2009\u2003;

		internal System.Windows.Shapes.Path \u0002\u2009\u2003;

		internal System.Windows.Shapes.Path \u0008\u2009\u2003;

		internal System.Windows.Shapes.Path \u0005\u2009\u2003;

		internal System.Windows.Shapes.Path \u000e\u2009\u2003;

		internal System.Windows.Shapes.Path \u0003\u2003\u2003;

		internal System.Windows.Shapes.Path \u0006\u2003\u2003;

		internal System.Windows.Shapes.Path \u000f\u2003\u2003;

		internal System.Windows.Shapes.Path \u0002\u2003\u2003;

		internal System.Windows.Shapes.Path \u0008\u2003\u2003;

		internal System.Windows.Shapes.Path \u0005\u2003\u2003;

		internal System.Windows.Shapes.Path \u000e\u2003\u2003;

		internal System.Windows.Shapes.Path \u0003\u2004\u2003;

		internal System.Windows.Shapes.Path \u0006\u2004\u2003;

		internal System.Windows.Shapes.Path \u000f\u2004\u2003;

		internal System.Windows.Shapes.Path \u0002\u2004\u2003;

		internal System.Windows.Shapes.Path \u0008\u2004\u2003;

		internal Grid \u0005\u2004\u2003;

		internal Grid \u000e\u2004\u2003;

		internal Rectangle \u0003\u2000\u2003;

		internal Grid \u0006\u2000\u2003;

		internal Ellipse \u000f\u2000\u2003;

		internal Grid \u0002\u2000\u2003;

		internal Ellipse \u0008\u2000\u2003;

		internal Grid \u0005\u2000\u2003;

		internal Ellipse \u000e\u2000\u2003;

		internal Grid \u0003\u2007\u2003;

		internal Ellipse \u0006\u2007\u2003;

		internal Grid \u000f\u2007\u2003;

		internal Ellipse \u0002\u2007\u2003;

		internal Grid \u0008\u2007\u2003;

		internal Ellipse \u0005\u2007\u2003;

		internal Grid \u000e\u2007\u2003;

		internal Ellipse \u0003\u2005\u2003;

		internal Grid \u0006\u2005\u2003;

		internal Ellipse \u000f\u2005\u2003;

		internal Grid \u0002\u2005\u2003;

		internal Ellipse \u0008\u2005\u2003;

		internal Grid \u0005\u2005\u2003;

		internal Ellipse \u000e\u2005\u2003;

		internal TextBlock \u0003\u200b\u2003;

		internal System.Windows.Controls.ComboBox \u0006\u200b\u2003;

		internal TextBlock \u000f\u200b\u2003;

		internal System.Windows.Controls.ComboBox \u0002\u200b\u2003;

		internal StackPanel \u0008\u200b\u2003;

		internal StackPanel \u0005\u200b\u2003;

		internal TextBlock \u000e\u200b\u2003;

		internal TextBlock \u0003\u200a\u2003;

		internal TextBlock \u0006\u200a\u2003;

		internal TextBlock \u000f\u200a\u2003;

		private bool \u0002\u200a\u2003;

		public i11l1i1Ii1I1()
		{
			InitializeComponent();
		}

		public void \u0003(\u000f \u0003)
		{
			\u000f obj = this.m_\u0003;
			\u000f obj2;
			do
			{
				obj2 = obj;
				\u000f value = (\u000f)Delegate.Combine(obj2, \u0003);
				obj = Interlocked.CompareExchange(ref this.m_\u0003, value, obj2);
			}
			while ((object)obj != obj2);
		}

		public void \u0006(\u000f \u0003)
		{
			\u000f obj = this.m_\u0003;
			\u000f obj2;
			do
			{
				obj2 = obj;
				\u000f value = (\u000f)Delegate.Remove(obj2, \u0003);
				obj = Interlocked.CompareExchange(ref this.m_\u0003, value, obj2);
			}
			while ((object)obj != obj2);
		}

		private void \u0003(object \u0003, RoutedEventArgs \u0006)
		{
			this.\u0003();
		}

		public void \u0003()
		{
			global::\u0008\u2002.\u000f\u2002.\u0003();
		}

		public void \u0003(\u000f\u0005.\u0006 \u0003)
		{
			\u0008 = \u0003;
			switch (\u0008)
			{
			case (\u000f\u0005.\u0006)0:
				\u0005 = global::\u0008\u2002.\u000e.\u0005;
				break;
			case (\u000f\u0005.\u0006)1:
				\u0005 = global::\u0008\u2002.\u000e.\u000e;
				break;
			case (\u000f\u0005.\u0006)2:
				\u0005 = global::\u0008\u2002.\u000e.\u0003\u2002;
				break;
			case (\u000f\u0005.\u0006)3:
				\u0005 = global::\u0008\u2002.\u000e.\u0006\u2002;
				break;
			}
			\u000f\u2002 = \u0008\u2007.Width / 65.0;
			\u0006();
			this.m_\u0006 = true;
		}

		private void \u0006()
		{
			\u0008\u2002 = new List<int> { 0, 30, 40, 45, 50, 60, 70, 80, 90, 100 };
			\u0005\u2002 = new List<int> { 35, 40, 45, 50, 60, 65, 70, 80, 90, 100 };
			\u000e\u2002.Clear();
			\u0003\u2001.Clear();
			for (int i = 0; i < 10; i++)
			{
				\u0008\u2002[i] = \u0005.\u0005\u2002[i].\u0006\u2002;
				\u0005\u2002[i] = \u0005.\u0005\u2002[i].\u000f\u2002;
				\u000e\u2002.Add(\u0008\u2002[i] * 3);
				\u0003\u2001.Add(Math.Round((double)(\u0005\u2002[i] - 35) * 11.2, 0));
			}
			\u0006\u2001 = new List<Ellipse> { \u000f\u2000\u2003, \u0008\u2000\u2003, \u000e\u2000\u2003, \u0006\u2007\u2003, \u0002\u2007\u2003, \u0005\u2007\u2003, \u0003\u2005\u2003, \u000f\u2005\u2003, \u0008\u2005\u2003, \u000e\u2005\u2003 };
			\u000f\u2001 = new List<Grid> { \u0006\u2000\u2003, \u0002\u2000\u2003, \u0005\u2000\u2003, \u0003\u2007\u2003, \u000f\u2007\u2003, \u0008\u2007\u2003, \u000e\u2007\u2003, \u0006\u2005\u2003, \u0002\u2005\u2003, \u0005\u2005\u2003 };
			\u0002\u2001 = new List<System.Windows.Shapes.Path> { \u000f\u2003\u2003, \u0002\u2003\u2003, \u0008\u2003\u2003, \u0005\u2003\u2003, \u000e\u2003\u2003, \u0003\u2004\u2003, \u0006\u2004\u2003, \u000f\u2004\u2003, \u0002\u2004\u2003, \u0008\u2004\u2003 };
			for (int j = 0; j < 10; j++)
			{
				\u0006(j);
			}
		}

		public void \u000f()
		{
			switch (\u0008)
			{
			case (\u000f\u0005.\u0006)0:
				global::\u0008\u2002.\u000e.\u0005 = global::\u0008\u2002.\u000e.\u000f(global::\u0008\u2002.\u000e.\u0005);
				\u0005 = global::\u0008\u2002.\u000e.\u0005;
				break;
			case (\u000f\u0005.\u0006)1:
				global::\u0008\u2002.\u000e.\u000e = global::\u0008\u2002.\u000e.\u000f(global::\u0008\u2002.\u000e.\u000e);
				\u0005 = global::\u0008\u2002.\u000e.\u000e;
				break;
			case (\u000f\u0005.\u0006)2:
				global::\u0008\u2002.\u000e.\u0003\u2002 = global::\u0008\u2002.\u000e.\u000f(global::\u0008\u2002.\u000e.\u0003\u2002);
				\u0005 = global::\u0008\u2002.\u000e.\u0003\u2002;
				break;
			case (\u000f\u0005.\u0006)3:
				global::\u0008\u2002.\u000e.\u0006\u2002 = global::\u0008\u2002.\u000e.\u000f(global::\u0008\u2002.\u000e.\u0006\u2002);
				\u0005 = global::\u0008\u2002.\u000e.\u0006\u2002;
				break;
			}
			\u0006();
		}

		public void \u0002()
		{
			\u0005.\u0005\u2002[0].\u000f\u2002 = (byte)\u0005\u2002[0];
			\u0005.\u0005\u2002[1].\u000f\u2002 = (byte)\u0005\u2002[1];
			\u0005.\u0005\u2002[2].\u000f\u2002 = (byte)\u0005\u2002[2];
			\u0005.\u0005\u2002[3].\u000f\u2002 = (byte)\u0005\u2002[3];
			\u0005.\u0005\u2002[4].\u000f\u2002 = (byte)\u0005\u2002[4];
			\u0005.\u0005\u2002[5].\u000f\u2002 = (byte)\u0005\u2002[5];
			\u0005.\u0005\u2002[6].\u000f\u2002 = (byte)\u0005\u2002[6];
			\u0005.\u0005\u2002[7].\u000f\u2002 = (byte)\u0005\u2002[7];
			\u0005.\u0005\u2002[8].\u000f\u2002 = (byte)\u0005\u2002[8];
			\u0005.\u0005\u2002[9].\u000f\u2002 = (byte)\u0005\u2002[9];
			\u0005.\u0005\u2002[0].\u0006\u2002 = (byte)\u0008\u2002[0];
			\u0005.\u0005\u2002[1].\u0006\u2002 = (byte)\u0008\u2002[1];
			\u0005.\u0005\u2002[2].\u0006\u2002 = (byte)\u0008\u2002[2];
			\u0005.\u0005\u2002[3].\u0006\u2002 = (byte)\u0008\u2002[3];
			\u0005.\u0005\u2002[4].\u0006\u2002 = (byte)\u0008\u2002[4];
			\u0005.\u0005\u2002[5].\u0006\u2002 = (byte)\u0008\u2002[5];
			\u0005.\u0005\u2002[6].\u0006\u2002 = (byte)\u0008\u2002[6];
			\u0005.\u0005\u2002[7].\u0006\u2002 = (byte)\u0008\u2002[7];
			\u0005.\u0005\u2002[8].\u0006\u2002 = (byte)\u0008\u2002[8];
			\u0005.\u0005\u2002[9].\u0006\u2002 = (byte)\u0008\u2002[9];
			switch (\u0008)
			{
			case (\u000f\u0005.\u0006)0:
				global::\u0008\u2002.\u000e.\u0005 = \u0005;
				global::\u0008\u2002.\u000e.\u0005 = global::\u0008\u2002.\u000e.\u0003(global::\u0008\u2002.\u000e.\u0005);
				break;
			case (\u000f\u0005.\u0006)1:
				global::\u0008\u2002.\u000e.\u000e = \u0005;
				global::\u0008\u2002.\u000e.\u000e = global::\u0008\u2002.\u000e.\u0003(global::\u0008\u2002.\u000e.\u000e);
				break;
			case (\u000f\u0005.\u0006)2:
				global::\u0008\u2002.\u000e.\u0003\u2002 = \u0005;
				global::\u0008\u2002.\u000e.\u0003\u2002 = global::\u0008\u2002.\u000e.\u0003(global::\u0008\u2002.\u000e.\u0003\u2002);
				break;
			case (\u000f\u0005.\u0006)3:
				global::\u0008\u2002.\u000e.\u0006\u2002 = \u0005;
				global::\u0008\u2002.\u000e.\u0006\u2002 = global::\u0008\u2002.\u000e.\u0003(global::\u0008\u2002.\u000e.\u0006\u2002);
				break;
			}
		}

		private void \u0003(object \u0003, System.Windows.Input.MouseEventArgs \u0006)
		{
			Console.WriteLine(\u0002\u0005.\u0003(1031808259));
			if (\u0006.LeftButton == MouseButtonState.Released)
			{
				\u000e = false;
				\u0003\u2002 = false;
				base.Cursor = System.Windows.Input.Cursors.Arrow;
				return;
			}
			double num = Math.Floor(\u0006.GetPosition(\u0003\u2000\u2003).X);
			double num2 = Math.Floor(\u0006.GetPosition(\u0003\u2000\u2003).Y);
			Console.WriteLine(\u0002\u0005.\u0003(1031805675) + this.m_\u000f + \u0002\u0005.\u0003(1031805652) + num2 + \u0002\u0005.\u0003(1031805635) + num);
			Thickness thickness = default(Thickness);
			thickness.Left = num;
			thickness.Top = num2;
			base.Cursor = System.Windows.Input.Cursors.SizeAll;
			double num3 = 0.0;
			double num4 = 0.0;
			double num5 = 728.0;
			double num6 = 300.0;
			if (this.m_\u000f != 0)
			{
				num3 = \u0003\u2001[this.m_\u000f - 1];
				num4 = \u000e\u2002[this.m_\u000f - 1];
			}
			if (this.m_\u000f != 9)
			{
				num5 = \u0003\u2001[this.m_\u000f + 1];
				num6 = \u000e\u2002[this.m_\u000f + 1];
			}
			Console.WriteLine(\u0002\u0005.\u0003(1031805620) + num4 + \u0002\u0005.\u0003(1031805601) + num6 + \u0002\u0005.\u0003(1031805593) + num3 + \u0002\u0005.\u0003(1031805601) + num5);
			if (num >= num5 - \u000f\u2002)
			{
				thickness.Left = num5 - \u000f\u2002;
			}
			else if (num <= num3)
			{
				thickness.Left = num3 + \u000f\u2002;
			}
			else
			{
				thickness.Left = num;
			}
			if (num2 >= num6 - 3.0)
			{
				thickness.Top = num6 - 3.0;
			}
			else if (num2 <= num4)
			{
				thickness.Top = num4 + 3.0;
			}
			else
			{
				thickness.Top = num2;
			}
			Console.WriteLine(\u0002\u0005.\u0003(1031805620) + thickness.Top + \u0002\u0005.\u0003(1031805593) + thickness.Left);
			int num7 = (int)Math.Ceiling(thickness.Left / \u000f\u2002 + 35.0);
			if (this.m_\u000f != 9 && num7 >= \u0005\u2002[this.m_\u000f + 1])
			{
				\u0005\u2002[this.m_\u000f] = \u0005\u2002[this.m_\u000f + 1] - 1;
			}
			else if (this.m_\u000f != 0 && num7 <= \u0005\u2002[this.m_\u000f - 1])
			{
				\u0005\u2002[this.m_\u000f] = \u0005\u2002[this.m_\u000f - 1] + 1;
			}
			else
			{
				\u0005\u2002[this.m_\u000f] = num7;
			}
			\u0008\u2002[this.m_\u000f] = (int)Math.Ceiling(thickness.Top / 3.0);
			Console.WriteLine(\u0002\u0005.\u0003(1031805620) + \u0008\u2002[this.m_\u000f] + \u0002\u0005.\u0003(1031805576) + thickness.Top + \u0002\u0005.\u0003(1031805593) + \u0005\u2002[this.m_\u000f] + \u0002\u0005.\u0003(1031805570) + thickness.Left + \u0002\u0005.\u0003(1031805564) + num7);
			this.\u0003(this.m_\u000f);
			this.m_\u0002 = false;
			\u0002\u200b\u2003.SelectedValue = \u0005\u2002[this.m_\u000f];
			\u0006\u200b\u2003.SelectedValue = \u0008\u2002[this.m_\u000f];
			this.\u0006(this.m_\u000f);
			this.m_\u0002 = true;
			this.m_\u0003();
		}

		private void \u0003(object \u0003, MouseButtonEventArgs \u0006)
		{
			Console.WriteLine(\u0002\u0005.\u0003(1031805544));
			\u0003\u2000\u2003.Visibility = Visibility.Hidden;
			base.Cursor = System.Windows.Input.Cursors.SizeAll;
		}

		private void \u0003(int \u0003)
		{
			Thickness thickness = default(Thickness);
			thickness.Left = Math.Round((double)(\u0005\u2002[\u0003] - 35) * 11.2, 0);
			thickness.Top = \u0008\u2002[\u0003] * 3;
			\u0003\u2001[\u0003] = thickness.Left;
			\u000e\u2002[\u0003] = thickness.Top;
		}

		private void \u0006(int \u0003)
		{
			\u0003 obj = new \u0003();
			obj.\u0003 = this;
			obj.\u0006 = \u0003;
			base.Dispatcher.BeginInvoke(new Action(obj.\u0003));
		}

		private void \u0006(object \u0003, MouseButtonEventArgs \u0006)
		{
			\u0003\u2000\u2003.Visibility = Visibility.Visible;
			Console.WriteLine(\u0002\u0005.\u0003(1031805512));
			Ellipse ellipse = (Ellipse)\u0003;
			string value = ellipse.Name.Remove(0, 1);
			this.m_\u000f = Convert.ToInt32(value);
			\u000f(this.m_\u000f);
		}

		private void \u0006(object \u0003, System.Windows.Input.MouseEventArgs \u0006)
		{
		}

		private void \u000f(object \u0003, MouseButtonEventArgs \u0006)
		{
			Console.WriteLine(\u0002\u0005.\u0003(1031805480));
			\u0003\u2000\u2003.Visibility = Visibility.Hidden;
		}

		private void \u0003(object \u0003, SelectionChangedEventArgs \u0006)
		{
			if (this.m_\u0002)
			{
				\u0008\u2002[this.m_\u000f] = Convert.ToInt32(\u0006\u200b\u2003.SelectedValue);
				this.\u0003(this.m_\u000f);
				this.\u0006(this.m_\u000f);
				this.m_\u0003();
			}
		}

		private void \u0006(object \u0003, SelectionChangedEventArgs \u0006)
		{
			if (this.m_\u0002)
			{
				\u0005\u2002[this.m_\u000f] = Convert.ToInt32(\u0002\u200b\u2003.SelectedValue);
				this.\u0003(this.m_\u000f);
				this.\u0006(this.m_\u000f);
				this.m_\u0003();
			}
		}

		public void \u000f(int \u0003)
		{
			\u0006 obj = new \u0006();
			obj.\u0003 = this;
			obj.\u0006 = \u0003;
			base.Dispatcher.BeginInvoke(new Action(obj.\u0003));
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!\u0002\u200a\u2003)
			{
				\u0002\u200a\u2003 = true;
				Uri resourceLocator = new Uri(\u0002\u0005.\u0003(1031805462), UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		private void 4qwhgehlclvxzz5pvgf7bmk97bj96j5p\u2009\u2005\u2006\u0003(int \u0003, object \u0006)
		{
			switch (\u0003)
			{
			case 1:
				\u0008\u2001 = (i11l1i1Ii1I1)\u0006;
				\u0008\u2001.Loaded += this.\u0003;
				break;
			case 2:
				\u0005\u2001 = (Grid)\u0006;
				break;
			case 3:
				\u000e\u2001 = (StackPanel)\u0006;
				break;
			case 4:
				\u0003\u2009 = (TextBlock)\u0006;
				break;
			case 5:
				\u0006\u2009 = (TextBlock)\u0006;
				break;
			case 6:
				\u000f\u2009 = (TextBlock)\u0006;
				break;
			case 7:
				\u0002\u2009 = (TextBlock)\u0006;
				break;
			case 8:
				\u0008\u2009 = (TextBlock)\u0006;
				break;
			case 9:
				\u0005\u2009 = (TextBlock)\u0006;
				break;
			case 10:
				\u000e\u2009 = (TextBlock)\u0006;
				break;
			case 11:
				\u0003\u2003 = (TextBlock)\u0006;
				break;
			case 12:
				\u0006\u2003 = (TextBlock)\u0006;
				break;
			case 13:
				\u000f\u2003 = (TextBlock)\u0006;
				break;
			case 14:
				\u0002\u2003 = (TextBlock)\u0006;
				break;
			case 15:
				\u0008\u2003 = (TextBlock)\u0006;
				break;
			case 16:
				\u0005\u2003 = (StackPanel)\u0006;
				break;
			case 17:
				\u000e\u2003 = (TextBlock)\u0006;
				break;
			case 18:
				\u0003\u2004 = (TextBlock)\u0006;
				break;
			case 19:
				\u0006\u2004 = (TextBlock)\u0006;
				break;
			case 20:
				\u000f\u2004 = (TextBlock)\u0006;
				break;
			case 21:
				\u0002\u2004 = (TextBlock)\u0006;
				break;
			case 22:
				\u0008\u2004 = (TextBlock)\u0006;
				break;
			case 23:
				\u0005\u2004 = (TextBlock)\u0006;
				break;
			case 24:
				\u000e\u2004 = (TextBlock)\u0006;
				break;
			case 25:
				\u0003\u2000 = (TextBlock)\u0006;
				break;
			case 26:
				\u0006\u2000 = (StackPanel)\u0006;
				break;
			case 27:
				\u000f\u2000 = (TextBlock)\u0006;
				break;
			case 28:
				\u0002\u2000 = (TextBlock)\u0006;
				break;
			case 29:
				\u0008\u2000 = (TextBlock)\u0006;
				break;
			case 30:
				\u0005\u2000 = (TextBlock)\u0006;
				break;
			case 31:
				\u000e\u2000 = (TextBlock)\u0006;
				break;
			case 32:
				\u0003\u2007 = (TextBlock)\u0006;
				break;
			case 33:
				\u0006\u2007 = (TextBlock)\u0006;
				break;
			case 34:
				\u000f\u2007 = (TextBlock)\u0006;
				break;
			case 35:
				\u0002\u2007 = (TextBlock)\u0006;
				break;
			case 36:
				\u0008\u2007 = (Grid)\u0006;
				break;
			case 37:
				\u0005\u2007 = (StackPanel)\u0006;
				break;
			case 38:
				\u000e\u2007 = (StackPanel)\u0006;
				break;
			case 39:
				\u0003\u2005 = (Rectangle)\u0006;
				break;
			case 40:
				\u0006\u2005 = (Rectangle)\u0006;
				break;
			case 41:
				\u000f\u2005 = (Rectangle)\u0006;
				break;
			case 42:
				\u0002\u2005 = (Rectangle)\u0006;
				break;
			case 43:
				\u0008\u2005 = (Rectangle)\u0006;
				break;
			case 44:
				\u0005\u2005 = (Rectangle)\u0006;
				break;
			case 45:
				\u000e\u2005 = (Rectangle)\u0006;
				break;
			case 46:
				\u0003\u200b = (Rectangle)\u0006;
				break;
			case 47:
				\u0006\u200b = (Rectangle)\u0006;
				break;
			case 48:
				\u000f\u200b = (Rectangle)\u0006;
				break;
			case 49:
				\u0002\u200b = (Rectangle)\u0006;
				break;
			case 50:
				\u0008\u200b = (Rectangle)\u0006;
				break;
			case 51:
				\u0005\u200b = (Rectangle)\u0006;
				break;
			case 52:
				\u000e\u200b = (Rectangle)\u0006;
				break;
			case 53:
				\u0003\u200a = (Rectangle)\u0006;
				break;
			case 54:
				\u0006\u200a = (Rectangle)\u0006;
				break;
			case 55:
				\u000f\u200a = (Rectangle)\u0006;
				break;
			case 56:
				\u0002\u200a = (Rectangle)\u0006;
				break;
			case 57:
				\u0008\u200a = (Rectangle)\u0006;
				break;
			case 58:
				\u0005\u200a = (Rectangle)\u0006;
				break;
			case 59:
				\u000e\u200a = (Rectangle)\u0006;
				break;
			case 60:
				\u0003\u2006 = (Rectangle)\u0006;
				break;
			case 61:
				\u0006\u2006 = (Rectangle)\u0006;
				break;
			case 62:
				\u000f\u2006 = (Rectangle)\u0006;
				break;
			case 63:
				\u0002\u2006 = (Rectangle)\u0006;
				break;
			case 64:
				\u0008\u2006 = (Rectangle)\u0006;
				break;
			case 65:
				\u0005\u2006 = (Rectangle)\u0006;
				break;
			case 66:
				\u000e\u2006 = (Rectangle)\u0006;
				break;
			case 67:
				\u0003\u2008 = (Rectangle)\u0006;
				break;
			case 68:
				\u0006\u2008 = (Rectangle)\u0006;
				break;
			case 69:
				\u000f\u2008 = (Rectangle)\u0006;
				break;
			case 70:
				\u0002\u2008 = (Rectangle)\u0006;
				break;
			case 71:
				\u0008\u2008 = (Rectangle)\u0006;
				break;
			case 72:
				\u0005\u2008 = (Rectangle)\u0006;
				break;
			case 73:
				\u000e\u2008 = (Rectangle)\u0006;
				break;
			case 74:
				\u0003\u2002\u2009 = (Rectangle)\u0006;
				break;
			case 75:
				\u0006\u2002\u2009 = (Rectangle)\u0006;
				break;
			case 76:
				\u000f\u2002\u2009 = (Rectangle)\u0006;
				break;
			case 77:
				\u0002\u2002\u2009 = (Rectangle)\u0006;
				break;
			case 78:
				\u0008\u2002\u2009 = (Rectangle)\u0006;
				break;
			case 79:
				\u0005\u2002\u2009 = (Rectangle)\u0006;
				break;
			case 80:
				\u000e\u2002\u2009 = (Rectangle)\u0006;
				break;
			case 81:
				\u0003\u2001\u2009 = (Rectangle)\u0006;
				break;
			case 82:
				\u0006\u2001\u2009 = (Rectangle)\u0006;
				break;
			case 83:
				\u000f\u2001\u2009 = (Rectangle)\u0006;
				break;
			case 84:
				\u0002\u2001\u2009 = (Rectangle)\u0006;
				break;
			case 85:
				\u0008\u2001\u2009 = (Rectangle)\u0006;
				break;
			case 86:
				\u0005\u2001\u2009 = (Rectangle)\u0006;
				break;
			case 87:
				\u000e\u2001\u2009 = (Rectangle)\u0006;
				break;
			case 88:
				\u0003\u2009\u2009 = (Rectangle)\u0006;
				break;
			case 89:
				\u0006\u2009\u2009 = (Rectangle)\u0006;
				break;
			case 90:
				\u000f\u2009\u2009 = (Rectangle)\u0006;
				break;
			case 91:
				\u0002\u2009\u2009 = (Rectangle)\u0006;
				break;
			case 92:
				\u0008\u2009\u2009 = (Rectangle)\u0006;
				break;
			case 93:
				\u0005\u2009\u2009 = (Rectangle)\u0006;
				break;
			case 94:
				\u000e\u2009\u2009 = (Rectangle)\u0006;
				break;
			case 95:
				\u0003\u2003\u2009 = (Rectangle)\u0006;
				break;
			case 96:
				\u0006\u2003\u2009 = (Rectangle)\u0006;
				break;
			case 97:
				\u000f\u2003\u2009 = (Rectangle)\u0006;
				break;
			case 98:
				\u0002\u2003\u2009 = (Rectangle)\u0006;
				break;
			case 99:
				\u0008\u2003\u2009 = (Rectangle)\u0006;
				break;
			case 100:
				\u0005\u2003\u2009 = (Rectangle)\u0006;
				break;
			case 101:
				\u000e\u2003\u2009 = (Rectangle)\u0006;
				break;
			case 102:
				\u0003\u2004\u2009 = (Rectangle)\u0006;
				break;
			case 103:
				\u0006\u2004\u2009 = (Rectangle)\u0006;
				break;
			case 104:
				\u000f\u2004\u2009 = (Rectangle)\u0006;
				break;
			case 105:
				\u0002\u2004\u2009 = (StackPanel)\u0006;
				break;
			case 106:
				\u0008\u2004\u2009 = (Rectangle)\u0006;
				break;
			case 107:
				\u0005\u2004\u2009 = (Rectangle)\u0006;
				break;
			case 108:
				\u000e\u2004\u2009 = (Rectangle)\u0006;
				break;
			case 109:
				\u0003\u2000\u2009 = (Rectangle)\u0006;
				break;
			case 110:
				\u0006\u2000\u2009 = (Rectangle)\u0006;
				break;
			case 111:
				\u000f\u2000\u2009 = (Rectangle)\u0006;
				break;
			case 112:
				\u0002\u2000\u2009 = (Rectangle)\u0006;
				break;
			case 113:
				\u0008\u2000\u2009 = (Rectangle)\u0006;
				break;
			case 114:
				\u0005\u2000\u2009 = (Rectangle)\u0006;
				break;
			case 115:
				\u000e\u2000\u2009 = (Rectangle)\u0006;
				break;
			case 116:
				\u0003\u2007\u2009 = (Rectangle)\u0006;
				break;
			case 117:
				\u0006\u2007\u2009 = (Rectangle)\u0006;
				break;
			case 118:
				\u000f\u2007\u2009 = (Rectangle)\u0006;
				break;
			case 119:
				\u0002\u2007\u2009 = (Rectangle)\u0006;
				break;
			case 120:
				\u0008\u2007\u2009 = (Rectangle)\u0006;
				break;
			case 121:
				\u0005\u2007\u2009 = (Rectangle)\u0006;
				break;
			case 122:
				\u000e\u2007\u2009 = (Rectangle)\u0006;
				break;
			case 123:
				\u0003\u2005\u2009 = (Rectangle)\u0006;
				break;
			case 124:
				\u0006\u2005\u2009 = (Rectangle)\u0006;
				break;
			case 125:
				\u000f\u2005\u2009 = (Rectangle)\u0006;
				break;
			case 126:
				\u0002\u2005\u2009 = (Rectangle)\u0006;
				break;
			case 127:
				\u0008\u2005\u2009 = (Rectangle)\u0006;
				break;
			case 128:
				\u0005\u2005\u2009 = (Rectangle)\u0006;
				break;
			case 129:
				\u000e\u2005\u2009 = (Rectangle)\u0006;
				break;
			case 130:
				\u0003\u200b\u2009 = (Rectangle)\u0006;
				break;
			case 131:
				\u0006\u200b\u2009 = (Rectangle)\u0006;
				break;
			case 132:
				\u000f\u200b\u2009 = (Rectangle)\u0006;
				break;
			case 133:
				\u0002\u200b\u2009 = (Rectangle)\u0006;
				break;
			case 134:
				\u0008\u200b\u2009 = (Rectangle)\u0006;
				break;
			case 135:
				\u0005\u200b\u2009 = (Rectangle)\u0006;
				break;
			case 136:
				\u000e\u200b\u2009 = (Rectangle)\u0006;
				break;
			case 137:
				\u0003\u200a\u2009 = (Rectangle)\u0006;
				break;
			case 138:
				\u0006\u200a\u2009 = (Rectangle)\u0006;
				break;
			case 139:
				\u000f\u200a\u2009 = (Rectangle)\u0006;
				break;
			case 140:
				\u0002\u200a\u2009 = (Rectangle)\u0006;
				break;
			case 141:
				\u0008\u200a\u2009 = (Rectangle)\u0006;
				break;
			case 142:
				\u0005\u200a\u2009 = (Rectangle)\u0006;
				break;
			case 143:
				\u000e\u200a\u2009 = (Rectangle)\u0006;
				break;
			case 144:
				\u0003\u2006\u2009 = (Rectangle)\u0006;
				break;
			case 145:
				\u0006\u2006\u2009 = (Rectangle)\u0006;
				break;
			case 146:
				\u000f\u2006\u2009 = (Rectangle)\u0006;
				break;
			case 147:
				\u0002\u2006\u2009 = (Rectangle)\u0006;
				break;
			case 148:
				\u0008\u2006\u2009 = (Rectangle)\u0006;
				break;
			case 149:
				\u0005\u2006\u2009 = (Rectangle)\u0006;
				break;
			case 150:
				\u000e\u2006\u2009 = (Rectangle)\u0006;
				break;
			case 151:
				\u0003\u2008\u2009 = (Rectangle)\u0006;
				break;
			case 152:
				\u0006\u2008\u2009 = (Rectangle)\u0006;
				break;
			case 153:
				\u000f\u2008\u2009 = (Rectangle)\u0006;
				break;
			case 154:
				\u0002\u2008\u2009 = (Rectangle)\u0006;
				break;
			case 155:
				\u0008\u2008\u2009 = (Rectangle)\u0006;
				break;
			case 156:
				\u0005\u2008\u2009 = (Rectangle)\u0006;
				break;
			case 157:
				\u000e\u2008\u2009 = (Rectangle)\u0006;
				break;
			case 158:
				\u0003\u2002\u2003 = (Rectangle)\u0006;
				break;
			case 159:
				\u0006\u2002\u2003 = (Rectangle)\u0006;
				break;
			case 160:
				\u000f\u2002\u2003 = (Rectangle)\u0006;
				break;
			case 161:
				\u0002\u2002\u2003 = (Rectangle)\u0006;
				break;
			case 162:
				\u0008\u2002\u2003 = (Rectangle)\u0006;
				break;
			case 163:
				\u0005\u2002\u2003 = (Rectangle)\u0006;
				break;
			case 164:
				\u000e\u2002\u2003 = (Rectangle)\u0006;
				break;
			case 165:
				\u0003\u2001\u2003 = (Rectangle)\u0006;
				break;
			case 166:
				\u0006\u2001\u2003 = (Rectangle)\u0006;
				break;
			case 167:
				\u000f\u2001\u2003 = (Rectangle)\u0006;
				break;
			case 168:
				\u0002\u2001\u2003 = (Rectangle)\u0006;
				break;
			case 169:
				\u0008\u2001\u2003 = (Rectangle)\u0006;
				break;
			case 170:
				\u0005\u2001\u2003 = (Rectangle)\u0006;
				break;
			case 171:
				\u000e\u2001\u2003 = (Rectangle)\u0006;
				break;
			case 172:
				\u0003\u2009\u2003 = (StackPanel)\u0006;
				break;
			case 173:
				\u0006\u2009\u2003 = (StackPanel)\u0006;
				break;
			case 174:
				\u000f\u2009\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 175:
				\u0002\u2009\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 176:
				\u0008\u2009\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 177:
				\u0005\u2009\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 178:
				\u000e\u2009\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 179:
				\u0003\u2003\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 180:
				\u0006\u2003\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 181:
				\u000f\u2003\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 182:
				\u0002\u2003\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 183:
				\u0008\u2003\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 184:
				\u0005\u2003\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 185:
				\u000e\u2003\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 186:
				\u0003\u2004\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 187:
				\u0006\u2004\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 188:
				\u000f\u2004\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 189:
				\u0002\u2004\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 190:
				\u0008\u2004\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 191:
				\u0005\u2004\u2003 = (Grid)\u0006;
				break;
			case 192:
				\u000e\u2004\u2003 = (Grid)\u0006;
				break;
			case 193:
				\u0003\u2000\u2003 = (Rectangle)\u0006;
				\u0003\u2000\u2003.MouseMove += this.\u0003;
				\u0003\u2000\u2003.MouseLeftButtonUp += this.\u0003;
				\u0003\u2000\u2003.PreviewMouseMove += this.\u0003;
				break;
			case 194:
				\u0006\u2000\u2003 = (Grid)\u0006;
				break;
			case 195:
				\u000f\u2000\u2003 = (Ellipse)\u0006;
				\u000f\u2000\u2003.MouseLeftButtonDown += this.\u0006;
				\u000f\u2000\u2003.MouseEnter += this.\u0006;
				\u000f\u2000\u2003.MouseLeftButtonUp += \u000f;
				break;
			case 196:
				\u0002\u2000\u2003 = (Grid)\u0006;
				break;
			case 197:
				\u0008\u2000\u2003 = (Ellipse)\u0006;
				\u0008\u2000\u2003.MouseLeftButtonDown += this.\u0006;
				\u0008\u2000\u2003.MouseEnter += this.\u0006;
				\u0008\u2000\u2003.MouseLeftButtonUp += \u000f;
				break;
			case 198:
				\u0005\u2000\u2003 = (Grid)\u0006;
				break;
			case 199:
				\u000e\u2000\u2003 = (Ellipse)\u0006;
				\u000e\u2000\u2003.MouseLeftButtonDown += this.\u0006;
				\u000e\u2000\u2003.MouseEnter += this.\u0006;
				\u000e\u2000\u2003.MouseLeftButtonUp += \u000f;
				break;
			case 200:
				\u0003\u2007\u2003 = (Grid)\u0006;
				break;
			case 201:
				\u0006\u2007\u2003 = (Ellipse)\u0006;
				\u0006\u2007\u2003.MouseLeftButtonDown += this.\u0006;
				\u0006\u2007\u2003.MouseEnter += this.\u0006;
				\u0006\u2007\u2003.MouseLeftButtonUp += \u000f;
				break;
			case 202:
				\u000f\u2007\u2003 = (Grid)\u0006;
				break;
			case 203:
				\u0002\u2007\u2003 = (Ellipse)\u0006;
				\u0002\u2007\u2003.MouseLeftButtonDown += this.\u0006;
				\u0002\u2007\u2003.MouseEnter += this.\u0006;
				\u0002\u2007\u2003.MouseLeftButtonUp += \u000f;
				break;
			case 204:
				\u0008\u2007\u2003 = (Grid)\u0006;
				break;
			case 205:
				\u0005\u2007\u2003 = (Ellipse)\u0006;
				\u0005\u2007\u2003.MouseLeftButtonDown += this.\u0006;
				\u0005\u2007\u2003.MouseEnter += this.\u0006;
				\u0005\u2007\u2003.MouseLeftButtonUp += \u000f;
				break;
			case 206:
				\u000e\u2007\u2003 = (Grid)\u0006;
				break;
			case 207:
				\u0003\u2005\u2003 = (Ellipse)\u0006;
				\u0003\u2005\u2003.MouseLeftButtonDown += this.\u0006;
				\u0003\u2005\u2003.MouseEnter += this.\u0006;
				\u0003\u2005\u2003.MouseLeftButtonUp += \u000f;
				break;
			case 208:
				\u0006\u2005\u2003 = (Grid)\u0006;
				break;
			case 209:
				\u000f\u2005\u2003 = (Ellipse)\u0006;
				\u000f\u2005\u2003.MouseLeftButtonDown += this.\u0006;
				\u000f\u2005\u2003.MouseEnter += this.\u0006;
				\u000f\u2005\u2003.MouseLeftButtonUp += \u000f;
				break;
			case 210:
				\u0002\u2005\u2003 = (Grid)\u0006;
				break;
			case 211:
				\u0008\u2005\u2003 = (Ellipse)\u0006;
				\u0008\u2005\u2003.MouseLeftButtonDown += this.\u0006;
				\u0008\u2005\u2003.MouseEnter += this.\u0006;
				\u0008\u2005\u2003.MouseLeftButtonUp += \u000f;
				break;
			case 212:
				\u0005\u2005\u2003 = (Grid)\u0006;
				break;
			case 213:
				\u000e\u2005\u2003 = (Ellipse)\u0006;
				\u000e\u2005\u2003.MouseLeftButtonDown += this.\u0006;
				\u000e\u2005\u2003.MouseEnter += this.\u0006;
				\u000e\u2005\u2003.MouseLeftButtonUp += \u000f;
				break;
			case 214:
				\u0003\u200b\u2003 = (TextBlock)\u0006;
				break;
			case 215:
				\u0006\u200b\u2003 = (System.Windows.Controls.ComboBox)\u0006;
				\u0006\u200b\u2003.SelectionChanged += this.\u0003;
				break;
			case 216:
				\u000f\u200b\u2003 = (TextBlock)\u0006;
				break;
			case 217:
				\u0002\u200b\u2003 = (System.Windows.Controls.ComboBox)\u0006;
				\u0002\u200b\u2003.SelectionChanged += this.\u0006;
				break;
			case 218:
				\u0008\u200b\u2003 = (StackPanel)\u0006;
				break;
			case 219:
				\u0005\u200b\u2003 = (StackPanel)\u0006;
				break;
			case 220:
				\u000e\u200b\u2003 = (TextBlock)\u0006;
				break;
			case 221:
				\u0003\u200a\u2003 = (TextBlock)\u0006;
				break;
			case 222:
				\u0006\u200a\u2003 = (TextBlock)\u0006;
				break;
			case 223:
				\u000f\u200a\u2003 = (TextBlock)\u0006;
				break;
			default:
				\u0002\u200a\u2003 = true;
				break;
			}
		}

		void IComponentConnector.Connect(int \u0003, object \u0006)
		{
			//ILSpy generated this explicit interface implementation from .override directive in 4qwhgehlclvxzz5pvgf7bmk97bj96j5p   
			this.4qwhgehlclvxzz5pvgf7bmk97bj96j5p\u2009\u2005\u2006\u0003(\u0003, \u0006);
		}
	}
	public sealed class i11lIl111i1I : Window, IComponentConnector
	{
		private struct \u0002
		{
			private int \u0003;

			private int \u0006;

			private int \u000f;

			private \u000e[] \u0002;
		}

		private sealed class \u0003
		{
			public EntryWrittenEventArgs \u0003;

			public i11lIl111i1I \u0006;

			internal void \u0003()
			{
				string[] array = this.\u0003.Entry.Message.Split('^');
				if (array.Length == 1)
				{
					if (array[0] == \u0002\u0005.\u0003(1031812840) && global::\u0008\u2002.\u0006.\u0005\u2001)
					{
						global::\u0008\u2002.\u0008.\u000e\u2002();
						\u0006.\u0008\u2009.\u000f\u2009();
					}
				}
				else if (array[0] == \u0002\u0005.\u0003(1031812838))
				{
					string value = array[1];
					\u0006.\u0003(Convert.ToInt32(value));
				}
				else if (array[0] == \u0002\u0005.\u0003(1031812819))
				{
					string text = array[1];
					\u0006.\u0003(text);
				}
				else if (array[0] == \u0002\u0005.\u0003(1031812804))
				{
					string text2 = array[1];
					\u0006.\u0003(text2, array[2]);
				}
			}
		}

		public struct \u0003\u2002
		{
			public int \u0003;

			public int \u0006;

			[MarshalAs(UnmanagedType.LPStr)]
			public string \u000f;
		}

		private struct \u0005
		{
			public IntPtr \u0003;
		}

		private sealed class \u0006
		{
			public string \u0003;

			public string \u0006;

			public i11lIl111i1I \u000f;

			internal void \u0003()
			{
				string text = this.\u0003;
				string text2 = text;
				if (text2 == \u0002\u0005.\u0003(1031812788))
				{
					if (\u0006 == \u0002\u0005.\u0003(1031811328))
					{
						\u000f.\u0008\u2009.\u0003\u2001();
					}
					else
					{
						\u000f.\u0008\u2009.\u0006\u2001();
					}
				}
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
		private struct \u0006\u2002
		{
			public bool \u0003;

			public int \u0006;

			public char \u000f;

			public IntPtr \u0002;

			public bool \u0008;

			public int \u0005;
		}

		private struct \u0008
		{
			public Guid \u0003;

			public ulong \u0006;

			public ulong \u000f;

			public ulong \u0002;

			public ulong \u0008;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct \u000e
		{
			[FieldOffset(0)]
			public ushort \u0003;

			[FieldOffset(4)]
			public ushort \u0006;

			[FieldOffset(8)]
			public long \u000f;

			[FieldOffset(16)]
			public byte[] \u0002;
		}

		private sealed class \u000f
		{
			public object \u0003;

			internal void \u0003()
			{
				byte[] array = (byte[])this.\u0003;
				for (int i = 0; i < array.Length; i++)
				{
				}
			}
		}

		public struct COPYDATASTRUCT
		{
			public int dwData;

			public int cbData;

			public uint lpData;
		}

		private static Guid m_\u0003 = new Guid(\u0002\u0005.\u0003(1031812782));

		private static Guid m_\u0006 = new Guid(\u0002\u0005.\u0003(1031812729));

		private static Guid m_\u000f = new Guid(\u0002\u0005.\u0003(1031812692));

		private static Guid m_\u0002 = new Guid(\u0002\u0005.\u0003(1031812655));

		private \u0006\u2002 m_\u0008;

		private \u0005 m_\u0005;

		public readonly BackgroundWorker \u000e = new BackgroundWorker();

		private DispatcherTimer m_\u0003\u2002 = new DispatcherTimer();

		private l1lI1II1iIIIi m_\u0006\u2002 = new l1lI1II1iIIIi();

		private double m_\u000f\u2002 = 1920.0;

		private double m_\u0002\u2002 = 1080.0;

		private double \u0008\u2002 = 1920.0;

		private double \u0005\u2002 = 1080.0;

		private bool \u000e\u2002 = true;

		private int \u0003\u2001;

		private int \u0006\u2001;

		private EventLog \u000f\u2001;

		private bool \u0002\u2001 = false;

		internal i11lIl111i1I \u0008\u2001;

		internal Rectangle \u0005\u2001;

		internal Rectangle \u000e\u2001;

		internal Rectangle \u0003\u2009;

		internal Viewbox \u0006\u2009;

		internal Grid \u000f\u2009;

		internal StackPanel \u0002\u2009;

		internal l1illIiliii11 \u0008\u2009;

		internal l1Ii11l1ii1II \u0005\u2009;

		internal Viewbox \u000e\u2009;

		internal Grid \u0003\u2003;

		internal System.Windows.Shapes.Path \u0006\u2003;

		internal Rectangle \u000f\u2003;

		internal Image \u0002\u2003;

		internal System.Windows.Shapes.Path \u0008\u2003;

		internal Rectangle \u0005\u2003;

		internal Viewbox \u000e\u2003;

		internal Grid \u0003\u2004;

		internal System.Windows.Shapes.Path \u0006\u2004;

		internal System.Windows.Controls.CheckBox \u000f\u2004;

		internal System.Windows.Controls.Button \u0002\u2004;

		internal System.Windows.Controls.Button \u0008\u2004;

		private bool \u0005\u2004;

		public i11lIl111i1I()
		{
			InitializeComponent();
			Thread thread = new Thread((ThreadStart)\u0003);
			thread.SetApartmentState(ApartmentState.STA);
			thread.IsBackground = true;
			thread.Start();
			\u000f\u2001 = new EventLog();
			try
			{
				if (EventLog.Exists(\u0002\u0005.\u0003(1031813656)))
				{
					\u000f\u2001.Source = \u0002\u0005.\u0003(1031813694);
					\u000f\u2001.Log = \u0002\u0005.\u0003(1031813656);
				}
				else
				{
					\u0002\u2001 = true;
				}
			}
			catch (Exception ex)
			{
				\u000f\u2001.WriteEntry(ex.Message);
				\u0002\u2001 = true;
			}
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "RtlMoveMemory", SetLastError = true)]
		private static extern bool \u0003(byte[] \u0003, uint \u0006, int \u000f);

		[DllImport("User32.dll", EntryPoint = "SendMessage")]
		private static extern int \u0003(int \u0003, int \u0006, int \u000f, ref COPYDATASTRUCT \u0002);

		[DllImport("user32.dll", EntryPoint = "ShowWindow")]
		private static extern int \u0003(int \u0003, int \u0006);

		[DllImport("user32.dll", EntryPoint = "FindWindow")]
		public static extern int \u0003(string \u0003, string \u0006);

		[DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
		public static extern IntPtr \u0003(IntPtr \u0003);

		[DllImport("InsydeDCHU.dll", EntryPoint = "GetDCHU_Data_Integer")]
		public static extern int \u0003(int \u0003, ref int \u0006);

		[DllImport("InsydeDCHU.dll", EntryPoint = "SetDCHU_Data")]
		public static extern int \u0003(int \u0003, byte[] \u0006);

		[DllImport("InsydeDCHU.dll", EntryPoint = "GetDCHU_Data_Buffer")]
		public static extern int \u0003(int \u0003, ref byte \u0006);

		[DllImport("InsydeDCHU.dll", EntryPoint = "ReadAppSettings")]
		public static extern int \u0003(int \u0003, int \u0006, int \u000f, ref byte \u0002);

		[DllImport("InsydeDCHU.dll", EntryPoint = "WriteAppSettings")]
		public static extern int \u0006(int \u0003, int \u0006, int \u000f, ref byte \u0002);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateFile", SetLastError = true)]
		private static extern IntPtr \u0003(string \u0003, FileAccess \u0006, FileShare \u000f, IntPtr \u0002, FileMode \u0008, FileOptions \u0005, IntPtr \u000e);

		[DllImport("kernel32.dll", EntryPoint = "DeviceIoControl", SetLastError = true)]
		private static extern bool \u0003(IntPtr \u0003, uint \u0006, ref \u0008 \u000f, int \u0002, ref \u0002 \u0008, uint \u0005, ref uint \u000e, IntPtr \u0003\u2002);

		[DllImport("kernel32.dll", EntryPoint = "DeviceIoControl", SetLastError = true)]
		private static extern int \u0003(IntPtr \u0003, uint \u0006, ref \u0005 \u000f, int \u0002, byte[] \u0008, uint \u0005, ref uint \u000e, IntPtr \u0003\u2002);

		[DllImport("kernel32.dll", EntryPoint = "DeviceIoControl", SetLastError = true)]
		private static extern int \u0003(IntPtr \u0003, uint \u0006, int \u000f, int \u0002, ref long \u0008, uint \u0005, ref uint \u000e, IntPtr \u0003\u2002);

		[DllImport("kernel32.dll", EntryPoint = "WaitForSingleObject", SetLastError = true)]
		private static extern uint \u0003(IntPtr \u0003, uint \u0006);

		[DllImport("kernel32.dll", EntryPoint = "ResetEvent")]
		public static extern bool \u0003(IntPtr \u0003);

		[DllImport("kernel32.dll", EntryPoint = "CloseHandle")]
		private static extern void \u0003(IntPtr \u0003);

		[DllImport("kernel32.dll", EntryPoint = "CreateEvent")]
		private static extern IntPtr \u0003(IntPtr \u0003, bool \u0006, bool \u000f, string \u0002);

		private void \u0003()
		{
			base.Dispatcher.BeginInvoke(new Action(\u000f\u2002));
			Dispatcher.Run();
		}

		private void \u0003(Window \u0003)
		{
			if (\u0003.Dispatcher.CheckAccess())
			{
				\u0003.Close();
			}
			else
			{
				\u0003.Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(\u0003.Close));
			}
		}

		private void \u0003(object \u0003, RoutedEventArgs \u0006)
		{
			try
			{
				this.\u0006();
				base.Visibility = Visibility.Hidden;
				global::\u0008\u2002.\u0003 = new \u0006\u2001();
				global::\u0008\u2002.\u0002\u2002 = new \u0003\u2001();
				global::\u0008\u2002.\u000f\u2002 = new \u0002\u2001();
				\u0002();
				global::\u0008\u2002.\u0006 = new \u000f\u2001();
				global::\u0008\u2002.\u0002 = new \u0005\u2002();
				global::\u0008\u2002.\u0008 = new \u0002\u2002();
				global::\u0008\u2002.\u0005 = new \u0006\u0005();
				\u000f\u2001.EntryWritten += this.\u0003;
				\u000f\u2001.EnableRaisingEvents = true;
				base.Dispatcher.BeginInvoke(new Action(\u0002\u2002));
			}
			catch
			{
				\u0002\u2001 = true;
				\u0005\u2009.\u0003(0, 10);
				base.Visibility = Visibility.Visible;
			}
		}

		private void \u0006()
		{
			string processName = Process.GetCurrentProcess().ProcessName;
			if (Process.GetProcessesByName(processName).GetUpperBound(0) > 0)
			{
				Environment.Exit(Environment.ExitCode);
			}
		}

		private void \u0003(object \u0003, EntryWrittenEventArgs \u0006)
		{
			\u0003 obj = new \u0003();
			obj.\u0003 = \u0006;
			obj.\u0006 = this;
			base.Dispatcher.BeginInvoke(new Action(obj.\u0003));
		}

		private void \u0003(int \u0003)
		{
			switch (\u0003)
			{
			case 39:
				\u0008\u2009.\u0008\u2000\u2009.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031813114));
				break;
			case 40:
				\u0008\u2009.\u0008\u2000\u2009.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031813110));
				break;
			case 41:
				\u0008\u2009.\u0008\u2000\u2009.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031813080));
				break;
			case 42:
				\u0008\u2009.\u0008\u2000\u2009.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031813078));
				break;
			case 43:
				\u0008\u2009.\u0008\u2000\u2009.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031813063));
				break;
			case 94:
			case 95:
				global::\u0008\u2002.\u0008.\u000e\u2002();
				\u0008\u2009.\u000f\u2009();
				break;
			case 106:
				\u0008\u2009.\u000e\u200a\u2009.IsChecked = true;
				break;
			case 107:
				\u0008\u2009.\u0002\u2006\u2009.IsChecked = true;
				break;
			case 112:
				\u0008\u2009.\u0003\u200a\u2009.IsChecked = true;
				break;
			case 143:
				\u0008\u2009.\u0008\u200a\u2009.IsChecked = true;
				break;
			}
		}

		private void \u0003(string \u0003)
		{
			if (!(\u0003 == \u0002\u0005.\u0003(1031813034)))
			{
				if (\u0003 == \u0002\u0005.\u0003(1031813028))
				{
					\u0008\u2009.\u000e\u2002();
				}
			}
			else
			{
				\u0008\u2009.\u0008();
				\u0008\u2009.\u000e\u2002();
				\u0008\u2009.\u0006\u2009\u2009.\u0003();
				\u0008\u2009.\u0005\u2001\u2009.\u0003();
				\u0008\u2009.\u000f\u2001\u2009.\u0003();
			}
		}

		private void \u0003(string \u0003, string \u0006)
		{
			\u0006 obj = new \u0006();
			obj.\u0003 = \u0003;
			obj.\u0006 = \u0006;
			obj.\u000f = this;
			base.Dispatcher.BeginInvoke(new Action(obj.\u0003));
		}

		private void \u0003(object \u0003, EventArgs \u0006)
		{
			\u000e();
			base.Visibility = Visibility.Visible;
			base.Topmost = false;
			if (\u0002\u2001)
			{
				this.\u0003(this.m_\u0006\u2002);
				\u0005\u2009.\u0003(0, 10);
				return;
			}
			Version version = System.Windows.Application.ResourceAssembly.GetName().Version;
			global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031813022) + version);
			global::\u0008\u2002.\u0008.\u0003\u2001 = version.Major;
			global::\u0008\u2002.\u0008.\u0006\u2001 = version.Minor;
			global::\u0008\u2002.\u0008.\u0002\u2001 = version.Build;
			global::\u0008\u2002.\u0008.\u000f\u2001 = version.Revision;
			if (!\u000e\u2002)
			{
				\u0005\u2009.\u0003(0, 10);
				return;
			}
			global::\u0008\u2002.\u0002.\u0003();
			if (!global::\u0008\u2002.\u0006.\u000f\u2001)
			{
				\u0005\u2009.\u0003(0, 10);
				return;
			}
			byte[] array = new byte[4];
			array = global::\u0008\u2002.\u0002.\u0003(0, 12, 4);
			if (array[0] == 0)
			{
				global::\u0008\u2002.\u0006.\u0003\u2009 = true;
			}
			if (version.Major != array[0] || version.Minor != array[1] || version.Build != array[2] || version.Revision != array[3])
			{
				byte[] array2 = new byte[4]
				{
					(byte)global::\u0008\u2002.\u0008.\u0003\u2001,
					(byte)global::\u0008\u2002.\u0008.\u0006\u2001,
					(byte)global::\u0008\u2002.\u0008.\u0002\u2001,
					(byte)global::\u0008\u2002.\u0008.\u000f\u2001
				};
				global::\u0008\u2002.\u0002.\u0003(0, 12, 4, array2);
			}
			\u0005();
			\u000f();
		}

		private void \u000f()
		{
			byte[] array = new byte[256];
			array = global::\u0008\u2002.\u0002.\u0003(17);
			switch (array[2])
			{
			}
			byte b = array[3];
			byte b2 = array[4];
			byte b3 = array[5];
			Color color = ((b != 0 || b2 != 0 || b3 != 0) ? Color.FromArgb(byte.MaxValue, b, b2, b3) : Color.FromArgb(byte.MaxValue, byte.MaxValue, 0, 210));
			Brush fill = new SolidColorBrush(color);
			\u000f\u2003.Fill = fill;
			byte b4 = array[6];
			byte b5 = array[7];
			byte b6 = array[8];
			color = ((b4 != 0 || b5 != 0 || b6 != 0) ? Color.FromArgb(byte.MaxValue, b4, b5, b6) : Color.FromArgb(byte.MaxValue, 0, 168, byte.MaxValue));
			fill = new SolidColorBrush(color);
			\u0008\u2009.\u000e\u2000.Foreground = fill;
			byte b7 = array[6];
			byte b8 = array[7];
			byte b9 = array[8];
			color = ((b7 != 0 || b8 != 0 || b9 != 0) ? Color.FromArgb(byte.MaxValue, b7, b8, b9) : Color.FromArgb(byte.MaxValue, byte.MaxValue, 0, 210));
			fill = new SolidColorBrush(color);
			\u0008\u2009.\u0008\u2007.Fill = fill;
		}

		private void \u0002()
		{
		}

		private void \u0008()
		{
		}

		protected override void OnSourceInitialized(EventArgs \u0003)
		{
		}

		private IntPtr \u0003(IntPtr \u0003, int \u0006, IntPtr \u000f, IntPtr \u0002, ref bool \u0008)
		{
			\u0003\u2001 = \u000f.ToInt32();
			if (\u0006 == 74)
			{
				default(COPYDATASTRUCT).GetType();
				COPYDATASTRUCT cOPYDATASTRUCT = (COPYDATASTRUCT)Marshal.PtrToStructure(\u0002, typeof(COPYDATASTRUCT));
				byte[] array = new byte[cOPYDATASTRUCT.cbData];
				i11lIl111i1I.\u0003(array, cOPYDATASTRUCT.lpData, cOPYDATASTRUCT.cbData);
				\u0006\u2001 = cOPYDATASTRUCT.dwData;
				Thread thread = new Thread((ParameterizedThreadStart)this.\u0003);
				thread.Start(array);
			}
			return IntPtr.Zero;
		}

		public void \u0003(object \u0003)
		{
			byte[] array = (byte[])\u0003;
			if (\u0003\u2001 == 1)
			{
				return;
			}
			if (\u0003\u2001 == 2)
			{
				if (array.Length < 5)
				{
					this.\u0003(\u0006\u2001, (object)array);
				}
				else
				{
					\u0006(array);
				}
			}
			else if (\u0003\u2001 == 3)
			{
				global::\u0008\u2002.\u0003\u2002 = array;
				global::\u0008\u2002.\u0006\u2002.\u0003 = Color.FromRgb(global::\u0008\u2002.\u0003\u2002[30], global::\u0008\u2002.\u0003\u2002[31], global::\u0008\u2002.\u0003\u2002[32]);
				global::\u0008\u2002.\u0006\u2002.\u0006 = Color.FromRgb(global::\u0008\u2002.\u0003\u2002[1], global::\u0008\u2002.\u0003\u2002[2], global::\u0008\u2002.\u0003\u2002[3]);
				global::\u0008\u2002.\u0006\u2002.\u000f = Color.FromRgb(global::\u0008\u2002.\u0003\u2002[4], global::\u0008\u2002.\u0003\u2002[5], global::\u0008\u2002.\u0003\u2002[6]);
				global::\u0008\u2002.\u0006\u2002.\u0002 = Color.FromRgb(global::\u0008\u2002.\u0003\u2002[7], global::\u0008\u2002.\u0003\u2002[8], global::\u0008\u2002.\u0003\u2002[9]);
				global::\u0008\u2002.\u0006\u2002.\u0008 = Color.FromRgb(global::\u0008\u2002.\u0003\u2002[10], global::\u0008\u2002.\u0003\u2002[11], global::\u0008\u2002.\u0003\u2002[12]);
				global::\u0008\u2002.\u0006\u2002.\u0005 = Color.FromRgb(global::\u0008\u2002.\u0003\u2002[13], global::\u0008\u2002.\u0003\u2002[14], global::\u0008\u2002.\u0003\u2002[15]);
				global::\u0008\u2002.\u0006\u2002.\u000e = Color.FromRgb(global::\u0008\u2002.\u0003\u2002[16], global::\u0008\u2002.\u0003\u2002[17], global::\u0008\u2002.\u0003\u2002[18]);
				global::\u0008\u2002.\u0006\u2002.\u0002\u2002 = global::\u0008\u2002.\u0003\u2002[19];
				global::\u0008\u2002.\u0006\u2002.\u0008\u2002 = global::\u0008\u2002.\u0003\u2002[20];
				global::\u0008\u2002.\u0006\u2002.\u0005\u2002 = global::\u0008\u2002.\u0003\u2002[21];
				global::\u0008\u2002.\u0006\u2002.\u000e\u2002 = global::\u0008\u2002.\u0003\u2002[22];
				global::\u0008\u2002.\u0006\u2002.\u0006\u2001 = global::\u0008\u2002.\u0003\u2002[23];
				global::\u0008\u2002.\u0006\u2002.\u000f\u2001 = global::\u0008\u2002.\u0003\u2002[24];
				global::\u0008\u2002.\u0006\u2002.\u0006\u2002 = global::\u0008\u2002.\u0003\u2002[25];
				global::\u0008\u2002.\u0006\u2002.\u0003\u2002 = global::\u0008\u2002.\u0003\u2002[26];
				global::\u0008\u2002.\u0006\u2002.\u000f\u2002 = global::\u0008\u2002.\u0003\u2002[27];
				global::\u0008\u2002.\u0006\u2002.\u0002\u2001 = global::\u0008\u2002.\u0003\u2002[28];
			}
		}

		public void \u0006(object \u0003)
		{
			\u000f obj = new \u000f();
			obj.\u0003 = \u0003;
			base.Dispatcher.BeginInvoke(new Action(obj.\u0003));
		}

		private void \u0003(int \u0003, object \u0006)
		{
			byte[] array = (byte[])\u0006;
			this.\u0003((\u0003 << 8) | array[0], array[1], array[2], array[3]);
		}

		private void \u0003(int \u0003, byte \u0006, byte \u000f, byte \u0002)
		{
		}

		private void \u0005()
		{
			\u0008\u2009.\u0003();
			Thread.Sleep(1);
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		private void \u0006(object \u0003, RoutedEventArgs \u0006)
		{
			Close();
		}

		private void \u000f(object \u0003, RoutedEventArgs \u0006)
		{
			base.WindowState = WindowState.Minimized;
		}

		private void \u0002(object \u0003, RoutedEventArgs \u0006)
		{
			if (\u000f\u2004.IsChecked == true)
			{
				\u0006\u2002();
			}
			else
			{
				\u0003\u2002();
			}
		}

		private void \u000e()
		{
			double primaryScreenWidth = SystemParameters.PrimaryScreenWidth;
			double primaryScreenHeight = SystemParameters.PrimaryScreenHeight;
			if (global::\u0008\u2002.\u0003.\u0003() != primaryScreenWidth)
			{
				global::\u0008\u2002.\u0003.\u0003(primaryScreenWidth);
				global::\u0008\u2002.\u0003.\u0002(0);
			}
			if (global::\u0008\u2002.\u0003.\u0006() != primaryScreenHeight)
			{
				global::\u0008\u2002.\u0003.\u0006(primaryScreenHeight);
				global::\u0008\u2002.\u0003.\u0002(0);
			}
			float num = 2560f / (float)primaryScreenWidth;
			float num2 = 1440f / (float)primaryScreenHeight;
			\u0008\u2002 = Math.Round(this.m_\u000f\u2002 / (double)num, 0);
			\u0005\u2002 = Math.Round(this.m_\u0002\u2002 / (double)num2, 0);
			base.MaxWidth = \u0008\u2002;
			base.MaxHeight = \u0005\u2002;
			\u0006\u2009.Width = base.MaxWidth;
			\u0006\u2009.Height = base.MaxHeight;
			\u0005\u2001.Width = base.MaxWidth;
			\u000e\u2001.Width = base.MaxWidth;
			\u0003\u2009.Width = base.MaxWidth;
			\u0005\u2001.Height = base.MaxHeight;
			\u000e\u2001.Height = base.MaxHeight;
			\u0003\u2009.Height = base.MaxHeight;
			if (global::\u0008\u2002.\u0003.\u0002() == 0)
			{
				base.Width = base.MaxWidth;
				base.Height = base.MaxHeight;
				global::\u0008\u2002.\u0003.\u0002((int)base.Width);
				global::\u0008\u2002.\u0003.\u0008((int)base.Height);
			}
			else
			{
				base.Width = global::\u0008\u2002.\u0003.\u0002();
				base.Height = global::\u0008\u2002.\u0003.\u0008();
			}
			\u000e\u2003.Width = 500f / num;
			\u000e\u2003.Height = 77f / num2;
			base.MinWidth = \u000e\u2003.Width;
			base.MinHeight = \u000e\u2003.Height + 200.0;
			\u000f\u2004.IsChecked = false;
			\u000e\u2009.Width = 1905f / num;
			\u000e\u2009.Height = 81f / num2;
			if (global::\u0008\u2002.\u0003.\u0003() == 1)
			{
				base.MaxWidth = double.PositiveInfinity;
				base.MaxHeight = double.PositiveInfinity;
				base.Width = double.NaN;
				base.Height = double.NaN;
				base.WindowState = WindowState.Maximized;
				\u000f\u2004.IsChecked = true;
				\u0006\u2009.Width = base.Width;
				\u0006\u2009.Height = base.Height;
				\u0005\u2001.Width = primaryScreenWidth;
				\u000e\u2001.Width = primaryScreenWidth;
				\u0003\u2009.Width = primaryScreenWidth;
				\u0005\u2001.Height = primaryScreenHeight;
				\u000e\u2001.Height = primaryScreenHeight;
				\u0003\u2009.Height = primaryScreenHeight;
			}
			else
			{
				base.Topmost = false;
			}
			\u0008\u2001.Left = (primaryScreenWidth - base.Width) / 2.0;
			\u0008\u2001.Top = (primaryScreenHeight - base.Height) / 2.0;
			if (\u0008\u2001.Left < 0.0)
			{
				\u0008\u2001.Left = 0.0;
			}
			if (\u0008\u2001.Top < 0.0)
			{
				\u0008\u2001.Top = 0.0;
			}
		}

		private void \u0003\u2002()
		{
			double primaryScreenWidth = SystemParameters.PrimaryScreenWidth;
			double primaryScreenHeight = SystemParameters.PrimaryScreenHeight;
			float num = 2560f / (float)primaryScreenWidth;
			float num2 = 1440f / (float)primaryScreenHeight;
			base.WindowState = WindowState.Normal;
			base.MaxWidth = \u0008\u2002;
			base.MaxHeight = \u0005\u2002;
			\u0006\u2009.Width = base.MaxWidth;
			\u0006\u2009.Height = base.MaxHeight;
			\u0005\u2001.Width = base.MaxWidth;
			\u000e\u2001.Width = base.MaxWidth;
			\u0003\u2009.Width = base.MaxWidth;
			\u0005\u2001.Height = base.MaxHeight;
			\u000e\u2001.Height = base.MaxHeight;
			\u0003\u2009.Height = base.MaxHeight;
			base.Width = global::\u0008\u2002.\u0003.\u0002();
			base.Height = global::\u0008\u2002.\u0003.\u0008();
			\u000e\u2003.Width = 500f / num;
			\u000e\u2003.Height = 77f / num2;
			base.MinWidth = \u000e\u2003.Width;
			base.MinHeight = \u000e\u2003.Height + 200.0;
			if (\u0008\u2001.Left < 0.0)
			{
				\u0008\u2001.Left = 0.0;
			}
			if (\u0008\u2001.Top < 0.0)
			{
				\u0008\u2001.Top = 0.0;
			}
			\u000f\u2004.IsChecked = false;
			global::\u0008\u2002.\u0003.\u0003(0);
			base.Topmost = false;
		}

		private void \u0006\u2002()
		{
			base.Visibility = Visibility.Hidden;
			base.WindowState = WindowState.Normal;
			base.MaxWidth = double.PositiveInfinity;
			base.MaxHeight = double.PositiveInfinity;
			base.WindowState = WindowState.Maximized;
			base.Visibility = Visibility.Visible;
			\u0006\u2009.Width = base.Width;
			\u0006\u2009.Height = base.Height;
			\u0005\u2001.Width = base.Width;
			\u000e\u2001.Width = base.Width;
			\u0003\u2009.Width = base.Width;
			\u0005\u2001.Height = base.Height;
			\u000e\u2001.Height = base.Height;
			\u0003\u2009.Height = base.Height;
			\u000f\u2004.IsChecked = true;
			global::\u0008\u2002.\u0003.\u0003(1);
		}

		private void \u0003(object \u0003, SizeChangedEventArgs \u0006)
		{
			if (\u000f\u2004.IsChecked != true)
			{
				if (base.WindowState == WindowState.Maximized)
				{
					\u0006\u2002();
					return;
				}
				global::\u0008\u2002.\u0003.\u0002((int)base.Width);
				global::\u0008\u2002.\u0003.\u0008((int)base.Height);
			}
		}

		private void \u0006(object \u0003, EventArgs \u0006)
		{
			if (base.WindowState == WindowState.Maximized)
			{
				global::\u0008\u2002.\u0003.\u0003(1);
			}
			else
			{
				global::\u0008\u2002.\u0003.\u0003(0);
			}
		}

		private void \u0003(object \u0003, MouseButtonEventArgs \u0006)
		{
			if (base.Width >= \u0008\u2002 - 1.0)
			{
				base.Width -= 0.5;
				base.Height -= 0.5;
			}
			DragMove();
		}

		private void \u0003(object \u0003, System.Windows.Input.MouseEventArgs \u0006)
		{
			if (\u0006.LeftButton == MouseButtonState.Pressed && base.WindowState == WindowState.Maximized)
			{
				\u0003\u2002();
			}
		}

		private void \u000f(object \u0003, EventArgs \u0006)
		{
			base.Dispatcher.InvokeShutdown();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!\u0005\u2004)
			{
				\u0005\u2004 = true;
				Uri resourceLocator = new Uri(\u0002\u0005.\u0003(1031813001), UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate \u0003(Type \u0003, string \u0006)
		{
			return Delegate.CreateDelegate(\u0003, this, \u0006);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		private void jhj8n428xvrarumjjgb6lnjsudjagrwq\u2009\u2005\u2006\u0006(int \u0003, object \u0006)
		{
			switch (\u0003)
			{
			case 1:
				\u0008\u2001 = (i11lIl111i1I)\u0006;
				\u0008\u2001.Loaded += this.\u0003;
				\u0008\u2001.ContentRendered += this.\u0003;
				\u0008\u2001.Closed += \u000f;
				\u0008\u2001.SizeChanged += this.\u0003;
				\u0008\u2001.StateChanged += this.\u0006;
				break;
			case 2:
				\u0005\u2001 = (Rectangle)\u0006;
				break;
			case 3:
				\u000e\u2001 = (Rectangle)\u0006;
				break;
			case 4:
				\u0003\u2009 = (Rectangle)\u0006;
				break;
			case 5:
				\u0006\u2009 = (Viewbox)\u0006;
				break;
			case 6:
				\u000f\u2009 = (Grid)\u0006;
				break;
			case 7:
				\u0002\u2009 = (StackPanel)\u0006;
				break;
			case 8:
				\u0008\u2009 = (l1illIiliii11)\u0006;
				break;
			case 9:
				\u0005\u2009 = (l1Ii11l1ii1II)\u0006;
				break;
			case 10:
				\u000e\u2009 = (Viewbox)\u0006;
				break;
			case 11:
				\u0003\u2003 = (Grid)\u0006;
				break;
			case 12:
				\u0006\u2003 = (System.Windows.Shapes.Path)\u0006;
				\u0006\u2003.MouseLeftButtonDown += this.\u0003;
				\u0006\u2003.MouseMove += this.\u0003;
				break;
			case 13:
				\u000f\u2003 = (Rectangle)\u0006;
				break;
			case 14:
				\u0002\u2003 = (Image)\u0006;
				break;
			case 15:
				\u0008\u2003 = (System.Windows.Shapes.Path)\u0006;
				\u0008\u2003.MouseLeftButtonDown += this.\u0003;
				\u0008\u2003.MouseMove += this.\u0003;
				break;
			case 16:
				\u0005\u2003 = (Rectangle)\u0006;
				break;
			case 17:
				\u000e\u2003 = (Viewbox)\u0006;
				break;
			case 18:
				\u0003\u2004 = (Grid)\u0006;
				break;
			case 19:
				\u0006\u2004 = (System.Windows.Shapes.Path)\u0006;
				\u0006\u2004.MouseLeftButtonDown += this.\u0003;
				\u0006\u2004.MouseMove += this.\u0003;
				break;
			case 20:
				\u000f\u2004 = (System.Windows.Controls.CheckBox)\u0006;
				\u000f\u2004.Click += \u0002;
				break;
			case 21:
				\u0002\u2004 = (System.Windows.Controls.Button)\u0006;
				\u0002\u2004.Click += \u000f;
				break;
			case 22:
				\u0008\u2004 = (System.Windows.Controls.Button)\u0006;
				\u0008\u2004.Click += this.\u0006;
				break;
			default:
				\u0005\u2004 = true;
				break;
			}
		}

		void IComponentConnector.Connect(int \u0003, object \u0006)
		{
			//ILSpy generated this explicit interface implementation from .override directive in jhj8n428xvrarumjjgb6lnjsudjagrwq   
			this.jhj8n428xvrarumjjgb6lnjsudjagrwq\u2009\u2005\u2006\u0006(\u0003, \u0006);
		}

		private void \u000f\u2002()
		{
			this.m_\u0006\u2002 = new l1lI1II1iIIIi();
			this.m_\u0006\u2002.Show();
		}

		private void \u0002\u2002()
		{
			\u0003(this.m_\u0006\u2002);
		}
	}
	public sealed class i1iIIiiIl1ll : System.Windows.Application
	{
		private bool m_\u0003;

		protected override void OnStartup(StartupEventArgs \u0003)
		{
			if (\u0003.Args != null && \u0003.Args.Length != 0)
			{
				base.Properties[\u0002\u0005.\u0003(1031813884)] = \u0003.Args[0];
			}
			else
			{
				base.Properties[\u0002\u0005.\u0003(1031813884)] = 0;
			}
			base.OnStartup(\u0003);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void \u0003()
		{
			if (!this.m_\u0003)
			{
				this.m_\u0003 = true;
				base.StartupUri = new Uri(\u0002\u0005.\u0003(1031813870), UriKind.Relative);
				Uri resourceLocator = new Uri(\u0002\u0005.\u0003(1031813844), UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[STAThread]
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public static void \u0003()
		{
			i1iIIiiIl1ll i1iIIiiIl1ll2 = new i1iIIiiIl1ll();
			i1iIIiiIl1ll2.\u0003();
			i1iIIiiIl1ll2.Run();
		}
	}
	public sealed class l1IIi1iiI1III : System.Windows.Controls.UserControl, IComponentConnector
	{
		public struct \u0003
		{
			public byte \u0003;

			public byte \u0006;

			public byte \u000f;

			public byte \u0002;

			public byte \u0008;

			public byte \u0005;

			public byte \u000e;

			public byte \u0003\u2002;

			public byte \u0006\u2002;

			public byte \u000f\u2002;

			public byte \u0002\u2002;

			public byte \u0008\u2002;

			public int \u0005\u2002;

			public int \u000e\u2002;

			public int \u0003\u2001;

			public int \u0006\u2001;

			public int \u000f\u2001;

			public int \u0002\u2001;

			public byte \u0008\u2001;
		}

		public delegate void \u0006();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private \u0006 m_\u0003;

		private bool m_\u0006 = false;

		private bool m_\u000f = false;

		private int m_\u0002 = 0;

		private \u0003 m_\u0008 = default(\u0003);

		internal l1IIi1iiI1III \u0005;

		internal TextBlock \u000e;

		internal TextBlock \u0003\u2002;

		internal TextBlock \u0006\u2002;

		internal TextBlock \u000f\u2002;

		internal TextBlock \u0002\u2002;

		internal TextBlock \u0008\u2002;

		internal TextBlock \u0005\u2002;

		internal TextBlock \u000e\u2002;

		internal TextBlock \u0003\u2001;

		internal TextBlock \u0006\u2001;

		internal TextBlock \u000f\u2001;

		internal TextBlock \u0002\u2001;

		internal TextBlock \u0008\u2001;

		internal TextBlock \u0005\u2001;

		internal TextBlock \u000e\u2001;

		internal TextBlock \u0003\u2009;

		internal TextBlock \u0006\u2009;

		internal TextBlock \u000f\u2009;

		internal TextBlock \u0002\u2009;

		internal TextBlock \u0008\u2009;

		internal TextBlock \u0005\u2009;

		internal TextBlock \u000e\u2009;

		internal TextBlock \u0003\u2003;

		internal System.Windows.Shapes.Path \u0006\u2003;

		internal System.Windows.Shapes.Path \u000f\u2003;

		internal System.Windows.Shapes.Path \u0002\u2003;

		internal Rectangle \u0008\u2003;

		internal Grid \u0005\u2003;

		internal Ellipse \u000e\u2003;

		internal Grid \u0003\u2004;

		internal Ellipse \u0006\u2004;

		internal Grid \u000f\u2004;

		internal Ellipse \u0002\u2004;

		internal Grid \u0008\u2004;

		internal Ellipse \u0005\u2004;

		internal StackPanel \u000e\u2004;

		internal TextBlock \u0003\u2000;

		internal TextBlock \u0006\u2000;

		internal TextBlock \u000f\u2000;

		internal TextBlock \u0002\u2000;

		internal TextBlock \u0008\u2000;

		internal TextBlock \u0005\u2000;

		private bool \u000e\u2000;

		public l1IIi1iiI1III()
		{
			InitializeComponent();
		}

		public void \u0003(\u0006 \u0003)
		{
			\u0006 obj = this.m_\u0003;
			\u0006 obj2;
			do
			{
				obj2 = obj;
				\u0006 value = (\u0006)Delegate.Combine(obj2, \u0003);
				obj = Interlocked.CompareExchange(ref this.m_\u0003, value, obj2);
			}
			while ((object)obj != obj2);
		}

		public void \u0006(\u0006 \u0003)
		{
			\u0006 obj = this.m_\u0003;
			\u0006 obj2;
			do
			{
				obj2 = obj;
				\u0006 value = (\u0006)Delegate.Remove(obj2, \u0003);
				obj = Interlocked.CompareExchange(ref this.m_\u0003, value, obj2);
			}
			while ((object)obj != obj2);
		}

		private void \u0003(object \u0003, RoutedEventArgs \u0006)
		{
			this.\u0003();
		}

		public void \u0003()
		{
			global::\u0008\u2002.\u000f\u2002.\u0003();
			\u0003\u2000.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808504));
			\u0008\u2009.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808504));
			\u0002\u2000.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812296));
			\u0005\u2009.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812296)) + \u0002\u0005.\u0003(1031808499);
			\u000e\u2003.ToolTip = \u0003\u2000.Text + \u0002\u0005.\u0003(1031808494) + this.m_\u0008.\u000e + \u0002\u0005.\u0003(1031808472) + \u0002\u2000.Text + \u0002\u0005.\u0003(1031808494) + this.m_\u0008.\u0003 + \u0002\u0005.\u0003(1031808465);
			\u0002\u2004.ToolTip = \u0003\u2000.Text + \u0002\u0005.\u0003(1031808494) + this.m_\u0008.\u0003\u2002 + \u0002\u0005.\u0003(1031808472) + \u0002\u2000.Text + \u0002\u0005.\u0003(1031808494) + this.m_\u0008.\u0006 + \u0002\u0005.\u0003(1031808465);
			\u0005\u2004.ToolTip = \u0003\u2000.Text + \u0002\u0005.\u0003(1031808494) + this.m_\u0008.\u0006\u2002 + \u0002\u0005.\u0003(1031808472) + \u0002\u2000.Text + \u0002\u0005.\u0003(1031808494) + this.m_\u0008.\u000f + \u0002\u0005.\u0003(1031808465);
			\u0006\u2004.ToolTip = \u0003\u2000.Text + \u0002\u0005.\u0003(1031808494) + this.m_\u0008.\u000f\u2002 + \u0002\u0005.\u0003(1031808472) + \u0002\u2000.Text + \u0002\u0005.\u0003(1031808494) + this.m_\u0008.\u0002 + \u0002\u0005.\u0003(1031808465);
		}

		public void \u0003(\u0008\u2002.\u0003 \u0003)
		{
			string text = string.Empty;
			int num = \u0003.\u000e;
			int num2 = \u0003.\u0003;
			int num3 = \u0003.\u0003\u2002;
			int num4 = \u0003.\u0006;
			int num5 = \u0003.\u0006\u2002;
			int num6 = \u0003.\u000f;
			int num7 = \u0003.\u000f\u2002;
			int num8 = \u0003.\u0002;
			if (0 >= num || num >= num3 || num3 >= num5 || num5 >= num7 || num7 > 100)
			{
				text += string.Format(\u0002\u0005.\u0003(1031808459), \u0003);
			}
			if (0 >= num2 || num2 >= num4 || num4 >= num6 || num6 >= num8 || num8 > 100)
			{
				text += string.Format(\u0002\u0005.\u0003(1031808431), \u0003);
			}
			if (text != string.Empty)
			{
				text += \u0002\u0005.\u0003(1031814562);
				text += \u0002\u0005.\u0003(1031808386);
				num = 50;
				num3 = 60;
				num5 = 70;
				num7 = 100;
				num2 = 40;
				num4 = 60;
				num6 = 80;
				num8 = 100;
			}
			this.\u0003(num, num2);
			\u0006(num3, num4);
			\u000f(num5, num6);
			\u0002(num7, num8);
		}

		private void \u0003(object \u0003, System.Windows.Input.MouseEventArgs \u0006)
		{
			if (\u0006.LeftButton == MouseButtonState.Released)
			{
				this.m_\u0006 = false;
				this.m_\u000f = false;
				return;
			}
			double num = Math.Floor(\u0006.GetPosition(\u0008\u2003).X);
			double num2 = Math.Floor(\u0006.GetPosition(\u0008\u2003).Y);
			Thickness thickness = default(Thickness);
			thickness.Left = num;
			thickness.Top = num2;
			if (this.m_\u0006)
			{
				if (num >= \u0008\u2004.Margin.Left - 7.0)
				{
					thickness.Left = \u0008\u2004.Margin.Left - 7.0;
				}
				else if (num <= \u0005\u2003.Margin.Left)
				{
					thickness.Left = \u0005\u2003.Margin.Left + 7.0;
				}
				else
				{
					thickness.Left = num;
				}
				if (num2 >= \u0008\u2004.Margin.Top - 3.0)
				{
					thickness.Top = \u0008\u2004.Margin.Top - 3.0;
				}
				else if (num2 <= \u0005\u2003.Margin.Top)
				{
					thickness.Top = \u0005\u2003.Margin.Top + 3.0;
				}
				else
				{
					thickness.Top = num2;
				}
				\u0008\u2000.Text = (Math.Ceiling(thickness.Left / 7.0) + 35.0).ToString();
				\u0006\u2000.Text = Math.Ceiling(thickness.Top / 3.0).ToString();
				this.\u0006(Convert.ToInt16(\u0006\u2000.Text), Convert.ToInt16(\u0008\u2000.Text));
			}
			if (this.m_\u000f)
			{
				if (num >= \u0003\u2004.Margin.Left - 7.0)
				{
					thickness.Left = \u0003\u2004.Margin.Left - 7.0;
				}
				else if (num <= \u000f\u2004.Margin.Left)
				{
					thickness.Left = \u000f\u2004.Margin.Left + 7.0;
				}
				else
				{
					thickness.Left = num;
				}
				if (num2 >= \u0003\u2004.Margin.Top - 3.0)
				{
					thickness.Top = \u0003\u2004.Margin.Top - 6.0;
				}
				else if (num2 <= \u000f\u2004.Margin.Top)
				{
					thickness.Top = \u000f\u2004.Margin.Top + 3.0;
				}
				else
				{
					thickness.Top = num2;
				}
				\u0008\u2000.Text = (Math.Ceiling(thickness.Left / 7.0) + 35.0).ToString();
				\u0006\u2000.Text = Math.Ceiling(thickness.Top / 3.0).ToString();
				\u000f(Convert.ToInt16(\u0006\u2000.Text), Convert.ToInt16(\u0008\u2000.Text));
			}
		}

		private void \u0003(int \u0003, int \u0006)
		{
			Thickness margin = default(Thickness);
			this.m_\u0008.\u000e = (byte)\u0003;
			this.m_\u0008.\u0003 = (byte)\u0006;
			\u000e\u2003.ToolTip = \u0003\u2000.Text + \u0002\u0005.\u0003(1031808494) + \u0003 + \u0002\u0005.\u0003(1031808472) + \u0002\u2000.Text + \u0002\u0005.\u0003(1031808494) + \u0006 + \u0002\u0005.\u0003(1031808465);
			margin.Left = (\u0006 - 35) * 7;
			margin.Top = \u0003 * 3;
			margin.Bottom = -20.0;
			margin.Right = -20.0;
			\u0005\u2003.Margin = margin;
			double width = \u000f\u2004.Margin.Left - \u0005\u2003.Margin.Left;
			double height = \u000f\u2004.Margin.Top - \u0005\u2003.Margin.Top;
			\u0006\u2003.Width = width;
			\u0006\u2003.Height = height;
		}

		private void \u0006(int \u0003, int \u0006)
		{
			Thickness margin = default(Thickness);
			this.m_\u0008.\u0003\u2002 = (byte)\u0003;
			this.m_\u0008.\u0006 = (byte)\u0006;
			\u0002\u2004.ToolTip = \u0003\u2000.Text + \u0002\u0005.\u0003(1031808494) + \u0003 + \u0002\u0005.\u0003(1031808472) + \u0002\u2000.Text + \u0002\u0005.\u0003(1031808494) + \u0006 + \u0002\u0005.\u0003(1031808465);
			margin.Left = (\u0006 - 35) * 7;
			margin.Top = \u0003 * 3;
			margin.Bottom = -20.0;
			margin.Right = -20.0;
			\u000f\u2004.Margin = margin;
			double width = \u000f\u2004.Margin.Left - \u0005\u2003.Margin.Left;
			double height = \u000f\u2004.Margin.Top - \u0005\u2003.Margin.Top;
			\u0006\u2003.Width = width;
			\u0006\u2003.Height = height;
			width = \u0008\u2004.Margin.Left - \u000f\u2004.Margin.Left;
			height = \u0008\u2004.Margin.Top - \u000f\u2004.Margin.Top;
			\u000f\u2003.Width = width;
			\u000f\u2003.Height = height;
		}

		private void \u000f(int \u0003, int \u0006)
		{
			Thickness margin = default(Thickness);
			this.m_\u0008.\u0006\u2002 = (byte)\u0003;
			this.m_\u0008.\u000f = (byte)\u0006;
			\u0005\u2004.ToolTip = \u0003\u2000.Text + \u0002\u0005.\u0003(1031808494) + \u0003 + \u0002\u0005.\u0003(1031808472) + \u0002\u2000.Text + \u0002\u0005.\u0003(1031808494) + \u0006 + \u0002\u0005.\u0003(1031808465);
			margin.Left = (\u0006 - 35) * 7;
			margin.Top = \u0003 * 3;
			margin.Bottom = -20.0;
			margin.Right = -20.0;
			\u0008\u2004.Margin = margin;
			double width = \u0008\u2004.Margin.Left - \u000f\u2004.Margin.Left;
			double height = \u0008\u2004.Margin.Top - \u000f\u2004.Margin.Top;
			\u000f\u2003.Width = width;
			\u000f\u2003.Height = height;
			width = \u0003\u2004.Margin.Left - \u0008\u2004.Margin.Left;
			height = \u0003\u2004.Margin.Top - \u0008\u2004.Margin.Top;
			\u0002\u2003.Width = width;
			\u0002\u2003.Height = height;
		}

		private void \u0002(int \u0003, int \u0006)
		{
			Thickness margin = default(Thickness);
			this.m_\u0008.\u000f\u2002 = (byte)\u0003;
			this.m_\u0008.\u0002 = (byte)\u0006;
			\u0006\u2004.ToolTip = \u0003\u2000.Text + \u0002\u0005.\u0003(1031808494) + \u0003 + \u0002\u0005.\u0003(1031808472) + \u0002\u2000.Text + \u0002\u0005.\u0003(1031808494) + \u0006 + \u0002\u0005.\u0003(1031808465);
			margin.Left = (\u0006 - 35) * 7;
			margin.Top = \u0003 * 3;
			margin.Bottom = -20.0;
			margin.Right = -20.0;
			\u0003\u2004.Margin = margin;
			double width = \u0003\u2004.Margin.Left - \u0008\u2004.Margin.Left;
			double height = \u0003\u2004.Margin.Top - \u0008\u2004.Margin.Top;
			\u0002\u2003.Width = width;
			\u0002\u2003.Height = height;
		}

		private void \u0003(int \u0003, int \u0006, int \u000f, int \u0002)
		{
			Thickness margin = default(Thickness);
			this.m_\u0008.\u0003\u2002 = (byte)\u0003;
			this.m_\u0008.\u0006 = (byte)\u000f;
			\u0002\u2004.ToolTip = \u0003\u2000.Text + \u0002\u0005.\u0003(1031808494) + \u0003 + \u0002\u0005.\u0003(1031808472) + \u0002\u2000.Text + \u0002\u0005.\u0003(1031808494) + \u000f + \u0002\u0005.\u0003(1031808465);
			margin.Left = (\u000f - 35) * 7;
			margin.Top = \u0003 * 3;
			margin.Bottom = -20.0;
			margin.Right = -20.0;
			\u000f\u2004.Margin = margin;
			this.m_\u0008.\u0006\u2002 = (byte)\u0006;
			this.m_\u0008.\u000f = (byte)\u0002;
			\u0005\u2004.ToolTip = \u0003\u2000.Text + \u0002\u0005.\u0003(1031808494) + \u0006 + \u0002\u0005.\u0003(1031808472) + \u0002\u2000.Text + \u0002\u0005.\u0003(1031808494) + \u0002 + \u0002\u0005.\u0003(1031808465);
			margin.Left = (\u0002 - 35) * 7;
			margin.Top = \u0006 * 3;
			margin.Bottom = -20.0;
			margin.Right = -20.0;
			\u0008\u2004.Margin = margin;
			double width = \u000f\u2004.Margin.Left - \u0005\u2003.Margin.Left;
			double height = \u000f\u2004.Margin.Top - \u0005\u2003.Margin.Top;
			\u0006\u2003.Width = width;
			\u0006\u2003.Height = height;
			width = \u0008\u2004.Margin.Left - \u000f\u2004.Margin.Left;
			height = \u0008\u2004.Margin.Top - \u000f\u2004.Margin.Top;
			\u000f\u2003.Width = width;
			\u000f\u2003.Height = height;
			width = \u0003\u2004.Margin.Left - \u0008\u2004.Margin.Left;
			height = \u0003\u2004.Margin.Top - \u0008\u2004.Margin.Top;
			\u0002\u2003.Width = width;
			\u0002\u2003.Height = height;
		}

		private void \u0003(object \u0003, MouseButtonEventArgs \u0006)
		{
			this.m_\u0003();
			this.m_\u0006 = true;
			this.m_\u000f = false;
			this.\u0003(2);
			\u0005.Tag = \u0002\u0005.\u0003(1031811328);
		}

		private void \u0006(object \u0003, MouseButtonEventArgs \u0006)
		{
			this.m_\u0003();
			this.m_\u0006 = false;
			this.m_\u000f = true;
			this.\u0003(3);
			\u0005.Tag = \u0002\u0005.\u0003(1031811328);
		}

		public void \u0003(int \u0003)
		{
			this.m_\u0002 = \u0003;
			Color color = Color.FromArgb(byte.MaxValue, 160, 160, 160);
			Brush stroke = new SolidColorBrush(color);
			\u0002\u2004.Stroke = stroke;
			\u0005\u2004.Stroke = stroke;
			\u0002\u2004.StrokeThickness = 2.0;
			\u0005\u2004.StrokeThickness = 2.0;
			\u0008\u2000.Text = string.Empty;
			\u0006\u2000.Text = string.Empty;
			color = Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			stroke = new SolidColorBrush(color);
			if (\u0003 == 2)
			{
				\u0002\u2004.Stroke = stroke;
				\u0002\u2004.StrokeThickness = 4.0;
				double num = \u000f\u2004.Margin.Left / 7.0 + 35.0;
				\u0008\u2000.Text = num.ToString();
				num = \u000f\u2004.Margin.Top / 3.0;
				\u0006\u2000.Text = num.ToString();
			}
			if (\u0003 == 3)
			{
				\u0005\u2004.Stroke = stroke;
				\u0005\u2004.StrokeThickness = 4.0;
				double num2 = \u0008\u2004.Margin.Left / 7.0 + 35.0;
				\u0008\u2000.Text = num2.ToString();
				num2 = \u0008\u2004.Margin.Top / 3.0;
				\u0006\u2000.Text = num2.ToString();
			}
		}

		private void \u0003(object \u0003, DependencyPropertyChangedEventArgs \u0006)
		{
			if (\u0005.IsEnabled)
			{
				\u0006\u2003.StrokeThickness = 6.0;
				\u000f\u2003.StrokeThickness = 6.0;
				\u0002\u2003.StrokeThickness = 6.0;
			}
			else
			{
				\u0006\u2003.StrokeThickness = 3.0;
				\u000f\u2003.StrokeThickness = 3.0;
				\u0002\u2003.StrokeThickness = 3.0;
			}
		}

		public void \u0006(int \u0003)
		{
			\u0005.Tag = \u0002\u0005.\u0003(1031815033);
			double num = \u000f\u2004.Margin.Top / 3.0;
			double num2 = \u0008\u2004.Margin.Top / 3.0;
			double num3 = \u000f\u2004.Margin.Left / 7.0 + 35.0;
			double num4 = \u0008\u2004.Margin.Left / 7.0 + 35.0;
			switch (\u0003)
			{
			case 1:
				global::\u0008\u2002.\u0008.\u0003.\u0003\u2002 = (byte)num;
				global::\u0008\u2002.\u0008.\u0003.\u0006\u2002 = (byte)num2;
				global::\u0008\u2002.\u0008.\u0003.\u0006 = (byte)num3;
				global::\u0008\u2002.\u0008.\u0003.\u000f = (byte)num4;
				break;
			case 2:
				global::\u0008\u2002.\u0008.\u0006.\u0003\u2002 = (byte)num;
				global::\u0008\u2002.\u0008.\u0006.\u0006\u2002 = (byte)num2;
				global::\u0008\u2002.\u0008.\u0006.\u0006 = (byte)num3;
				global::\u0008\u2002.\u0008.\u0006.\u000f = (byte)num4;
				break;
			case 3:
				global::\u0008\u2002.\u0008.\u000f.\u0003\u2002 = (byte)num;
				global::\u0008\u2002.\u0008.\u000f.\u0006\u2002 = (byte)num2;
				global::\u0008\u2002.\u0008.\u000f.\u0006 = (byte)num3;
				global::\u0008\u2002.\u0008.\u000f.\u000f = (byte)num4;
				break;
			}
		}

		public void \u000f(int \u0003)
		{
			\u0005.Tag = \u0002\u0005.\u0003(1031815033);
			switch (\u0003)
			{
			case 1:
				global::\u0008\u2002.\u0008.\u0003.\u0003\u2002 = global::\u0008\u2002.\u0008.\u0003.\u0002\u2002;
				global::\u0008\u2002.\u0008.\u0003.\u0006\u2002 = global::\u0008\u2002.\u0008.\u0003.\u0008\u2002;
				global::\u0008\u2002.\u0008.\u0003.\u0006 = global::\u0008\u2002.\u0008.\u0003.\u0008;
				global::\u0008\u2002.\u0008.\u0003.\u000f = global::\u0008\u2002.\u0008.\u0003.\u0005;
				this.\u0003(global::\u0008\u2002.\u0008.\u0003.\u0003\u2002, global::\u0008\u2002.\u0008.\u0003.\u0006\u2002, global::\u0008\u2002.\u0008.\u0003.\u0006, global::\u0008\u2002.\u0008.\u0003.\u000f);
				break;
			case 2:
				global::\u0008\u2002.\u0008.\u0006.\u0003\u2002 = global::\u0008\u2002.\u0008.\u0006.\u0002\u2002;
				global::\u0008\u2002.\u0008.\u0006.\u0006\u2002 = global::\u0008\u2002.\u0008.\u0006.\u0008\u2002;
				global::\u0008\u2002.\u0008.\u0006.\u0006 = global::\u0008\u2002.\u0008.\u0006.\u0008;
				global::\u0008\u2002.\u0008.\u0006.\u000f = global::\u0008\u2002.\u0008.\u0006.\u0005;
				this.\u0003(global::\u0008\u2002.\u0008.\u0006.\u0003\u2002, global::\u0008\u2002.\u0008.\u0006.\u0006\u2002, global::\u0008\u2002.\u0008.\u0006.\u0006, global::\u0008\u2002.\u0008.\u0006.\u000f);
				break;
			case 3:
				global::\u0008\u2002.\u0008.\u000f.\u0003\u2002 = global::\u0008\u2002.\u0008.\u000f.\u0002\u2002;
				global::\u0008\u2002.\u0008.\u000f.\u0006\u2002 = global::\u0008\u2002.\u0008.\u000f.\u0008\u2002;
				global::\u0008\u2002.\u0008.\u000f.\u0006 = global::\u0008\u2002.\u0008.\u000f.\u0008;
				global::\u0008\u2002.\u0008.\u000f.\u000f = global::\u0008\u2002.\u0008.\u000f.\u0005;
				this.\u0003(global::\u0008\u2002.\u0008.\u000f.\u0003\u2002, global::\u0008\u2002.\u0008.\u000f.\u0006\u2002, global::\u0008\u2002.\u0008.\u000f.\u0006, global::\u0008\u2002.\u0008.\u000f.\u000f);
				break;
			}
		}

		private void \u0006(object \u0003, System.Windows.Input.MouseEventArgs \u0006)
		{
			\u0008\u2003.Visibility = Visibility.Visible;
		}

		private void \u000f(object \u0003, MouseButtonEventArgs \u0006)
		{
			\u0008\u2003.Visibility = Visibility.Hidden;
		}

		private void \u0002(object \u0003, MouseButtonEventArgs \u0006)
		{
			\u0008\u2003.Visibility = Visibility.Hidden;
		}

		private void \u000f(object \u0003, System.Windows.Input.MouseEventArgs \u0006)
		{
			\u0008\u2003.Visibility = Visibility.Visible;
		}

		private void \u0008(object \u0003, MouseButtonEventArgs \u0006)
		{
			\u0008\u2003.Visibility = Visibility.Hidden;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!\u000e\u2000)
			{
				\u000e\u2000 = true;
				Uri resourceLocator = new Uri(\u0002\u0005.\u0003(1031808337), UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		private void 4nsdtp8ymt76rfk64gxd8bccf56qh6mg\u2009\u2005\u2006\u0003(int \u0003, object \u0006)
		{
			switch (\u0003)
			{
			case 1:
				\u0005 = (l1IIi1iiI1III)\u0006;
				\u0005.IsEnabledChanged += this.\u0003;
				\u0005.Loaded += this.\u0003;
				break;
			case 2:
				\u000e = (TextBlock)\u0006;
				break;
			case 3:
				\u0003\u2002 = (TextBlock)\u0006;
				break;
			case 4:
				\u0006\u2002 = (TextBlock)\u0006;
				break;
			case 5:
				\u000f\u2002 = (TextBlock)\u0006;
				break;
			case 6:
				\u0002\u2002 = (TextBlock)\u0006;
				break;
			case 7:
				\u0008\u2002 = (TextBlock)\u0006;
				break;
			case 8:
				\u0005\u2002 = (TextBlock)\u0006;
				break;
			case 9:
				\u000e\u2002 = (TextBlock)\u0006;
				break;
			case 10:
				\u0003\u2001 = (TextBlock)\u0006;
				break;
			case 11:
				\u0006\u2001 = (TextBlock)\u0006;
				break;
			case 12:
				\u000f\u2001 = (TextBlock)\u0006;
				break;
			case 13:
				\u0002\u2001 = (TextBlock)\u0006;
				break;
			case 14:
				\u0008\u2001 = (TextBlock)\u0006;
				break;
			case 15:
				\u0005\u2001 = (TextBlock)\u0006;
				break;
			case 16:
				\u000e\u2001 = (TextBlock)\u0006;
				break;
			case 17:
				\u0003\u2009 = (TextBlock)\u0006;
				break;
			case 18:
				\u0006\u2009 = (TextBlock)\u0006;
				break;
			case 19:
				\u000f\u2009 = (TextBlock)\u0006;
				break;
			case 20:
				\u0002\u2009 = (TextBlock)\u0006;
				break;
			case 21:
				\u0008\u2009 = (TextBlock)\u0006;
				break;
			case 22:
				\u0005\u2009 = (TextBlock)\u0006;
				break;
			case 23:
				\u000e\u2009 = (TextBlock)\u0006;
				break;
			case 24:
				\u0003\u2003 = (TextBlock)\u0006;
				break;
			case 25:
				\u0006\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 26:
				\u000f\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 27:
				\u0002\u2003 = (System.Windows.Shapes.Path)\u0006;
				break;
			case 28:
				\u0008\u2003 = (Rectangle)\u0006;
				\u0008\u2003.MouseMove += this.\u0003;
				\u0008\u2003.MouseLeftButtonUp += \u0002;
				break;
			case 29:
				\u0005\u2003 = (Grid)\u0006;
				break;
			case 30:
				\u000e\u2003 = (Ellipse)\u0006;
				break;
			case 31:
				\u0003\u2004 = (Grid)\u0006;
				break;
			case 32:
				\u0006\u2004 = (Ellipse)\u0006;
				break;
			case 33:
				\u000f\u2004 = (Grid)\u0006;
				break;
			case 34:
				\u0002\u2004 = (Ellipse)\u0006;
				\u0002\u2004.MouseLeftButtonDown += this.\u0003;
				\u0002\u2004.MouseEnter += this.\u0006;
				\u0002\u2004.MouseLeftButtonUp += \u000f;
				break;
			case 35:
				\u0008\u2004 = (Grid)\u0006;
				break;
			case 36:
				\u0005\u2004 = (Ellipse)\u0006;
				\u0005\u2004.MouseLeftButtonDown += this.\u0006;
				\u0005\u2004.MouseEnter += \u000f;
				\u0005\u2004.MouseLeftButtonUp += \u0008;
				break;
			case 37:
				\u000e\u2004 = (StackPanel)\u0006;
				break;
			case 38:
				\u0003\u2000 = (TextBlock)\u0006;
				break;
			case 39:
				\u0006\u2000 = (TextBlock)\u0006;
				break;
			case 40:
				\u000f\u2000 = (TextBlock)\u0006;
				break;
			case 41:
				\u0002\u2000 = (TextBlock)\u0006;
				break;
			case 42:
				\u0008\u2000 = (TextBlock)\u0006;
				break;
			case 43:
				\u0005\u2000 = (TextBlock)\u0006;
				break;
			default:
				\u000e\u2000 = true;
				break;
			}
		}

		void IComponentConnector.Connect(int \u0003, object \u0006)
		{
			//ILSpy generated this explicit interface implementation from .override directive in 4nsdtp8ymt76rfk64gxd8bccf56qh6mg   
			this.4nsdtp8ymt76rfk64gxd8bccf56qh6mg\u2009\u2005\u2006\u0003(\u0003, \u0006);
		}
	}
	public sealed class l1Ii11l1ii1II : System.Windows.Controls.UserControl, IComponentConnector
	{
		public delegate void \u0003();

		public delegate void \u0006();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private \u0003 m_\u0003;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private \u0006 m_\u0006;

		internal l1Ii11l1ii1II \u000f;

		internal Grid \u0002;

		internal Image \u0008;

		internal Image \u0005;

		internal Image \u000e;

		internal System.Windows.Controls.Button \u0003\u2002;

		internal TextBlock \u0006\u2002;

		internal System.Windows.Controls.Button \u000f\u2002;

		private bool \u0002\u2002;

		public l1Ii11l1ii1II()
		{
			InitializeComponent();
		}

		public void \u0003(\u0003 \u0003)
		{
			\u0003 obj = this.m_\u0003;
			\u0003 obj2;
			do
			{
				obj2 = obj;
				\u0003 value = (\u0003)Delegate.Combine(obj2, \u0003);
				obj = Interlocked.CompareExchange(ref this.m_\u0003, value, obj2);
			}
			while ((object)obj != obj2);
		}

		public void \u0006(\u0003 \u0003)
		{
			\u0003 obj = this.m_\u0003;
			\u0003 obj2;
			do
			{
				obj2 = obj;
				\u0003 value = (\u0003)Delegate.Remove(obj2, \u0003);
				obj = Interlocked.CompareExchange(ref this.m_\u0003, value, obj2);
			}
			while ((object)obj != obj2);
		}

		public void \u0003(\u0006 \u0003)
		{
			\u0006 obj = this.m_\u0006;
			\u0006 obj2;
			do
			{
				obj2 = obj;
				\u0006 value = (\u0006)Delegate.Combine(obj2, \u0003);
				obj = Interlocked.CompareExchange(ref this.m_\u0006, value, obj2);
			}
			while ((object)obj != obj2);
		}

		public void \u0006(\u0006 \u0003)
		{
			\u0006 obj = this.m_\u0006;
			\u0006 obj2;
			do
			{
				obj2 = obj;
				\u0006 value = (\u0006)Delegate.Remove(obj2, \u0003);
				obj = Interlocked.CompareExchange(ref this.m_\u0006, value, obj2);
			}
			while ((object)obj != obj2);
		}

		private void \u0003(object \u0003, RoutedEventArgs \u0006)
		{
			this.m_\u0003();
		}

		private void \u0003()
		{
			Thread.Sleep(50);
			this.m_\u0006();
		}

		private void \u0006(object \u0003, RoutedEventArgs \u0006)
		{
			\u000f.Visibility = Visibility.Hidden;
			Thread thread = new Thread(this.\u0003);
			thread.Start();
		}

		public void \u0003(int \u0003, int \u0006)
		{
			\u000f\u2002.Content = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808880));
			\u0003\u2002.Content = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808874));
			switch (\u0003)
			{
			case 0:
				\u0005.Visibility = Visibility.Hidden;
				\u000e.Visibility = Visibility.Visible;
				break;
			case 1:
				\u0005.Visibility = Visibility.Visible;
				\u000e.Visibility = Visibility.Hidden;
				break;
			default:
				\u0005.Visibility = Visibility.Hidden;
				\u000e.Visibility = Visibility.Visible;
				break;
			}
			switch (\u0006)
			{
			case 0:
				\u0006\u2002.Text = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808856));
				break;
			case 1:
				\u0006\u2002.Text = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808840));
				break;
			case 2:
				\u0006\u2002.Text = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808824));
				break;
			case 3:
				\u0006\u2002.Text = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808810));
				break;
			case 4:
				\u0006\u2002.Text = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808807));
				break;
			case 5:
				\u0006\u2002.Text = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808788));
				break;
			case 6:
				\u0006\u2002.Text = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808774));
				\u0003\u2002.Visibility = Visibility.Hidden;
				\u000f\u2002.Content = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808247));
				break;
			case 7:
				\u0006\u2002.Text = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808226));
				\u000f\u2002.Content = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808247));
				\u0003\u2002.Content = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808209));
				break;
			case 8:
				\u0006\u2002.Text = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808203));
				\u0003\u2002.Visibility = Visibility.Hidden;
				\u000f\u2002.Content = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808247));
				break;
			case 9:
				\u0006\u2002.Text = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808188));
				\u000f\u2002.Content = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808247));
				\u0003\u2002.Content = \u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031808209));
				break;
			case 10:
				\u0006\u2002.Text = \u0002\u0005.\u0003(1031808175);
				\u0003\u2002.Visibility = Visibility.Hidden;
				\u000f\u2002.Visibility = Visibility.Hidden;
				break;
			}
			\u000f.Visibility = Visibility.Visible;
		}

		private void \u0003(object \u0003, DependencyPropertyChangedEventArgs \u0006)
		{
			_ = \u000f.Visibility;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!\u0002\u2002)
			{
				\u0002\u2002 = true;
				Uri resourceLocator = new Uri(\u0002\u0005.\u0003(1031808132), UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		private void xml79zqh9ztap6fvzndge4cx6qdzshul\u2009\u2005\u2006\u0003(int \u0003, object \u0006)
		{
			switch (\u0003)
			{
			case 1:
				\u000f = (l1Ii11l1ii1II)\u0006;
				\u000f.IsVisibleChanged += this.\u0003;
				break;
			case 2:
				\u0002 = (Grid)\u0006;
				break;
			case 3:
				\u0008 = (Image)\u0006;
				break;
			case 4:
				\u0005 = (Image)\u0006;
				break;
			case 5:
				\u000e = (Image)\u0006;
				break;
			case 6:
				\u0003\u2002 = (System.Windows.Controls.Button)\u0006;
				\u0003\u2002.Click += this.\u0003;
				break;
			case 7:
				\u0006\u2002 = (TextBlock)\u0006;
				break;
			case 8:
				\u000f\u2002 = (System.Windows.Controls.Button)\u0006;
				\u000f\u2002.Click += this.\u0006;
				break;
			default:
				\u0002\u2002 = true;
				break;
			}
		}

		void IComponentConnector.Connect(int \u0003, object \u0006)
		{
			//ILSpy generated this explicit interface implementation from .override directive in xml79zqh9ztap6fvzndge4cx6qdzshul   
			this.xml79zqh9ztap6fvzndge4cx6qdzshul\u2009\u2005\u2006\u0003(\u0003, \u0006);
		}
	}
	public sealed class l1illIiliii11 : System.Windows.Controls.UserControl, IComponentConnector
	{
		[Serializable]
		private sealed class \u0003
		{
			public static readonly \u0003 \u0003 = new \u0003();

			public static Action \u0006;

			internal void \u0003()
			{
			}
		}

		private DispatcherTimer m_\u0003 = new DispatcherTimer();

		private int m_\u0006 = 0;

		private bool m_\u000f = false;

		private double m_\u0002 = 255.0;

		private bool m_\u0008 = false;

		internal l1illIiliii11 \u0005;

		internal Grid \u000e;

		internal Rectangle \u0003\u2002;

		internal Grid \u0006\u2002;

		internal Rectangle \u000f\u2002;

		internal Rectangle \u0002\u2002;

		internal Rectangle \u0008\u2002;

		internal Image \u0005\u2002;

		internal TextBlock \u000e\u2002;

		internal TextBlock \u0003\u2001;

		internal TextBlock \u0006\u2001;

		internal TextBlock \u000f\u2001;

		internal TextBlock \u0002\u2001;

		internal TextBlock \u0008\u2001;

		internal TextBlock \u0005\u2001;

		internal Rectangle \u000e\u2001;

		internal Rectangle \u0003\u2009;

		internal TextBlock \u0006\u2009;

		internal TextBlock \u000f\u2009;

		internal TextBlock \u0002\u2009;

		internal TextBlock \u0008\u2009;

		internal Image \u0005\u2009;

		internal Grid \u000e\u2009;

		internal System.Windows.Controls.RadioButton \u0003\u2003;

		internal System.Windows.Controls.RadioButton \u0006\u2003;

		internal Viewbox \u000f\u2003;

		internal Viewbox \u0002\u2003;

		internal Grid \u0008\u2003;

		internal Rectangle \u0005\u2003;

		internal Rectangle \u000e\u2003;

		internal Image \u0003\u2004;

		internal TextBlock \u0006\u2004;

		internal TextBlock \u000f\u2004;

		internal TextBlock \u0002\u2004;

		internal TextBlock \u0008\u2004;

		internal TextBlock \u0005\u2004;

		internal TextBlock \u000e\u2004;

		internal TextBlock \u0003\u2000;

		internal Rectangle \u0006\u2000;

		internal Rectangle \u000f\u2000;

		internal TextBlock \u0002\u2000;

		internal TextBlock \u0008\u2000;

		internal Grid \u0005\u2000;

		internal TextBlock \u000e\u2000;

		internal System.Windows.Controls.RadioButton \u0003\u2007;

		internal System.Windows.Controls.RadioButton \u0006\u2007;

		internal Viewbox \u000f\u2007;

		internal Grid \u0002\u2007;

		internal Rectangle \u0008\u2007;

		internal Rectangle \u0005\u2007;

		internal Image \u000e\u2007;

		internal TextBlock \u0003\u2005;

		internal TextBlock \u0006\u2005;

		internal TextBlock \u000f\u2005;

		internal TextBlock \u0002\u2005;

		internal TextBlock \u0008\u2005;

		internal TextBlock \u0005\u2005;

		internal TextBlock \u000e\u2005;

		internal TextBlock \u0003\u200b;

		internal TextBlock \u0006\u200b;

		internal Rectangle \u000f\u200b;

		internal Rectangle \u0002\u200b;

		internal Grid \u0008\u200b;

		internal Grid \u0005\u200b;

		internal Rectangle \u000e\u200b;

		internal TextBlock \u0003\u200a;

		internal System.Windows.Controls.CheckBox \u0006\u200a;

		internal System.Windows.Controls.CheckBox \u000f\u200a;

		internal System.Windows.Controls.CheckBox \u0002\u200a;

		internal System.Windows.Controls.CheckBox \u0008\u200a;

		internal System.Windows.Controls.CheckBox \u0005\u200a;

		internal System.Windows.Controls.CheckBox \u000e\u200a;

		internal System.Windows.Controls.CheckBox \u0003\u2006;

		internal System.Windows.Controls.CheckBox \u0006\u2006;

		internal System.Windows.Controls.CheckBox \u000f\u2006;

		internal TextBlock \u0002\u2006;

		internal System.Windows.Controls.ComboBox \u0008\u2006;

		internal System.Windows.Controls.ComboBox \u0005\u2006;

		internal System.Windows.Controls.Button \u000e\u2006;

		internal Grid \u0003\u2008;

		internal System.Windows.Controls.Button \u0006\u2008;

		internal Grid \u000f\u2008;

		internal Image \u0002\u2008;

		internal TextBlock \u0008\u2008;

		internal Grid \u0005\u2008;

		internal Rectangle \u000e\u2008;

		internal TextBlock \u0003\u2002\u2009;

		internal TextBlock \u0006\u2002\u2009;

		internal TextBlock \u000f\u2002\u2009;

		internal TextBlock \u0002\u2002\u2009;

		internal Slider \u0008\u2002\u2009;

		internal System.Windows.Controls.ComboBox \u0005\u2002\u2009;

		internal Grid \u000e\u2002\u2009;

		internal StackPanel \u0003\u2001\u2009;

		internal StackPanel \u0006\u2001\u2009;

		internal l1IIi1iiI1III \u000f\u2001\u2009;

		internal StackPanel \u0002\u2001\u2009;

		internal StackPanel \u0008\u2001\u2009;

		internal l1IIi1iiI1III \u0005\u2001\u2009;

		internal StackPanel \u000e\u2001\u2009;

		internal StackPanel \u0003\u2009\u2009;

		internal l1IIi1iiI1III \u0006\u2009\u2009;

		internal System.Windows.Controls.RadioButton \u000f\u2009\u2009;

		internal System.Windows.Controls.RadioButton \u0002\u2009\u2009;

		internal System.Windows.Controls.RadioButton \u0008\u2009\u2009;

		internal Grid \u0005\u2009\u2009;

		internal WrapPanel \u000e\u2009\u2009;

		internal System.Windows.Controls.Button \u0003\u2003\u2009;

		internal System.Windows.Controls.Button \u0006\u2003\u2009;

		internal Grid \u000f\u2003\u2009;

		internal System.Windows.Controls.TabControl \u0002\u2003\u2009;

		internal TabItem \u0008\u2003\u2009;

		internal i11l1i1Ii1I1 \u0005\u2003\u2009;

		internal TabItem \u000e\u2003\u2009;

		internal i11l1i1Ii1I1 \u0003\u2004\u2009;

		internal TabItem \u0006\u2004\u2009;

		internal i11l1i1Ii1I1 \u000f\u2004\u2009;

		internal TabItem \u0002\u2004\u2009;

		internal i11l1i1Ii1I1 \u0008\u2004\u2009;

		internal Grid \u0005\u2004\u2009;

		internal WrapPanel \u000e\u2004\u2009;

		internal System.Windows.Controls.Button \u0003\u2000\u2009;

		internal System.Windows.Controls.Button \u0006\u2000\u2009;

		internal Grid \u000f\u2000\u2009;

		internal TextBlock \u0002\u2000\u2009;

		internal TextBlock \u0008\u2000\u2009;

		internal Viewbox \u0005\u2000\u2009;

		internal Grid \u000e\u2000\u2009;

		internal lll1llIli1Ii \u0003\u2007\u2009;

		internal TextBlock \u0006\u2007\u2009;

		internal TextBlock \u000f\u2007\u2009;

		internal TextBlock \u0002\u2007\u2009;

		internal TextBlock \u0008\u2007\u2009;

		internal TextBlock \u0005\u2007\u2009;

		internal Rectangle \u000e\u2007\u2009;

		internal Rectangle \u0003\u2005\u2009;

		internal Rectangle \u0006\u2005\u2009;

		internal Viewbox \u000f\u2005\u2009;

		internal Grid \u0002\u2005\u2009;

		internal lll1llIli1Ii \u0008\u2005\u2009;

		internal TextBlock \u0005\u2005\u2009;

		internal TextBlock \u000e\u2005\u2009;

		internal TextBlock \u0003\u200b\u2009;

		internal TextBlock \u0006\u200b\u2009;

		internal TextBlock \u000f\u200b\u2009;

		internal Rectangle \u0002\u200b\u2009;

		internal Rectangle \u0008\u200b\u2009;

		internal StackPanel \u0005\u200b\u2009;

		internal StackPanel \u000e\u200b\u2009;

		internal System.Windows.Controls.RadioButton \u0003\u200a\u2009;

		internal StackPanel \u0006\u200a\u2009;

		internal System.Windows.Controls.CheckBox \u000f\u200a\u2009;

		internal StackPanel \u0002\u200a\u2009;

		internal System.Windows.Controls.RadioButton \u0008\u200a\u2009;

		internal StackPanel \u0005\u200a\u2009;

		internal System.Windows.Controls.RadioButton \u000e\u200a\u2009;

		internal StackPanel \u0003\u2006\u2009;

		internal System.Windows.Controls.RadioButton \u0006\u2006\u2009;

		internal StackPanel \u000f\u2006\u2009;

		internal System.Windows.Controls.RadioButton \u0002\u2006\u2009;

		internal StackPanel \u0008\u2006\u2009;

		internal System.Windows.Controls.RadioButton \u0005\u2006\u2009;

		internal StackPanel \u000e\u2006\u2009;

		internal System.Windows.Controls.RadioButton \u0003\u2008\u2009;

		internal l1Ii11l1ii1II \u0006\u2008\u2009;

		private bool \u000f\u2008\u2009;

		public l1illIiliii11()
		{
			InitializeComponent();
		}

		[DllImport("NVGPU_DLL.dll", EntryPoint = "InitGPU_API")]
		public static extern int \u0003();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Pstate")]
		public static extern int \u0006();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_LF")]
		public static extern int \u000f();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_setF")]
		public static extern int \u0002();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Volt")]
		public static extern int \u0008();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Temp")]
		public static extern int \u0005();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "CheckGPU_Thermal")]
		public static extern bool \u0003();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_defaultMaxTemp")]
		public static extern int \u000e();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Graphics_Clock")]
		public static extern int \u0003\u2002();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Memory_Clock")]
		public static extern int \u0006\u2002();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Processor_Clock")]
		public static extern int \u000f\u2002();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Set_GPU_OVERCLOCK")]
		public static extern int \u0003(bool \u0003, int \u0006, int \u000f, int \u0002);

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Max_Clock")]
		public static extern void \u0003();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Boost_Clock")]
		public static extern int \u0002\u2002();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Base_Clock")]
		public static extern int \u0008\u2002();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_ALLInfo")]
		public static extern bool \u0006();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Pstate_Freq")]
		public static extern int \u0003(int \u0003, int \u0006);

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Overclock_range")]
		public static extern int \u0005\u2002();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Overclock_Max")]
		public static extern int \u000e\u2002();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Overclock_rangeMax")]
		public static extern int \u0003\u2001();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Overclock_rangeMin")]
		public static extern int \u0006\u2001();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_Memory_range")]
		public static extern int \u000f\u2001();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_Memory_range_max")]
		public static extern int \u0002\u2001();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_Memory_range_min")]
		public static extern int \u0008\u2001();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_Memory_OC_max")]
		public static extern int \u0005\u2001();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Memory_boost")]
		public static extern int \u000e\u2001();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Memory_base")]
		public static extern int \u0003\u2009();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_Volt_range")]
		public static extern int \u0006\u2009();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_name")]
		public static extern IntPtr \u0003();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_name2")]
		public static extern IntPtr \u0006();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "CloseGPU_API")]
		public static extern void \u0006();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "run_ReadOCinfo")]
		public static extern bool \u000f();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Set_GPU_Number")]
		public static extern int \u0003(int \u0003);

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_TotalNumber")]
		public static extern int \u000f\u2009();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_VFPointFile")]
		public static extern IntPtr \u0003(string \u0003);

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_NVDeviceID")]
		public static extern int \u0006(int \u0003);

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Set_VFOffset")]
		public static extern int \u0003(int \u0003, int \u0006, int \u000f);

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Set_ManualMode")]
		public static extern int \u000f(int \u0003);

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Set_LinearMode")]
		public static extern int \u0002(int \u0003);

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Get_GPU_Util")]
		public static extern int \u0002\u2009();

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Set_CoreOC")]
		public static extern int \u0006(int \u0003, int \u0006);

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Set_MEMOC")]
		public static extern int \u000f(int \u0003, int \u0006);

		[DllImport("NVGPU_DLL.dll", EntryPoint = "Check_GPU_VRAM_Clock")]
		public static extern bool \u0002();

		[DllImport("kernel32", CharSet = CharSet.Unicode, EntryPoint = "GetPrivateProfileString")]
		public static extern int \u0003(string \u0003, string \u0006, string \u000f, StringBuilder \u0002, int \u0008, string \u0005);

		[DllImport("kernel32", EntryPoint = "WritePrivateProfileString")]
		private static extern long \u0003(string \u0003, string \u0006, string \u000f, string \u0002);

		[DllImport("InsydeDCHU.dll", EntryPoint = "GetDCHU_Data_Integer")]
		public static extern int \u0003(int \u0003, ref int \u0006);

		[DllImport("InsydeDCHU.dll", EntryPoint = "SetDCHU_Data")]
		public static extern int \u0003(int \u0003, byte[] \u0006);

		[DllImport("InsydeDCHU.dll", EntryPoint = "GetDCHU_Data_Buffer")]
		public static extern int \u0003(int \u0003, ref byte \u0006);

		[DllImport("InsydeDCHU.dll", EntryPoint = "ReadAppSettings")]
		public static extern int \u0003(int \u0003, int \u0006, int \u000f, ref byte \u0002);

		[DllImport("InsydeDCHU.dll", EntryPoint = "WriteAppSettings")]
		public static extern int \u0006(int \u0003, int \u0006, int \u000f, ref byte \u0002);

		public void \u0003()
		{
			this.m_\u0003.Interval = TimeSpan.FromMilliseconds(1000.0);
			this.m_\u0003.Tick += \u0003;
			\u0006\u2008\u2009.\u0003((l1Ii11l1ii1II.\u0003)this.\u000e\u2001);
			\u0006\u2008\u2009.\u0003((l1Ii11l1ii1II.\u0006)this.\u0005\u2001);
			this.\u0006();
			string text = global::\u0008\u2002.\u0003.\u0003();
			string text2 = global::\u0008\u2002.\u0002.\u0003();
			if (text != text2)
			{
				global::\u0008\u2002.\u0003.\u0003(text2);
				global::\u0008\u2002.\u0006.\u0006\u2009 = true;
			}
			this.\u0005\u2002();
			this.m_\u000f = true;
			this.\u0008();
			\u000f();
			\u0002();
			this.\u000e\u2002();
			this.\u0005();
		}

		public void \u0006()
		{
			byte[] array = new byte[256];
			array = global::\u0008\u2002.\u0002.\u0003(17);
			global::\u0008\u2002.\u0006.\u0008\u2001 = array[2];
		}

		private void \u000f()
		{
			try
			{
				byte r = Convert.ToByte(global::\u0008\u2002.\u0002\u2002.\u0003(\u0002\u0005.\u0003(1031812954), \u0002\u0005.\u0003(1031812928)));
				byte g = Convert.ToByte(global::\u0008\u2002.\u0002\u2002.\u0003(\u0002\u0005.\u0003(1031812954), \u0002\u0005.\u0003(1031812916)));
				byte b = Convert.ToByte(global::\u0008\u2002.\u0002\u2002.\u0003(\u0002\u0005.\u0003(1031812954), \u0002\u0005.\u0003(1031812888)));
				Color color = Color.FromArgb(byte.MaxValue, r, g, b);
				Brush foreground = new SolidColorBrush(color);
				\u000e\u2000.Foreground = foreground;
			}
			catch
			{
			}
		}

		private void \u0002()
		{
			try
			{
				byte r = Convert.ToByte(global::\u0008\u2002.\u0002\u2002.\u0003(\u0002\u0005.\u0003(1031812954), \u0002\u0005.\u0003(1031812876)));
				byte g = Convert.ToByte(global::\u0008\u2002.\u0002\u2002.\u0003(\u0002\u0005.\u0003(1031812954), \u0002\u0005.\u0003(1031812336)));
				byte b = Convert.ToByte(global::\u0008\u2002.\u0002\u2002.\u0003(\u0002\u0005.\u0003(1031812954), \u0002\u0005.\u0003(1031812324)));
				Color color = Color.FromArgb(byte.MaxValue, r, g, b);
				new SolidColorBrush(color);
			}
			catch
			{
				this.m_\u0008 = true;
			}
		}

		public void \u0008()
		{
			global::\u0008\u2002.\u000f\u2002.\u0003();
			\u0003\u2000.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812296));
			\u0006\u2007\u2009.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812283));
			\u0006\u200b.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812296));
			\u0005\u2005\u2009.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812283));
			\u0008\u2008.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812267));
			\u0003\u200a\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812242));
			\u0008\u200a\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812227));
			\u0002\u2006\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812210));
			\u0003\u2003\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812192));
			\u0006\u2003\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812191));
			\u000f\u2009\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812171));
			\u0002\u2009\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812153));
			\u0008\u2009\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812151));
			\u0006\u2002\u2009.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812134));
			\u000f\u200a\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812116));
			if (global::\u0008\u2002.\u0006.\u0008\u2001 == 5)
			{
				\u0006\u2006\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812242));
			}
			else
			{
				\u0006\u2006\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812602));
			}
			\u0006\u2008.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812584));
			\u000e\u2006.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812575));
			\u0003\u200a.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812546));
			\u0006\u200a.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812523));
			\u000f\u200a.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812497));
			\u0002\u200a.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812486));
			\u0008\u200a.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812465));
			\u0005\u200a.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812460));
			\u000e\u200a.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812455));
			\u0003\u2006.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812434));
			\u0006\u2006.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812429));
			\u000f\u2006.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812408));
			\u0002\u2006.Text = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812403));
			int selectedIndex = \u0008\u2006.SelectedIndex;
			\u0008\u2006.Items.Clear();
			\u0008\u2006.Items.Add(global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812399)));
			\u0008\u2006.Items.Add(global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812377)));
			\u0008\u2006.SelectedIndex = selectedIndex;
			if (global::\u0008\u2002.\u0008.\u0006\u2009 >= 12)
			{
				\u0005\u2006.SelectedIndex = global::\u0008\u2002.\u0008.\u0006\u2009 - 12;
				\u0008\u2006.SelectedIndex = 1;
			}
			else
			{
				\u0005\u2006.SelectedIndex = global::\u0008\u2002.\u0008.\u0006\u2009;
				\u0008\u2006.SelectedIndex = 0;
			}
		}

		public void \u0005()
		{
			this.m_\u0003.Start();
			this.\u000e.Visibility = Visibility.Visible;
			global::\u0008\u2002.\u0003.\u0008 = \u000f\u2005\u2009.Height;
			global::\u0008\u2002.\u0003.\u0005 = \u000f\u2005\u2009.Width;
			global::\u0008\u2002.\u0003.\u000e = this.\u000f\u2003.Width;
			global::\u0008\u2002.\u0003.\u0003\u2002 = this.\u000f\u2003.Height;
			if (global::\u0008\u2002.\u0006.\u0005\u2001)
			{
				if (global::\u0008\u2002.\u0008.\u0008\u2001 == 1)
				{
					\u0003\u2009();
				}
				else
				{
					\u0005\u200b.Visibility = Visibility.Hidden;
				}
			}
			else
			{
				\u0005\u200b.Visibility = Visibility.Hidden;
			}
		}

		public void \u000e()
		{
			this.m_\u0003.Stop();
			this.\u000e.Visibility = Visibility.Hidden;
			\u0003\u2007\u2009.\u0003(0);
			\u0008\u2005\u2009.\u0003(0);
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		public void \u0003\u2002()
		{
		}

		private void \u0003(object \u0003, RoutedEventArgs \u0006)
		{
		}

		private void \u0006(object \u0003, RoutedEventArgs \u0006)
		{
		}

		private void \u000f(object \u0003, RoutedEventArgs \u0006)
		{
		}

		private void \u0002(object \u0003, RoutedEventArgs \u0006)
		{
		}

		private void \u0003(object \u0003, EventArgs \u0006)
		{
			base.Dispatcher.BeginInvoke(new Action(\u000f\u2003));
			if (this.m_\u0006 == 1)
			{
				this.m_\u0006 = 0;
			}
			else
			{
				this.m_\u0006++;
			}
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		private void \u0006(object \u0003, EventArgs \u0006)
		{
		}

		private void \u0006\u2002()
		{
			base.Dispatcher.BeginInvoke(new Action(\u0002\u2003));
		}

		private void \u000f\u2002()
		{
			global::\u0008\u2002.\u0008.\u0003();
		}

		private void \u0002\u2002()
		{
			double num = 0.0;
			if (\u0003\u2007.IsChecked == true)
			{
				num = global::\u0008\u2002.\u0008.\u0006.\u0006\u2001;
			}
			else if (\u0006\u2007.IsChecked == true)
			{
				if (global::\u0008\u2002.\u0008.\u0008\u2002)
				{
					num = global::\u0008\u2002.\u0008.\u000f.\u0006\u2001;
				}
				if (global::\u0008\u2002.\u0008.\u0005\u2002)
				{
					num = global::\u0008\u2002.\u0008.\u0002.\u0006\u2001;
				}
			}
			if (num != 0.0)
			{
				num = 60.0 / (5.565217391304348E-05 * num);
				num *= 2.0;
				num = Math.Round(num, 0);
				num = Math.Round(num, 0);
			}
			\u0005\u2007\u2009.Text = num.ToString();
			double num2 = 255.0;
			if (\u0002\u2006\u2009.IsChecked == false)
			{
				num2 = this.m_\u0002;
			}
			int num3 = 0;
			if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
			{
				if (global::\u0008\u2002.\u000e.\u000e.\u0002\u2002 != 0)
				{
					num3 = ((!global::\u0008\u2002.\u0005.\u0006 || \u0002\u2006\u2009.IsChecked != false) ? ((int)Math.Round(num / (double)global::\u0008\u2002.\u000e.\u000e.\u0002\u2002 * 100.0, 0)) : ((int)Math.Round(num / (double)global::\u0008\u2002.\u000e.\u000e.\u0008\u2002 * 100.0, 0)));
				}
				else
				{
					global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812371));
					global::\u0008\u2002.\u000e.\u0003();
					num3 = 0;
				}
			}
			else if (\u0003\u2007.IsChecked == true)
			{
				num3 = (int)Math.Round((double)(int)global::\u0008\u2002.\u0008.\u0006.\u0008\u2001 / num2 * 100.0, 0);
			}
			else if (\u0006\u2007.IsChecked == true)
			{
				num3 = (int)Math.Round((double)(int)global::\u0008\u2002.\u0008.\u000f.\u0008\u2001 / num2 * 100.0, 0);
			}
			if (num3 > 100 && global::\u0008\u2002.\u0008.\u000e\u2002 == 1)
			{
				\u0008\u2007\u2009.Text = \u0002\u0005.\u0003(1031809764);
				\u0002\u2007\u2009.Visibility = Visibility.Hidden;
				Color color = Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
				Brush foreground = new SolidColorBrush(color);
				\u0008\u2007\u2009.Foreground = foreground;
			}
			else
			{
				Color color2 = Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
				Brush foreground2 = new SolidColorBrush(color2);
				\u0008\u2007\u2009.Foreground = foreground2;
				\u0002\u2007\u2009.Visibility = Visibility.Visible;
				if (num3 > 100)
				{
					\u0008\u2007\u2009.Text = \u0002\u0005.\u0003(1031809744);
				}
				else
				{
					\u0008\u2007\u2009.Text = num3.ToString();
				}
			}
			\u0003\u2007\u2009.\u0003(num3 / 2);
		}

		private void \u0008\u2002()
		{
			double num = 0.0;
			num = ((this.\u0003\u2003.IsChecked != true) ? ((double)global::\u0008\u2002.\u0008.\u0002.\u0006\u2001) : ((double)global::\u0008\u2002.\u0008.\u0003.\u0006\u2001));
			if (num != 0.0)
			{
				num = 60.0 / (5.565217391304348E-05 * num);
				num *= 2.0;
				num = Math.Round(num, 0);
			}
			\u000f\u200b\u2009.Text = num.ToString();
			int num2 = 0;
			if (!global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
			{
				num2 = ((\u0002\u2006\u2009.IsChecked != true) ? ((int)Math.Round((double)(int)global::\u0008\u2002.\u0008.\u0003.\u0008\u2001 / global::\u0008\u2002.\u0005.\u0003 * 100.0, 0)) : ((int)Math.Round((double)(int)global::\u0008\u2002.\u0008.\u0003.\u0008\u2001 / 255.0 * 100.0, 0)));
			}
			else if (global::\u0008\u2002.\u000e.\u0005.\u0002\u2002 != 0)
			{
				num2 = ((!global::\u0008\u2002.\u0005.\u0006 || \u0002\u2006\u2009.IsChecked != false) ? ((int)Math.Round(num / (double)global::\u0008\u2002.\u000e.\u0005.\u0002\u2002 * 100.0, 0)) : ((int)Math.Round(num / (double)global::\u0008\u2002.\u000e.\u0005.\u0008\u2002 * 100.0, 0)));
			}
			else
			{
				global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031809738));
				global::\u0008\u2002.\u000e.\u0003();
				num2 = 0;
			}
			if (num2 > 100 && global::\u0008\u2002.\u0008.\u000e\u2002 == 1)
			{
				\u0006\u200b\u2009.Text = \u0002\u0005.\u0003(1031809764);
				Color color = Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
				Brush foreground = new SolidColorBrush(color);
				\u0006\u200b\u2009.Foreground = foreground;
				\u0003\u200b\u2009.Visibility = Visibility.Hidden;
			}
			else
			{
				Color color2 = Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
				Brush foreground2 = new SolidColorBrush(color2);
				\u0006\u200b\u2009.Foreground = foreground2;
				\u0003\u200b\u2009.Visibility = Visibility.Visible;
				if (num2 > 100)
				{
					\u0006\u200b\u2009.Text = \u0002\u0005.\u0003(1031809744);
				}
				else
				{
					\u0006\u200b\u2009.Text = num2.ToString();
				}
			}
			\u0008\u2005\u2009.\u0003(num2 / 2);
			\u000f(num2);
		}

		private void \u0003(int \u0003)
		{
			double angle = (double)\u0003 * 1.8 + 225.0;
			RotateTransform renderTransform = new RotateTransform(angle);
			\u000f\u200b.RenderTransform = renderTransform;
			\u0002\u200b.RenderTransform = renderTransform;
			\u000e\u2005.Text = \u0003.ToString();
		}

		private void \u0006(int \u0003)
		{
			if (!(\u0008\u2000.Text == \u0003.ToString()))
			{
				if (\u0003 == 20)
				{
					\u0003 = 0;
					\u0008\u2000.Text = \u0002\u0005.\u0003(1031809691);
				}
				else
				{
					\u0008\u2000.Text = \u0003.ToString();
				}
				double angle = (double)\u0003 * 1.8 + 225.0;
				RotateTransform renderTransform = new RotateTransform(angle);
				\u0006\u2000.RenderTransform = renderTransform;
				\u000f\u2000.RenderTransform = renderTransform;
			}
		}

		private void \u000f(int \u0003)
		{
			if (!(\u0008\u2000.Text == \u0003.ToString()))
			{
				if (\u0003 > 100 && global::\u0008\u2002.\u0008.\u000e\u2002 == 1)
				{
					\u0003 = 110;
				}
				else if (\u0003 > 100)
				{
					\u0003 = 100;
				}
				double angle = (double)\u0003 * 1.8 + 225.0;
				RotateTransform renderTransform = new RotateTransform(angle);
				this.\u000e\u2001.RenderTransform = renderTransform;
				this.\u0003\u2009.RenderTransform = renderTransform;
				if (\u0003 > 100)
				{
					this.\u000f\u2009.Text = \u0002\u0005.\u0003(1031809764);
					this.\u0006\u2009.Visibility = Visibility.Hidden;
				}
				else
				{
					this.\u000f\u2009.Text = \u0003.ToString();
					this.\u0006\u2009.Visibility = Visibility.Visible;
				}
			}
		}

		public void \u0005\u2002()
		{
			global::\u0008\u2002.\u0008.\u0002();
			if (global::\u0008\u2002.\u0006.\u0003\u2009 || global::\u0008\u2002.\u0003.\u000f() == 0 || global::\u0008\u2002.\u0006.\u0006\u2009)
			{
				global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031814718));
				global::\u0008\u2002.\u0008.\u000e();
				global::\u0008\u2002.\u0008.\u0006();
				if (global::\u0008\u2002.\u0008.\u0005\u2009 && !global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
				{
					global::\u0008\u2002.\u0008.\u000f();
				}
				global::\u0008\u2002.\u0005.\u0003(\u0006\u0005.\u0003.Auto);
				global::\u0008\u2002.\u0008.\u0006(0);
				global::\u0008\u2002.\u0003.\u000f(1);
			}
			if (global::\u0008\u2002.\u0008.\u000e == 1)
			{
				\u0008\u200a\u2009.IsChecked = true;
				\u0005\u2008.Visibility = Visibility.Hidden;
				\u000e\u2002\u2009.Visibility = Visibility.Hidden;
				\u000f\u2003\u2009.Visibility = Visibility.Hidden;
			}
			else if (global::\u0008\u2002.\u0008.\u000e == 5)
			{
				\u000e\u200a\u2009.IsChecked = true;
				\u0005\u2008.Visibility = Visibility.Hidden;
				\u000e\u2002\u2009.Visibility = Visibility.Hidden;
				\u000f\u2003\u2009.Visibility = Visibility.Hidden;
			}
			else if (global::\u0008\u2002.\u0008.\u000e == 6)
			{
				\u0002\u2006\u2009.IsChecked = true;
				\u0005\u2008.Visibility = Visibility.Hidden;
				if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
				{
					\u000e\u2002\u2009.Visibility = Visibility.Hidden;
				}
				else
				{
					\u000f\u2003\u2009.Visibility = Visibility.Hidden;
				}
			}
			else
			{
				\u0003\u200a\u2009.IsChecked = true;
				if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
				{
					\u0005\u2008.Visibility = Visibility.Hidden;
				}
				else
				{
					\u0005\u2008.Visibility = Visibility.Visible;
				}
				\u000e\u2002\u2009.Visibility = Visibility.Hidden;
				\u000f\u2003\u2009.Visibility = Visibility.Hidden;
			}
			if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
			{
				global::\u0008\u2002.\u0006.\u000e\u2001 = false;
			}
			if (!global::\u0008\u2002.\u0006.\u000e\u2001)
			{
				\u0005\u2008.Visibility = Visibility.Hidden;
				if (global::\u0008\u2002.\u0008.\u0003\u2002 != 0)
				{
					global::\u0008\u2002.\u0008.\u0003\u2002 = 0;
					global::\u0008\u2002.\u0008.\u0006(0);
				}
			}
			int num = 100;
			List<decimal> list = new List<decimal>();
			for (int i = 0; i <= num; i++)
			{
				list.Add(i);
			}
			\u0008\u2002\u2009.Maximum = list.Count - 1;
			\u000f\u2002\u2009.Text = list[0].ToString();
			\u0002\u2002\u2009.Text = list[list.Count - 1].ToString();
			\u0005\u2002\u2009.ItemsSource = list;
			\u0005\u2002\u2009.Text = global::\u0008\u2002.\u0008.\u0003\u2002.ToString();
			if (!global::\u0008\u2002.\u0006.\u0002\u2001)
			{
				\u0005\u200b\u2009.Children.Remove(\u0005\u200a\u2009);
			}
			if (!global::\u0008\u2002.\u0006.\u0006\u2002)
			{
				\u0005\u200b\u2009.Children.Remove(\u0003\u2006\u2009);
			}
			if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0003\u2002)
			{
				global::\u0008\u2002.\u0005.\u0003();
				global::\u0008\u2002.\u0005.\u0006();
			}
			if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
			{
				global::\u0008\u2002.\u000e = new \u000f\u0005();
				global::\u0008\u2002.\u000e.\u0003();
				this.\u000e\u2009.Visibility = Visibility.Hidden;
				\u0005\u2000.Visibility = Visibility.Hidden;
				this.\u0006\u2002.Visibility = Visibility.Hidden;
				\u000e\u2000\u2009.Visibility = Visibility.Hidden;
				\u0008\u2003.Visibility = Visibility.Hidden;
				if (global::\u0008\u2002.\u0008.\u000f\u2002)
				{
					if (global::\u0008\u2002.\u0008.\u0005\u2009)
					{
						\u0005\u2003\u2009.\u0003(\u0006\u2003);
					}
					if (!global::\u0008\u2002.\u0008.\u0002\u2002)
					{
						this.\u0006\u2002.Visibility = Visibility.Visible;
						\u0002\u2005\u2009.Visibility = Visibility.Hidden;
						if (global::\u0008\u2002.\u0008.\u0005\u2002)
						{
							this.\u000e\u2009.Visibility = Visibility.Visible;
						}
					}
				}
				if (global::\u0008\u2002.\u0008.\u0002\u2002)
				{
					if (global::\u0008\u2002.\u0008.\u0005\u2009)
					{
						\u0003\u2004\u2009.\u0003(\u0006\u2003);
					}
					\u0002\u2005\u2009.Visibility = Visibility.Visible;
					\u000e\u2000\u2009.Visibility = Visibility.Visible;
					\u0008\u2003.Visibility = Visibility.Visible;
					if (global::\u0008\u2002.\u0008.\u0005\u2002)
					{
						\u0005\u2000.Visibility = Visibility.Visible;
					}
				}
				else
				{
					\u000e\u2003\u2009.Visibility = Visibility.Collapsed;
				}
				if (global::\u0008\u2002.\u0008.\u0008\u2002)
				{
					if (global::\u0008\u2002.\u0008.\u0005\u2009)
					{
						\u000f\u2004\u2009.\u0003(\u0006\u2003);
					}
					\u0005\u2000.Visibility = Visibility.Visible;
				}
				else
				{
					\u0006\u2004\u2009.Visibility = Visibility.Collapsed;
				}
				if (global::\u0008\u2002.\u0008.\u0005\u2002)
				{
					if (global::\u0008\u2002.\u0008.\u0005\u2009)
					{
						\u0008\u2004\u2009.\u0003(\u0006\u2003);
					}
					if (!global::\u0008\u2002.\u0008.\u0002\u2002)
					{
						\u0008\u2003\u2009.Header = \u0002\u0005.\u0003(1031809685);
						\u0002\u2004\u2009.Header = \u0002\u0005.\u0003(1031809665);
					}
					else
					{
						\u000e\u2003\u2009.Header = \u0002\u0005.\u0003(1031809685);
						\u0002\u2004\u2009.Header = \u0002\u0005.\u0003(1031809665);
					}
				}
				else
				{
					\u0002\u2004\u2009.Visibility = Visibility.Collapsed;
				}
				if (!global::\u0008\u2002.\u0008.\u0005\u2009)
				{
					\u0005\u200b\u2009.Children.Remove(\u000f\u2006\u2009);
				}
			}
			else
			{
				\u0003\u2007.IsChecked = true;
				\u0006\u2007.IsChecked = false;
				this.\u0006\u2002.Visibility = Visibility.Hidden;
				this.\u000e\u2009.Visibility = Visibility.Hidden;
				\u0005\u2000.Visibility = Visibility.Hidden;
				if (global::\u0008\u2002.\u0008.\u0008 == 2)
				{
					if (global::\u0008\u2002.\u0008.\u0005\u2009)
					{
						\u0006\u2009\u2009.\u0003(global::\u0008\u2002.\u0008.\u0003);
						\u0005\u2001\u2009.\u0003(global::\u0008\u2002.\u0008.\u0006);
						\u000f\u2001\u2009.Visibility = Visibility.Hidden;
						\u0008\u2009\u2009.Visibility = Visibility.Hidden;
						\u0006\u2009\u2009.\u0003(this.\u000f\u2001);
						\u0005\u2001\u2009.\u0003(this.\u0002\u2001);
					}
					else
					{
						\u0005\u200b\u2009.Children.Remove(\u000f\u2006\u2009);
					}
				}
				else if (global::\u0008\u2002.\u0008.\u0008 == 3)
				{
					\u0005\u2000.Visibility = Visibility.Visible;
					if (global::\u0008\u2002.\u0008.\u0005\u2009)
					{
						\u0006\u2009\u2009.\u0003(global::\u0008\u2002.\u0008.\u0003);
						\u0005\u2001\u2009.\u0003(global::\u0008\u2002.\u0008.\u0006);
						\u000f\u2001\u2009.\u0003(global::\u0008\u2002.\u0008.\u000f);
						\u0005\u2001\u2009.\u0003(this.\u0002\u2001);
						\u000f\u2001\u2009.\u0003(this.\u0008\u2001);
					}
					else
					{
						\u0005\u200b\u2009.Children.Remove(\u000f\u2006\u2009);
					}
				}
				else
				{
					\u0005\u200b\u2009.Children.Remove(\u000f\u2006\u2009);
					this.\u0006\u2002.Visibility = Visibility.Visible;
					\u000e\u2000\u2009.Visibility = Visibility.Hidden;
					\u0002\u2005\u2009.Visibility = Visibility.Hidden;
					\u0008\u2003.Visibility = Visibility.Hidden;
				}
			}
			if (global::\u0008\u2002.\u0006.\u0005\u2001)
			{
				if (global::\u0008\u2002.\u0008.\u0003\u2009 == 1)
				{
					\u0006\u200a.IsChecked = true;
				}
				else
				{
					\u0006\u200a.IsChecked = false;
				}
				if (global::\u0008\u2002.\u0008.\u000e\u2001 == 0)
				{
					\u000f\u200a.IsChecked = false;
				}
				else
				{
					if ((global::\u0008\u2002.\u0008.\u000e\u2001 & 1) == 1)
					{
						\u0002\u200a.IsChecked = true;
					}
					else
					{
						\u0002\u200a.IsChecked = false;
					}
					if ((global::\u0008\u2002.\u0008.\u000e\u2001 & 2) == 2)
					{
						\u0008\u200a.IsChecked = true;
					}
					else
					{
						\u0008\u200a.IsChecked = false;
					}
					if ((global::\u0008\u2002.\u0008.\u000e\u2001 & 4) == 4)
					{
						\u0005\u200a.IsChecked = true;
					}
					else
					{
						\u0005\u200a.IsChecked = false;
					}
					if ((global::\u0008\u2002.\u0008.\u000e\u2001 & 8) == 8)
					{
						\u000e\u200a.IsChecked = true;
					}
					else
					{
						\u000e\u200a.IsChecked = false;
					}
					if ((global::\u0008\u2002.\u0008.\u000e\u2001 & 0x10) == 16)
					{
						\u0003\u2006.IsChecked = true;
					}
					else
					{
						\u0003\u2006.IsChecked = false;
					}
					if ((global::\u0008\u2002.\u0008.\u000e\u2001 & 0x20) == 32)
					{
						\u0006\u2006.IsChecked = true;
					}
					else
					{
						\u0006\u2006.IsChecked = false;
					}
					if ((global::\u0008\u2002.\u0008.\u000e\u2001 & 0x40) == 64)
					{
						\u000f\u2006.IsChecked = true;
					}
					else
					{
						\u000f\u2006.IsChecked = false;
					}
				}
				if (global::\u0008\u2002.\u0008.\u0006\u2009 >= 12)
				{
					\u0005\u2006.SelectedIndex = global::\u0008\u2002.\u0008.\u0006\u2009 - 12;
					\u0008\u2006.SelectedIndex = 1;
				}
				else
				{
					\u0005\u2006.SelectedIndex = global::\u0008\u2002.\u0008.\u0006\u2009;
					\u0008\u2006.SelectedIndex = 0;
				}
			}
			else
			{
				\u0003\u2008.Visibility = Visibility.Hidden;
			}
		}

		public void \u000e\u2002()
		{
			byte[] array = global::\u0008\u2002.\u0002.\u0003(1, 1, 1);
			global::\u0008\u2002.\u0008.\u000f\u2009 = array[0];
			\u0003\u200a\u2009.IsEnabled = true;
			\u0008\u200a\u2009.IsEnabled = true;
			\u0002\u2006\u2009.IsEnabled = true;
			\u000e\u2007\u2009.Visibility = Visibility.Hidden;
			\u0003\u2005\u2009.Visibility = Visibility.Hidden;
			\u0008\u200b\u2009.Visibility = Visibility.Hidden;
			\u0002\u200b\u2009.Visibility = Visibility.Hidden;
			this.\u000f\u2002.Visibility = Visibility.Hidden;
			this.\u0005\u2009.Visibility = Visibility.Hidden;
			global::\u0008\u2002.\u0008.\u000e\u2002 = 0;
			\u0008\u2006\u2009.Visibility = Visibility.Collapsed;
			\u000f\u2000\u2009.Visibility = Visibility.Collapsed;
			\u000e\u2006\u2009.Visibility = Visibility.Collapsed;
			\u0006\u200a\u2009.Visibility = Visibility.Collapsed;
			if (global::\u0008\u2002.\u0006.\u0005\u2009)
			{
				byte[] array2 = global::\u0008\u2002.\u0002.\u0003(4, 5, 1);
				global::\u0008\u2002.\u0008.\u000e = array2[0];
				if (array[0] == 3)
				{
					if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0002\u2009)
					{
						\u0008\u2006\u2009.Visibility = Visibility.Visible;
					}
					if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0002\u2001)
					{
						\u0006\u200a\u2009.Visibility = Visibility.Visible;
					}
					if (global::\u0008\u2002.\u0008.\u000e == 1)
					{
						\u0008\u200a\u2009.IsChecked = true;
					}
					else if (global::\u0008\u2002.\u0008.\u000e == 5)
					{
						\u000e\u200a\u2009.IsChecked = true;
					}
					else if (global::\u0008\u2002.\u0008.\u000e == 6)
					{
						\u0002\u2006\u2009.IsChecked = true;
					}
					else if (global::\u0008\u2002.\u0008.\u000e == 9)
					{
						\u0005\u2006\u2009.IsChecked = true;
					}
					else
					{
						\u0003\u200a\u2009.IsChecked = true;
						if (global::\u0008\u2002.\u0005.\u0003() == 1)
						{
							\u000f\u200a\u2009.IsChecked = true;
						}
						else
						{
							\u000f\u200a\u2009.IsChecked = false;
						}
					}
				}
				else if (array[0] == 2)
				{
					if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0003\u2002)
					{
						\u000e\u2006\u2009.Visibility = Visibility.Visible;
					}
					byte[] array3 = global::\u0008\u2002.\u0002.\u0003(4, 8, 1);
					if (array3[0] == 1)
					{
						global::\u0008\u2002.\u0008.\u000e\u2002 = 1;
						\u0003\u2008\u2009.IsChecked = true;
					}
					else if (global::\u0008\u2002.\u0008.\u000e == 1)
					{
						\u0008\u200a\u2009.IsChecked = true;
					}
					else if (global::\u0008\u2002.\u0008.\u000e == 5)
					{
						\u000e\u200a\u2009.IsChecked = true;
					}
					else if (global::\u0008\u2002.\u0008.\u000e == 6)
					{
						\u0002\u2006\u2009.IsChecked = true;
					}
					else
					{
						\u0003\u200a\u2009.IsChecked = true;
					}
				}
				else if (global::\u0008\u2002.\u0008.\u000e == 1)
				{
					\u0008\u200a\u2009.IsChecked = true;
				}
				else if (global::\u0008\u2002.\u0008.\u000e == 5)
				{
					\u000e\u200a\u2009.IsChecked = true;
				}
				else if (global::\u0008\u2002.\u0008.\u000e == 6)
				{
					\u0002\u2006\u2009.IsChecked = true;
				}
				else
				{
					\u0003\u200a\u2009.IsChecked = true;
				}
			}
			else if (array[0] == 0)
			{
				if (global::\u0008\u2002.\u0006.\u0008\u2001 == 5)
				{
					\u0006\u2006\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812242));
				}
				else
				{
					\u0006\u2006\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812602));
				}
				byte[] array4 = global::\u0008\u2002.\u0002.\u0003(4, 5, 1);
				global::\u0008\u2002.\u0008.\u000e = array4[0];
				if (global::\u0008\u2002.\u0008.\u000e == 1)
				{
					\u0008\u200a\u2009.IsChecked = true;
				}
				else if (global::\u0008\u2002.\u0008.\u000e == 5)
				{
					\u000e\u200a\u2009.IsChecked = true;
				}
				else if (global::\u0008\u2002.\u0008.\u000e == 6)
				{
					\u0002\u2006\u2009.IsChecked = true;
				}
				else
				{
					\u0003\u200a\u2009.IsChecked = true;
				}
			}
			else if (array[0] == 2)
			{
				\u0003\u200a\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812242));
				if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0003\u2002)
				{
					byte[] array5 = global::\u0008\u2002.\u0002.\u0003(4, 8, 1);
					if (array5[0] == 1)
					{
						this.\u000f\u2002.Visibility = Visibility.Visible;
						this.\u0005\u2009.Visibility = Visibility.Visible;
						\u000e\u2007\u2009.Visibility = Visibility.Visible;
						\u0003\u2005\u2009.Visibility = Visibility.Visible;
						\u0008\u200b\u2009.Visibility = Visibility.Visible;
						\u0002\u200b\u2009.Visibility = Visibility.Visible;
						\u0002\u2006\u2009.IsEnabled = false;
						global::\u0008\u2002.\u0008.\u000e\u2002 = 1;
					}
					else
					{
						this.\u000f\u2002.Visibility = Visibility.Hidden;
						this.\u0005\u2009.Visibility = Visibility.Hidden;
						\u000e\u2007\u2009.Visibility = Visibility.Hidden;
						\u0003\u2005\u2009.Visibility = Visibility.Hidden;
						\u0008\u200b\u2009.Visibility = Visibility.Hidden;
						\u0002\u200b\u2009.Visibility = Visibility.Hidden;
					}
				}
				byte[] array6 = global::\u0008\u2002.\u0002.\u0003(4, 5, 1);
				global::\u0008\u2002.\u0008.\u000e = array6[0];
				if (global::\u0008\u2002.\u0008.\u000e == 1)
				{
					\u0008\u200a\u2009.IsChecked = true;
				}
				else if (global::\u0008\u2002.\u0008.\u000e == 5)
				{
					\u000e\u200a\u2009.IsChecked = true;
				}
				else if (global::\u0008\u2002.\u0008.\u000e == 6)
				{
					\u0002\u2006\u2009.IsChecked = true;
				}
				else
				{
					\u0003\u200a\u2009.IsChecked = true;
				}
			}
			else
			{
				\u0003\u200a\u2009.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812242));
				byte[] array7 = global::\u0008\u2002.\u0002.\u0003(4, 5, 1);
				global::\u0008\u2002.\u0008.\u000e = array7[0];
				if (global::\u0008\u2002.\u0008.\u000e == 1)
				{
					\u0008\u200a\u2009.IsChecked = true;
				}
				else if (global::\u0008\u2002.\u0008.\u000e == 5)
				{
					\u000e\u200a\u2009.IsChecked = true;
				}
				else if (global::\u0008\u2002.\u0008.\u000e == 6)
				{
					\u0002\u2006\u2009.IsChecked = true;
				}
				else
				{
					\u0003\u200a\u2009.IsChecked = true;
				}
			}
			if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001 && \u0002\u2006\u2009.IsChecked == true)
			{
				\u0003\u2003();
			}
		}

		public void \u0003\u2001()
		{
			this.\u000f\u2002.Visibility = Visibility.Visible;
			this.\u0005\u2009.Visibility = Visibility.Visible;
			\u000e\u2007\u2009.Visibility = Visibility.Visible;
			\u0003\u2005\u2009.Visibility = Visibility.Visible;
			\u0008\u200b\u2009.Visibility = Visibility.Visible;
			\u0002\u200b\u2009.Visibility = Visibility.Visible;
			\u0002\u2006\u2009.IsEnabled = false;
			\u0003\u200a\u2009.IsChecked = true;
			global::\u0008\u2002.\u0008.\u000e\u2002 = 1;
		}

		public void \u0006\u2001()
		{
			this.\u000f\u2002.Visibility = Visibility.Hidden;
			this.\u0005\u2009.Visibility = Visibility.Hidden;
			\u000e\u2007\u2009.Visibility = Visibility.Hidden;
			\u0003\u2005\u2009.Visibility = Visibility.Hidden;
			\u0008\u200b\u2009.Visibility = Visibility.Hidden;
			\u0002\u200b\u2009.Visibility = Visibility.Hidden;
			\u0002\u2006\u2009.IsEnabled = true;
			\u0003\u200a\u2009.IsChecked = true;
			global::\u0008\u2002.\u0008.\u000e\u2002 = 0;
		}

		private void \u0003(object \u0003, SelectionChangedEventArgs \u0006)
		{
			try
			{
				\u0008\u2002\u2009.Value = \u0005\u2002\u2009.SelectedIndex;
			}
			catch
			{
			}
		}

		private void \u0003(object \u0003, RoutedPropertyChangedEventArgs<double> \u0006)
		{
			\u0005\u2002\u2009.SelectedIndex = (int)\u0008\u2002\u2009.Value;
			if (this.m_\u000f)
			{
				byte b = Convert.ToByte(\u0005\u2002\u2009.SelectedValue);
				global::\u0008\u2002.\u0008.\u0006(b);
			}
		}

		private void \u0008(object \u0003, RoutedEventArgs \u0006)
		{
			if (this.m_\u000f)
			{
				if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
				{
					\u000f\u2003\u2009.Visibility = Visibility.Hidden;
				}
				else
				{
					\u000e\u2002\u2009.Visibility = Visibility.Hidden;
				}
				if (global::\u0008\u2002.\u0006.\u000e\u2001)
				{
					\u0005\u2008.Visibility = Visibility.Visible;
				}
				\u0005\u2009\u2009.Visibility = Visibility.Hidden;
				\u000f\u2000\u2009.Visibility = Visibility.Hidden;
			}
		}

		private void \u0005(object \u0003, RoutedEventArgs \u0006)
		{
			if (\u0003\u200a\u2009.IsChecked != true)
			{
				return;
			}
			if (global::\u0008\u2002.\u0008.\u000f\u2009 == 3)
			{
				if (global::\u0008\u2002.\u0005.\u0003() == 1)
				{
					global::\u0008\u2002.\u0005.\u0003(\u0003: true);
					\u000f\u200a\u2009.IsChecked = true;
				}
				else
				{
					global::\u0008\u2002.\u0005.\u0003(\u0003: false);
					\u000f\u200a\u2009.IsChecked = false;
				}
			}
			else if (global::\u0008\u2002.\u0008.\u000f\u2009 == 2 && global::\u0008\u2002.\u0006.\u000e\u2009.\u0003\u2002 && global::\u0008\u2002.\u0006.\u0005\u2009 && global::\u0008\u2002.\u0008.\u000e\u2002 == 1)
			{
				global::\u0008\u2002.\u0005.\u0006(\u0003: false);
				global::\u0008\u2002.\u0005.\u0003(\u0006\u0005.\u0003.Auto);
			}
			else
			{
				global::\u0008\u2002.\u0005.\u0003(\u0006\u0005.\u0003.Auto);
			}
		}

		private void \u000e(object \u0003, RoutedEventArgs \u0006)
		{
			if (this.m_\u000f)
			{
				if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
				{
					\u000f\u2003\u2009.Visibility = Visibility.Hidden;
				}
				else
				{
					\u000e\u2002\u2009.Visibility = Visibility.Hidden;
				}
				\u0005\u2008.Visibility = Visibility.Hidden;
				\u0005\u2009\u2009.Visibility = Visibility.Hidden;
				\u000f\u2000\u2009.Visibility = Visibility.Hidden;
			}
		}

		private void \u0003\u2002(object \u0003, RoutedEventArgs \u0006)
		{
			if (\u0008\u200a\u2009.IsChecked == true)
			{
				if (global::\u0008\u2002.\u0008.\u000f\u2009 == 2 && global::\u0008\u2002.\u0006.\u000e\u2009.\u0003\u2002 && global::\u0008\u2002.\u0006.\u0005\u2009 && global::\u0008\u2002.\u0008.\u000e\u2002 == 1)
				{
					global::\u0008\u2002.\u0005.\u0006(\u0003: false);
				}
				global::\u0008\u2002.\u0005.\u0003(\u0006\u0005.\u0003.Max);
			}
		}

		private void \u0006\u2002(object \u0003, RoutedEventArgs \u0006)
		{
			if (this.m_\u000f)
			{
				if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
				{
					\u000f\u2003\u2009.Visibility = Visibility.Hidden;
				}
				else
				{
					\u000e\u2002\u2009.Visibility = Visibility.Hidden;
				}
				\u0005\u2008.Visibility = Visibility.Hidden;
				\u0005\u2009\u2009.Visibility = Visibility.Hidden;
				\u000f\u2000\u2009.Visibility = Visibility.Hidden;
			}
		}

		private void \u000f\u2002(object \u0003, RoutedEventArgs \u0006)
		{
			if (\u000e\u200a\u2009.IsChecked == true)
			{
				if (global::\u0008\u2002.\u0008.\u000f\u2009 == 2 && global::\u0008\u2002.\u0006.\u000e\u2009.\u0003\u2002 && global::\u0008\u2002.\u0006.\u0005\u2009 && global::\u0008\u2002.\u0008.\u000e\u2002 == 1)
				{
					global::\u0008\u2002.\u0005.\u0006(\u0003: false);
				}
				global::\u0008\u2002.\u0005.\u0003(\u0006\u0005.\u0003.Maxq);
			}
		}

		private void \u0002\u2002(object \u0003, RoutedEventArgs \u0006)
		{
			if (this.m_\u000f)
			{
				if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
				{
					\u000f\u2003\u2009.Visibility = Visibility.Hidden;
				}
				else
				{
					\u000e\u2002\u2009.Visibility = Visibility.Hidden;
				}
				\u0005\u2008.Visibility = Visibility.Hidden;
				\u0005\u2009\u2009.Visibility = Visibility.Hidden;
				\u000f\u2000\u2009.Visibility = Visibility.Hidden;
			}
		}

		private void \u0008\u2002(object \u0003, RoutedEventArgs \u0006)
		{
			if (\u0006\u2006\u2009.IsChecked == true)
			{
				global::\u0008\u2002.\u0005.\u0003(\u0006\u0005.\u0003.silen);
			}
		}

		private void \u0005\u2002(object \u0003, RoutedEventArgs \u0006)
		{
			if (\u0005\u2006\u2009.IsChecked == true)
			{
				if (global::\u0008\u2002.\u0008.\u000f\u2009 == 2 && global::\u0008\u2002.\u0006.\u000e\u2009.\u0003\u2002 && global::\u0008\u2002.\u0006.\u0005\u2009 && global::\u0008\u2002.\u0008.\u000e\u2002 == 1)
				{
					global::\u0008\u2002.\u0005.\u0006(\u0003: false);
				}
				global::\u0008\u2002.\u0005.\u0003(\u0006\u0005.\u0003.IFSC);
			}
		}

		private void \u000e\u2002(object \u0003, RoutedEventArgs \u0006)
		{
			if (this.m_\u000f)
			{
				if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
				{
					\u000f\u2003\u2009.Visibility = Visibility.Hidden;
				}
				else
				{
					\u000e\u2002\u2009.Visibility = Visibility.Hidden;
				}
				if (global::\u0008\u2002.\u0006.\u000e\u2001)
				{
					\u0005\u2008.Visibility = Visibility.Visible;
				}
				\u0005\u2009\u2009.Visibility = Visibility.Hidden;
				\u000f\u2000\u2009.Visibility = Visibility.Visible;
			}
		}

		private void \u0003\u2001(object \u0003, RoutedEventArgs \u0006)
		{
			if (\u0003\u2008\u2009.IsChecked == true)
			{
				global::\u0008\u2002.\u0005.\u0006(\u0003: true);
			}
		}

		private void \u0006\u2001(object \u0003, RoutedEventArgs \u0006)
		{
			if (this.m_\u000f)
			{
				if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
				{
					\u000f\u2003\u2009.Visibility = Visibility.Hidden;
				}
				else
				{
					\u000e\u2002\u2009.Visibility = Visibility.Hidden;
				}
				if (global::\u0008\u2002.\u0006.\u000e\u2001)
				{
					\u0005\u2008.Visibility = Visibility.Visible;
				}
				\u0005\u2009\u2009.Visibility = Visibility.Hidden;
				\u000f\u2000\u2009.Visibility = Visibility.Hidden;
			}
		}

		private void \u000f\u2001(object \u0003, RoutedEventArgs \u0006)
		{
			if (\u000f\u200a\u2009.IsChecked == true)
			{
				global::\u0008\u2002.\u0005.\u0003(\u0003: true);
			}
			else
			{
				global::\u0008\u2002.\u0005.\u0003(\u0003: false);
			}
		}

		private void \u0002\u2001(object \u0003, RoutedEventArgs \u0006)
		{
		}

		private void \u0008\u2001(object \u0003, RoutedEventArgs \u0006)
		{
			if (this.m_\u000f)
			{
				if (global::\u0008\u2002.\u0006.\u000e\u2009.\u0005\u2001)
				{
					\u000f\u2003\u2009.Visibility = Visibility.Visible;
					\u0003\u2003();
				}
				else
				{
					\u000e\u2002\u2009.Visibility = Visibility.Visible;
				}
				\u0005\u2008.Visibility = Visibility.Hidden;
				\u0005\u2009\u2009.Visibility = Visibility.Visible;
				\u000f\u2000\u2009.Visibility = Visibility.Hidden;
			}
		}

		private void \u0005\u2001(object \u0003, RoutedEventArgs \u0006)
		{
			if (\u0002\u2006\u2009.IsChecked == true)
			{
				global::\u0008\u2002.\u0005.\u0003(\u0006\u0005.\u0003.Custom);
			}
		}

		private void \u000e\u2001(object \u0003, RoutedEventArgs \u0006)
		{
			\u000e\u2001\u2009.IsEnabled = false;
			\u0006\u2009\u2009.\u0006(1);
			\u0005\u2001\u2009.\u0006(2);
			\u000f\u2001\u2009.\u0006(3);
			global::\u0008\u2002.\u0008.\u000f();
			\u0006\u2003\u2009.IsEnabled = false;
			\u000e\u2001\u2009.IsEnabled = true;
		}

		private void \u0003\u2009(object \u0003, RoutedEventArgs \u0006)
		{
			switch (global::\u0008\u2002.\u0008.\u0008)
			{
			case 1:
				\u0006\u2009\u2009.\u000f(1);
				\u0006\u2009\u2009.\u0003(0);
				break;
			case 2:
				\u0006\u2009\u2009.\u000f(1);
				\u0005\u2001\u2009.\u000f(2);
				\u0006\u2009\u2009.\u0003(0);
				\u0005\u2001\u2009.\u0003(0);
				break;
			default:
				\u0006\u2009\u2009.\u000f(1);
				\u0005\u2001\u2009.\u000f(2);
				\u000f\u2001\u2009.\u000f(3);
				\u0006\u2009\u2009.\u0003(0);
				\u0005\u2001\u2009.\u0003(0);
				\u000f\u2001\u2009.\u0003(0);
				break;
			}
			global::\u0008\u2002.\u0008.\u000f();
			\u0006\u2003\u2009.IsEnabled = false;
		}

		private void i1iIIiiIl1ll(object \u0003, System.Windows.Input.MouseEventArgs \u0006)
		{
			object tag = \u0006\u2009\u2009.Tag;
			object tag2 = \u0005\u2001\u2009.Tag;
			object tag3 = \u000f\u2001\u2009.Tag;
			if (tag.ToString() == \u0002\u0005.\u0003(1031811328) || tag2.ToString() == \u0002\u0005.\u0003(1031811328) || tag3.ToString() == \u0002\u0005.\u0003(1031811328))
			{
				\u0006\u2003\u2009.IsEnabled = true;
			}
		}

		private void \u0006\u2009(object \u0003, RoutedEventArgs \u0006)
		{
			\u000e\u2001\u2009.Children.RemoveAt(0);
			\u0002\u2001\u2009.Children.RemoveAt(0);
			\u0003\u2001\u2009.Children.RemoveAt(0);
			\u000e\u2001\u2009.Children.Add(\u0003\u2009\u2009);
			\u0002\u2001\u2009.Children.Add(\u0008\u2001\u2009);
			\u0003\u2001\u2009.Children.Add(\u0006\u2001\u2009);
			\u0005\u2001\u2009.\u0003(0);
		}

		private void \u000f\u2009(object \u0003, RoutedEventArgs \u0006)
		{
			\u000e\u2001\u2009.Children.RemoveAt(0);
			\u0002\u2001\u2009.Children.RemoveAt(0);
			\u0003\u2001\u2009.Children.RemoveAt(0);
			\u000e\u2001\u2009.Children.Add(\u0008\u2001\u2009);
			\u0002\u2001\u2009.Children.Add(\u0003\u2009\u2009);
			\u0003\u2001\u2009.Children.Add(\u0006\u2001\u2009);
			\u0006\u2009\u2009.\u0003(0);
			\u000f\u2001\u2009.\u0003(0);
		}

		private void \u0002\u2009(object \u0003, RoutedEventArgs \u0006)
		{
			\u000e\u2001\u2009.Children.RemoveAt(0);
			\u0002\u2001\u2009.Children.RemoveAt(0);
			\u0003\u2001\u2009.Children.RemoveAt(0);
			\u000e\u2001\u2009.Children.Add(\u0006\u2001\u2009);
			\u0002\u2001\u2009.Children.Add(\u0008\u2001\u2009);
			\u0003\u2001\u2009.Children.Add(\u0003\u2009\u2009);
			\u0006\u2009\u2009.\u0003(0);
			\u0005\u2001\u2009.\u0003(0);
		}

		private void \u000f\u2001()
		{
			if (\u000f\u2009\u2009.IsChecked != true)
			{
				\u000e\u2001\u2009.Children.RemoveAt(0);
				\u0002\u2001\u2009.Children.RemoveAt(0);
				\u0003\u2001\u2009.Children.RemoveAt(0);
				\u000e\u2001\u2009.Children.Add(\u0003\u2009\u2009);
				\u0002\u2001\u2009.Children.Add(\u0008\u2001\u2009);
				\u0003\u2001\u2009.Children.Add(\u0006\u2001\u2009);
				\u0005\u2001\u2009.\u0003(0);
				\u000f\u2009\u2009.IsChecked = true;
			}
		}

		private void \u0002\u2001()
		{
			if (\u0002\u2009\u2009.IsChecked != true)
			{
				\u000e\u2001\u2009.Children.RemoveAt(0);
				\u0002\u2001\u2009.Children.RemoveAt(0);
				\u0003\u2001\u2009.Children.RemoveAt(0);
				\u000e\u2001\u2009.Children.Add(\u0008\u2001\u2009);
				\u0002\u2001\u2009.Children.Add(\u0003\u2009\u2009);
				\u0003\u2001\u2009.Children.Add(\u0006\u2001\u2009);
				\u0006\u2009\u2009.\u0003(0);
				\u000f\u2001\u2009.\u0003(0);
				\u0002\u2009\u2009.IsChecked = true;
			}
		}

		private void \u0008\u2001()
		{
			\u000e\u2001\u2009.Children.RemoveAt(0);
			\u0002\u2001\u2009.Children.RemoveAt(0);
			\u0003\u2001\u2009.Children.RemoveAt(0);
			\u000e\u2001\u2009.Children.Add(\u0006\u2001\u2009);
			\u0002\u2001\u2009.Children.Add(\u0008\u2001\u2009);
			\u0003\u2001\u2009.Children.Add(\u0003\u2009\u2009);
			\u0006\u2009\u2009.\u0003(0);
			\u0005\u2001\u2009.\u0003(0);
			\u0008\u2009\u2009.IsChecked = true;
		}

		private void \u0005\u2001()
		{
			base.Dispatcher.BeginInvoke(new Action(l1illIiliii11.\u0003.\u0003.\u0003));
		}

		private void \u000e\u2001()
		{
		}

		private void \u0008\u2009(object \u0003, RoutedEventArgs \u0006)
		{
		}

		private void \u0005\u2009(object \u0003, RoutedEventArgs \u0006)
		{
		}

		private void \u000e\u2009(object \u0003, RoutedEventArgs \u0006)
		{
			if (\u0005\u200b.Visibility == Visibility.Visible)
			{
				\u0005\u200b.Visibility = Visibility.Hidden;
				\u000f\u2008.Visibility = Visibility.Visible;
				\u0005\u200b\u2009.Visibility = Visibility.Visible;
				Color color = Color.FromArgb(byte.MaxValue, 39, 39, 39);
				Brush background = new SolidColorBrush(color);
				\u0006\u2008.Background = background;
				\u000f\u2005\u2009.Height = global::\u0008\u2002.\u0003.\u0008;
				\u000f\u2005\u2009.Width = global::\u0008\u2002.\u0003.\u0005;
				\u0005\u2000\u2009.Height = global::\u0008\u2002.\u0003.\u0008;
				\u0005\u2000\u2009.Width = global::\u0008\u2002.\u0003.\u0005;
				this.\u000f\u2003.Width = global::\u0008\u2002.\u0003.\u000e;
				this.\u000f\u2003.Height = global::\u0008\u2002.\u0003.\u0003\u2002;
			}
			else
			{
				\u0003\u2009();
			}
		}

		private void \u0003\u2009()
		{
			\u0005\u200b.Visibility = Visibility.Visible;
			\u000f\u2008.Visibility = Visibility.Hidden;
			\u0005\u200b\u2009.Visibility = Visibility.Hidden;
			\u0006\u2008.Background = this.\u0003\u2002.Fill;
			\u000f\u2005\u2009.Height = global::\u0008\u2002.\u0003.\u0008 * 1.45;
			\u000f\u2005\u2009.Width = global::\u0008\u2002.\u0003.\u0005 * 1.45;
			\u0005\u2000\u2009.Height = global::\u0008\u2002.\u0003.\u0008 * 1.45;
			\u0005\u2000\u2009.Width = global::\u0008\u2002.\u0003.\u0005 * 1.45;
			this.\u000f\u2003.Width = global::\u0008\u2002.\u0003.\u000e * 0.8;
			this.\u000f\u2003.Height = global::\u0008\u2002.\u0003.\u0003\u2002 * 0.8;
			global::\u0008\u2002.\u0008.\u000e\u2002();
			this.\u000f\u2009();
		}

		private void \u0003\u2003(object \u0003, RoutedEventArgs \u0006)
		{
			if (global::\u0008\u2002.\u0008.\u0008\u2001 == 0)
			{
				this.\u0006\u2009();
				global::\u0008\u2002.\u0008.\u0006\u2002();
			}
			else
			{
				this.\u0002\u2009();
				global::\u0008\u2002.\u0008.\u0002\u2002();
			}
		}

		public void \u0006\u2009()
		{
			if (!\u0008\u2005\u2009.\u0003())
			{
				\u0008\u2005\u2009.\u0003(\u0003: true);
				\u0003\u2007\u2009.\u0003(\u0003: true);
			}
			\u000e\u2006.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031809661));
			\u000e\u2006.IsEnabled = false;
			\u0006\u2008.Visibility = Visibility.Hidden;
		}

		public void \u000f\u2009()
		{
			if (global::\u0008\u2002.\u0008.\u0008\u2001 == 1)
			{
				this.\u0006\u2009();
				if (\u0005\u200b.Visibility == Visibility.Hidden)
				{
					\u0003\u2009();
				}
				return;
			}
			int num = Convert.ToInt16(\u0006\u200b\u2009.Text);
			int num2 = Convert.ToInt16(\u0008\u2007\u2009.Text);
			if (num > 50 || num2 > 50 || global::\u0008\u2002.\u0008.\u000e == 1)
			{
				\u0008\u2009();
			}
			else
			{
				this.\u0002\u2009();
			}
		}

		public void \u0002\u2009()
		{
			if (\u0008\u2005\u2009.\u0003())
			{
				\u0008\u2005\u2009.\u0003(\u0003: false);
				\u0003\u2007\u2009.\u0003(\u0003: false);
			}
			\u000e\u2006.IsEnabled = true;
			\u000e\u2006.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031812575));
			\u0006\u2008.Visibility = Visibility.Visible;
		}

		public void \u0008\u2009()
		{
			\u000e\u2006.Content = global::\u0008\u2002.\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031809635));
			\u000e\u2006.IsEnabled = false;
			\u0006\u2008.Visibility = Visibility.Visible;
		}

		private void \u0005\u2009()
		{
			int num = 0;
			int num2 = 0;
			if (\u000f\u200a.IsChecked == true)
			{
				num2 = ((\u0008\u2006.SelectedIndex != 1) ? \u0005\u2006.SelectedIndex : (\u0005\u2006.SelectedIndex + 12));
				if (\u0002\u200a.IsChecked == true)
				{
					num |= 1;
				}
				if (\u0008\u200a.IsChecked == true)
				{
					num |= 2;
				}
				if (\u0005\u200a.IsChecked == true)
				{
					num |= 4;
				}
				if (\u000e\u200a.IsChecked == true)
				{
					num |= 8;
				}
				if (\u0003\u2006.IsChecked == true)
				{
					num |= 0x10;
				}
				if (\u0006\u2006.IsChecked == true)
				{
					num |= 0x20;
				}
				if (\u000f\u2006.IsChecked == true)
				{
					num |= 0x40;
				}
				if (num == 0)
				{
					\u000f\u200a.IsChecked = false;
					\u0002\u200a.IsChecked = true;
					\u0008\u200a.IsChecked = true;
					\u0005\u200a.IsChecked = true;
					\u000e\u200a.IsChecked = true;
					\u0003\u2006.IsChecked = true;
					\u0006\u2006.IsChecked = true;
					\u000f\u2006.IsChecked = true;
				}
			}
			if (\u0006\u200a.IsChecked == true)
			{
				global::\u0008\u2002.\u0008.\u0003(1, num, num2);
			}
			else
			{
				global::\u0008\u2002.\u0008.\u0003(0, num, num2);
			}
		}

		private void \u0006\u2003(object \u0003, RoutedEventArgs \u0006)
		{
			\u0005\u2009();
		}

		private void \u0006(object \u0003, SelectionChangedEventArgs \u0006)
		{
			if (this.m_\u000f)
			{
				\u0005\u2009();
			}
		}

		public void \u000e\u2009()
		{
			byte[] array = new byte[256];
			byte[] array2 = new byte[256];
			array[0] = 1;
			array[1] = 11;
			array[2] = 0;
			array[3] = 0;
			array[4] = 0;
			array[5] = 0;
			array[6] = 192;
			global::\u0008\u2002.\u0002.\u0003(4, array, ref array2);
			global::\u000f\u2002.\u0003(\u0002\u0005.\u0003(1031814311) + array2[2]);
			this.m_\u0002 = (int)array2[2];
		}

		private void \u000f\u2003(object \u0003, RoutedEventArgs \u0006)
		{
			if (global::\u0008\u2002.\u0008.\u000f\u2002)
			{
				\u0005\u2003\u2009.\u0002();
			}
			if (global::\u0008\u2002.\u0008.\u0002\u2002)
			{
				\u0003\u2004\u2009.\u0002();
			}
			if (global::\u0008\u2002.\u0008.\u0008\u2002)
			{
				\u000f\u2004\u2009.\u0002();
			}
			if (global::\u0008\u2002.\u0008.\u0005\u2002)
			{
				\u0008\u2004\u2009.\u0002();
			}
			global::\u0008\u2002.\u000e.\u0003\u2002();
			\u0006\u2000\u2009.IsEnabled = false;
		}

		private void \u0002\u2003(object \u0003, RoutedEventArgs \u0006)
		{
			if (\u0002\u2003\u2009.SelectedIndex == 0)
			{
				\u0005\u2003\u2009.\u000f();
			}
			else if (\u0002\u2003\u2009.SelectedIndex == 1)
			{
				\u0003\u2004\u2009.\u000f();
			}
			else if (\u0002\u2003\u2009.SelectedIndex == 2)
			{
				\u000f\u2004\u2009.\u000f();
			}
			else if (\u0002\u2003\u2009.SelectedIndex == 3)
			{
				\u0008\u2004\u2009.\u000f();
			}
			global::\u0008\u2002.\u000e.\u0003\u2002();
			\u0006\u2000\u2009.IsEnabled = false;
		}

		private void \u0003\u2003()
		{
			global::\u0008\u2002.\u000e.\u0005();
			if (global::\u0008\u2002.\u0008.\u000f\u2002)
			{
				\u0005\u2003\u2009.\u0003((\u000f\u0005.\u0006)0);
			}
			if (global::\u0008\u2002.\u0008.\u0002\u2002)
			{
				\u0003\u2004\u2009.\u0003((\u000f\u0005.\u0006)1);
			}
			if (global::\u0008\u2002.\u0008.\u0008\u2002)
			{
				\u000f\u2004\u2009.\u0003((\u000f\u0005.\u0006)2);
			}
			if (global::\u0008\u2002.\u0008.\u0005\u2002)
			{
				\u0008\u2004\u2009.\u0003((\u000f\u0005.\u0006)3);
			}
		}

		private void \u0006\u2003()
		{
			\u0006\u2000\u2009.IsEnabled = true;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!\u000f\u2008\u2009)
			{
				\u000f\u2008\u2009 = true;
				Uri resourceLocator = new Uri(\u0002\u0005.\u0003(1031809623), UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate \u0003(Type \u0003, string \u0006)
		{
			return Delegate.CreateDelegate(\u0003, this, \u0006);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		private void tsqss2yjgg8d9ucjgscaac7f6bbwdmzt\u2009\u2005\u2006\u0003(int \u0003, object \u0006)
		{
			switch (\u0003)
			{
			case 1:
				this.\u0005 = (l1illIiliii11)\u0006;
				break;
			case 2:
				this.\u000e = (Grid)\u0006;
				break;
			case 3:
				this.\u0003\u2002 = (Rectangle)\u0006;
				break;
			case 4:
				this.\u0006\u2002 = (Grid)\u0006;
				break;
			case 5:
				this.\u000f\u2002 = (Rectangle)\u0006;
				break;
			case 6:
				this.\u0002\u2002 = (Rectangle)\u0006;
				break;
			case 7:
				this.\u0008\u2002 = (Rectangle)\u0006;
				break;
			case 8:
				this.\u0005\u2002 = (Image)\u0006;
				break;
			case 9:
				this.\u000e\u2002 = (TextBlock)\u0006;
				break;
			case 10:
				this.\u0003\u2001 = (TextBlock)\u0006;
				break;
			case 11:
				this.\u0006\u2001 = (TextBlock)\u0006;
				break;
			case 12:
				this.\u000f\u2001 = (TextBlock)\u0006;
				break;
			case 13:
				this.\u0002\u2001 = (TextBlock)\u0006;
				break;
			case 14:
				this.\u0008\u2001 = (TextBlock)\u0006;
				break;
			case 15:
				this.\u0005\u2001 = (TextBlock)\u0006;
				break;
			case 16:
				this.\u000e\u2001 = (Rectangle)\u0006;
				break;
			case 17:
				this.\u0003\u2009 = (Rectangle)\u0006;
				break;
			case 18:
				this.\u0006\u2009 = (TextBlock)\u0006;
				break;
			case 19:
				this.\u000f\u2009 = (TextBlock)\u0006;
				break;
			case 20:
				this.\u0002\u2009 = (TextBlock)\u0006;
				break;
			case 21:
				this.\u0008\u2009 = (TextBlock)\u0006;
				break;
			case 22:
				this.\u0005\u2009 = (Image)\u0006;
				break;
			case 23:
				this.\u000e\u2009 = (Grid)\u0006;
				break;
			case 24:
				this.\u0003\u2003 = (System.Windows.Controls.RadioButton)\u0006;
				break;
			case 25:
				this.\u0006\u2003 = (System.Windows.Controls.RadioButton)\u0006;
				break;
			case 26:
				this.\u000f\u2003 = (Viewbox)\u0006;
				break;
			case 27:
				this.\u0002\u2003 = (Viewbox)\u0006;
				break;
			case 28:
				\u0008\u2003 = (Grid)\u0006;
				break;
			case 29:
				\u0005\u2003 = (Rectangle)\u0006;
				break;
			case 30:
				\u000e\u2003 = (Rectangle)\u0006;
				break;
			case 31:
				\u0003\u2004 = (Image)\u0006;
				break;
			case 32:
				\u0006\u2004 = (TextBlock)\u0006;
				break;
			case 33:
				\u000f\u2004 = (TextBlock)\u0006;
				break;
			case 34:
				\u0002\u2004 = (TextBlock)\u0006;
				break;
			case 35:
				\u0008\u2004 = (TextBlock)\u0006;
				break;
			case 36:
				\u0005\u2004 = (TextBlock)\u0006;
				break;
			case 37:
				\u000e\u2004 = (TextBlock)\u0006;
				break;
			case 38:
				\u0003\u2000 = (TextBlock)\u0006;
				break;
			case 39:
				\u0006\u2000 = (Rectangle)\u0006;
				break;
			case 40:
				\u000f\u2000 = (Rectangle)\u0006;
				break;
			case 41:
				\u0002\u2000 = (TextBlock)\u0006;
				break;
			case 42:
				\u0008\u2000 = (TextBlock)\u0006;
				break;
			case 43:
				\u0005\u2000 = (Grid)\u0006;
				break;
			case 44:
				\u000e\u2000 = (TextBlock)\u0006;
				break;
			case 45:
				\u0003\u2007 = (System.Windows.Controls.RadioButton)\u0006;
				\u0003\u2007.Checked += \u0008\u2009;
				break;
			case 46:
				\u0006\u2007 = (System.Windows.Controls.RadioButton)\u0006;
				\u0006\u2007.Checked += \u0005\u2009;
				break;
			case 47:
				\u000f\u2007 = (Viewbox)\u0006;
				break;
			case 48:
				\u0002\u2007 = (Grid)\u0006;
				break;
			case 49:
				\u0008\u2007 = (Rectangle)\u0006;
				break;
			case 50:
				\u0005\u2007 = (Rectangle)\u0006;
				break;
			case 51:
				\u000e\u2007 = (Image)\u0006;
				break;
			case 52:
				\u0003\u2005 = (TextBlock)\u0006;
				break;
			case 53:
				\u0006\u2005 = (TextBlock)\u0006;
				break;
			case 54:
				\u000f\u2005 = (TextBlock)\u0006;
				break;
			case 55:
				\u0002\u2005 = (TextBlock)\u0006;
				break;
			case 56:
				\u0008\u2005 = (TextBlock)\u0006;
				break;
			case 57:
				\u0005\u2005 = (TextBlock)\u0006;
				break;
			case 58:
				\u000e\u2005 = (TextBlock)\u0006;
				break;
			case 59:
				\u0003\u200b = (TextBlock)\u0006;
				break;
			case 60:
				\u0006\u200b = (TextBlock)\u0006;
				break;
			case 61:
				\u000f\u200b = (Rectangle)\u0006;
				break;
			case 62:
				\u0002\u200b = (Rectangle)\u0006;
				break;
			case 63:
				\u0008\u200b = (Grid)\u0006;
				break;
			case 64:
				\u0005\u200b = (Grid)\u0006;
				break;
			case 65:
				\u000e\u200b = (Rectangle)\u0006;
				break;
			case 66:
				\u0003\u200a = (TextBlock)\u0006;
				break;
			case 67:
				\u0006\u200a = (System.Windows.Controls.CheckBox)\u0006;
				\u0006\u200a.Click += \u0006\u2003;
				break;
			case 68:
				\u000f\u200a = (System.Windows.Controls.CheckBox)\u0006;
				\u000f\u200a.Click += \u0006\u2003;
				break;
			case 69:
				\u0002\u200a = (System.Windows.Controls.CheckBox)\u0006;
				\u0002\u200a.Click += \u0006\u2003;
				break;
			case 70:
				\u0008\u200a = (System.Windows.Controls.CheckBox)\u0006;
				\u0008\u200a.Click += \u0006\u2003;
				break;
			case 71:
				\u0005\u200a = (System.Windows.Controls.CheckBox)\u0006;
				\u0005\u200a.Click += \u0006\u2003;
				break;
			case 72:
				\u000e\u200a = (System.Windows.Controls.CheckBox)\u0006;
				\u000e\u200a.Click += \u0006\u2003;
				break;
			case 73:
				\u0003\u2006 = (System.Windows.Controls.CheckBox)\u0006;
				\u0003\u2006.Click += \u0006\u2003;
				break;
			case 74:
				\u0006\u2006 = (System.Windows.Controls.CheckBox)\u0006;
				\u0006\u2006.Click += \u0006\u2003;
				break;
			case 75:
				\u000f\u2006 = (System.Windows.Controls.CheckBox)\u0006;
				\u000f\u2006.Click += \u0006\u2003;
				break;
			case 76:
				\u0002\u2006 = (TextBlock)\u0006;
				break;
			case 77:
				\u0008\u2006 = (System.Windows.Controls.ComboBox)\u0006;
				\u0008\u2006.SelectionChanged += this.\u0006;
				break;
			case 78:
				\u0005\u2006 = (System.Windows.Controls.ComboBox)\u0006;
				\u0005\u2006.SelectionChanged += this.\u0006;
				break;
			case 79:
				\u000e\u2006 = (System.Windows.Controls.Button)\u0006;
				\u000e\u2006.Click += \u0003\u2003;
				break;
			case 80:
				\u0003\u2008 = (Grid)\u0006;
				break;
			case 81:
				\u0006\u2008 = (System.Windows.Controls.Button)\u0006;
				\u0006\u2008.Click += \u000e\u2009;
				break;
			case 82:
				\u000f\u2008 = (Grid)\u0006;
				break;
			case 83:
				\u0002\u2008 = (Image)\u0006;
				break;
			case 84:
				\u0008\u2008 = (TextBlock)\u0006;
				break;
			case 85:
				\u0005\u2008 = (Grid)\u0006;
				break;
			case 86:
				\u000e\u2008 = (Rectangle)\u0006;
				break;
			case 87:
				\u0003\u2002\u2009 = (TextBlock)\u0006;
				break;
			case 88:
				\u0006\u2002\u2009 = (TextBlock)\u0006;
				break;
			case 89:
				\u000f\u2002\u2009 = (TextBlock)\u0006;
				break;
			case 90:
				\u0002\u2002\u2009 = (TextBlock)\u0006;
				break;
			case 91:
				\u0008\u2002\u2009 = (Slider)\u0006;
				\u0008\u2002\u2009.ValueChanged += this.\u0003;
				break;
			case 92:
				\u0005\u2002\u2009 = (System.Windows.Controls.ComboBox)\u0006;
				\u0005\u2002\u2009.SelectionChanged += this.\u0003;
				break;
			case 93:
				\u000e\u2002\u2009 = (Grid)\u0006;
				break;
			case 94:
				\u0003\u2001\u2009 = (StackPanel)\u0006;
				break;
			case 95:
				\u0006\u2001\u2009 = (StackPanel)\u0006;
				break;
			case 96:
				\u000f\u2001\u2009 = (l1IIi1iiI1III)\u0006;
				break;
			case 97:
				\u0002\u2001\u2009 = (StackPanel)\u0006;
				break;
			case 98:
				\u0008\u2001\u2009 = (StackPanel)\u0006;
				break;
			case 99:
				\u0005\u2001\u2009 = (l1IIi1iiI1III)\u0006;
				break;
			case 100:
				\u000e\u2001\u2009 = (StackPanel)\u0006;
				break;
			case 101:
				\u0003\u2009\u2009 = (StackPanel)\u0006;
				break;
			case 102:
				\u0006\u2009\u2009 = (l1IIi1iiI1III)\u0006;
				break;
			case 103:
				\u000f\u2009\u2009 = (System.Windows.Controls.RadioButton)\u0006;
				\u000f\u2009\u2009.Checked += \u0006\u2009;
				break;
			case 104:
				\u0002\u2009\u2009 = (System.Windows.Controls.RadioButton)\u0006;
				\u0002\u2009\u2009.Checked += \u000f\u2009;
				break;
			case 105:
				\u0008\u2009\u2009 = (System.Windows.Controls.RadioButton)\u0006;
				\u0008\u2009\u2009.Checked += \u0002\u2009;
				break;
			case 106:
				\u0005\u2009\u2009 = (Grid)\u0006;
				break;
			case 107:
				\u000e\u2009\u2009 = (WrapPanel)\u0006;
				break;
			case 108:
				\u0003\u2003\u2009 = (System.Windows.Controls.Button)\u0006;
				\u0003\u2003\u2009.Click += \u0003\u2009;
				break;
			case 109:
				\u0006\u2003\u2009 = (System.Windows.Controls.Button)\u0006;
				\u0006\u2003\u2009.Click += \u000e\u2001;
				break;
			case 110:
				\u000f\u2003\u2009 = (Grid)\u0006;
				break;
			case 111:
				\u0002\u2003\u2009 = (System.Windows.Controls.TabControl)\u0006;
				break;
			case 112:
				\u0008\u2003\u2009 = (TabItem)\u0006;
				break;
			case 113:
				\u0005\u2003\u2009 = (i11l1i1Ii1I1)\u0006;
				break;
			case 114:
				\u000e\u2003\u2009 = (TabItem)\u0006;
				break;
			case 115:
				\u0003\u2004\u2009 = (i11l1i1Ii1I1)\u0006;
				break;
			case 116:
				\u0006\u2004\u2009 = (TabItem)\u0006;
				break;
			case 117:
				\u000f\u2004\u2009 = (i11l1i1Ii1I1)\u0006;
				break;
			case 118:
				\u0002\u2004\u2009 = (TabItem)\u0006;
				break;
			case 119:
				\u0008\u2004\u2009 = (i11l1i1Ii1I1)\u0006;
				break;
			case 120:
				\u0005\u2004\u2009 = (Grid)\u0006;
				break;
			case 121:
				\u000e\u2004\u2009 = (WrapPanel)\u0006;
				break;
			case 122:
				\u0003\u2000\u2009 = (System.Windows.Controls.Button)\u0006;
				\u0003\u2000\u2009.Click += \u0002\u2003;
				break;
			case 123:
				\u0006\u2000\u2009 = (System.Windows.Controls.Button)\u0006;
				\u0006\u2000\u2009.Click += \u000f\u2003;
				break;
			case 124:
				\u000f\u2000\u2009 = (Grid)\u0006;
				break;
			case 125:
				\u0002\u2000\u2009 = (TextBlock)\u0006;
				break;
			case 126:
				\u0008\u2000\u2009 = (TextBlock)\u0006;
				break;
			case 127:
				\u0005\u2000\u2009 = (Viewbox)\u0006;
				break;
			case 128:
				\u000e\u2000\u2009 = (Grid)\u0006;
				break;
			case 129:
				\u0003\u2007\u2009 = (lll1llIli1Ii)\u0006;
				break;
			case 130:
				\u0006\u2007\u2009 = (TextBlock)\u0006;
				break;
			case 131:
				\u000f\u2007\u2009 = (TextBlock)\u0006;
				break;
			case 132:
				\u0002\u2007\u2009 = (TextBlock)\u0006;
				break;
			case 133:
				\u0008\u2007\u2009 = (TextBlock)\u0006;
				break;
			case 134:
				\u0005\u2007\u2009 = (TextBlock)\u0006;
				break;
			case 135:
				\u000e\u2007\u2009 = (Rectangle)\u0006;
				break;
			case 136:
				\u0003\u2005\u2009 = (Rectangle)\u0006;
				break;
			case 137:
				\u0006\u2005\u2009 = (Rectangle)\u0006;
				break;
			case 138:
				\u000f\u2005\u2009 = (Viewbox)\u0006;
				break;
			case 139:
				\u0002\u2005\u2009 = (Grid)\u0006;
				break;
			case 140:
				\u0008\u2005\u2009 = (lll1llIli1Ii)\u0006;
				break;
			case 141:
				\u0005\u2005\u2009 = (TextBlock)\u0006;
				break;
			case 142:
				\u000e\u2005\u2009 = (TextBlock)\u0006;
				break;
			case 143:
				\u0003\u200b\u2009 = (TextBlock)\u0006;
				break;
			case 144:
				\u0006\u200b\u2009 = (TextBlock)\u0006;
				break;
			case 145:
				\u000f\u200b\u2009 = (TextBlock)\u0006;
				break;
			case 146:
				\u0002\u200b\u2009 = (Rectangle)\u0006;
				break;
			case 147:
				\u0008\u200b\u2009 = (Rectangle)\u0006;
				break;
			case 148:
				\u0005\u200b\u2009 = (StackPanel)\u0006;
				break;
			case 149:
				\u000e\u200b\u2009 = (StackPanel)\u0006;
				break;
			case 150:
				\u0003\u200a\u2009 = (System.Windows.Controls.RadioButton)\u0006;
				\u0003\u200a\u2009.Checked += \u0008;
				\u0003\u200a\u2009.Click += \u0005;
				break;
			case 151:
				\u0006\u200a\u2009 = (StackPanel)\u0006;
				break;
			case 152:
				\u000f\u200a\u2009 = (System.Windows.Controls.CheckBox)\u0006;
				\u000f\u200a\u2009.Click += \u000f\u2001;
				\u000f\u200a\u2009.Checked += \u0002\u2001;
				break;
			case 153:
				\u0002\u200a\u2009 = (StackPanel)\u0006;
				break;
			case 154:
				\u0008\u200a\u2009 = (System.Windows.Controls.RadioButton)\u0006;
				\u0008\u200a\u2009.Checked += \u000e;
				\u0008\u200a\u2009.Click += \u0003\u2002;
				break;
			case 155:
				\u0005\u200a\u2009 = (StackPanel)\u0006;
				break;
			case 156:
				\u000e\u200a\u2009 = (System.Windows.Controls.RadioButton)\u0006;
				\u000e\u200a\u2009.Checked += \u0006\u2002;
				\u000e\u200a\u2009.Click += \u000f\u2002;
				break;
			case 157:
				\u0003\u2006\u2009 = (StackPanel)\u0006;
				break;
			case 158:
				\u0006\u2006\u2009 = (System.Windows.Controls.RadioButton)\u0006;
				\u0006\u2006\u2009.Checked += \u0002\u2002;
				\u0006\u2006\u2009.Click += \u0008\u2002;
				break;
			case 159:
				\u000f\u2006\u2009 = (StackPanel)\u0006;
				break;
			case 160:
				\u0002\u2006\u2009 = (System.Windows.Controls.RadioButton)\u0006;
				\u0002\u2006\u2009.Checked += \u0008\u2001;
				\u0002\u2006\u2009.Click += \u0005\u2001;
				break;
			case 161:
				\u0008\u2006\u2009 = (StackPanel)\u0006;
				break;
			case 162:
				\u0005\u2006\u2009 = (System.Windows.Controls.RadioButton)\u0006;
				\u0005\u2006\u2009.Click += \u0005\u2002;
				\u0005\u2006\u2009.Checked += \u000e\u2002;
				break;
			case 163:
				\u000e\u2006\u2009 = (StackPanel)\u0006;
				break;
			case 164:
				\u0003\u2008\u2009 = (System.Windows.Controls.RadioButton)\u0006;
				\u0003\u2008\u2009.Click += \u0003\u2001;
				\u0003\u2008\u2009.Checked += \u0006\u2001;
				break;
			case 165:
				\u0006\u2008\u2009 = (l1Ii11l1ii1II)\u0006;
				break;
			default:
				\u000f\u2008\u2009 = true;
				break;
			}
		}

		void IComponentConnector.Connect(int \u0003, object \u0006)
		{
			//ILSpy generated this explicit interface implementation from .override directive in tsqss2yjgg8d9ucjgscaac7f6bbwdmzt   
			this.tsqss2yjgg8d9ucjgscaac7f6bbwdmzt\u2009\u2005\u2006\u0003(\u0003, \u0006);
		}

		private void \u000f\u2003()
		{
			if (this.m_\u0006 != 0 && this.m_\u0006 == 1)
			{
				Thread thread = new Thread(this.\u000f\u2002);
				thread.Start();
				\u0006\u2002();
			}
		}

		private void \u0002\u2003()
		{
			\u0002\u2002();
			\u0008\u2002();
			\u0003(global::\u0008\u2002.\u0008.\u0003.\u0002\u2001);
			if (\u0003\u2007.IsChecked == true)
			{
				\u0006(global::\u0008\u2002.\u0008.\u0006.\u0002\u2001);
			}
			else if (\u0006\u2007.IsChecked == true && global::\u0008\u2002.\u0008.\u0008\u2002)
			{
				\u0006(global::\u0008\u2002.\u0008.\u000f.\u0002\u2001);
			}
			if (!global::\u0008\u2002.\u0006.\u0005\u2001 || \u0005\u200b.Visibility != 0)
			{
				return;
			}
			if (global::\u0008\u2002.\u0008.\u0008\u2001 == 1)
			{
				this.\u0006\u2009();
				return;
			}
			int num = Convert.ToInt16(\u0006\u200b\u2009.Text);
			int num2 = Convert.ToInt16(\u0008\u2007\u2009.Text);
			if (num > 50 || num2 > 50)
			{
				\u0008\u2009();
			}
			else
			{
				this.\u0002\u2009();
			}
		}
	}
	public sealed class l1lI1II1iIIIi : Window, IComponentConnector
	{
		private double m_\u0003 = 0.0;

		private double \u0006 = 0.0;

		internal Canvas \u000f;

		internal Ellipse \u0002;

		internal Ellipse \u0008;

		internal Ellipse \u0005;

		internal Ellipse \u000e;

		internal Ellipse \u0003\u2002;

		internal Ellipse \u0006\u2002;

		internal Ellipse \u000f\u2002;

		internal Ellipse \u0002\u2002;

		private bool \u0008\u2002;

		public l1lI1II1iIIIi()
		{
			InitializeComponent();
			this.m_\u0003 = base.Width;
			\u0006 = base.Height;
			\u0003();
		}

		private void \u0003()
		{
			double primaryScreenWidth = SystemParameters.PrimaryScreenWidth;
			double primaryScreenHeight = SystemParameters.PrimaryScreenHeight;
			double num = 1920.0 / primaryScreenWidth * 1.2;
			double num2 = 1080.0 / primaryScreenHeight * 1.2;
			base.Width = this.m_\u0003 / num;
			base.Height = \u0006 / num2;
			base.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			InvalidateVisual();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!\u0008\u2002)
			{
				\u0008\u2002 = true;
				Uri resourceLocator = new Uri(\u0002\u0005.\u0003(1031805903), UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		private void kkp3rjq7a7q43e24e9w7zj2ufckw256u\u2009\u2005\u2006\u0003(int \u0003, object \u0006)
		{
			switch (\u0003)
			{
			case 1:
				\u000f = (Canvas)\u0006;
				break;
			case 2:
				\u0002 = (Ellipse)\u0006;
				break;
			case 3:
				\u0008 = (Ellipse)\u0006;
				break;
			case 4:
				\u0005 = (Ellipse)\u0006;
				break;
			case 5:
				\u000e = (Ellipse)\u0006;
				break;
			case 6:
				\u0003\u2002 = (Ellipse)\u0006;
				break;
			case 7:
				\u0006\u2002 = (Ellipse)\u0006;
				break;
			case 8:
				\u000f\u2002 = (Ellipse)\u0006;
				break;
			case 9:
				\u0002\u2002 = (Ellipse)\u0006;
				break;
			default:
				\u0008\u2002 = true;
				break;
			}
		}

		void IComponentConnector.Connect(int \u0003, object \u0006)
		{
			//ILSpy generated this explicit interface implementation from .override directive in kkp3rjq7a7q43e24e9w7zj2ufckw256u   
			this.kkp3rjq7a7q43e24e9w7zj2ufckw256u\u2009\u2005\u2006\u0003(\u0003, \u0006);
		}
	}
	public sealed class lll1llIli1Ii : System.Windows.Controls.UserControl, IComponentConnector
	{
		private int m_\u0003 = 0;

		private bool m_\u0006 = false;

		private System.Windows.Forms.Timer \u000f = new System.Windows.Forms.Timer();

		private int \u0002 = 0;

		private int \u0008 = 360;

		private double \u0005 = 360.0;

		private double \u000e = 0.0;

		private int \u0003\u2002 = 0;

		internal lll1llIli1Ii \u0006\u2002;

		internal Rectangle \u000f\u2002;

		internal Rectangle \u0002\u2002;

		internal Rectangle \u0008\u2002;

		private bool \u0005\u2002;

		public lll1llIli1Ii()
		{
			InitializeComponent();
			\u000f.Interval = 50;
			\u000f.Tick += \u0003;
			base.FontSize = 360.0;
		}

		public int \u0003()
		{
			return this.m_\u0003;
		}

		public void \u0003(int \u0003)
		{
			if (this.m_\u0003 != \u0003)
			{
				this.m_\u0003 = \u0003;
				\u0006(\u0003);
			}
		}

		public bool \u0003()
		{
			return this.m_\u0006;
		}

		public void \u0003(bool \u0003)
		{
			this.m_\u0006 = \u0003;
			if (this.m_\u0006)
			{
				base.FontSize = 360.0;
				\u0002 = 0;
				\u0005 = 360.0;
				\u000f\u2002.Visibility = Visibility.Hidden;
				\u0008\u2002.Visibility = Visibility.Visible;
			}
			else
			{
				\u000f\u2002.Visibility = Visibility.Visible;
				\u0008\u2002.Visibility = Visibility.Hidden;
				\u0002\u2002.StrokeDashOffset = 360.0;
			}
		}

		private void \u0003(object \u0003, RoutedEventArgs \u0006)
		{
			this.\u0003(\u0003: false);
		}

		private void \u0003(object \u0003, EventArgs \u0006)
		{
			base.Dispatcher.BeginInvoke(new Action(this.\u0003));
		}

		public void \u0006(int \u0003)
		{
			\u0003\u2002 = \u0003 / 2;
			if (\u0003 == 0)
			{
				\u000f.Stop();
				return;
			}
			\u000f.Start();
			if (\u0003 > 1 && (double)\u0003 <= 12.5)
			{
				\u0008 = 2;
			}
			else if ((double)\u0003 > 12.5 && \u0003 <= 25)
			{
				\u0008 = 10;
			}
			else if (\u0003 > 25 && (double)\u0003 <= 37.5)
			{
				\u0008 = 18;
			}
			else if ((double)\u0003 > 37.5 && \u0003 <= 50)
			{
				\u0008 = 36;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!\u0005\u2002)
			{
				\u0005\u2002 = true;
				Uri resourceLocator = new Uri(\u0002\u0005.\u0003(1031808057), UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		private void 8gmqyqec6vsc4gv8lq4pmzg7b8a2fvcq\u2009\u2005\u2006\u0003(int \u0003, object \u0006)
		{
			switch (\u0003)
			{
			case 1:
				\u0006\u2002 = (lll1llIli1Ii)\u0006;
				\u0006\u2002.Loaded += this.\u0003;
				break;
			case 2:
				\u000f\u2002 = (Rectangle)\u0006;
				break;
			case 3:
				\u0002\u2002 = (Rectangle)\u0006;
				break;
			case 4:
				\u0008\u2002 = (Rectangle)\u0006;
				break;
			default:
				\u0005\u2002 = true;
				break;
			}
		}

		void IComponentConnector.Connect(int \u0003, object \u0006)
		{
			//ILSpy generated this explicit interface implementation from .override directive in 8gmqyqec6vsc4gv8lq4pmzg7b8a2fvcq   
			this.8gmqyqec6vsc4gv8lq4pmzg7b8a2fvcq\u2009\u2005\u2006\u0003(\u0003, \u0006);
		}

		private void \u0003()
		{
			if (!this.\u0003())
			{
				\u0002 += \u0008;
				base.FontSize = \u0002;
				if (\u0002 >= 360)
				{
					\u0002 = \u0008;
				}
			}
			else
			{
				\u0005 -= \u0008;
				\u0002\u2002.StrokeDashOffset = \u0005;
				if (\u0005 <= 0.0)
				{
					\u0005 = 360.0 - \u0005;
				}
			}
		}
	}
}
namespace FanSpeedSetting.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings m_\u0003 = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings \u0003()
		{
			return Settings.m_\u0003;
		}
	}
}
namespace XamlGeneratedNamespace
{
	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class GeneratedInternalTypeHelper : InternalTypeHelper
	{
		protected override object CreateInstance(Type \u0003, CultureInfo \u0006)
		{
			return Activator.CreateInstance(\u0003, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, \u0006);
		}

		protected override object GetPropertyValue(PropertyInfo \u0003, object \u0006, CultureInfo \u000f)
		{
			return \u0003.GetValue(\u0006, BindingFlags.Default, null, null, \u000f);
		}

		protected override void SetPropertyValue(PropertyInfo \u0003, object \u0006, object \u000f, CultureInfo \u0002)
		{
			\u0003.SetValue(\u0006, \u000f, BindingFlags.Default, null, null, \u0002);
		}

		protected override Delegate CreateDelegate(Type \u0003, object \u0006, string \u000f)
		{
			return (Delegate)\u0006.GetType().InvokeMember(\u0002\u0005.\u0003(1031805853), BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, \u0006, new object[2] { \u0003, \u000f }, null);
		}

		protected override void AddEventHandler(EventInfo \u0003, object \u0006, Delegate \u000f)
		{
			\u0003.AddEventHandler(\u0006, \u000f);
		}
	}
}
