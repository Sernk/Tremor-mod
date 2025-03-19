using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Projectiles
{
	public class projMotherboardLaser : ModProjectile
	{
		const float YOffset = 95f;
		Color LaserColor = Color.Purple;

		public override void SetDefaults()
		{
			Projectile.width = 2;
			Projectile.height = 2;
			Projectile.timeLeft = 30;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Motherboard Laser");
		}
		
		public override void AI()
		{
			Projectile.Center = new Vector2(Main.npc[(int)Projectile.ai[0]].Center.X, Main.npc[(int)Projectile.ai[0]].Center.Y + ((Projectile.localAI[1] == 1) ? YOffset : 0));
			Projectile.localAI[0] += 1f;
			Projectile.alpha = (int)Projectile.localAI[0] * 4;
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

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			PlayZapSound();
			return base.OnTileCollide(oldVelocity);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			PlayZapSound();
		}

		private void PlayZapSound()
		{
			//var zapSound = new LegacySoundStyle(SoundID.Trackable, TremorUtils.GetIdForSoundName($"dd2_lightning_bug_zap_{Main.rand.Next(3)}"));
			//SoundEngine.PlaySound(zapSound.WithPitchVariance(Main.rand.NextFloat() * .5f).WithVolume(Main.soundVolume * 0.5f));
		}

		public override bool PreDraw(ref Color lightColor)
		{
            SpriteBatch spriteBatch = Main.spriteBatch;
            Vector2 endPoint = Main.npc[(int)Projectile.ai[1]].Center;
			Vector2 unit = endPoint - Projectile.Center;
			float length = unit.Length();
			unit.Normalize();
			for (float k = 0; k <= length; k += 5f)
			{
				Vector2 drawPos = Projectile.Center + unit * k - Main.screenPosition;
				Color alpha = new Color(LaserColor.R, LaserColor.G, LaserColor.B, Projectile.alpha);
				spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, alpha, MathHelper.ToDegrees(Main.rand.Next(0, 181)), new Vector2(2, 2), 1f, SpriteEffects.None, 0f);
			}
			return false;
		}
	}
}
