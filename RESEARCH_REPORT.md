# DR521 EC Fan-Protocol Research Report

**Target:** ASUS/ASUS ROG NUC DR521 (Clevo CNW728A7X1 chassis), Insyde EC via `InsydeDCHU.dll` + `AcpiBridge.sys` (`\ACPI#CLV0001#1`, interface GUID `{86994C74-AD43-4812-B7E7-0C420B5C5FD7}`).
**Constraint honored:** strictly read-only against the EC (buffer reads, integer reads, `ReadAppSettings` store reads). No `SetDCHU_Data*`, no writes, no reboots, no installs.
**Method:** two purpose-built C# probes (`probeR1.cs` → `re/probeR1.out.txt`, `probeR2.cs` → `re/probeR2.out.txt`, both built with `csc.exe` into `re/bin`), plus static analysis of decompiled vendor app `FanSpeedSetting` (CoolDeck-style OEM control center) and `acpibridge.asm` disassembly.
**Date:** 2026-09-21/22 UTC.

---

## Executive answers

| Q | Answer |
|---|--------|
| Q1: Can the EC hold >4-point fan curves? | **No.** The EC-side table is fixed at 3 stored + 1 synthetic points per fan, 4 fans max. cmd18 is not a curve. |
| Q2: How to detect fan2/fan3? | They are physically absent on this machine (all-zero tach/duty/temp channels, all runs). No capability bit is populated; use a persistence rule on tach/duty (see §2). |
| Q3: Integer-cmd value map | Full 0..2047 sweep done. Only ~25 cmds return genuine values; everything else is identity-echo or `0x80000002` (unsupported). Notable: **cmd66=2 — the only candidate "fan count" value found.** Bitmaps in §3. |
| Q4: Does EC/vendor software mutate the table on its own? | **No EC autonomy found.** Two mutations observed, both on the Windows side: (1) cached cmd13 mirror re-syncing ~seconds after a write; (2) a cross-probe store(4) rewrite to stock values, best-attributed to a concurrently-running **CoolDeck** GUI instance (PID 22304) using its "Stage to EC store" `WriteAppSettings(4,…)` path — full attribution impossible post-mortem (exe overwritten, log records no DCHU writes). During quiescence, nothing drifts. §4. |

---

## 1. Q1 — point-count ceiling: no >4-point curves

### Transport-level evidence
- **cmd13** (256-byte buffer read) is the *current effective curve table*: 4 fan slots × 8 bytes at @16/@24/@32/@40, each slot = 4×`(temp,duty)` byte-pairs. **Tail @48..255 is all zero in every read across both probes** (`probeR1` `cmd13 tail @48..255 nonzero count=0`; `probeR2` 15-sample loop identical). No hidden 5th+ point region exists in the EC's exported table.
- **cmd14** (the only curve-upload command, 0x0E) has a **fixed 2-point format**: payload bytes 2..13 = 2 (temp,duty) pairs × 3 fans, bytes 14..31 = 3×2 **big-endian 16-bit slopes** per fan (decompiled `FanSpeedSetting` builder, lines 1003-1040). The 4th point on each fan is **synthetic**: the vendor parser for cmd13 always appends `(100°C,100%)` when displaying (line 836-1000); i.e. EC semantics are *3 stored points + implicit endpoint* — 4 is the hard ceiling, and uploads even via cmd14 carry only 2 of them plus slopes.
- **Persistent store `ReadAppSettings(4,0,256)`** mirrors the same ceiling: 3×18-byte fan blocks at @16/@34/@52 = `d0,d1,d2,100,d1,d2 | t0,t1,t2,100,t1,t2 | slope0,slope1,slope2 (LE16)` (source lines 1042-1240). No room for a 4th real point.

