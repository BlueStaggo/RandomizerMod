using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomizerMod
{
    public class RandomizerModItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (!Main.dedServ && !Main.gameMenu)
            {
                base.SetDefaults(item);
                if (RandomizerMod.Config.ItemNameRandomization)
                {
                    string name1 = Lang.GetItemNameValue(Main.rand.Next(ItemLoader.ItemCount));
                    if (name1.Contains(' '))
                    {
                        string[] splitName = name1.Split(' ');
                        if (Main.rand.Next(2) == 0)
                            name1 = splitName.First();
                        else
                            name1 = splitName.Last();
                    }
                    string name2 = Lang.GetItemNameValue(Main.rand.Next(ItemLoader.ItemCount));
                    if (name2.Contains(' '))
                    {
                        string[] splitName = name2.Split(' ');
                        if (Main.rand.Next(2) == 0)
                            name2 = splitName.First();
                        else
                            name2 = splitName.Last();
                    }
                    if (Main.rand.Next(2) == 0)
                        item.SetNameOverride(name1 + " " + name2);
                    else
                    {
                        string name3 = Lang.GetItemNameValue(Main.rand.Next(ItemLoader.ItemCount));
                        if (name3.Contains(' '))
                        {
                            string[] splitName = name3.Split(' ');
                            if (Main.rand.Next(2) == 0)
                                name3 = splitName.First();
                            else
                                name3 = splitName.Last();
                        }
                        item.SetNameOverride(name1 + " " + name2 + " " + name3);
                    }

                }
                if (RandomizerMod.Config.StatsRandomization)
                {
                    if (item.damage > 1 && item.pick == 0 && item.axe == 0 && item.hammer == 0)
                    {
                        item.damage = Main.rand.Next(10, 60) + (Main.rand.Next(2) == 0 ? (Main.rand.Next(20, 100) + (Main.rand.Next(2) == 0 ? Main.rand.Next(40, 200) : 0)) : 0);
                        item.knockBack = Main.rand.Next(10, 60) + (Main.rand.Next(2) == 0 ? (Main.rand.Next(20, 100) + (Main.rand.Next(2) == 0 ? Main.rand.Next(40, 200) : 0)) : 0);
                        item.useTime = Main.rand.Next(10, 20) + (Main.rand.Next(2) == 0 ? (Main.rand.Next(10, 40) + (Main.rand.Next(2) == 0 ? Main.rand.Next(10, 40) : 0)) : 0);
                        item.crit = Main.rand.Next(0, 100);
                    }
                    else if (item.defense > 0)
                    {
                        item.defense = Main.rand.Next(10, 60) + (Main.rand.Next(2) == 0 ? (Main.rand.Next(20, 100) + (Main.rand.Next(2) == 0 ? Main.rand.Next(40, 200) : 0)) : 0);
                    }
                }
                if (RandomizerMod.Config.RandomProjRandomization)
                {
                    if (item.damage > 1 && item.pick == 0 && item.axe == 0 && item.hammer == 0)
                    {
                        item.shoot = Main.rand.Next(ProjectileLoader.ProjectileCount);
                        item.shootSpeed = Main.rand.NextFloat(5f, 30f);
                    }
                }
                if (RandomizerMod.Config.ItemSpritesRandomization)
                {
					TextureAssets.Item[item.type] = TextureAssets.Item[Main.rand.Next(ItemLoader.ItemCount)];
				}
                if (RandomizerMod.Config.SoundsRandomization)
                {
                    item.UseSound = new LegacySoundStyle(SoundID.Item, Main.rand.Next(SoundEngine.LegacySoundPlayer.SoundItem.Length));
                }
            }
        }
    }
}
