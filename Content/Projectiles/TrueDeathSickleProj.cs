using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Projectiles
{
	public class TrueDeathSickleProj : ModProjectile
	{
		public override void SetDefaults()
		{

			Projectile.width = 120;
			Projectile.height = 112;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.hide = true;
			Projectile.ownerHitCheck = false;
			Projectile.DamageType = DamageClass.Melee;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("True Death Sickle");

		}

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.soundDelay--;

            if (Projectile.soundDelay <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item71, Projectile.Center);
                Projectile.soundDelay = 45;
            }

            if (Main.myPlayer == Projectile.owner)
            {
                if (!player.channel || player.noItems || player.CCed)
                {
                    Projectile.Kill();
                }
                else
                {
                    Projectile.ai[0] -= 1f;
                    if (Projectile.ai[0] <= 0f)
                    {
                        Vector2 spawnPosition = Projectile.Center + new Vector2(Main.rand.Next(-7, 8), Main.rand.Next(-7, 8));
                        Vector2 velocity = Vector2.Zero;
                        float projectileSpeed = 14f;
                        int num6 = 274;

                        Vector2 mousePosition = Main.MouseWorld;

                        velocity = mousePosition - spawnPosition;
                        velocity.Normalize();
                        velocity *= projectileSpeed;

                        int proj = Projectile.NewProjectile(
                            Projectile.GetSource_FromThis(),
                            spawnPosition.X, spawnPosition.Y,
                            velocity.X, velocity.Y,
                            num6,
                            Projectile.damage,
                            Projectile.knockBack,
                            Projectile.owner,
                            0f, 120f
                        );

                        Main.projectile[proj].ai[1] = 120f;
                        Main.projectile[proj].extraUpdates = 1;
                        Main.projectile[proj].alpha = 0;

                        Projectile.ai[0] = 50f;
                    }
                }
            }

            Projectile.Center = player.MountedCenter;
            Projectile.position.X += player.width / 2 * player.direction;
            Projectile.spriteDirection = player.direction;
            Projectile.timeLeft = 2;
            Projectile.rotation += 0.19f * player.direction;

            if (Projectile.rotation > MathHelper.TwoPi)
                Projectile.rotation -= MathHelper.TwoPi;
            else if (Projectile.rotation < 0)
                Projectile.rotation += MathHelper.TwoPi;

            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = Projectile.rotation;
        }
    }
}