### cmd18 is not a bigger curve (it's a config/status block — never used by vendor app)
cmd18 exists and returns 37-49 nonzero bytes (count drifts), stable across a 4 s window (probeR2 §C), but:
- The decompiled vendor app **never reads buffer 18 anywhere** (full call-site inventory: only buffer cmds 12, 13, 17 are read; cmd17 = RGB/skin config). `SupportXTUFanTable=1` in oem.ini is never acted on by this app — no code path builds a >4-point table.
- Its structure is configuration, not a (temp,duty) point list (full hex in §5 appendix). Two *identical* 20-byte copies at @48/@80, RPM-ish LE16s `1000,1200,1200` at @16, `45000` at @8 and @192, `56250` at @12, descending bytes `45,44,43,42` at @208. Nothing scales with the actual curves when curves changed (probe34/35 wrote different curves; cmd18 content unchanged).

### Call-site inventory (final nail)
Full enumeration of the 58 DCHU wrapper call sites in decompiled `FanSpeedSetting` (7.87.0.0): commands used are only {init-0, 1(2,1), 12, 13, 14, store(0,12,4), store(1,1/32), store(4,0/5/7/8/9/80/81-85), store(7,0,256), 17, 118, 121, 257, 1029, 1031, 1094-1101}. **Buffer(18) appears nowhere.** `SupportXTUFanTable=1` is consumed by neither this app nor `InsydeDCHU*.dll`/`DCHUService.exe` (zero `XTU|FanTable|Curve` strings in all five binaries; fan symbols in DCHUService.exe are only Get/Set CPU/VGA duty + `GetFan12RPM`/`GetFan34RPM`/`GetFANCount` named APIs — and note `GetFANCount` there implies the *service* knows the count, but no integer cmd we can read exposes it except cmd66).

### Other places checked for >4 points (all negative)
- Buffer sweep cmds 0..2047 (both probes): **only 12, 13, 18 return any data**; all others zero-fill with rc==cmd.
- Store sweep hi=0..31 ×256 B: data only in hi=0,1,2,4,5; hi=6..15 zero; **hi≥16 is DLL-heap garbage** (`0xCC` filler then pointer-like junk — `ReadAppSettings` returns uninitialized buffers for unsupported hii). No curve blob larger than hi=4's 3×18B exists.
- Integer sweep: no cmd returns a pointer/length suggesting a larger table (see §3).

**Conclusion:** EC curve storage = 3 points/fan + synthetic (100,100); upload = 2 points + slopes; 4 fans max. A >4-point UI curve must be piecewise-linearly *approximated down* to 3 points before upload. Any "XTU fan table" marketing flag is unimplemented on this platform.

---

## 2. Q2 — fan2/fan3 presence detection

### Raw observations (all runs, 2 probes, 25+ samples)
- **cmd12 (live sensors):** fan2 and fan3 tach **and** duty **and** their temps are **identically zero in every single sample** (probeR1 10×1.5 s, probeR2 15×2 s), while fan0/fan1 tach+move live and duty tracks temperature. This is a dead channel, not a stopped fan: a connected fan would still report tach at duty>0 (duty0 ramped to 255 during probeR2; f2/f3 duty byte stayed 0 — EC drives nothing there).
- **cmd13:** fan2/fan3 slots have near-zero content except residue from earlier deliberate probe writes (probe35 wrote (60,140)(80,212) into slot2; slot3 only ever shows `00 00 00 01 …`).
- **No capability bits available:** the vendor's own fan-tab builder gates tabs on nested flags parsed **only from `store(7)` response byte[17]** (source 1986-2151 setters → 4977-5004 tab gates; fan0 shown if flag0||flag5, fan1/2/3 if flag1/2/3) — but on this machine **store(7) is entirely zero**, the init switch takes the case-0 path, and the parser that would populate those flags never runs → the app falls back to its **default block (all fan-tab flags = true)**. So even the vendor UI shows all fan tabs (CPU + GPU1 + GPU2) without knowing the hardware — tab visibility is *not* capability-derived here. cmd16 bits 0-6 are **write-only inside FanSpeedSetting** (dead; consumers are in other builds), bit7 gates only the version-store handshake (7411). cmd13@50 = 0.
- **cmd66 = 2** (integer): the only observed read whose value equals a plausible fan count for a 2-fan machine. Unverified semantics (could also be "number of CPU/GPU sensor groups"); treat as hint, not contract.

