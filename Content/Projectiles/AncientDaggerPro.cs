using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Projectiles
{
	public class AncientDaggerPro : ModProjectile
	{
		public override void SetDefaults()
		{

			Projectile.friendly = true;
			Projectile.aiStyle = 1;
			Projectile.timeLeft = 1200;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ancient Dagger Pro");

		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
			Projectile.position.X = Projectile.position.X + Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
			Projectile.width = 80;
			Projectile.height = 80;
			Projectile.position.X = Projectile.position.X - Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
			for (int num628 = 0; num628 < 40; num628++)
			{
				int num629 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num629].velocity *= 3f;
				if (Main.rand.NextBool(2))
				{
					Main.dust[num629].scale = 0.5f;
					Main.dust[num629].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num630 = 0; num630 < 70; num630++)
			{
				int num631 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num631].noGravity = true;
				Main.dust[num631].velocity *= 5f;
				num631 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num631].velocity *= 2f;
			}
			for (int num632 = 0; num632 < 3; num632++)
			{
				float scaleFactor10 = 0.33f;
				if (num632 == 1)
				{
					scaleFactor10 = 0.66f;
				}
				if (num632 == 2)
				{
					scaleFactor10 = 1f;
				}
				int num633 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num633].velocity *= scaleFactor10;
				Gore expr_13E6D_cp_0 = Main.gore[num633];
				expr_13E6D_cp_0.velocity.X = expr_13E6D_cp_0.velocity.X + 1f;
				Gore expr_13E8D_cp_0 = Main.gore[num633];
				expr_13E8D_cp_0.velocity.Y = expr_13E8D_cp_0.velocity.Y + 1f;
				num633 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 2f);
				Main.gore[num633].velocity *= scaleFactor10;
				Gore expr_13F30_cp_0 = Main.gore[num633];
				expr_13F30_cp_0.velocity.X = expr_13F30_cp_0.velocity.X - 1f;
				Gore expr_13F50_cp_0 = Main.gore[num633];
				expr_13F50_cp_0.velocity.Y = expr_13F50_cp_0.velocity.Y + 1f;
				num633 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num633].velocity *= scaleFactor10;
				Gore expr_13FF3_cp_0 = Main.gore[num633];
				expr_13FF3_cp_0.velocity.X = expr_13FF3_cp_0.velocity.X + 1f;
				Gore expr_14013_cp_0 = Main.gore[num633];
				expr_14013_cp_0.velocity.Y = expr_14013_cp_0.velocity.Y - 1f;
				num633 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num633].velocity *= scaleFactor10;
				Gore expr_140B6_cp_0 = Main.gore[num633];
				expr_140B6_cp_0.velocity.X = expr_140B6_cp_0.velocity.X - 1f;
				Gore expr_140D6_cp_0 = Main.gore[num633];
				expr_140D6_cp_0.velocity.Y = expr_140D6_cp_0.velocity.Y - 1f;
			}
			Projectile.position.X = Projectile.position.X + Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.position.X = Projectile.position.X - Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;

			if (Projectile.owner == Main.myPlayer)
			{
				int num220 = Main.rand.Next(3, 8);
				for (int num221 = 0; num221 < num220; num221++)
				{
					Vector2 value17 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
					value17.Normalize();
					value17 *= Main.rand.Next(10, 201) * 0.01f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, value17.X, value17.Y, ModContent.ProjectileType<BoomCloudPro>(), Projectile.damage, 1f, Projectile.owner, 0f, Main.rand.Next(-45, 1));
				}
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

	}
}
