# CoolDeck

A modern, standalone replacement for the stock CLEVO **Fan Speed Setting** app
(`CLEVOCO.504814C03D814`), built for the **DEVIL RAYS DR521** (Clevo P955ET1
board, INSYDE BIOS 1.07.04TBPC1, IT858 EC firmware 07.02).

Single self-contained `.exe` + the vendor `InsydeDCHU.dll`. **No administrator
rights, no background service, no UWP app, no third-party dependencies.**

```
bin\CoolDeck.exe          GUI (dark WPF, software-rendered for RDP/lock-screen safety)
bin\InsydeDCHU.dll        vendor DCHU protocol DLL (App build, 11.5 MB, exports SetDCHU_DataEx)
bin\cooldeck.log          optional crash breadcrumb log
```

---

## The reverse-engineered protocol

Everything below was derived by decompiling `FanSpeedSetting.exe` (ILSpy 8.2) and
verified live against the EC. Nothing here is documented by Clevo.

### Transport

`InsydeDCHU.dll` itself opens the device interface and talks to the driver:

```
CM_Get_Device_Interface_ListW  →  \\?\acpi#clv0001#1#{86994c74-ad43-4812-b7e7-0c420b5c5fd7}
DeviceIoControl                →  AcpiBridge.sys (ACPI\CLV0001)
```

`AcpiBridge.sys` is a ~8 KB WDF driver that only forwards ACPI IRP calls; the fan
PWM closed loop lives entirely in EC firmware. `DCHUService.exe` is **not** required.

### Exports

```csharp
int GetDCHU_Data_Integer(int cmd, ref int val)
int GetDCHU_Data_Buffer (int cmd, ref byte buf)     // buf = 256 bytes
int SetDCHU_Data       (int cmd, byte[] buf, int len)
int SetDCHU_DataEx     (int cmd, byte[] in, int len, ref byte out)
int ReadAppSettings    (int hi, int lo, int len, ref byte buf)
int WriteAppSettings   (int hi, int lo, int len, ref byte buf)
```

`ReadAppSettings` / `WriteAppSettings` pack `(hi, lo)` into a 16-bit store address.
Return code `4096` = OK for reads, `1` = OK for writes. `len > 4` works for
`(hi=4, lo=0)` but silently returns zeros for most other offsets.

### Sensor block — `GetDCHU_Data_Buffer(12)`

All multi-byte tachometer fields are **big-endian**.

| offset | meaning |
|---|---|
| `@2..3`  | fan 1 tachometer count |
| `@4..5`  | fan 2 tachometer count |
| `@6..7`  | fan 3 tachometer count |
| `@36..37`| fan 4 tachometer count |
| `@16` `@19` `@22` `@38` | fan 1..4 duty, 0–255 |
| `@18` `@21` `@24` `@40` | fan 1..4 temperature, **identity °C** (raw 92 → 92 °C) |

The tachometer count is **not** RPM — it is inversely proportional to it. The stock
app converts it in `\u0002\u2002()` (FanSpeedSetting.decompiled.cs:9639-9643):

```
rpm = round( 60 / (5.565217391304348e-05 * raw) * 2 )
    = round( 2156000 / raw )
```

Verified sample-by-sample against the stock app's own `T_CPUFan_RPM_value` /
`T_GPUFan_RPM_value`: raw 463 → 4657 (app: 4657), raw 464 → 4647 (app: 4647).

### Fan curve — `GetDCHU_Data_Buffer(13)`

Four points of `(temp °C, duty 0–255)` per fan, fan *n* at `16 + n*8`, point *k* at
`+2k`. Read-only in practice — see limitations below.

Stock curves on this machine:

| fan | curve |
|---|---|
| CPU | (40, 35 %) (60, 55 %) (80, 83 %) (100, 100 %) |
| GPU | (48, 35 %) (60, 51 %) (80, 83 %) (95, 100 %) |
| GPU 2 / Case | not present (all zero) |

### Fan mode — store 4, offset 5

| value | meaning | stock UI label |
|---|---|---|
| 0 | Automatic | Automatic |
| 1 | Maximum | Maximum |
| 2 | Noiseless | Noiseless mode |
| 3 | Silent | — |
| 5 | MaxQ | Turbo |
| 6 | Custom | Custom |
| 8 | NoiselessEx | — |
| 9 | IFSC | IQST |

