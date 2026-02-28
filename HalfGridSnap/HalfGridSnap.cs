using BepInEx;
using BepInEx.Logging;
using CommonAPI;
using CommonAPI.Systems;
using CommonAPI.Systems.ModLocalization;
using HarmonyLib;
using UnityEngine;

namespace HalfGridSnap;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency(CommonAPIPlugin.GUID)]
[CommonAPISubmoduleDependency(nameof(CustomKeyBindSystem))]
[CommonAPISubmoduleDependency(nameof(LocalizationModule))]
public class HalfGridSnap : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    private readonly Harmony harmony = new(PluginInfo.PLUGIN_GUID);
    private const string keyName = "切换是否半格点吸附";
    public bool IsEnable
    {
        get { return field; }
        set
        {
            if (field == value)
                return;
            else
                field = value;

            if (IsEnable)
            {
                harmony.PatchAll(typeof(PlanetGridPatch));
            }
            else
            {
                harmony.UnpatchSelf();
            }
        }
    }

    private void Awake()
    {
        Logger = base.Logger;
        RegisterKeyBinds();
        IsEnable = true;
        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
    }

    private void RegisterKeyBinds()
    {
        var defaultKey = new CombineKey
        {
            keyCode = (int)KeyCode.BackQuote,
            action = ECombineKeyAction.OnceClick,
        };

        var builtinKey = new BuiltinKey
        {
            name = keyName,
            key = defaultKey,
            canOverride = true,
            conflictGroup = KeyBindConflict.KEYBOARD_KEYBIND | KeyBindConflict.BUILD_MODE_1,
        };
        CustomKeyBindSystem.RegisterKeyBind<ReleaseKeyBind>(builtinKey);
        LocalizationModule.RegisterTranslation(keyName, "Toggle Half Grid Snap Function");
    }

    private void Update()
    {
        if (CustomKeyBindSystem.GetKeyBind(keyName).keyValue)
        {
            IsEnable = !IsEnable;
        }
    }
}
