using Terraria;
using Terraria.ModLoader;

namespace RandomizerMod;

public class RandomizerModProjectile : GlobalProjectile
{
    private static bool copyingAI = false;

    public override void SetDefaults(Projectile projectile)
    {
        if (copyingAI) return;

        int randomAI = -1;
        var copyAISetting = RandomizerMod.Config.ProjectileRandomization.CopyAI;
        if (copyAISetting.Type > 0)
        {
            copyingAI = true;
            var copyProjectile = new Projectile();
            copyProjectile.SetDefaults(copyAISetting.Type);
            randomAI = copyProjectile.aiStyle;
            copyingAI = false;
        }

        if (randomAI < 0)
            if (RandomizerMod.Config.ProjectileRandomization.AIRandomization == RandomMode.Randomize)
                randomAI = Main.rand.Next(1, ProjectileLoader.ProjectileCount - 1);
            else if (RandomizerMod.Config.ProjectileRandomization.AIRandomization == RandomMode.Shuffle)
                randomAI = RandomizerMod.Instance.ProjectileAIShuffleMap[projectile.aiStyle];

        if (randomAI >= 0)
            projectile.aiStyle = randomAI;
    }
}
