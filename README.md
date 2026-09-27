# fanctl

Fan control tooling for Clevo barebones (DEVIL RAYS DR521 / P955ET1, Insyde EC).

- **CoolDeck/** — the app: pure-C# WPF fan monitor & curve editor (csc-only
  toolchain, no XAML, no NuGet). See [CoolDeck/README.md](CoolDeck/README.md).
  Build: `powershell -ExecutionPolicy Bypass -File CoolDeck/build.ps1`
- **re/** — reverse-engineering notes (decompiled vendor app, protocol findings).
- **RESEARCH_REPORT.md** — background research report.
