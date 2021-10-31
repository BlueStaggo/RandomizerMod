using Terraria;
using Terraria.ModLoader;

namespace RandomizerMod
{
    public class RandomizerModProjectile : GlobalProjectile
    {
        public override void SetDefaults(Projectile projectile)
        {
            base.SetDefaults(projectile);

            if (ModContent.GetInstance<RandomizerModConfig>().ProjAIRandomization)
                projectile.aiStyle = Main.rand.Next(ProjectileLoader.ProjectileCount);
        }
    }
}
