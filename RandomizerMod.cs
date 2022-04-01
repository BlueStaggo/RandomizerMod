using Terraria.ModLoader;

namespace RandomizerMod
{
	public class RandomizerMod : Mod
	{
		public static RandomizerMod Instance { get; private set; }

        public override void Load()
        {
            Instance = this;
        }

        public override void Unload()
        {
            Instance = null;
        }
    }
}