### Recommended detection rule for CoolDeck (safe, no writes)
```
present(ch) := over N≥4 reads at ≥1.5 s intervals:
   tach(ch) > 0 in ≥2 reads  OR  duty(ch) > 0 in ≥2 reads  OR  temp(ch) > 5 in ≥2 reads
```
- All-zero across the window → channel absent (hide tab). Any persistence → present.
- Must tolerate the **EC transient-stub window**: cmd12 legitimately returns `cpuT=1,gpuT=1,tach=0,duty=0` (rc still == 12!) for up to ~5 s after heavy DCHU traffic (probeR1 samples2-7, probeR2 samples0-4). A rule that trusts a single read will mis-detect spinning fans as absent. Same caution applies to any `rpm>0`-per-read UI logic (CoolDeck `App.cs:267` / `MainWindow.cs:877`).
- Do **not** use cmd13 slot content for presence (it mirrors configured curves, not hardware — fan2 slot was non-zero while the channel is dead).

---

## 3. Q3 — command/value inventory (full sweep results)

### rc convention
- `GetDCHU_Data_Buffer(cmd,…)`: **rc == cmd on success** (holds for every cmd probed, 0..2047; a "success" stub read still returns rc==cmd with garbage/zero contents).
- `GetDCHU_Data_Integer(cmd)`: **rc == val always** (2027/2027 lines). So the integer return *is* the value; failures/unsupported = `0x80000002` (`-2147483646`), all-ones `0xFFFFFFFF` on cmds 8, 57, 102.

### Integer commands with genuine (non-identity) values — complete set from 0..2047 sweep
```
cmd=1   val=83          0x53   bits 0,1,4,6
cmd=5   val=1           0x1
cmd=6   val=1           0x1
cmd=8   val=-1          0xFFFFFFFF
cmd=9   val=1           0x1
cmd=10  val=1           0x1
cmd=14  val=20          0x14
cmd=16  val=147         0x93   bits 0,1,4,7
cmd=50  val=3275        0xCCB
cmd=56  val=1           0x1
cmd=57  val=-1          0xFFFFFFFF
cmd=59  val=5880763     0x59BBBB
cmd=65  val=1644167168  0x62000000
cmd=66  val=2           0x2    <- only fan-count-shaped value
cmd=70  val=8512        0x2140
cmd=82  val=73990149    0x4690005
cmd=96  val=540         0x21C  bits 2,3,4,9
cmd=99  val=6437119     0x6238FF
cmd=100 val=3670527     0x3801FF
cmd=102 val=-1          0xFFFFFFFF
cmd=110 val=256         0x100
cmd=111 val=3684607     0x3838FF
cmd=112 val=33554890    0x20001CA
cmd=119 val=6577920     0x645F00
cmd=122 val=2013397075  0x78020053 bits 0,1,4,6,17,27-30
```
Response classes over the full 0..2047 sweep (2027 B-lines emitted where rc!=0 or val!=0; rc==val held on **2027/2027**, zero violations): **(i)** genuine values (25, table above); **(ii)** identity echoes `rc==val==cmd` — 36 in 0..122 (e.g. 3,19,20,29,31-34,38,39,42,44,71-79,85-91,94,101-109) + 117/118/121 (indistinguishable from data; treat as unsupported); **(iii)** sentinel `0x80000002` on scattered {0,11,15,21-28,30,35-37,40,41,43,45-48,53-55,58,64,68,75,77,80,83,84,88,89,92,93,95,97,114,120} + contiguous 123..2047; **(iv)** silent `rc==val==0` on the 21 cmds emitting no B-line (2048 probed − 2027 lines): verified 113/115/116 in 111..2047, remaining 18 below 111. Census 111..2047 fully accounted: 1927 sentinel + 3 identity + 4 genuine + 3 silent = 1937. → **the integer interface implements 0..122 only**; int cmd18 is silent (buffer 18 ≠ integer 18 — different interfaces).

