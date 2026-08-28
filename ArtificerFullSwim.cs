using System;
using System.Security.Permissions;
using BepInEx;
using Menu.Remix.MixedUI;
using static MoreSlugcats.MoreSlugcatsEnums;

#pragma warning disable CS0618
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618

namespace ArtificerFullSwim;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
public class ArtificerFullSwimMain : BaseUnityPlugin {
    public const string PLUGIN_GUID = "zohnannor.artificerfullswim";
    public const string PLUGIN_NAME = "Artificer Full Swim";
    public const string PLUGIN_VERSION = "1.0.0";


    private bool initDone = false;
    public static ArtificerFullSwimOptions Options;

    public const float TRIGGER_DEPTH = -444650f;
    private bool endingTriggerLogged = false;

    public void OnEnable() {
        On.RainWorld.OnModsInit += OnModsInit;
    }

    public void OnDisable() {
        On.RainWorld.OnModsInit -= OnModsInit;
    }

    private void OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self) {
        orig(self);
        if (initDone) {
            return;
        }

        On.VoidSea.VoidWorm.Update += VoidWorm_Update;

        Options = new ArtificerFullSwimOptions();
        MachineConnector.SetRegisteredOI(PLUGIN_GUID, Options);

        Logger.LogDebug($"{PLUGIN_NAME} v{PLUGIN_VERSION} loaded");
        initDone = true;
    }

    private void VoidWorm_Update(On.VoidSea.VoidWorm.orig_Update orig, VoidSea.VoidWorm self, bool eu) {
        orig(self, eu);

        if (!self.mainWorm || self.voidSea.room.game.StoryCharacter != SlugcatStatsName.Artificer) {
            return;
        }

        var player = self.voidSea.room.game.FirstRealizedPlayer;
        if (ModManager.CoopAvailable) {
            player = self.voidSea.room.game.RealizedPlayerFollowedByCamera;
        }
        if (player == null) {
            return;
        }

        self.voidSea.fadeOutLights = false;

        if (player.mainBodyChunk.pos.y < TRIGGER_DEPTH) {
            self.voidSea.fadeOutLights = true;
            if (!endingTriggerLogged) {
                Logger.LogInfo($"[{player.room.game.clock}] Artificer reached target depth of {TRIGGER_DEPTH}, triggering the ending!");
                endingTriggerLogged = true;
            }
        } else {
            int depth = (int)Math.Round(player.mainBodyChunk.pos.y);
            if (depth % 1000 == 0) {
                Logger.LogInfo($"[{player.room.game.clock}] Artificer reached depth {depth}!");
            }
        }
    }
}


public class ArtificerFullSwimOptions : OptionInterface {
    public readonly Configurable<bool> Enabled;
    private OpTab mainTab;
    private OpCheckBox _enabledCheckbox;

    private const string description = "Disable this to toggle the mod's functionality without restarting the game.";

    public ArtificerFullSwimOptions() {
        Enabled = config.Bind("enabled", true);
    }

    public override void Initialize() {
        base.Initialize();

        mainTab = new OpTab(this, "Main");
        Tabs = [mainTab];
        _enabledCheckbox = new OpCheckBox(Enabled, 5f, 527f) {
            description = description
        };

        mainTab.AddItems([
            _enabledCheckbox,
            new OpLabel(
                37f,
                530f,
                "Enabled"
            ) {
                alignment = FLabelAlignment.Left,
                description = description
            }
        ]);
    }
}
