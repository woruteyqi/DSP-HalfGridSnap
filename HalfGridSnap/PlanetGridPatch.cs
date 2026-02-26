using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace HalfGridSnap;

public static class PlanetGridPatch
{
    /// <summary>
    /// 使用 Transpiler 替换 PlanetGrid 中的硬编码常数。
    /// 将所有的 5.0f (标准网格) 替换为 10.0f (半格点)。
    /// </summary>
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(PlanetGrid), nameof(PlanetGrid.SnapTo), [typeof(Vector3)])]
    [HarmonyPatch(typeof(PlanetGrid), nameof(PlanetGrid.SnapInCurrentSeg), [typeof(Vector3)])]
    [HarmonyPatch(typeof(PlanetGrid), nameof(PlanetGrid.SnapTo), [typeof(Vector3), typeof(int)])]
    [HarmonyPatch(typeof(PlanetGrid), nameof(PlanetGrid.SnapTo), [typeof(float), typeof(float)])]
    public static IEnumerable<CodeInstruction> SnapTo_Transpiler(
        IEnumerable<CodeInstruction> instructions
    )
    {
        foreach (var instruction in instructions)
        {
            // 查找指令中的 ldc.r4 5.0
            if (instruction.opcode == OpCodes.Ldc_R4 && (float)instruction.operand == 5f)
            {
                // 将其替换为 10.0f
                yield return new CodeInstruction(OpCodes.Ldc_R4, 10f);
            }
            else
            {
                yield return instruction;
            }
        }
    }
}
