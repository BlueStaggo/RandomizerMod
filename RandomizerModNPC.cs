using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomizerMod
{
    public class RandomizerModNPC : GlobalNPC
    {
        internal static List<int> ImportantNPCs = new() { NPCID.LunarTowerNebula, NPCID.LunarTowerSolar, NPCID.LunarTowerStardust, NPCID.LunarTowerVortex, NPCID.CultistArcherBlue, NPCID.CultistDevote, NPCID.CultistTablet, NPCID.VoodooDemon, NPCID.DD2EterniaCrystal, NPCID.DD2LanePortal };
        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);
            if (ModContent.GetInstance<RandomizerModConfig>().NPCNameRandomization)
            {
                string name1 = Lang.GetNPCNameValue(Main.rand.Next(NPCLoader.NPCCount));
                if (name1.Contains(' '))
                {
                    string[] splitName = name1.Split(' ');
                    if (Main.rand.Next(2) == 0)
                        name1 = splitName.First();
                    else
                        name1 = splitName.Last();
                }
                string name2 = Lang.GetNPCNameValue(Main.rand.Next(NPCLoader.NPCCount));
                if (name2.Contains(' '))
                {
                    string[] splitName = name2.Split(' ');
                    if (Main.rand.Next(2) == 0)
                        name2 = splitName.First();
                    else
                        name2 = splitName.Last();
                }
                if (Main.rand.Next(2) == 0)
                    npc.GivenName = name1 + " " + name2;
                else
                {
                    string name3 = Lang.GetNPCNameValue(Main.rand.Next(NPCLoader.NPCCount));
                    if (name3.Contains(' '))
                    {
                        string[] splitName = name3.Split(' ');
                        if (Main.rand.Next(2) == 0)
                            name3 = splitName.First();
                        else
                            name3 = splitName.Last();
                    }
                    npc.GivenName = name1 + " " + name2 + " " + name3;
                }

            }
            if (ImportantNPCs.Contains(npc.type))
            {
                if (ModContent.GetInstance<RandomizerModConfig>().AIRandomizationSettings.affectsImportants)
                {
                    npc.aiStyle = Main.rand.Next(Main.npc.Length);
                }
            }
            if (npc.boss)
            {
                if (ModContent.GetInstance<RandomizerModConfig>().AIRandomizationSettings.affectsBosses)
                {
                    npc.aiStyle = Main.rand.Next(Main.npc.Length);
                }
            }
            if (npc.townNPC)
            {
                if (ModContent.GetInstance<RandomizerModConfig>().AIRandomizationSettings.affectsTownNPCs)
                {
                    npc.aiStyle = Main.rand.Next(Main.npc.Length);
                }
            }
            if (ModContent.GetInstance<RandomizerModConfig>().AIRandomizationSettings.enabled)
            {
                string forcedAI = ModContent.GetInstance<RandomizerModConfig>().AIRandomizationSettings.MemeAIRandomizationSettings.ForcedAI;
                if (forcedAI != "None")
                    npc.aiStyle = forcedAI switch
                    {
                        "Goldfish" => 16,
                        "Spiky Ball" => 20,
                        "Fishron" => 69,
                        "Bird" => 24,
                        "Empress of Light" => 120,
                        _ => npc.aiStyle
                    };
                else if (!ImportantNPCs.Contains(npc.type) && !npc.boss && !npc.townNPC)
                    npc.aiStyle = Main.rand.Next(Main.npc.Length);
            }
            if (ModContent.GetInstance<RandomizerModConfig>().SoundsRandomization)
            {
                npc.HitSound = new LegacySoundStyle(SoundID.NPCHit, Main.rand.Next(SoundEngine.LegacySoundPlayer.SoundNpcHit.Length));
                npc.DeathSound = new LegacySoundStyle(SoundID.NPCKilled, Main.rand.Next(SoundEngine.LegacySoundPlayer.SoundNpcKilled.Length));
            }
            if (ModContent.GetInstance<RandomizerModConfig>().AIRandomizationSettings.MemeAIRandomizationSettings.RandomSize)
            {
                npc.scale = Main.rand.NextFloat(0.1f, 3f);
            }
        }

        public static void RandomiseShops(Chest shop, ref int nextSlot)
        {
            int numberitems = Main.rand.Next(1, 40);
            List<int> itemlist = new();
            int[] itemarray;
            Item item = new();
            for (int i = 0; i < ItemLoader.ItemCount; i++)
            {
                item.SetDefaults(i);
                if (item.type != ItemID.None)
                {
                    itemlist.Add(i);
                }
            }
            itemarray = itemlist.ToArray();
            for (int i = 0; i < numberitems; i++)
            {
                shop.item[nextSlot].SetDefaults(Main.rand.Next(itemarray));
                shop.item[nextSlot].value = Main.rand.Next(1, 1000000);
                nextSlot++;
            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (ModContent.GetInstance<RandomizerModConfig>().NPCShopRandomization)
            {
                for (int i = 0; i < 40; i++)
                {
                    shop.item[i].TurnToAir();
                }
                nextSlot = 0;
                RandomiseShops(shop, ref nextSlot);
            }
        }

        public override bool PreKill(NPC npc)
        {
            return true;
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (ModContent.GetInstance<RandomizerModConfig>().NPCLootRandomization.enabled)
            {
                if (ModContent.GetInstance<RandomizerModConfig>().NPCLootRandomization.bossesOnly)
                {
                    if (npc.boss)
                    {
                        npcLoot.RemoveWhere(rule => rule is not null);
                    }
                }
                else
                {
                    npcLoot.RemoveWhere(rule => rule is not null);
                }
            }
        }

        public override void OnKill(NPC npc)
        {
            if (ModContent.GetInstance<RandomizerModConfig>().NPCLootRandomization.enabled)
            {
                if (ModContent.GetInstance<RandomizerModConfig>().NPCLootRandomization.bossesOnly)
                {
                    if (npc.boss)
                    {
                        Item.NewItem(new EntitySource_DropAsItem(npc), npc.position, Main.rand.Next(ItemLoader.ItemCount));
                    }
                }
                else
                {
                    Item.NewItem(new EntitySource_DropAsItem(npc), npc.position, Main.rand.Next(ItemLoader.ItemCount));
                }
            }
        }

        public override void PostAI(NPC npc)
        {
            base.PostAI(npc);
            if (ModContent.GetInstance<RandomizerModConfig>().AIRandomizationSettings.overridesImmortality)
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
}
