using Terraria;
using Terraria.ModLoader;

namespace TremorMod.Content.Projectiles
{
	public class DarkhalisPro : ModProjectile
	{
		public override void SetDefaults()
		{
            Projectile.CloneDefaults(595);
            Projectile.width = 100;
            Projectile.height = 70;
            AIType = 595;
            Main.projFrames[Projectile.type] = 28;
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("DarkhalisPro");

		}

	}
}