### Bitmap semantics (from decompiled consumer code)
- **cmd16 `0x93` = bits {0,1,4,7}.** Consumer: source 1935-2005 decodes `GetDCHU_Data_Integer(16)` bits0-7 into 8 feature bools feeding nested fan/keyboard flags; corroborated by mirror byte `store(0,252)=0x93` (exact match — the value is persisted and re-uploaded via the store blob, so it is a model capability word, not telemetry).
- **cmd96 `0x21C` = bits {2,3,4,9}.** Consumers: bit10 → "disable" flag, bit7 → feature bool (1935-2005). Here bits 2,3,4 set + bit9: consistent with "fan-speed-offset supported" style flags (oem.ini `SupportFanSpeedOffset=1` matches a set bit here).
- **cmd122 `0x78020053`.** Consumer bit15 (undisclosed feature). Low byte 0x53 == cmd1 value (0x53) — cmd1/cmd122 low byte mirror.
- **cmd70 = 8512 = 0x2140**, cmd50=0xCCB, cmd59=0x59BBBB, cmd82=0x4690005: no consumer exists in the vendor app (it only ever queries 16/96/122 + store reads) → semantics unknown; value shapes (0x62…, 0x38…, 0x3838FF, 0x645F00) look like packed IDs/BOM fields, not bitmaps. Do not build features on them.

### Packed store map (hi,lo = (addr-…)/256 addressing, 16 pages max)
```
hi=0  @0: 50 F3 50 F3 | @4: 01 07 | @8: 07 58 (=0x5708? model id) | @12: 07 57
      @240: 58 15 F3 50 | @252: 93  <- exact mirror of cmd16 capability word
      (source: store(0,12,4) = app version stamp, auto-written when absent → @12 07 57 00 00 ↔ '7.87.0.0')
hi=1  9 nz: @1=02 (=store(1,1), fan-hotkey selection, value 2 here), @2=08, @4=01, @6=01, @16=02, @27=01,@28=01, @40=02, @53=A5
hi=2  @4=06; @32..39 = 01 00 00 04 01 00 00 1E; @80..107 = (01 00 00 C8)x4,(01 00 00 FF),(01 00 FF 00),(01 00 00 FF)
hi=4  THE fan table store: @0=07 57 | @4=02 (pts?), @5=06 (mode: 6=Custom), @6=02 (offset)
      fan blocks @16/@34/@52, 18 B each: d0,d1,d2,64h,d1,d2 | t0,t1,t2,64h,t1,t2 | 3× LE16 slopes
hi=5  8 nz: 20 25 00 00 00 00 01 6B 05 01 01 CC (unknown small config; not touched by vendor fan code)
hi=3,6..15 ZERO (incl. hi=7 = where other models keep capability bits → empty on DR521, explains case-0 init path)
hi>=16  GARBAGE (0xCC fill / heap pointers) — do not read
```

---

## 4. Q4 — is the EC re-deriving curves behind our back?

**Two independent drift events observed; both explained by a Windows-side active writer, not EC autonomy.**

