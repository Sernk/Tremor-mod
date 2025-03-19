using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Projectiles
{
    public class PurplePulsePro : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 90;
            Projectile.height = 34;
            Projectile.hostile = true;
            Projectile.timeLeft = 300;  // ����� ����� �������
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            this.Projectile.rotation = this.Projectile.velocity.ToRotation();

            if (this.Projectile.localAI[0] == 0f)
            {
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item91, this.Projectile.position);
            }

            this.Projectile.localAI[0] += 1f;

            if (this.Projectile.localAI[0] > 3f)
            {
                int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch);
                Main.dust[dustID].noGravity = true;
            }

            // ����� ����� ����� ������� �������������, ������� ������ ������
            if (Projectile.timeLeft <= 1)
            {
                Explode();  // ����� ������
            }
        }

        // �������� ������� PurpleBoomPro (�����)
        private void Explode()
        {
            // ������� ������ PurpleBoomPro �� ����� �������� �������
            Projectile.NewProjectile(
                Projectile.GetSource_Death(),  // �������� ������
                Projectile.Center.X,  // ���������� ������ �������
                Projectile.Center.Y,
                0f, 0f,  // ��������� �������� (������ ���������� �� �����)
                ModContent.ProjectileType<PurpleBoomPro>(),  // ��� ������� (�����)
                Projectile.damage,  // ���� ������
                0f,  // ���� �����
                Projectile.owner  // �������� �������
            );
        }
    }
}
