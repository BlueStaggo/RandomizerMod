using Terraria.ModLoader;

namespace RandomizerMod
{
	public class RandomizerMod : Mod
	{
		public static RandomizerMod Instance { get; private set; }
        public static RandomizerModConfig Config { get; private set; }

        public override void Load()
        {
            Instance = this;
            Config = ModContent.GetInstance<RandomizerModConfig>();
        }

        public override void Unload()
        {
            Instance = null;
            Config = null;
        }
    }
}
