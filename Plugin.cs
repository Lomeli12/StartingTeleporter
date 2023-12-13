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
            
            var originalDiscord = AccessTools.Method(typeof(StartOfRound), "SetDiscordStatusDetails");
            var patchDiscord =
                AccessTools.Method(typeof(STPatches), nameof(STPatches.discordPatch));

            harmony.Patch(originalDiscord, new HarmonyMethod(patchDiscord));
        }
    }
}