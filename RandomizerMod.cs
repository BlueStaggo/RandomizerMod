using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;

namespace RandomizerMod;

public class RandomizerMod : Mod
{
    public static RandomizerMod Instance { get; private set; }
    public static RandomizerModConfig Config { get; private set; }

    public ShuffleMap<int> ItemShuffleMap { get; private set; }
    public ShuffleMap<int> TileShuffleMap { get; private set; }
    public ShuffleMap<int> WallShuffleMap { get; private set; }
    public ShuffleMap<int> ProjectileShuffleMap { get; private set; }
    public ShuffleMap<int> ProjectileAIShuffleMap { get; private set; }
    public ShuffleMap<int> NPCAIShuffleMap { get; private set; }

    public override void Load()
    {
        Instance = this;
        Config = ModContent.GetInstance<RandomizerModConfig>();
        InitializeShuffleMaps();
    }

    public void InitializeShuffleMaps()
    {
        ItemShuffleMap = new(Main.rand, Enumerable.Range(1, ItemLoader.ItemCount).Where(x => x != ModContent.ItemType<UnloadedItem>()).ToList());
        TileShuffleMap = new(Main.rand, Enumerable.Range(0, TileLoader.TileCount).Where(x => x != ModContent.TileType<UnloadedTile>()).ToList());
        WallShuffleMap = new(Main.rand, Enumerable.Range(1, WallLoader.WallCount).Where(x => x != ModContent.WallType<UnloadedWall>()).ToList());
        ProjectileShuffleMap = new(Main.rand, Enumerable.Range(0, ProjectileLoader.ProjectileCount).ToList());
        ProjectileAIShuffleMap = new(Main.rand, Enumerable.Range(0, 196).ToList());
        NPCAIShuffleMap = new(Main.rand, Enumerable.Range(0, 126).ToList());
    }

    public override void Unload()
    {
        Instance = null;
        Config = null;
    }

    public static int GetRandomItem()
    {
        int item;
        do item = Main.rand.Next(1, ItemLoader.ItemCount - 1);
        while (item == ModContent.ItemType<UnloadedItem>());
        return item;
    }

    public static int GetRandomTile()
    {
        int tile;
        do tile = Main.rand.Next(1, TileLoader.TileCount - 1);
        while (tile == ModContent.TileType<UnloadedTile>());
        return tile;
    }

    public static int GetRandomWall()
    {
        int wall;
        do wall = Main.rand.Next(1, WallLoader.WallCount - 1);
        while (wall == ModContent.WallType<UnloadedWall>());
        return wall;
    }
}