(The `FanMode` enum at FanSpeedSetting.decompiled.cs:2221 and the checked-event
handlers at lines 10486 / 10512 / 10524.)

### Speed offset — store 4, offset 7

Percentage 0–100 that scales the automatic curve.

### Write commands

`SetDCHU_Data(cmd, sub, value)` uses a 4-byte payload `= (sub << 24) | value`:

```csharp
// fan mode
SetDCHU_Data(121, sub:1,  value:mode)                 // live EC channel
WriteAppSettings(4, 5, [mode])                        // persisted store

// speed offset
SetDCHU_Data(121, sub:14, value:round(255*pct/100))   // live EC channel
WriteAppSettings(4, 7, [pct])                         // persisted store

// anti-dust trigger
SetDCHU_Data(118, sub:1, value:packedNow)             // sec | min<<6 | hour<<12 | dow<<17
```

### Settings store 4 layout (curve staging area)

18 bytes per fan, fan *n* at `16 + n*18`:

```
[+0..2]   duty% at points 0,1,2          [+3] = 100
[+4..5]   duty% at points 1,2 (duplicate)
[+6..8]   temp °C at points 0,1,2        [+9] = 100
[+10..11] temp °C at points 1,2 (duplicate)
[+12..13] slope 0→1   [+14..15] slope 1→2   [+16..17] slope 2→3   (signed LE16)
```

Slope = `round((dNext - dCur) / (tNext - tCur) * 2.55 * 16)` — confirmed against the
live store (fan 1: 41, 57, 35; fan 2: 54, 65, 35).

### Useful commands

| command | result |
|---|---|
| `GetDCHU_Data_Buffer(12)` | sensor block (above) |
| `GetDCHU_Data_Buffer(13)` | fan curves (above) |
| `GetDCHU_Data_Buffer(18)` | XTU fan table (two identical copies at @48 and @80) |
| `SetDCHU_DataEx(4, [1,sub,0,0,0,0,magic], 256, out)` | `out[0]=0xFA` status, `out[2..]` data |
| `SetDCHU_DataEx` sub 1, magic 222 | EC version string (`07.02`) |
| `SetDCHU_DataEx` sub 3, magic 222 | EC chip (`IT858`) |
| `GetDCHU_Data_Integer(16)` | 147 = feature bitmask |
| `GetDCHU_Data_Integer(96)` | 540 = feature switches |

Magic bytes are per-sub and must match or the call returns all zeros: sub 9/11 → 192,
sub 163 → 184, serial-number subs → 222.

### Dead ends (do not re-try)

- `Win32_Fan` does not exist on this machine (both `root\CIMV2` and `root\WMI`).
- There is no Intel DPTF / `esif.sys`, and no `Kernel-Processor-Power` throttling events.
- `GetDCHU_Data_Buffer` / `GetDCHU_Data_Integer` respond to **nothing above cmd 122** —
  the whole 16-bit space 0..2047 was swept. The values `1029 / 1031 / 257 / 1094..1101`
  in the decompiled code are `ReadAppSettings` packed addresses, not hidden commands.
- The stock app's `.NET` strings are encrypted by a commercial obfuscator with
  stack-walk anti-tamper; static decryption is not practical.
- Full ASCII/UTF-16 string scans of `AcpiBridge.sys` are useless — the driver is
  small and the output is pure x86 instruction noise.

---

## Known limitation: the fan curve is not live-writable

On this firmware the EC curve cannot be changed over DCHU. Verified:

- `WriteAppSettings(4, 0, 256, buf)` **does** persist (return `1`, the store changes).
- `GetDCHU_Data_Buffer(13)` keeps returning the built-in curve afterwards.
- No `SetDCHU_Data(121, sub=34)` / mode-switch / XTU-table combination pushes it.

The stock app's curve editor is therefore also inert on this machine — it writes the
same staging area and nothing more. CoolDeck reproduces that behaviour and labels the
button honestly ("Stage to EC store") instead of pretending it works. Everything else
— live telemetry, mode, offset, anti-dust — is fully functional.

---

## Build

Requires only the .NET Framework 4.x compiler that ships with Windows — no .NET SDK,
no NuGet, no MSBuild project files.

