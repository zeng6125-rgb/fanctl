param([string]$Out = 'D:\workroom\fanctl\CoolDeck\shot3.png')

Add-Type -AssemblyName System.Drawing

Add-Type -TypeDefinition @'
using System;
using System.Runtime.InteropServices;
public static class Grab {
    [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr h);
    [DllImport("user32.dll")] public static extern bool ShowWindow(IntPtr h, int c);
    [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr h, out RECT r);
    [DllImport("user32.dll")] public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);
    [DllImport("user32.dll")] public static extern IntPtr GetWindowDC(IntPtr h);
    [DllImport("user32.dll")] public static extern int ReleaseDC(IntPtr h, IntPtr dc);
    [DllImport("gdi32.dll")] public static extern IntPtr CreateCompatibleDC(IntPtr dc);
    [DllImport("gdi32.dll")] public static extern IntPtr CreateCompatibleBitmap(IntPtr dc, int w, int h);
    [DllImport("gdi32.dll")] public static extern IntPtr SelectObject(IntPtr dc, IntPtr obj);
    [DllImport("gdi32.dll")] public static extern bool DeleteObject(IntPtr obj);
    [DllImport("gdi32.dll")] public static extern bool DeleteDC(IntPtr dc);
    [StructLayout(LayoutKind.Sequential)] public struct RECT { public int L, T, R, B; }
}
'@

$p = Get-Process -Name 'CoolDeck' -ErrorAction SilentlyContinue | Select-Object -First 1
if (-not $p) { 'COOLDECK NOT RUNNING'; exit }

[Grab]::ShowWindow($p.MainWindowHandle, 9) | Out-Null
Start-Sleep -Milliseconds 600
[Grab]::SetForegroundWindow($p.MainWindowHandle) | Out-Null
Start-Sleep -Milliseconds 500

$r = New-Object 'Grab+RECT'
[void][Grab]::GetWindowRect($p.MainWindowHandle, [ref]$r)
$w = $r.R - $r.L; $h = $r.B - $r.T
"window rect: $($r.L),$($r.T) ${w}x${h}"
if ($w -le 0 -or $h -le 0) { 'EMPTY RECT'; exit }

$hdc = [Grab]::GetWindowDC($p.MainWindowHandle)
$mem = [Grab]::CreateCompatibleDC($hdc)
$hb  = [Grab]::CreateCompatibleBitmap($hdc, $w, $h)
[void][Grab]::SelectObject($mem, $hb)
# nFlags=2 -> PW_RENDERFULLCONTENT
[void][Grab]::PrintWindow($p.MainWindowHandle, $mem, 2)
[void][Grab]::ReleaseDC($p.MainWindowHandle, $hdc)
[void][Grab]::DeleteDC($mem)

$bmp = New-Object System.Drawing.Bitmap([System.Drawing.Image]::FromHbitmap($hb))
$bmp.Save($Out, [System.Drawing.Imaging.ImageFormat]::Png)
"saved $Out  ($($bmp.Width)x$($bmp.Height))"
$bmp.Dispose()
[void][Grab]::DeleteObject($hb)
