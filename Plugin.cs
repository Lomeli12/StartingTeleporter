using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace StartingTeleporter {
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Lethal Company.exe")]
    public class Plugin : BaseUnityPlugin {
        public static STConfig modConfig;
        public static ManualLogSource log;

        private void Awake() {
            log = Logger;
            log.LogInfo($"Loading {PluginInfo.PLUGIN_NAME}");
            modConfig = new STConfig(Config);

            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            
            var originalStart = AccessTools.Method(typeof(StartOfRound), "Start");
            var patchStart =
                AccessTools.Method(typeof(STPatches), nameof(STPatches.startPatch));

            harmony.Patch(originalStart, new HarmonyMethod(patchStart));
        }
    }
}