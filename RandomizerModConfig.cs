using Terraria.ModLoader.Config;

namespace RandomizerMod;

public partial class RandomizerModConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [Header("ReloadWarning")]

    public ItemRandomizations ItemRandomization { get; init; } = new();
    public ProjectileRandomizations ProjectileRandomization { get; init; } = new();
    public NPCRandomizations NPCRandomization { get; init; } = new();
    public WorldRandomizations WorldRandomization { get; init; } = new();

    [SeparatePage]
    public class ItemRandomizations
    {
        public bool ItemNameRandomization { get; set; }
        public bool ItemSpritesRandomization { get; set; }
        [DrawTicks] public RandomMode RecipeRandomization { get; set; }
        public bool StatsRandomization { get; set; }
        [DrawTicks] public RandomMode PlacedTileRandomization { get; set; }
        [DrawTicks] public RandomMode PlacedWallRandomization { get; set; }
    }

    [SeparatePage]
    public class NPCRandomizations
    {
        public NPCAIRandomizations AIRandomization { get; init; } = new();

        public bool NameRandomization { get; set; }
        public bool ItemDropsRandomization { get; set; }
        public bool BossDropsRandomization { get; set; }
        public bool ShopRandomization { get; set; }
    }

    [SeparatePage]
    public class WorldRandomizations
    {
        public bool ChestsRandomization { get; set; }
        public bool TileRandomization { get; set; }
    }

    [SeparatePage]
    public class ProjectileRandomizations
    {
        [DrawTicks] public RandomMode ItemProjRandomization { get; set; }
        public bool ShootSpeedRandomization { get; set; }
        [DrawTicks] public RandomMode AIRandomization { get; set; }
        public ProjectileDefinition CopyAI { get; set; } = new();
    }

    [SeparatePage]
    public class NPCAIRandomizations
    {
        public bool Enabled { get; set; }
        public bool AffectsTownNPCs { get; set; }
        public bool AffectsBosses { get; set; }
        public bool AffectsImportants { get; set; }
        public bool OverridesImmortality { get; set; }
        public bool ShuffleAI { get; set; }
        public bool RandomSize { get; set; }
        public NPCDefinition CopyAI { get; set; } = new();
    }
}

public enum RandomMode
{
    Off,
    Shuffle,
    Randomize
}
