# HalfGridSnap

允许将坐标吸附到“半格点”。本插件通过 Harmony Transpiler 替换游戏中 `PlanetGrid` 的硬编码常量，从而改变网格吸附间距，实现半格点吸附效果。

## 主要功能
- 将 `PlanetGrid` 中的硬编码常量 `5.0f` 替换为 `10.0f`，改变吸附间距以实现半格点行为。

## 安装
1. 直接拷贝输出的 DLL 到 `BepInEx/plugins` 或者通过模组管理器导入 Output 目录下的 zip 压缩包进行安装。

## 从源码构建
1. 使用支持 .NET Standard 2.1 的 SDK / Visual Studio（例如 Visual Studio 2026）。
2. 恢复 NuGet 包或手动引用 BepInEx 与 HarmonyLib 的程序集（项目中应已有引用）。
3. 编辑项目 .csproj 里面的 GameDir 为你的实际游戏目录。
4. 在解决方案中选择 Release/Debug，生成项目：
   - Visual Studio：打开解决方案 -> 右键项目 -> __生成__。

## 实现说明（开发者）
- 关键实现位于 `PlanetGridPatch.cs`：
  - 使用 `HarmonyTranspiler` 遍历 IL 指令，查找 `ldc.r4 5.0`，并将其替换为 `ldc.r4 10.0`。
  - 这样修改后，`PlanetGrid` 的吸附行为会以新的常量进行计算，从而实现期望的吸附效果。