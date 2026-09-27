// CoolDeck — localisation. English text is the key; a Chinese table overrides it.
//
// WHY KEY-BY-ENGLISH
// Inventing key names for ~60 strings just moves the unreadability into the source. Using the
// English copy as the key keeps the code legible, and an untranslated key degrades to English
// instead of rendering blank.
//
// Language follows the OS UI culture (zh-* -> Chinese), with a manual override written next to
// the theme preference so a Chinese Windows user can still pick English, and vice versa.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CoolDeck
{
    public static class Loc
    {
        public static bool IsZh { get; private set; }

        static Loc()
        {
            IsZh = Detect();
        }

        static bool Detect()
        {
            try
            {
                string forced = LangOverride();
                if (forced == "zh") return true;
                if (forced == "en") return false;
            }
            catch { }
            try
            {
                string c = CultureInfo.CurrentUICulture.Name;
                if (string.IsNullOrEmpty(c)) c = CultureInfo.CurrentCulture.Name;
                return c.StartsWith("zh", StringComparison.OrdinalIgnoreCase);
            }
            catch { return false; }
        }

        static string LangOverride()
        {
            string p = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "CoolDeck", "lang.txt");
            if (!File.Exists(p)) return null;
            return File.ReadAllText(p).Trim().ToLowerInvariant();
        }

        public static bool SaveLangOverride(string code)
        {
            try
            {
                string dir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CoolDeck");
                Directory.CreateDirectory(dir);
                File.WriteAllText(Path.Combine(dir, "lang.txt"), code);
                return true;
            }
            catch { return false; }
        }

        /// <summary>Look up text; returns the key unchanged when there is no translation.
        /// Never throws: a fan controller must not die because of a string table.</summary>
        public static string T(string en)
        {
            if (!IsZh || en == null) return en;
            try
            {
                string zh;
                if (Zh.TryGetValue(en, out zh)) return zh;
                // Long UI paragraphs are written in C# as multi-line concatenations, so the
                // runtime string can differ from the dictionary key only in whitespace.
                if (ZhNorm.TryGetValue(Norm(en), out zh)) return zh;
            }
            catch { }
            return en;
        }

        static string Norm(string s)
        {
            return System.Text.RegularExpressions.Regex.Replace(s, @"\s+", " ").Trim();
        }

        // Built lazily, NOT as a static field initialised next to Zh(): static field
        // initialisers run in textual order, and Zh() is declared at the bottom of this class,
        // so an eager ZhNorm would have walked a null dictionary and thrown from the static
        // constructor, taking the whole app down on first use.
        static Dictionary<string, string> _zhNorm;

        static Dictionary<string, string> ZhNorm
        {
            get
            {
                if (_zhNorm == null)
                {
                    var d = new Dictionary<string, string>(StringComparer.Ordinal);
                    foreach (KeyValuePair<string, string> kv in Zh)
                    {
                        string k = Norm(kv.Key);
                        if (!d.ContainsKey(k)) d.Add(k, kv.Value);
                    }
                    _zhNorm = d;
                }
                return _zhNorm;
            }
        }

        /// <summary>Formatted variant — the format string itself is translated, args are not.</summary>
        public static string F(string enFormat, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, T(enFormat), args);
        }

        // Built lazily inside a try/catch. A dictionary initializer calls Add(), so one
        // duplicated key throws from the type initialiser and takes the whole process down —
        // which is exactly what happened once. A string table must never be able to kill a fan
        // controller; on any failure it now degrades to English.
        static Dictionary<string, string> _zh;

        static Dictionary<string, string> Zh
        {
            get
            {
                if (_zh == null)
                {
                    try { _zh = BuildZh(); }
                    catch (Exception ex)
                    {
                        // Degrade to English, but say so — a silent fallback would just look
                        // like "the translation doesn't work" with nothing to go on.
                        try { Trace.W("Loc table failed to build, falling back to English: " + ex.Message); }
                        catch { }
                        _zh = new Dictionary<string, string>(StringComparer.Ordinal);
                    }
                }
                return _zh;
            }
        }

        static Dictionary<string, string> BuildZh()
        {
            return new Dictionary<string, string>(StringComparer.Ordinal)
            {
            // ---------------- tray menu ----------------
            { "Open CoolDeck", "打开 CoolDeck" },
            { "Start with Windows", "开机自动启动" },
            { "Fan control exclusivity…", "风扇控制权说明…" },
            { "Exit", "退出" },
            { "Language", "界面语言" },
            { "Chinese", "中文" },
            { "English", "English" },

            // ---------------- title bar / status ----------------
            { "fan control", "风扇控制" },
            { "Switch to dark", "切换到深色" },
            { "Switch to light", "切换到浅色" },
            { "light", "浅色" },
            { "dark", "深色" },

            // ---------------- sections ----------------
            { "Fan Mode", "风扇模式" },
            { "applies immediately to the embedded controller", "立即写入嵌入式控制器" },
            { "Fan Curve", "风扇曲线" },
            { "read live from the EC — drag points to edit", "实时读取自 EC — 拖动圆点编辑" },
            { "Speed Offset", "转速偏置" },
            { "a boost added on top of the automatic curve", "在自动曲线之上额外加成" },
            { "Sensors", "传感器" },
            { "live EC telemetry", "EC 实时遥测" },
            { "Actions", "操作" },

            // ---------------- fan mode pills ----------------
            // Mode names are kept in Latin: they are firmware vocabulary the vendor docs use,
            // and a Chinese-only label would make them impossible to look up.
            { "Automatic", "自动" },
            { "Silent", "静音" },
            { "Noiseless", "无声" },
            { "Maximum", "最大" },
            { "MaxQ", "MaxQ" },
            { "Custom", "自定义" },
            { "IQST", "IQST" },
            { "NoiselessEx", "超静音" },
            { "accepted by the EC, but has no measurable effect on AC power", "EC 接受该模式，但插电时无可测量效果" },
            { "Silent and Noiseless do nothing on AC power here — for quiet use Offset 0% or the Quiet preset.",
              "在本固件上，插电时“静音/无声”模式不起作用 — 想要安静请把偏置设为 0% 或改用“安静”预设。" },

            // ---------------- fan tabs / sensor table ----------------
            { "CPU Fan", "CPU 风扇" },
            { "GPU Fan", "GPU 风扇" },
            { "GPU Fan 2", "GPU 风扇 2" },
            { "Case Fan", "机箱风扇" },
            { "CPU", "CPU" },
            { "GPU", "GPU" },
            { "GPU 2", "GPU 2" },
            { "Case", "机箱" },
            { "FAN", "风扇" },
            { "RPM", "转速" },
            { "DUTY", "占空比" },
            { "TEMP", "温度" },
            { "show all EC slots", "显示全部 EC 通道" },
            { "hide empty EC slots", "隐藏空闲 EC 通道" },

            // ---------------- curve editor ----------------
            { "Default", "恢复默认" },
            { "Save curve to fans", "保存曲线到风扇" },
            { "Save curve to fans  •", "保存曲线到风扇  •" },
            { "Quiet", "安静" },
            { "Balanced", "均衡" },
            { "Stock", "原厂" },
            { "Cool", "强力" },
            { "User", "用户" },
            { "click to load your saved curve; Save curve to fans stores the current one here",
              "点击可载入你已保存的自定义曲线；按“保存曲线到风扇”会把当前曲线存到这里" },
            { "no saved curve yet", "还没有已保存的曲线" },
            { "drag the two middle points, then Save. This fan's window is {0}–{1} °C; the EC owns both ends.",
              "拖动中间两个圆点后点“保存”。该风扇可调区间为 {0}–{1} °C，两端由 EC 固定。" },
            { "unsaved changes — press Save to upload them. This fan's window is {0}–{1} °C; the EC owns both ends.",
              "有未保存的修改 — 点“保存”写入。该风扇可调区间为 {0}–{1} °C，两端由 EC 固定。" },
            { "could not write the theme preference to ", "无法写入主题偏好到 " },
            { "Quiet keeps the fans near idle until 75 °C — watch your temperatures after applying.",
              "“安静”预设会让风扇在 75°C 前接近停转 — 应用后请留意温度。" },
            { "Duty %", "占空比 %" },
            { "Temperature °C", "温度 °C" },
            { "not present", "未安装" },

            // ---------------- offset card ----------------
            { "Boost +25", "加速 +25" },
            { "Max +100", "最高 +100" },
            { "Raises the duty the automatic curve asks for. 0 % = untouched; above ~40 % pins the fans at full speed.",
              "在自动曲线要求的占空比之上再抬高。0% 表示不改变；超过约 40% 就会把风扇顶到满速。" },

            // ---------------- actions card ----------------
            { "Re-apply my curve at startup", "开机时自动重新应用我的曲线" },
            { "Run Anti-Dust", "运行除尘" },
            { "Refresh now", "立即刷新" },
            { "profile  ", "配置文件  " },
            { "{0} % duty", "{0} % 占空比" },

            // ---------------- status bar ----------------
            { "InsydeDCHU.dll not found — install the CLEVO Control Center",
              "未找到 InsydeDCHU.dll — 请安装 CLEVO 控制中心" },
            { "driver unavailable", "驱动不可用" },
            { "EC returned no data", "EC 未返回数据" },
            { "curve saved to the EC and to your profile — it will be re-applied at startup",
              "曲线已写入 EC 并保存到配置 — 开机时会自动重新应用" },
            { "curve upload failed", "曲线写入失败" },
            { "updated {0:HH:mm:ss} · poll {1:N1} s · {2}", "已更新 {0:HH:mm:ss} · 轮询 {1:N1} 秒 · {2}" },

            // ---------------- exclusivity dialog ----------------
            { "Fan control exclusivity", "风扇控制权说明" },
            { "Who is controlling the fans?", "现在是谁在控制风扇？" },
            { "CoolDeck writes fan curves and modes straight through the vendor InsydeDCHU protocol. It does not install a service, a driver or a startup task of its own, and it changes nothing until you press a button.",
              "CoolDeck 直接通过厂商的 InsydeDCHU 协议写入风扇曲线与模式。它不安装服务、驱动或自启动项，在你点击按钮之前不会改动任何东西。" },
            { "The embedded controller is the real authority. Whatever CoolDeck uploads can be overwritten at any time by another program writing the same registers, and the EC itself also re-derives its curve table while the fan mode is Automatic. That is why saving a curve switches the mode to Custom first.",
              "真正的权威是嵌入式控制器（EC）。CoolDeck 写入的内容随时可能被其他程序改写同一组寄存器覆盖；而风扇模式处于“自动”时，EC 也会自行维护它的曲线表。这就是为什么保存曲线必须先把模式切到“自定义”。" },
            { "What CoolDeck can do about it: tell you which vendor fan programs are currently running, and get you to the Windows startup settings so you can stop them launching. You can also just close them.",
              "CoolDeck 能做的：告诉你当前有哪些厂商风扇程序在运行，并帮你打开 Windows 启动设置以便禁止它们自启。你也可以直接手动关闭它们。" },
            { "What CoolDeck will NOT do: the vendor hotkey service (DCHUService) runs as LocalSystem and starts automatically. A normal unelevated program cannot stop or disable it, and CoolDeck will not pretend to. Disabling it needs administrator rights and is left to you. Leaving that service running is harmless on its own — it is the curve-writing apps that conflict.",
              "CoolDeck 不会做的事：厂商热键服务（DCHUService）以 LocalSystem 身份运行并自动启动。普通的非管理员进程无法停止或禁用它，CoolDeck 也不会假装能做到。禁用它需要管理员权限，这件事交给你自己判断。该服务本身常驻是无害的 —— 真正会冲突的是那些会写曲线的上层程序。" },
            { "Vendor fan processes running right now", "当前正在运行的厂商风扇进程" },
            { "none found — CoolDeck appears to be the only writer.",
              "未发现 —— 目前看起来只有 CoolDeck 在写入。" },
            { "  none found — CoolDeck appears to be the only writer.",
              "  未发现 —— 目前看起来只有 CoolDeck 在写入。" },
            { "Present here does not mean they are fighting you: only the ones that\n  write fan curves do. Close them from Task Manager if curves keep changing.",
              "出现在此列表不代表它们在和你争夺控制权：只有会写风扇曲线的才会。\n  如果曲线反复被改动，可在任务管理器中结束它们。" },
            { "\n  Present here does not mean they are fighting you: only the ones that\n  write fan curves do. Close them from Task Manager if curves keep changing.",
              "\n  出现在此列表不代表它们在和你争夺控制权：只有会写风扇曲线的才会。\n  如果曲线反复被改动，可在任务管理器中结束它们。" },
            { "Re-scan", "重新扫描" },
            { "Show vendor fan apps", "显示厂商风扇进程" },
            { "Open Startup settings", "打开启动设置" },
            { "Close", "关闭" },
            { "could not open Startup settings: ", "无法打开启动设置：" },
            { "scan failed: ", "扫描失败：" },
            { "could not open the registry for reading", "无法以只读方式打开注册表" },
            { "Could not change the CoolDeck startup entry:", "无法修改 CoolDeck 的开机启动项：" },
            { "OK", "确定" },
            };
        }
    }
}
