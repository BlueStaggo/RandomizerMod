using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomizerMod;

public class RandomizerModSystem : ModSystem
{
    public override void SetStaticDefaults()
    {
        RandomizerMod.Instance.InitializeShuffleMaps();
    }

    public override void PostWorldGen()
    {
        if (RandomizerMod.Config.WorldRandomization.TileRandomization)
        {
            var framedTileShuffleMap = new ShuffleMap<int>(Main.rand, Enumerable.Range(0, TileLoader.TileCount).Where(t => !Main.tileFrameImportant[t]).ToList());
            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Main.tile[x, y];
                    if (tile.HasTile && !Main.tileFrameImportant[tile.TileType]) Main.tile[x, y].TileType
                        = (ushort)framedTileShuffleMap[tile.TileType];
                    if (tile.WallType > 0) Main.tile[x, y].WallType
                        = (ushort)RandomizerMod.Instance.WallShuffleMap[tile.WallType];
                }
            }
        }
        if (RandomizerMod.Config.WorldRandomization.ChestsRandomization)
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

    public override void PostAddRecipes()
    {
        static bool NonEmptyRecipe(Recipe r) => r is not null && r.createItem.type > ItemID.None;

        switch (RandomizerMod.Config.ItemRandomization.RecipeRandomization)
        {
            case RandomMode.Randomize:
                foreach (var recipe in Main.recipe.Where(NonEmptyRecipe))
                {
                    var newId = RandomizerMod.GetRandomItem();
                    var item = new Item();
                    item.SetDefaults(newId);

                    recipe.ReplaceResult(newId, Main.rand.Next(1, item.maxStack));
                }
                break;

            case RandomMode.Shuffle:
                var results = Main.recipe
                    .Where(NonEmptyRecipe)
                    .Select(r => r.createItem)
                    .OrderBy(_ => Main.rand.Next())
                    .ToList();

                int j = 0;
                foreach (var recipe in Main.recipe.Where(NonEmptyRecipe))
                {
                    recipe.ReplaceResult(results[j].type, results[j].stack);
                    j++;
                }

                break;
        }

        var FirstRecipeForItemField = typeof(RecipeLoader).GetField("FirstRecipeForItem", BindingFlags.NonPublic | BindingFlags.Static);
        var FirstRecipeForItem = (Recipe[])FirstRecipeForItemField.GetValue(null);
        Array.Fill(FirstRecipeForItem, null);

        foreach (var recipe in Main.recipe.Where(r => r is not null))
        {
            if (FirstRecipeForItem[recipe.createItem.type] == null)
                FirstRecipeForItem[recipe.createItem.type] = recipe;
        }
    }
}
