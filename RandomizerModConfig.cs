using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace RandomizerMod
{
    public class RandomizerModConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("NPC AI Randomization Settings")]
        [Tooltip("Configure the randomization of AI for all NPCs.")]
        public AIRandomizations AIRandomizationSettings { get; init; } = new();

        [Label("NPC Loot Randomization")]
        [Tooltip("Configure the randomization of extra drops from enemies.")]
        public NPCLootRandomizations NPCLootRandomization { get; init; } = new();

        [Label("Item Name Randomization")]
        [Tooltip("Toggle the randomization of names for all items.")]
        [DefaultValue(false)]
        public bool ItemNameRandomization { get; set; }

        [Label("NPC Name Randomization")]
        [Tooltip("Toggle the randomization of names for all NPCs.")]
        [DefaultValue(false)]
        public bool NPCNameRandomization { get; set; }

        [Label("Weapon Stats Randomization")]
        [Tooltip("Toggle the randomization of stats for all Weapons.")]
        [DefaultValue(false)]
        public bool StatsRandomization { get; set; }

        [Label("Random Weapon Projectiles")]
        [Tooltip("Toggle the randomization of projectiles that each weapon fires.")]
        [DefaultValue(false)]
        public bool RandomProjRandomization { get; set; }

        [Label("Item Sprites Randomization")]
        [Tooltip("Toggle the randomization of sprites for all items.")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool ItemSpritesRandomization { get; set; }

        [Label("NPC & Item Sounds Randomization")]
        [Tooltip("Toggle the randomization of sounds for all items and npcs.")]
        [DefaultValue(false)]
        public bool SoundsRandomization { get; set; }

        [Label("Projectile AI Randomization")]
        [Tooltip("Toggle the randomization of AI for all projectiles.")]
        [DefaultValue(false)]
        public bool ProjAIRandomization { get; set; }

        [Label("Town NPC Shop Randomization")]
        [Tooltip("Toggle the randomization of shops for all NPCs.")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool NPCShopRandomization { get; set; }

        [Label("Chest Contents Randomization")]
        [Tooltip("Toggle the randomization of items in all generated chests.")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool ChestsRandomization { get; set; }

        [Label("Recipe Randomization")]
        [Tooltip("Toggle the randomization of recipes.")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool RecipeRandomization { get; set; }

        [Header("[c/E61919:WARNING: May need a beefy pc to use!]")]
        [Label("World Generation Randomization")]
        [Tooltip("Toggles the randomization of all tiles generated in a world.")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool WorldGenRandomization { get; set; }

        [SeparatePage]
        public class NPCLootRandomizations
        {
            [Label("Do all enemies drop random items?")]
            [DefaultValue(false)]
            public bool Enabled { get; set; }

            [Label("Do only bosses drop random items?")]
            [DefaultValue(false)]
            public bool BossesOnly { get; set; }
        }

        [SeparatePage]
        public class AIRandomizations
        {
            [Label("Does randomization affect normal enemies?")]
            [DefaultValue(false)]
            public bool Enabled { get; set; }

            [Label("Does randomization affect town NPCs?")]
            [DefaultValue(false)]
            public bool AffectsTownNPCs { get; set; }

            [Label("Does randomization affect bosses?")]
            [DefaultValue(false)]
            public bool AffectsBosses { get; set; }

            [Label("Does randomization affect progression-important enemies?")]
            [DefaultValue(false)]
            public bool AffectsImportants { get; set; }

            [Label("Should immortality on randomized enemies be overriden?")]
            [DefaultValue(false)]
            public bool OverridesImmortality { get; set; }

            [Label("Meme AI Settings")]
            [Tooltip("Choose from a few meme options for AI, you are welcome hectic.")]
            public MemeAIRandomizations MemeAIRandomizationSettings { get; init; } = new();
        }

        [SeparatePage]
        public class MemeAIRandomizations
        {
            [Label("AI Override")]
            [Tooltip("Choose from a list of specific AIs that all NPCs will become.\nNote: this will affect all NPCs regardless of AI Randomization settings.")]
            [DrawTicks]
            [OptionStrings(new string[] { "None", "Goldfish", "Spiky Ball", "Fishron", "Bird", "Empress of Light" })]
            [DefaultValue("None")]
            public string ForcedAI { get; set; }

            [Label("Randomized Enemy Size")]
            [Tooltip("Randomize the size of all enemies between 0.1x and 3x")]
            [DefaultValue(false)]
            public bool RandomSize { get; set; }
        }
    }
}
