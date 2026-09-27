# fanctl

Fan control tooling for Clevo barebones (DEVIL RAYS DR521 / P955ET1, Insyde EC).

开箱即用（含 exe）：见 [Releases](https://github.com/zeng6125-rgb/fanctl/releases)
（下载 `CoolDeck-vX.Y.Z.zip`，解压运行 `CoolDeck.exe` 即可，需先装 CLEVO Control Center 驱动）。

- **CoolDeck/** — the app: pure-C# WPF fan monitor & curve editor (csc-only
  toolchain, no XAML, no NuGet). See [CoolDeck/README.md](CoolDeck/README.md).
  Build: `powershell -ExecutionPolicy Bypass -File CoolDeck/build.ps1`
- **re/** — reverse-engineering notes (decompiled vendor app, protocol findings).
- **RESEARCH_REPORT.md** — background research report.
