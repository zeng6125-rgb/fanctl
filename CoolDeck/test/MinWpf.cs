using System;
using System.Windows;

static class MinWpf
{
    [STAThread]
    static void Main(string[] a)
    {
        var w = new Window
        {
            Width = 300, Height = 200,
            WindowStyle = WindowStyle.None,
            AllowsTransparency = a.Length > 0 && a[0] == "trans",
            Background = a.Length > 0 && a[0] == "trans"
                ? System.Windows.Media.Brushes.Transparent
                : System.Windows.Media.Brushes.DarkSlateGray
        };
        w.Content = new System.Windows.Controls.TextBlock
        {
            Text = "hello", Foreground = System.Windows.Media.Brushes.White,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        w.Loaded += delegate { Console.WriteLine("LOADED OK"); };
        w.ContentRendered += delegate { Console.WriteLine("RENDERED OK"); };
        var app = new System.Windows.Application();
        app.Run(w);
        Console.WriteLine("CLOSED OK");
    }
}
