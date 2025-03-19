using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace TremorMod.Content.Projectiles
{
	public class FrostwindPro : ModProjectile
	{
		public override void SetDefaults()
		{

			Projectile.light = 0.8f;
			Projectile.width = 80;
			Projectile.height = 46;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.penetrate = -1;
			Projectile.alpha = 255;
			Projectile.timeLeft = 120;
			Main.projFrames[Projectile.type] = 6;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("FrostwindPro");

		}

		public override void AI()
		{
			int num613 = 10;
			int num614 = 15;
			float num615 = 1f;
			int num616 = 150;
			int num617 = 42;
			if (Projectile.velocity.X != 0f)
			{
				Projectile.direction = (Projectile.spriteDirection = -Math.Sign(Projectile.velocity.X));
			}
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 2)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 6)
			{
				Projectile.frame = 0;
			}
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.localAI[0] = 1f;
				Projectile.position.X = Projectile.position.X + Projectile.width / 2;
				Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
				Projectile.scale = (num613 + num614 - Projectile.ai[1]) * num615 / (num614 + num613);
				Projectile.width = (int)(num616 * Projectile.scale);
				Projectile.height = (int)(num617 * Projectile.scale);
				Projectile.position.X = Projectile.position.X - Projectile.width / 2;
				Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
				Projectile.netUpdate = true;
			}
			if (Projectile.ai[1] != -1f)
			{
				Projectile.scale = (num613 + num614 - Projectile.ai[1]) * num615 / (num614 + num613);
				Projectile.width = (int)(num616 * Projectile.scale);
				Projectile.height = (int)(num617 * Projectile.scale);
			}
			if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
			{
				Projectile.alpha -= 30;
				if (Projectile.alpha < 60)
				{
					Projectile.alpha = 60;
				}
			}
			else
			{
				Projectile.alpha += 30;
				if (Projectile.alpha > 150)
				{
					Projectile.alpha = 150;
				}
			}
			if (Projectile.ai[0] > 0f)
			{
				Projectile.ai[0] -= 1f;
			}
            if (Projectile.ai[0] == 1f && Projectile.ai[1] > 0f && Projectile.owner == Main.myPlayer)
            {
                Projectile.netUpdate = true;
                Vector2 center = Projectile.Center;
                center.Y -= num617 * Projectile.scale / 2f;

                float num618 = (num613 + num614 - Projectile.ai[1] + 1f) * num615 / (num614 + num613);
                center.Y -= num617 * num618 / 2f;
                center.Y += 2f;

                // ���������� �����
                IEntitySource source = Projectile.GetSource_FromThis(); // ������ ��������� IEntitySource
                Vector2 position = new Vector2(center.X, center.Y);
                Vector2 velocity = new Vector2(Projectile.velocity.X, Projectile.velocity.Y);
                int damage = (int)Projectile.damage; // ���������� � int
                int knockBack = (int)Projectile.knockBack; // ���������� � int

                // ����� ������
                Projectile.NewProjectile(source, position, velocity, Projectile.type, damage, knockBack, Projectile.owner, 10f, Projectile.ai[1] - 1f);
            }
            if (Projectile.ai[0] <= 0f)
			{
				float num622 = 0.104719758f;
				float num623 = Projectile.width / 5f;
				float num624 = (float)(Math.Cos(num622 * -(double)Projectile.ai[0]) - 0.5) * num623;
				Projectile.position.X = Projectile.position.X - num624 * -(float)Projectile.direction;
				Projectile.ai[0] -= 1f;
				num624 = (float)(Math.Cos(num622 * -(double)Projectile.ai[0]) - 0.5) * num623;
				Projectile.position.X = Projectile.position.X + num624 * -(float)Projectile.direction;
			}
		}

	}
}
