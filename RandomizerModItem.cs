using System;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace RandomizerMod;

public class RandomizerModItem : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        if (RandomizerMod.Config.ItemRandomization.ItemNameRandomization)
        {
            string name1 = Lang.GetItemNameValue(Main.rand.Next(ItemLoader.ItemCount));
            if (name1.Contains(' '))
            {
                string[] splitName = name1.Split(' ');
                if (Main.rand.NextBool(2))
                    name1 = splitName.First();
                else
                    name1 = splitName.Last();
            }
            string name2 = Lang.GetItemNameValue(Main.rand.Next(ItemLoader.ItemCount));
            if (name2.Contains(' '))
            {
                string[] splitName = name2.Split(' ');
                if (Main.rand.NextBool(2))
                    name2 = splitName.First();
                else
                    name2 = splitName.Last();
            }
            if (Main.rand.NextBool(2))
                item.SetNameOverride(name1 + " " + name2);
            else
            {
                string name3 = Lang.GetItemNameValue(Main.rand.Next(ItemLoader.ItemCount));
                if (name3.Contains(' '))
                {
                    string[] splitName = name3.Split(' ');
                    if (Main.rand.NextBool(2))
                        name3 = splitName.First();
                    else
                        name3 = splitName.Last();
                }
                item.SetNameOverride(name1 + " " + name2 + " " + name3);
            }
        }

        if (RandomizerMod.Config.ItemRandomization.StatsRandomization)
        {
            if (item.damage > 1 && item.pick == 0 && item.axe == 0 && item.hammer == 0)
            {
                item.damage = Main.rand.Next(10, 60) + (Main.rand.NextBool(2) ? (Main.rand.Next(20, 100) + (Main.rand.NextBool(2) ? Main.rand.Next(40, 200) : 0)) : 0);
                item.knockBack = Main.rand.Next(10, 60) + (Main.rand.NextBool(2) ? (Main.rand.Next(20, 100) + (Main.rand.NextBool(2) ? Main.rand.Next(40, 200) : 0)) : 0);
                item.useTime = Main.rand.Next(10, 20) + (Main.rand.NextBool(2) ? (Main.rand.Next(10, 40) + (Main.rand.NextBool(2) ? Main.rand.Next(10, 40) : 0)) : 0);
                item.crit = Main.rand.Next(0, 100);
            }
            else if (item.defense > 0)
            {
                item.defense = Main.rand.Next(10, 60) + (Main.rand.NextBool(2) ? (Main.rand.Next(20, 100) + (Main.rand.NextBool(2) ? Main.rand.Next(40, 200) : 0)) : 0);
            }
        }

        if (item.damage > 1 && item.pick == 0 && item.axe == 0 && item.hammer == 0)
        {
            switch (RandomizerMod.Config.ProjectileRandomization.ItemProjRandomization)
            {
                case RandomMode.Randomize:
                    item.shoot = Main.rand.Next(ProjectileLoader.ProjectileCount);
                    break;

                case RandomMode.Shuffle:
                    item.shoot = RandomizerMod.Instance.ProjectileShuffleMap[item.shoot];
                    break;
            }

            if (RandomizerMod.Config.ProjectileRandomization.ShootSpeedRandomization)
                item.shootSpeed = Main.rand.NextFloat(5f, 30f);
        }

        if (RandomizerMod.Config.ItemRandomization.ItemSpritesRandomization)
        {
            TextureAssets.Item[item.type] = TextureAssets.Item[Main.rand.Next(ItemLoader.ItemCount)];
        }

        if (item.createTile > -1)
        {
            switch (RandomizerMod.Config.ItemRandomization.PlacedTileRandomization)
            {
                case RandomMode.Randomize:
                    item.createTile = RandomizerMod.GetRandomTile();
                    break;

                case RandomMode.Shuffle:
                    item.createTile = RandomizerMod.Instance.TileShuffleMap[item.createTile];
                    break;
            }
        }

        if (item.createWall > -1)
        {
            switch (RandomizerMod.Config.ItemRandomization.PlacedWallRandomization)
            {
                case RandomMode.Randomize:
                    item.createWall = RandomizerMod.GetRandomWall();
                    break;

                case RandomMode.Shuffle:
                    item.createWall = RandomizerMod.Instance.WallShuffleMap[item.createWall];
                    break;
            }
        }
    }
}
