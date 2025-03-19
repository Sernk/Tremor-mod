using Terraria;
using System;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;


namespace TremorMod.Content.Projectiles
{
    public class LightningOrb : ModProjectile
    {
        private const int NormalFrameCount = 4;
        private int hitCount = 0; // ������� ������

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180; // �������� ����� 3 ������� (60 ������ = 1 �������)
            Projectile.light = 1f;
            Projectile.aiStyle = -1; // ���������������� ������ AI
        }

        public override void AI()
        {
            // ���������� �������� �������
            int totalFrames = 4; // ���������� ������
            //int frameHeight = 99; // ������ ������ �����
            //int frameWidth = 99; // ������ ������ �����

            // ������� ������
            Projectile.frameCounter++;

            // ������� ���� ����� ������������ �������
            if (Projectile.frameCounter >= 6) // �������� ��������, ��� ������ �����, ��� ���������
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= totalFrames) // ������� � ������� ����� ����� ����������
                {
                    Projectile.frame = 0;
                }
            }
            // ����������� �� ���������� ������
            Player targetPlayer = Main.player[Player.FindClosest(Projectile.Center, 0, 0)];
            if (targetPlayer != null && !targetPlayer.dead)
            {
                Vector2 direction = Vector2.Normalize(targetPlayer.Center - Projectile.Center);
                Projectile.velocity = direction * 10f; // �������� ������

                // �������� ���������� ��������
                if (Main.rand.NextBool(3))
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 100, default, 1.5f);
                    Main.dust[dust].noGravity = true;
                }
            }

            // ���������, ������� ��� ������ ����� ����
            if (hitCount >= 5)
            {
                Projectile.Kill(); // ������ �������� ����� 5 ������
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            hitCount++; // ����������� ������� ��� ����� �� ������
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 100, default, 1.5f);
                Main.dust[dust].noGravity = true;
            }
        }


        public override bool PreDraw(ref Color lightColor)
        {
            // �������� �������� �������
            Texture2D texture = ModContent.Request<Texture2D>("TremorMod/Content/Projectiles/LightningOrb").Value;
            // ������ ������ � ���������
            Rectangle frameRectangle = new Rectangle(0, Projectile.frame * 99, 99, 99);
            Vector2 position = Projectile.Center - Main.screenPosition;

            Main.spriteBatch.Draw(texture, position, frameRectangle, lightColor);

            return false; // ���������� false, ����� ����������� ����� ��������� �� ���������
        }
    }
}