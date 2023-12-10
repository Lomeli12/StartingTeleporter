using BepInEx.Configuration;

namespace StartingTeleporter;

public class STConfig {
    private static readonly string CATEGORY = "General";
    private ConfigFile config;
    private ConfigEntry<bool> configStartingTeleporter;
    private ConfigEntry<bool> configStartingInverseTeleporter;

    public STConfig(ConfigFile config) {
        this.config = config;
        configStartingTeleporter = this.config.Bind(CATEGORY,
            "StartingTeleporter",
            true,
            "Start the game with a normal Teleporter.");

        configStartingInverseTeleporter = this.config.Bind(CATEGORY,
            "StartingInverseTeleporter",
            false,
            "Start the game with an Inverse Teleporter");
    }

    public bool startWithTeleporter() => configStartingTeleporter.Value;

    public bool startWithInverseTeleporter() => configStartingInverseTeleporter.Value;
}