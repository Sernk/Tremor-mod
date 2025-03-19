using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Projectiles
{
    public class PurpleBoomPro : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 90;  // ������ ������
            Projectile.height = 34;
            Projectile.hostile = true;  // ����������
            Projectile.timeLeft = 7;  // ����������� ����� ����� (��������, 60 ������ = 1 �������)
            Projectile.penetrate = -1;  // ����������� �������������
            Projectile.light = 1f;  // ����� ���������
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;  // �� ��������������� � �������
        }

        public override void AI()
        {
            // �������� ��������
            for (int i = 0; i < 30; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6), 150, default, 1.8f);
                Main.dust[dust].noGravity = true;  // ���� ��� ����������
            }

            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.position);  // ���� ������

            // ��������� ����� ����� �������, ���� ����������
            if (Projectile.timeLeft <= 1)
            {
                // ������ �������� ����� ���� ��������
                Projectile.Kill();
            }
        }
    }
}