```powershell
.\build.ps1
```

This compiles `src\*.cs` with `csc.exe` against the WPF reference assemblies and
copies `InsydeDCHU.dll` next to the output.

### Why pure C# instead of XAML

The toolchain here has no .NET SDK, so XAML compilation (`PresentationBuildTasks`)
is not reachable in a one-command build. The whole UI is constructed in C# instead —
see `src/Controls.cs` for the hand-drawn `FanGauge`, `PillRow` and `CurveEditor`.

---

## Usage

### GUI

```
bin\CoolDeck.exe
```

- Two arc gauges: live RPM, duty %, temperature (colour-coded green/amber/red).
- Six mode pills — apply instantly to the EC.
- Fan curve chart for CPU / GPU / GPU2 / Case, points draggable.
- Speed offset slider (0–100 %).
- Sensor table for all four channels.
- Minimises to the tray; single-instance.

### CLI

```
CoolDeck.exe --status              # live telemetry as JSON
CoolDeck.exe --mode 6              # Custom
CoolDeck.exe --mode 0              # Automatic
CoolDeck.exe --offset 25           # +25 % on top of the automatic curve
CoolDeck.exe --curve 0 40 35 60 20 80 25
                                   # CPU curve (40,35%)(60,20%)(80,25%)(100,100%)
CoolDeck.exe --help
```

**Curve control.** Drag the two middle points, then press **Save curve to fans**. The
button turns amber (`•`) while there are unapplied edits. It uploads over **DCHU cmd 14**
and takes effect **immediately** — no reboot, no service restart. The 32-byte payload
carries `(t1,d1)` and `(t2,d2)` for up to three fans plus three signed BE16 slopes per fan
(`slope = round(Δduty/Δtemp × 2.55 × 16)`).

**Only the two middle points are draggable — and the editable window is per-fan.** Each fan's
curve is bounded by two EC-owned endpoints that differ by channel: the CPU fan runs
**40…100 °C**, the GPU fan runs **48…95 °C**. The editor reads that fan's own endpoints out of
cmd 13 and derives its X axis from them, so the GPU tab is labelled 48…95 and not 40…100. An
earlier build plotted a fixed 40…100 axis on every tab, which let you drag points outside the
range that fan can hold; `ApplyCurve` then clamped them silently — the "I saved it and it
changed" bug all over again. `ApplyCurve` now clamps to the same per-fan window the editor
shows, so UI and CLI agree.

### Can more curve points be added? (investigated, answer: no)

The EC table is a hard 4 points per fan and the upload channel exposes 2 of them. There is no
richer curve anywhere in the protocol:

- cmd 13 is exactly 4 `(temp, duty)` pairs per fan at `16 + fan*8`
- cmd 14 carries only `t1/d1` and `t2/d2` per fan — no field exists for the endpoints
- a full sweep of `GetDCHU_Data_Buffer` over cmd 0..255 and 0..2047 returns data for
  **only 12, 13 and 18**
- cmd 18, the "XTU fan table", is unrelated: it holds `21,21,47,20,31`, `45000`,
  `1000/1200/1200` and a trailing `45,44,43,42` — Intel XTU-side data, not a fan curve

### Can the outer points ever be changed? (investigated, answer: no)

Three separate attempts, all negative — recorded so nobody repeats them:

| Attempt | Result |
|---|---|
| Write `t0`/`d0` into the settings store (18-byte block, offsets `[+0]`/`[+6]`) | Store **retained** the value; the live curve (cmd 13) never adopted it, in Custom mode or across an Auto→Custom bounce |
| Store-only write of `t1`/`d1`/`t2`/`d2` (no cmd 14 at all) | Live curve **unchanged** → the store is persistence-only, not a control path |
| Vary **only `slope0`** in the cmd 14 payload, to back-door point 0's duty | Point 0 stayed at `(40 °C, 35 %)` for all four overrides |

**But the two settable points have far more range than first assumed** (measured): within a
fan's own window, `t1`/`t2` and `d1`/`d2` accept the full span, and every shape tried stuck —
`(41,0%)(99,0%)`, `(95,5%)(99,10%)`, `(45,10%)(70,20%)`.

