# HalfGridSnap | [中文说明](HalfGridSnap/README.md)

Allows snapping coordinates to "half-grid points". This plugin modifies the hardcoded constant in the game's `PlanetGrid` class using a Harmony Transpiler, thereby adjusting the grid snapping interval to enable half-grid snapping behavior.

## Main Features
- Replaces the hardcoded constant `5.0f` in `PlanetGrid` with `10.0f`, altering the snapping interval to achieve half-grid behavior.
- At runtime, toggle half-grid snap by pressing the default shortcut key `~` (default is enabled) ,the shortcut can be customized in-game.

## Installation
1. Directly copy the output DLL to `BepInEx/plugins`, or install by importing the ZIP archive from the `Output` directory via a mod manager.

## Building from Source
1. Use an SDK or Visual Studio that supports .NET Standard 2.1 (e.g., Visual Studio 2026).
2. Restore NuGet packages or manually reference the BepInEx and HarmonyLib assemblies (references should already be included in the project).
3. Edit the `GameDir` property in the project's `.csproj` file to point to your actual game installation directory.
4. Select **Release** or **Debug** configuration in the solution and build the project:
   - Visual Studio: Open the solution → Right-click the project → **Build**.

## Implementation Notes (for Developers)
- Core logic resides in `PlanetGridPatch.cs`:
  - Uses `HarmonyTranspiler` to traverse IL instructions, locate `ldc.r4 5.0`, and replace it with `ldc.r4 10.0`.
  - After this modification, `PlanetGrid` computes snapping behavior using the updated constant, achieving the intended half-grid snapping effect.
