# CoolDeck build script
# Compiles the pure-C# WPF app with the .NET Framework 4.x compiler (no SDK required).

$ErrorActionPreference = 'Stop'

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$src  = Join-Path $root 'src'
$out  = Join-Path $root 'bin'
if (-not (Test-Path $out)) { New-Item -ItemType Directory -Path $out | Out-Null }

$csc = 'C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe'
$wpf = 'C:\Windows\Microsoft.NET\Framework64\v4.0.30319\WPF'
$fw  = 'C:\Windows\Microsoft.NET\Framework64\v4.0.30319'

$refs = @(
    "$wpf\PresentationFramework.dll",
    "$wpf\PresentationCore.dll",
    "$wpf\WindowsBase.dll",
    "$fw\System.Xaml.dll",
    'System.dll',
    'System.Core.dll',
    'System.Drawing.dll',
    'System.Windows.Forms.dll',
    'System.Configuration.dll'
) | ForEach-Object { "-r:$_" }

$files = Get-ChildItem -Path $src -Filter '*.cs' | ForEach-Object { $_.FullName }

$ico = Join-Path $root 'app.ico'
$manifest = Join-Path $root 'app.manifest'
$argline = @(
    '-nologo', '-optimize+', '-target:winexe',
    "-out:$out\CoolDeck.exe",
    "-win32manifest:$manifest"
)
if (Test-Path $ico) { $argline += "-win32icon:$ico" }
$argline += $refs + $files
Write-Host ("argline[4] = [" + $argline[4] + "]  len=" + $argline.Count)

# csc via a response file: avoids PowerShell's native-argument quoting mangling
# (array elements containing ':' after a switch get split by the Win32 command-line parser)
$rsp = Join-Path $out 'build.rsp'
$argline | Set-Content -Path $rsp -Encoding ASCII

Write-Host "compiling CoolDeck..."
& $csc "@$rsp"
if ($LASTEXITCODE -ne 0) { Write-Host "BUILD FAILED"; exit 1 }

# ship the vendor DLL next to the exe when we have a copy in the tree
$vendors = @(
    (Join-Path $root 'lib\InsydeDCHU.dll'),
    'D:\workroom\fanctl\re\bin\InsydeDCHU.dll'
)
foreach ($v in $vendors) {
    if (Test-Path $v) {
        Copy-Item $v (Join-Path $out 'InsydeDCHU.dll') -Force
        Write-Host "vendored $(Split-Path -Leaf $v) -> bin\"
        break
    }
}

Write-Host "OK -> $out\CoolDeck.exe"
