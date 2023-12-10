using System;
using System.Reflection;

namespace StartingTeleporter;

internal class STPatches {
    private static readonly string TELEPORTER_NAME = "Teleporter";
    private static readonly string INVERSE_NAME = "Inverse Teleporter";

    private static bool idsInitialized;
    private static int teleporterID = -1;
    private static int inverseTeleporterID = -1;

    internal static void startPatch(ref StartOfRound __instance) {
        if (!idsInitialized)
            getTeleporterIDs(__instance);
        try {
            if (Plugin.modConfig.startWithTeleporter() && teleporterID != -1)
                unlockShipItem(__instance, teleporterID, TELEPORTER_NAME);
            if (Plugin.modConfig.startWithInverseTeleporter() && inverseTeleporterID != -1)
                unlockShipItem(__instance, inverseTeleporterID, INVERSE_NAME);
        } catch (Exception ex) {
            Plugin.log.LogError($"Failed to unlock items: \n{ex}");
        }
    }

    private static void getTeleporterIDs(StartOfRound instance) {
        Plugin.log.LogInfo("Getting item IDs");
        for (var i = 0; i < instance.unlockablesList.unlockables.Count; i++) {
            var unlockableItem = instance.unlockablesList.unlockables[i];
            // I really hate this, cause it's a localized name. Hopefully this gets changed to proper item ids
            var itemName = unlockableItem.unlockableName.ToLower();

            if (itemName.Equals(TELEPORTER_NAME.ToLower())) {
                Plugin.log.LogInfo($"{TELEPORTER_NAME} ID: {i}");
                teleporterID = i;
            }
            else if (itemName.Equals(INVERSE_NAME.ToLower())) {
                Plugin.log.LogInfo($"{INVERSE_NAME} ID: {i}");
                inverseTeleporterID = i;
            }
        }

        idsInitialized = true;
    }

    private static void unlockShipItem(StartOfRound instance, int unlockableID, string name) {
        try {
            Plugin.log.LogInfo($"Attempting to unlock {name}");
            var spawn = instance.GetType().GetMethod("UnlockShipObject",
                BindingFlags.NonPublic | BindingFlags.Instance);
            spawn.Invoke(instance, new object[] { unlockableID });
            Plugin.log.LogInfo($"Spawning {name}");
        } catch (NullReferenceException ex) {
            Plugin.log.LogError($"Could not invoke UnlockShipObject method: {ex}");
        }
    }
}