**Practical consequence — you can effectively bypass the floor point.** Set `t1` one degree
above the fan's floor with a low `d1`, and the fan is commanded near-idle from there all the
way up to `t2`, so the EC's floor point stops mattering. That is the closest thing to editing
point 1, achievable entirely through the two points you *can* drag.

**ORDER MATTERS — switch to Custom *before* uploading.** A curve uploaded while the mode is
Automatic does not survive; switching to Custom first does. `ApplyCurve` therefore does:

1. `SetMode(Custom)`
2. `SetDCHU_Data(14, payload)` — upload the curve
3. `StageCurve` — persist to the settings store so it survives a mode change

Measured: Custom-first → **0 rewrites in 90 s**; Auto-first → clobbered. Pressing Save
therefore also switches the fan mode to Custom.

*On the mechanism.* An earlier revision of this file claimed the EC "continuously re-derives"
its table in Automatic mode. A read-only sweep of the whole command space found **no evidence
of autonomous EC mutation**, so that wording was wrong. What is actually established:

- **cmd 13 is a lazily-refreshed mirror of the settings store, not live EC memory.** It can lag
  a write by a few seconds and re-sync mid-session. Polling it for deltas is unreliable.
- In Automatic mode the EC drives the fan from its own internal table, so an uploaded custom
  table has no effect until the mode is Custom.
- Consequence for any tooling: compare against `store(4)` synchronously, never against cmd 13.

**Never carry neighbouring fans' values through cmd 13.** cmd 14 uploads all three fans in one
payload, so a single-fan edit must resupply the other two. Reading those from cmd 13 — as an
earlier build did — re-uploads whatever the mirror happened to be holding and silently
overwrites channels the user never touched. This actually corrupted the CPU and GPU curves
during development. `ApplyCurve` now sources them from `store(4)`, and a regression test
confirms editing one fan leaves the other two byte-identical.

**The EC answers with a stub body after heavy traffic.** Following lots of DCHU reads, cmd 12
keeps returning `rc == 12` (success) but a placeholder payload — both temperatures pinned near
1 °C, every tach and duty zero — for up to ~10 s. Because the return code says success and the
body is *not* all-zero, a naive zero check does not catch it; this is what made the UI briefly
show "1 °C / not present". `Controller.IsStub()` recognises the shape and reuses the last real
reading, flagging the frame as stale.

Fan presence is likewise voted across polls (a channel is live only after two separate reads
show tach, duty or a plausible temperature) and then sticks, so neither the stub window nor a
fan idling at zero tach can hide a real fan.

The curve editor does **not** follow the EC while you have unsaved edits (that would wipe
your drag on the next poll), and it does not write anything until you press Save.

Presets: **Quiet / Balanced / Stock / Cool** fill the editor; they still need Save to take
effect.

### Your curve is permanent — and why that needed code of its own

Writing a curve to the EC is **not** persistence. Two things defeat it:

1. The fan **mode** is separate state, and a boot can come back in Automatic — and in
   Automatic the EC drives the fan from its own internal table, so your uploaded curve has no
   effect even though it is still sitting there.
2. Nothing in the vendor protocol re-asserts your choices on the next power cycle.

So CoolDeck keeps its own profile and re-applies it:

```
%APPDATA%\CoolDeck\profile.txt
    mode=6
    offset=0
    autoapply=1
    fan0=40:35,41:0,99:33,100:100      # t:d for each of the four points
    fan1=48:35,49:0,94:35,95:100
```

- **Saved automatically** after every deliberate change — Save curve, mode pill, offset slider
  or preset. There is no separate "save profile" button to forget.
- **Re-applied at startup**, ~3 s after launch. The delay is deliberate: right after boot the
  EC answers `cmd 12` with a plausible-looking stub body, and comparing against that would be
  comparing against fiction. If a stale frame is detected the restore retries.
- **Only the fans that differ are rewritten**, and physically absent channels are skipped, so a
  restore issues the minimum number of EC writes.
- **First run adopts what is already live** rather than writing in a default, so installing
  CoolDeck never overwrites a curve you had set with the vendor app.
- Toggle with **"Re-apply my curve at startup"** in the Actions card. The file is plain
  `key=value` text — hand-editable, no serializer involved (there is no JSON library available
  under csc 4.0, and a text file is friendlier anyway).

