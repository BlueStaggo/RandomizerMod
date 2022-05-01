using Terraria;
using Terraria.ModLoader;

namespace RandomizerMod
{
    public class RandomizerModProjectile : GlobalProjectile
    {
        public override void SetDefaults(Projectile projectile)
        {
            if (RandomizerMod.Config.ProjAIRandomization)
                projectile.aiStyle = Main.rand.Next(ProjectileLoader.ProjectileCount);
        }
    }
}
