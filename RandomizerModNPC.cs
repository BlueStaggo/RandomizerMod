using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomizerMod;

public class RandomizerModNPC : GlobalNPC
{
    internal static List<int> ImportantNPCs = new() { NPCID.LunarTowerNebula, NPCID.LunarTowerSolar, NPCID.LunarTowerStardust, NPCID.LunarTowerVortex, NPCID.CultistArcherBlue, NPCID.CultistDevote, NPCID.CultistTablet, NPCID.VoodooDemon, NPCID.DD2EterniaCrystal, NPCID.DD2LanePortal };

    private static bool copyingAI = false;

    public override void SetDefaults(NPC npc)
    {
        if (copyingAI) return;

        int randomAI = -1;
        var copyAISetting = RandomizerMod.Config.NPCRandomization.AIRandomization.CopyAI;
        if (copyAISetting.Type > 0)
        {
            copyingAI = true;
            var copyNPC = new NPC();
            copyNPC.SetDefaults(copyAISetting.Type);
            randomAI = copyNPC.aiStyle;
            copyingAI = false;
        }

        if (randomAI < 0) randomAI = RandomizerMod.Config.NPCRandomization.AIRandomization.ShuffleAI
            ? RandomizerMod.Instance.NPCAIShuffleMap[npc.aiStyle] : Main.rand.Next(126);

        if (RandomizerMod.Config.NPCRandomization.NameRandomization)
        {
            string name1 = Lang.GetNPCNameValue(Main.rand.Next(NPCLoader.NPCCount));
            if (name1.Contains(' '))
            {
                string[] splitName = name1.Split(' ');
                if (Main.rand.NextBool(2))
                    name1 = splitName.First();
                else
                    name1 = splitName.Last();
            }
            string name2 = Lang.GetNPCNameValue(Main.rand.Next(NPCLoader.NPCCount));
            if (name2.Contains(' '))
            {
                string[] splitName = name2.Split(' ');
                if (Main.rand.NextBool(2))
                    name2 = splitName.First();
                else
                    name2 = splitName.Last();
            }
            if (Main.rand.NextBool(2))
                npc.GivenName = name1 + " " + name2;
            else
            {
                string name3 = Lang.GetNPCNameValue(Main.rand.Next(NPCLoader.NPCCount));
                if (name3.Contains(' '))
                {
                    string[] splitName = name3.Split(' ');
                    if (Main.rand.NextBool(2))
                        name3 = splitName.First();
                    else
                        name3 = splitName.Last();
                }
                npc.GivenName = name1 + " " + name2 + " " + name3;
            }

        }
        if (ImportantNPCs.Contains(npc.type))
        {
            if (RandomizerMod.Config.NPCRandomization.AIRandomization.AffectsImportants)
                npc.aiStyle = randomAI;
        }
        if (npc.boss)
        {
            if (RandomizerMod.Config.NPCRandomization.AIRandomization.AffectsBosses)
                npc.aiStyle = randomAI;
        }
        if (npc.townNPC)
        {
            if (RandomizerMod.Config.NPCRandomization.AIRandomization.AffectsTownNPCs)
                npc.aiStyle = randomAI;
        }
        if (RandomizerMod.Config.NPCRandomization.AIRandomization.Enabled)
        {
            if (!ImportantNPCs.Contains(npc.type) && !npc.boss && !npc.townNPC)
                npc.aiStyle = randomAI;
        }
        if (RandomizerMod.Config.NPCRandomization.AIRandomization.RandomSize)
        {
            npc.scale = Main.rand.NextFloat(0.1f, 3f);
        }
    }

    public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
    {
        if (RandomizerMod.Config.NPCRandomization.ShopRandomization)
        {
            Array.Fill(items, null);
            int maxItems = Main.rand.Next(1, 40);
            for (int i = 0; i < maxItems; i++)
            {
                items[i] = new();
                items[i].SetDefaults(RandomizerMod.GetRandomItem());
                items[i].value = Main.rand.Next(1, 1000000);
            }
        }
    }

    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        if (npc.boss ? RandomizerMod.Config.NPCRandomization.BossDropsRandomization
            : RandomizerMod.Config.NPCRandomization.ItemDropsRandomization)
            npcLoot.RemoveWhere(rule => rule is not null);
    }

    public override void OnKill(NPC npc)
    {
        if (npc.boss ? RandomizerMod.Config.NPCRandomization.BossDropsRandomization
            : RandomizerMod.Config.NPCRandomization.ItemDropsRandomization)
            Item.NewItem(new EntitySource_Death(npc), npc.position, Vector2.Zero, Main.rand.Next(ItemLoader.ItemCount));
    }

    public override void PostAI(NPC npc)
    {
        base.PostAI(npc);
        if (RandomizerMod.Config.NPCRandomization.AIRandomization.OverridesImmortality)
        {
            if (npc.immortal || npc.dontTakeDamage || npc.dontTakeDamageFromHostiles)
            {
                npc.immortal = false;
                npc.dontTakeDamage = false;
                npc.dontTakeDamageFromHostiles = false;
            }
        }
    }
}
