using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Projectiles.Minions
{
	public class StarfishPro : ModProjectile
    {
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.CloneDefaults(388);
			AIType = 388;

			Projectile.width = 24;
			Projectile.height = 24;
			Main.projFrames[Projectile.type] = 1;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.ignoreWater = true;
                        Projectile.tileCollide = false;
                        ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}

		public override void SetStaticDefaults()
		{
		  // DisplayName.SetDefault("StarfishPro");
       
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
				if (Projectile.velocity.X != oldVelocity.X)
				{
					Projectile.velocity.X = oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y)
				{
					Projectile.velocity.Y = oldVelocity.Y;
				}
      return false;
		}
	}
}