Verified end to end by forcing the EC back to stock + Automatic and relaunching:

```
BEFORE: 40C/35%  60C/55%  80C/83%  100C/100%     <- reset to stock
AFTER : 40C/35%  41C/0%   99C/33%  100C/100%     <- user curve restored
log:    re-applying profile: fans [0,1] mode=6
```

**The EC overrides you when it needs to.** On this firmware the duty you see can exceed what
your curve asks for — e.g. 71 % while the curve implies 43 % at 90 °C. That is the EC's own
thermal protection stepping in, not a CoolDeck bug. A quiet curve lowers the *floor*; it
cannot stop the EC ramping up when the die gets hot.

**Speed offset** is a one-directional boost on top of the automatic curve: 0 % leaves
the curve untouched, ~40 % already pins the fans at full speed. It is the only live
quiet/loud lever besides the curve. **Silent** and **Noiseless** are accepted by the EC
but do nothing measurable on AC power on this firmware; both are dimmed in the UI.

Example:

```json
{
  "board": "DR521",
  "ec": "IT858 07.02",
  "mode": 0,
  "offsetPct": 0,
  "fans": [
    { "name": "CPU Fan", "present": true, "rpm": 4083, "dutyPct": 83, "tempC": 90 },
    { "name": "GPU Fan", "present": true, "rpm": 3724, "dutyPct": 83, "tempC": 50 },
    { "name": "GPU Fan 2", "present": false, "rpm": 0, "dutyPct": 0, "tempC": 0 },
    { "name": "Case Fan", "present": false, "rpm": 0, "dutyPct": 0, "tempC": 0 }
  ],
  "curves": [
    [[40,35],[60,55],[80,83],[100,100]],
    [[48,35],[60,51],[80,83],[95,100]],
    [[0,0],[0,0],[0,0],[0,0]],
    [[0,0],[0,0],[0,0],[0,0]]
  ]
}
```

---

## Source map

| file | contents |
|---|---|
| `src/Native.cs` | dynamic `LoadLibrary`/`GetProcAddress` binding to `InsydeDCHU.dll`, typed wrappers, DLL discovery |
| `src/Controller.cs` | high-level API: `Snapshot`, mode/offset get+set, curve read, curve staging, anti-dust |
| `src/Theme.cs` | colour palette and temperature/duty colour mapping |
| `src/Controls.cs` | `FanGauge` (270° arc), `PillRow` (mode selector), `CurveEditor` (draggable points) |
| `src/MainWindow.cs` | layout, 0.9 s poll timer, event wiring |
| `src/App.cs` | entry point, single-instance mutex, tray icon, `--status/--mode/--offset/--curve` CLI |
| `src/Trace.cs` | append-only breadcrumb log for render-thread crashes |
| `app.manifest` | PerMonitorV2 DPI awareness |

### DLL discovery order

1. Next to `CoolDeck.exe`
2. `%LOCALAPPDATA%\CoolDeck\lib\`
3. `C:\Program Files\WindowsApps\CLEVOCO.504814C03D814_*` (App build preferred)
4. `C:\Program Files (x86)\ControlCenter\`

The App build is preferred because it is the only one that exports
`SetDCHU_DataEx` (needed for the EC version/chip strings).

---

## 界面语言 / UI language

The UI follows the Windows **display language** automatically: a `zh-*` UI culture renders
Chinese, anything else renders English. There is no setting to hunt for — it just matches the
OS.

To override it manually, write a two-letter code into the file

``
%APPDATA%\CoolDeck\lang.txt        # contents: zh   or   en
``

and restart CoolDeck. Delete the file to go back to following the OS.

English text is the lookup key (`Loc.T("Open CoolDeck")`), so a missing translation degrades
to English rather than showing a blank or a `key.not.found` placeholder. The string table is
built inside a `try`/`catch`: a duplicated key in the dictionary once threw from the static
constructor and killed the process on launch, so a string table is now structurally unable to
take the app down — it falls back to English and logs the reason to `cooldeck.log`.

Deliberately left in Latin script: `CoolDeck`, `CPU`/`GPU`, `RPM`, `MaxQ`, `IQST`.
These are firmware/vendor vocabulary, and translating them makes them impossible to look up in
Clevo documentation or Task Manager.

