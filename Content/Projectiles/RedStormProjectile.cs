using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace TremorMod.Content.Projectiles
{
    public class RedStormProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            AIType = ProjectileID.WoodenArrowFriendly; // ��������� ������
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // ��� ��������� ������� ������ � ����
            int laserCount = 5; // ���������� �������
            for (int i = 0; i < laserCount; i++)
            {
                // ������� ������ ��� ������
                Vector2 laserPosition = new Vector2(
                    target.Center.X + Main.rand.Next(-100, 100), // ��������� �������� �� �����������
                    target.Center.Y - 600f                       // ������ ���������
                );

                Vector2 laserVelocity = new Vector2(0, 10f); // ����� �������� ����

                // ������� ������ ������
                Projectile.NewProjectile(
                    Projectile.GetSource_OnHit(target),
                    laserPosition,
                    laserVelocity,
                    ModContent.ProjectileType<RedStormLaser>(), // ��� ������� ��� ������
                    Projectile.damage / 2,  // ���� ������ (�������� �� ���������)
                    0f,                     // ��� ������
                    Projectile.owner        // ��������
                );
            }
        }
    }
}
