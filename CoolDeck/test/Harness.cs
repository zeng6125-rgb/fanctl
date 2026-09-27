using System;
using CoolDeck;

static class Harness
{
    static void Main()
    {
        Console.WriteLine("init...");
        var ctl = new Controller();
        string p = ctl.Init();
        Console.WriteLine("dll = " + (p ?? "<null>"));
        if (p == null) return;
        Console.WriteLine("chip={0} ver={1} board={2} hasEx={3}",
            ctl.EcChip, ctl.EcVersion, ctl.Board, Native.HasEx);

        for (int i = 0; i < 3; i++)
        {
            var s = ctl.Read();
            if (s.Error != null) { Console.WriteLine("ERR " + s.Error); return; }
            foreach (var f in s.Fans)
                Console.WriteLine("  {0,-10} rpm={1,6} duty={2,3}% temp={3,3}C raw={4}",
                    f.Name, f.Rpm, f.DutyPct, f.TempC, f.RawTach);
            Console.WriteLine("  mode={0} offset={1}", s.Mode, s.Offset);
            for (int k = 0; k < 4; k++)
            {
                string c = "";
                foreach (var pt in s.Curves[k]) c += "(" + pt.Temp + "," + pt.Duty + "%) ";
                Console.WriteLine("  curve[{0}] {1}", k, c);
            }
            System.Threading.Thread.Sleep(800);
        }
        Console.WriteLine("OK");
    }
}
