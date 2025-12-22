# DevGearbox — AI Coding Agent Guide

This repository is a Windows WPF application targeting net10.0-windows. Agents should focus on the established patterns: thin UI `UserControl` views under Components, core logic under Utils, and a centralized tool registry via `ToolFactory` populating a `TabControl` in the main window.

**Architecture**
- **App Type:** WPF desktop app — see [DevGearbox/DevGearbox.csproj](DevGearbox/DevGearbox.csproj) (`UseWPF` true, `TargetFramework` net10.0-windows).
- **UI Composition:** [DevGearbox/MainWindow.xaml](DevGearbox/MainWindow.xaml) defines a `TabControl` populated at startup from [DevGearbox/MainWindow.xaml.cs](DevGearbox/MainWindow.xaml.cs#L15-L34).
- **Tool Registry:** Tools are declared in [DevGearbox/Services/ToolFactory.cs](DevGearbox/Services/ToolFactory.cs) and materialize as tabs with `Header`, `ToolTip`, and `Content`.
- **Data Model:** Each tool entry is a `ToolItem` (name, icon, description, view) — see [DevGearbox/Models/ToolItem.cs](DevGearbox/Models/ToolItem.cs).
- **Business Logic:** Per-tool logic lives in Utils (e.g., [DevGearbox/Utils/JsonFormatter.cs](DevGearbox/Utils/JsonFormatter.cs), [DevGearbox/Utils/JwtAnalyzer.cs](DevGearbox/Utils/JwtAnalyzer.cs)). Views call these methods from click handlers.

**Key Conventions**
- **Thin Views:** Keep `Components/*View.xaml.cs` minimal; delegate parsing/transformations to `Utils/*`.
- **Offline-Only:** No network calls, telemetry, or external APIs. All operations must remain local.
- **Error Messages:** Utils methods return friendly, human-readable errors (e.g., "Invalid JSON: …"). Follow this style.
- **Dependencies:** Use existing libraries already referenced in the csproj (Newtonsoft.Json, System.IdentityModel.Tokens.Jwt, Bogus). Avoid introducing new runtime dependencies unless necessary.

**Adding a New Tool (Pattern)**
- **1) Create UI:** Add a `UserControl` under [DevGearbox/Components](DevGearbox/Components) (e.g., `FooToolView.xaml` + `.xaml.cs`). Wire buttons to call Utils.
- **2) Implement Logic:** Add a focused static helper under [DevGearbox/Utils](DevGearbox/Utils) (e.g., `FooTool.cs`) with clear methods and error strings.
- **3) Register:** Append a new `ToolItem` in [DevGearbox/Services/ToolFactory.cs](DevGearbox/Services/ToolFactory.cs) with `Name`, `Icon`, `Description`, and the `View` instance.
- **4) Test Manually:** Run the app and verify the new tab behaves like existing tools (clipboard, swap, status text patterns).

**Examples**
- **JSON ↔ CSV:** View uses Utils for conversions — see [DevGearbox/Components/JsonCsvConverterView.xaml.cs](DevGearbox/Components/JsonCsvConverterView.xaml.cs) calling [DevGearbox/Utils/JsonCsvConverter.cs](DevGearbox/Utils/JsonCsvConverter.cs).
- **JWT Tools:** [DevGearbox/Utils/JwtAnalyzer.cs](DevGearbox/Utils/JwtAnalyzer.cs) provides both analyze and debug flows; UI splits header/payload/signature.
- **Tab Population:** Startup loads tools via `ToolFactory.Instance.GetAllTools()` and adds `TabItem`s — see [DevGearbox/MainWindow.xaml.cs](DevGearbox/MainWindow.xaml.cs#L15-L34).

**Build & Run**
- **Prerequisites:** .NET SDK compatible with `net10.0-windows` on Windows.
- **CLI:**
	- `dotnet restore`
	- `dotnet build`
	- `dotnet run --project DevGearbox`
- **Script:** Quick build script at [build_script.ps1](build_script.ps1) performs clean/restore/build and outputs `build_result.txt`.
- **Executable:** Post-build output under `DevGearbox/bin/Debug/net10.0-windows/DevGearbox.exe`.

**Debugging Tips**
- Set `DevGearbox` as the startup project; use WPF event handlers in Components to exercise Utils methods.
- Keep logic in Utils unit-testable (static methods, pure transforms); views should catch exceptions and update status text.

**Style & Safety**
- Nullable and implicit usings are enabled in the project.
- Preserve the offline-first design; do not introduce network or telemetry.
- Match existing status text and clipboard UX in Components (e.g., copy buttons, swap input/output).

If anything here is unclear or missing (e.g., specific tool behaviors or preferred error formats), tell me what to refine and I’ll update this guide.