1. **Mid-probe cmd13 mutation (probeR1):** between sample0 and sample2, cmd13 bytes @14 4→2 and fan0/fan1 slots @18..29 changed from probe35 leftovers `(41,0)(99,120)` to `(60,140)(80,211)/(60,130)(80,211)` — exactly probe34's baseline upload. The EC-pushed snapshot re-synced from the store **~3 s after** the last write, without any client touching the EC.
2. **Cross-probe store rewrite (between probeR1 file-write 23:34:56Z and probeR2 launch 23:42:10Z; probeR2 file 23:42:48Z):** `store(4)` @16..69 changed from probe35 garbage (`d35,0,47 / t40,41,99`, slopes -1428/33/2162) to clean stock values `(35,55,83)/(40,60,80)` and fan2 slot ← fan0 copy. **Writer attribution (process+file forensics): CoolDeck GUI sessions were live exactly inside this window** — cooldeck.log blocks at 23:40:32Z and 23:42:07Z (PID 22304), both from `C:\Users\User\Desktop\CoolDeck\CoolDeck.exe`, which loads the vendor `InsydeDCHU.dll` (11.5 MB copy) and has a "Stage to EC store" action calling `WriteAppSettings(4,0,256,buf)`. Caveats: the log records UI lifecycle only (no DCHU-write lines exist in its format — zero `curve|store|write|save` matches across all 250 lines; absence is not exculpatory), and the desktop exe was overwritten 23:55:06Z (now SHA256-identical to the workspace build `DE04840875A1A68E2235F98E5A430F1690223F3ADEBAC4165E5E5CF1725199FC`), so the exact build 22304 ran is unresolvable post-mortem. Still: only CoolDeck (parent-side testing) was active in the window — vendor `FanSpeedSetting.exe`/`ControlCenter30.exe` were not running (ControlCenter UWP container hive last written 13:43-14:03Z). The rewritten values are **exactly CoolDeck's documented stock presets** (its README.md:83: CPU (40,35%)(60,55%)(80,83%)(100,100%); GPU (48,35%)(60,51%)(80,83%)(95,100%)) — a human-clicked "apply stock" is the only mechanism on this machine that produces that byte pattern. cmd13's fan2 slot then *also* populated at probeR2 s8 (`3C 8C 50 D4`) **without any change to the store** in the same sample → confirms the cmd13 buffer is a lazily-refreshed mirror, not live EC RAM.

   **Independent layout confirmation (via that README):** 18 B/fan block layout `[+0..2] duty% | [+3]=100 | [+4..5] dup | [+6..8] temp°C | [+9]=100 | [+12..17] 3× signed-LE16 slopes = round(Δduty/Δtemp × 2.55 × 16)` — recomputing from stock values gives 41,57,35 (fan0) / 54,65,35 (fan1), byte-exact against probeR2 §F `29 00 39 00 23 00` / `36 00 41 00 23 00`. Our §1/§3 decodes of this structure are validated by the vendor-app author's own documentation.
3. **Stability during quiescence:** probeR2's 15×2 s loop shows cmd13 (and cmd12 *sensor* fields changing live) with store constant → while no writer is active, **nothing drifts**. probeR1's cmd13 was frozen across samples 3-9 once synced.

**Verdict:** the EC does **not** autonomously alter the effective curve table; but the *table is shared* — any running Windows-side app (vendor ControlCenter service, or another CoolDeck instance) can rewrite store(4)/cmd14 at any moment. **Where state actually lives:** string-scans of `InsydeDCHU.dll`/`_svc.dll`/`DCHUService.exe`/`Device.dll` found **no registry or file paths at all** (only MFC noise + PDB paths `InsydeDCHU-20190703\x64\Debug`); `ReadAppSettings`/`WriteAppSettings` traffic goes through `CCDCHUService` (Running, Automatic, LocalSystem, `DriverStore\acpibridge1.inf_amd64_cedafa39846f03cf\DCHUService.exe`, PID 7952) into the ACPI-bridge driver — i.e. **the app-settings store is EC/driver-side RAM, not a file on disk**. Consequence: *every* writer is a client of the same live store, and the only durable copies are each app's own settings (vendor: `HKCU\Software\ControlCenter3.0` `ECVersion=1.07.02TE1` + UWP Helium hives; CoolDeck: `%APPDATA%\CoolDeck\`). Also note `FnKey.exe` (CLEVOCO.FnhotkeysandOSD UWP) **is running** — the fan-hotkey path (`store(1,1)` selection, mode switching) is live on this machine and can flip modes under you. **Open question (not testable under the read-only/no-reboot constraint):** whether this store survives power cycles (NVRAM) or reverts to EC firmware defaults on reboot — the hi=0 version-stamp auto-write (source 7417/7431) suggests it at least survives app sessions.

**Design consequence for CoolDeck:** don't detect "someone changed the curve" by polling cmd13 deltas (it refreshes asynchronously and lags writes by seconds, and a 4→2 style @14 mode-flag flip rides along with duty-ramp ticks); instead compare the **store(4) blocks** (synchronous persistence point) against your last-known config, and/or hold a watchdog that re-applies on mtime-like change. Do not assume cmd14 upload == persistent state.

