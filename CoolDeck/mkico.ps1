# Generates app.ico — a stylised fan / CoolDeck mark.
# Draws at 256px into a bitmap, scales down with high-quality interpolation,
# and emits a real multi-resolution .ico (16/24/32/48/64/128/256).
param([string]$Out = 'D:\workroom\fanctl\CoolDeck\app.ico')

Add-Type -AssemblyName System.Drawing

function New-FanBitmap([int]$size) {
    $bmp = New-Object System.Drawing.Bitmap($size, $size)
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    $g.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias
    $g.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
    $g.PixelOffsetMode = [System.Drawing.Drawing2D.PixelOffsetMode]::HighQuality
    $g.Clear([System.Drawing.Color]::Transparent)

    $s = $size / 256.0

    # rounded-square tile with the app's accent gradient
    $tile = New-Object System.Drawing.RectangleF([float](8*$s), [float](8*$s), [float](240*$s), [float](240*$s))
    $path = New-Object System.Drawing.Drawing2D.GraphicsPath
    $r = [float](56 * $s)
    $d = [float](2 * $r)
    $path.AddArc($tile.X, $tile.Y, $d, $d, 180, 90)
    $path.AddArc($tile.Right - $d, $tile.Y, $d, $d, 270, 90)
    $path.AddArc($tile.Right - $d, $tile.Bottom - $d, $d, $d, 0, 90)
    $path.AddArc($tile.X, $tile.Bottom - $d, $d, $d, 90, 90)
    $path.CloseFigure()

    # #22D3EE -> #818CF8 diagonal gradient
    $c1 = [System.Drawing.Color]::FromArgb(255, 0x22, 0xD3, 0xEE)
    $c2 = [System.Drawing.Color]::FromArgb(255, 0x81, 0x8C, 0xF8)
    $brush = New-Object System.Drawing.Drawing2D.LinearGradientBrush($tile, $c1, $c2, 45.0)
    $g.FillPath($brush, $path)
    $path.Dispose(); $brush.Dispose()

    # fan hub + blades, drawn in the dark tile colour
    $cx = [float](128 * $s); $cy = [float](128 * $s)
    $dark = [System.Drawing.Color]::FromArgb(235, 0x0B, 0x0D, 0x12)
    $hub = [float](26 * $s)
    $g.FillEllipse((New-Object System.Drawing.SolidBrush $dark), $cx - $hub, $cy - $hub, $hub * 2, $hub * 2)

    for ($i = 0; $i -lt 5; $i++) {
        $a = $i * 72.0 - 90.0
        $rad = $a * [Math]::PI / 180.0
        # blade: a fat teardrop from the hub outward
        $bx = $cx + [float]([Math]::Cos($rad) * 52 * $s)
        $by = $cy + [float]([Math]::Sin($rad) * 52 * $s)
        $bw = [float](58 * $s); $bh = [float](58 * $s)
        $g.FillEllipse((New-Object System.Drawing.SolidBrush $dark), $bx - $bw/2, $by - $bh/2, $bw, $bh)
    }

    # hub ring in accent colour so the centre reads as a hub
    $ring = [float](9 * $s)
    $g.FillEllipse((New-Object System.Drawing.SolidBrush $c1), $cx - $ring, $cy - $ring, $ring * 2, $ring * 2)

    $g.Dispose()
    return $bmp
}

$sizes = 16, 24, 32, 48, 64, 128, 256
$ms = New-Object System.IO.MemoryStream
$bw = New-Object System.IO.BinaryWriter($ms)

# ICONDIR
$bw.Write([UInt16]0)      # reserved
$bw.Write([UInt16]1)      # type = icon
$bw.Write([UInt16]$sizes.Length)

$offset = 6 + 16 * $sizes.Length
$pngs = @{}
foreach ($sz in $sizes) {
    $bmp = New-FanBitmap $sz
    $png = New-Object System.IO.MemoryStream
    $bmp.Save($png, [System.Drawing.Imaging.ImageFormat]::Png)
    $bytes = $png.ToArray()
    $pngs[$sz] = $bytes
    $bmp.Dispose(); $png.Dispose()

    $bw.Write([Byte]$(if ($sz -ge 256) { 0 } else { $sz }))  # width
    $bw.Write([Byte]$(if ($sz -ge 256) { 0 } else { $sz }))  # height
    $bw.Write([Byte]0)        # colours
    $bw.Write([Byte]0)        # reserved
    $bw.Write([UInt16]1)      # planes
    $bw.Write([UInt16]32)     # bpp
    $bw.Write([UInt32]$bytes.Length)
    $bw.Write([UInt32]$offset)
    $offset += $bytes.Length
}
foreach ($sz in $sizes) { $bw.Write($pngs[$sz]) }
$bw.Flush()

[System.IO.File]::WriteAllBytes($Out, $ms.ToArray())
$bw.Close(); $ms.Close()
"wrote $Out  ($((Get-Item $Out).Length) bytes, $($sizes.Count) sizes)"
