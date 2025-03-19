using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Utilities;

namespace TremorMod.Content.Projectiles
{
	public class projClamperLaser : ModProjectile
	{
		Vector2 Offset
		{
			get
			{
				float X; float Y;
				switch ((int)Main.npc[(int)Projectile.ai[1]].localAI[1])
				{
					case 1: X = -50; Y = 55; break;
					case 2: X = -30; Y = 60; break;
					case 3: X = 30; Y = 60; break;
					default: X = 50; Y = 55; break;
				}
				return new Vector2(X, Y);
			}
		}

		public override void SetDefaults()
		{

			Projectile.width = 8;
			Projectile.height = 10;
			Projectile.timeLeft = 2;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Clamper Laser");

		}

		public override void AI()
		{
			Projectile.Center = Main.npc[(int)Projectile.ai[0]].Center + Offset;
			Projectile.localAI[0] += 1f;
			Projectile.alpha = (int)Projectile.localAI[1];
			if (Projectile.localAI[0] > 45f)
			{
				Projectile.damage = 0;
			}
			if (Projectile.localAI[0] > 60f)
			{
				Projectile.Kill();
			}
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			float point = 0f;
			Vector2 endPoint = Main.npc[(int)Projectile.ai[1]].Center;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, endPoint, 4f, ref point);
		}

		public override bool PreDraw(ref Color lightColor)
		{
            SpriteBatch spriteBatch = Main.spriteBatch;
            Vector2 endPoint = Main.npc[(int)Projectile.ai[1]].Center;
			Vector2 unit = endPoint - Projectile.Center;
			float length = unit.Length();
			unit.Normalize();
			for (float k = 0; k <= length; k += 8f)
			{
				Vector2 drawPos = Projectile.Center + unit * k - Main.screenPosition;
				spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, new Color(255, 255, 255, 255), Helper.rotateBetween2Points(drawPos, endPoint), new Vector2(2, 2), 1f, SpriteEffects.None, 0f);
			}
			return false;
		}
	}
}
