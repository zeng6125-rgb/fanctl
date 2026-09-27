using System;
using System.Runtime.InteropServices;
using System.Reflection;

// Verifies CoolDeck.exe's StageCurve through its own compiled assembly:
// stages a distinctive curve for fan 0, reads the store back, restores.
static class Verify
{
    static void Main(string[] args)
    {
        string dll = args.Length > 0 ? args[0] : @"C:\Users\User\Desktop\CoolDeck\CoolDeck.exe";
        var asm = Assembly.LoadFrom(dll);
        Type ctl = null;
        foreach (var t in asm.GetTypes())
            if (t.Name == "Controller") { ctl = t; break; }
        if (ctl == null) { Console.WriteLine("Controller type not found"); return; }

        object inst = Activator.CreateInstance(ctl);
        var stage = ctl.GetMethod("StageCurve");
        var readCurves = ctl.GetMethod("ReadCurves");
        var readMode = ctl.GetMethod("ReadMode");
        var setMode = ctl.GetMethod("SetMode");
        var readOffset = ctl.GetMethod("ReadOffset");

        Type nat = null;
        foreach (var t in asm.GetTypes()) if (t.Name == "Native") { nat = t; break; }
        Console.WriteLine("dll: " + (string)nat.GetMethod("Init").Invoke(null, null));

        Console.WriteLine("mode=" + readMode.Invoke(inst, null) + " offset=" + readOffset.Invoke(inst, null));
        Console.WriteLine("curves before: " + Fmt(readCurves.Invoke(inst, null)));

        // distinctive: (40,44%)(60,55%)(80,83%)(100,100%)  -- only d0 changes
        Type fp = null;
        foreach (var t in asm.GetTypes()) if (t.Name == "FanPoint") { fp = t; break; }
        var pts = Array.CreateInstance(fp, 4);
        int[] tt = { 40, 60, 80, 100 }, dd = { 44, 55, 83, 100 };
        for (int i = 0; i < 4; i++)
        {
            object o = Activator.CreateInstance(fp);
            fp.GetField("Temp").SetValue(o, tt[i]);
            fp.GetField("Duty").SetValue(o, dd[i]);
            pts.SetValue(o, i);
        }
        try
        {
            bool ok = (bool)stage.Invoke(inst, new object[] { 0, pts });
            Console.WriteLine("StageCurve(0, d0=44) -> " + ok);
        }
        catch (Exception ex)
        {
            Console.WriteLine("StageCurve threw: " + ex.GetType().Name);
            Exception e = ex;
            while (e != null) { Console.WriteLine("  " + e.GetType().Name + ": " + e.Message); e = e.InnerException; }
            return;
        }
        Console.WriteLine("curves after : " + Fmt(readCurves.Invoke(inst, null)));

        // restore
        var orig = Array.CreateInstance(fp, 4);
        int[] ot = { 40, 60, 80, 100 }, od = { 35, 55, 83, 100 };
        for (int i = 0; i < 4; i++)
        {
            object o = Activator.CreateInstance(fp);
            fp.GetField("Temp").SetValue(o, ot[i]);
            fp.GetField("Duty").SetValue(o, od[i]);
            orig.SetValue(o, i);
        }
        stage.Invoke(inst, new object[] { 0, orig });
        Console.WriteLine("restored     : " + Fmt(readCurves.Invoke(inst, null)));
    }

    static string Fmt(object curves)
    {
        var arr = (Array)curves;
        string s = "";
        foreach (var fan in arr)
        {
            s += "[";
            foreach (var p in (Array)fan)
            {
                var ty = p.GetType();
                s += "(" + ty.GetField("Temp").GetValue(p) + "," + ty.GetField("Duty").GetValue(p) + ")";
            }
            s += "] ";
        }
        return s;
    }
}
