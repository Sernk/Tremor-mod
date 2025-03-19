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
	public class projMotherboardSuperLaser : ModProjectile
	{
		const int XOffsetMax = 300;
		const int XOffsetStep = 10;
		const int DustCount = 5;
		Color LaserColor = Color.Purple;
		int XOffsetNow;

		Vector2 endPoint
		{
			get
			{
				float X = Main.npc[(int)Projectile.ai[0]].Center.X + XOffsetNow;
				float Y = Main.npc[(int)Projectile.ai[0]].Center.Y;
				while (!WorldGen.SolidTile((int)X / 16, (int)Y / 16))
					if (Y + 8 > 8000)
						break;
					else
						Y += 8;
				return new Vector2(X, Y);
			}
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.timeLeft = 2;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.alpha = 100;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Motherboard Super Laser");
		}
		
		bool flag = true;
		public override void AI()
		{
			if (flag)
			{
				flag = false;
				XOffsetNow = (Projectile.ai[1] != 0) ? XOffsetMax : -XOffsetMax;
			}
			Projectile.Center = new Vector2(Main.npc[(int)Projectile.ai[0]].Center.X - 4, Main.npc[(int)Projectile.ai[0]].Center.Y + 88f);
			//for (int i = 0; i < DustCount; i++)
			//	Dust.NewDust(new Vector2(endPoint.X - 10, endPoint.Y + 10), 20, 20, DustID.Shadowflame);
			if (Projectile.ai[1] != 0)
			{
				XOffsetNow -= XOffsetStep;
				if (XOffsetNow <= -XOffsetMax)
					Projectile.active = false;
				else
					Projectile.timeLeft = 2;
				return;
			}
			XOffsetNow += XOffsetStep;
			if (XOffsetNow >= XOffsetMax)
				Projectile.active = false;
			else
				Projectile.timeLeft = 2;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			float point = 0f;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, endPoint, 4f, ref point);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			for (int i = 0; i < DustCount; i++)
				Dust.NewDust(new Vector2(endPoint.X - 10, endPoint.Y + 10), 20, 20, DustID.Shadowflame);

			PlayCollidingSound();

			return base.OnTileCollide(oldVelocity);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			PlayCollidingSound();
		}

		private void PlayCollidingSound()
		{
			//var zapSound = new LegacySoundStyle(SoundID.Trackable, TremorUtils.GetIdForSoundName($"dd2_sky_dragons_fury_circle_{Main.rand.Next(3)}"));
			//Main.PlayTrackedSound(zapSound.WithPitchVariance(Main.rand.NextFloat()).WithVolume(Main.soundVolume * 1.5f));
		}

		public override bool PreDraw(ref Color lightColor)
		{
            SpriteBatch spriteBatch = Main.spriteBatch;
            Vector2 unit = endPoint - Projectile.Center;
			float length = unit.Length();
			unit.Normalize();
			for (float k = 0; k <= length; k += 1f)
			{
				Vector2 drawPos = Projectile.Center + unit * k - Main.screenPosition;
				spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, new Color(LaserColor.R, LaserColor.G, LaserColor.B, Projectile.alpha), Helper.rotateBetween2Points(Main.npc[(int)Projectile.ai[0]].Center, endPoint), new Vector2(2, 2), 1f, SpriteEffects.None, 0f);
			}
			return false;
		}
	}
}
