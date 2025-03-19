using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace TremorMod.Content.Projectiles
{
    public class CyberRingPro : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 90;
            Projectile.height = 90;
            Projectile.hostile = true;
            Projectile.timeLeft = 500;
            Projectile.light = 0.8f;
            Projectile.tileCollide = false; // �������� ������ �����
            Projectile.penetrate = -1; // �� �������� ��� ������������
        }

        public override void AI()
        {
            // ����� ���� (������)
            Player target = Main.player[Player.FindClosest(Projectile.Center, Projectile.width, Projectile.height)];
            if (target != null && target.active && !target.dead)
            {
                // ����������� �� ������
                Vector2 direction = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero);

                // ������� ��������� ����������� �������
                float turnSpeed = 0.1f; // ��� ������ ��������, ��� ��������� ������ ����� ������������
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, direction * Projectile.velocity.Length(), turnSpeed);
            }

            // �������� �������
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            // ������ ����
            if (Main.rand.NextBool(3)) // ���� ���������� ����
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }
    }
}
