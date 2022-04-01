using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace RandomizerMod
{
    public class RandomizerModSystem : ModSystem
    {
        public override void PostWorldGen()
        {
            if (ModContent.GetInstance<RandomizerModConfig>().WorldGenRandomization)
            {
                for (int x = 0; x < Main.maxTilesX; x++)
                {
                    for (int y = (int)Main.worldSurface / 16; y < Main.maxTilesY; y++)
                    {
                        if (WorldGen.InWorld(x, y))
                        {
                            ushort id = (ushort)Main.rand.Next(TileLoader.TileCount);
                            while (Main.tileFrameImportant[id] || id == TileID.GoldCoinPile || id == TileID.SilverCoinPile || id == TileID.CopperCoinPile || id == TileID.PlatinumCoinPile)
                            {
                                id = (ushort)Main.rand.Next(TileLoader.TileCount);
                            }
                            if (!Main.tileFrameImportant[id])
                            {
                                if (y > 40 && y < Main.maxTilesY - 40 && x > 40 && x < Main.maxTilesX - 40)
                                {
                                    if(!Main.tileFrameImportant[Main.tile[x, y].TileType])
                                    {
                                        WorldGen.TileRunner(x, y, WorldGen.genRand.Next(1, 8), WorldGen.genRand.Next(1, 8), id, false, 0f, 0f, false, true);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (ModContent.GetInstance<RandomizerModConfig>().ChestsRandomization)
            {
                List<int> chestItems = new();
                Item testItem = new();
                for (int i = 0; i < ItemLoader.ItemCount; i++)
                {
                    testItem.SetDefaults(i);
                    if (testItem.type != ItemID.None && testItem.type != ModContent.ItemType<Terraria.ModLoader.Default.UnloadedItem>())
                        chestItems.Add(i);
                }
                var chestArray = chestItems.ToArray();

                for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
                {
                    Chest chest = Main.chest[chestIndex];
                    if (chest is null || !WorldGen.InWorld(chest.x, chest.y, 42))
                        continue;

                    Tile tile = Main.tile[chest.x, chest.y];
                    if (tile.TileType != TileID.Containers || chest.item == null)
                        continue;

                    RandomizerMod.Instance.Logger.Info("We got a chest!");

                    var maxContent = Main.rand.Next(1, 40);
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        chest.item[inventoryIndex].TurnToAir();
                        if (inventoryIndex < maxContent)
                        {
                            chest.item[inventoryIndex] = new Item();
                            var chestItem = chest.item[inventoryIndex];
                            chestItem.SetDefaults(Main.rand.NextFromList(chestArray));
                            chestItem.stack = Main.rand.Next(1, chestItem.maxStack);
                            chestItem.Prefix(-1);
                        }
                    }
                }
            }
        }
    }
}
