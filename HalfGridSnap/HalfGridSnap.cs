using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace HalfGridSnap;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class HalfGridSnap : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    private void Awake()
    {
        Harmony.CreateAndPatchAll(typeof(PlanetGridPatch));
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
    }
}