---

## 5. Appendix — raw dumps (verbatim)

### cmd18 full 256 B (probeR1 @ 23:34:36Z; nz=49, rc=18 stable; probeR2 read nz=37 — only zero-padding bytes vary in count with volatile fields)
```
    0: 02 00 FF 00 00 00 00 03 C8 AF 00 00 BA DB 00 00
   16: E8 03 B0 04 B0 04 00 00 00 00 00 00 00 00 00 00
   32: 00 ...
   48: 15 00 15 00 2F 00 14 00 1F 00 BE 2C 02 02 04 00
   64: 0C 00 18 00 00 ...
   80: 15 00 15 00 2F 00 14 00 1F 00 BE 2C 02 02 04 00
   96: 0C 00 18 00 00 ...
  112..191: all zero
  192: C8 AF 00 00 01 90 5F 01 00 01 1C 00 00 00 00 00
  208: 2D 2C 2B 2A 00 ...
```
LE16 decode: @0=2, @2=255, @6=768(BE=3), @8=45000, @12=56250, @16/18/20=1000,1200,1200, @48..66=21,21,47,20,31,11454,514,4,12,24, @80..98 identical copy, @192=45000, @196=36865(0x9001), @198=351, @200=256, @202=28, @208=11309/BE11564 (bytes 45,44,43,42 desc). @48..99 = 20-byte config record mirrored; **not** (temp,duty) curve data (values don't track real curves; 2 identical copies; no 4-pair stride alignment).

### cmd13 full (probeR1 sample0) — effective curves
```
  0: 00 00 00 00 C8 00 00 C8 00 00 C8 FF 02 00 04 06
 16: 28 59 29 00 63 78 64 FF 30 59 29 00 63 66 5F FF
 32: 00 00 3C 8C 50 D4 00 00 00 00 00 01 00 00 00 00
 48..255: ZERO
 fan0=(40,89)(41,0)(99,120)(100,255)  <- probe35 leftovers, drift to (40,89)(60,140)(80,211)(100,255)
 fan1=(48,89)(41,0)(99,102)(95,255)   <- drift to (48,89)(60,130)(80,211)(95,255)
 fan2=(0,0)(60,140)(80,212)(0,0)      <- probe35 write residue on absent channel
 fan3=(0,0)(0,1)(0,0)(0,0)
 @12=2 @13=0 @14=4→2→8(varying) @15=6(mode Custom) @43=1 @50=0
```
probeR2 stock reading: fan0 `28 59 3C 8C 50 D3 64 FF` fan1 `30 59 3C 82 50 D3 5F FF` (= README stock curves).

### cmd12 layout (live sensors)
```
 0: 00 00 03 27 08 85 00 00 C0 40 00 00 01 63 00 00
16: 73 3B 63 26 01 3F 00 01 00 01 00 00 00 00 00 00
   tach BE16: f0@2, f1@4, f2@6, f3@36 | duty u8: @16,@19,@22,@38 | temps u8: @18(cpu),@21(gpu),@24,@40
   @12=01 @13=0x63=99 (tracks cpuT) | transient stub window: cpuT=gpuT=1, all else 0, rc still 12
```

### Files
- `D:\workroom\fanctl\re\probeR1.cs` / `re\probeR1.out.txt` (15 249 B, 257 ln)
- `D:\workroom\fanctl\re\probeR2.cs` / `re\probeR2.out.txt` (150 021 B, 2 140 ln) — §A buffer rc, §B integer sweep, §C cmd18 stability, §D 15×2 s sampling, §E store hi=10..31, §F store re-read
- `re\bin\probeR1.exe`, `re\bin\probeR2.exe` (csc Framework64, zero warnings)
- `re\probeR2_analysis_out.txt` + `re\probeR2_cmdout.txt` (filtered sweep stdout: 25 genuine ints, census, rc==val verification)
