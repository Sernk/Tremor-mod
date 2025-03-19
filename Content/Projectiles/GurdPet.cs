using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Filters = Terraria.Graphics.Effects.Filters;

namespace TremorMod.Content.Projectiles
{
	public class GurdPet : ModProjectile
	{
		public override void SetDefaults()
		{
            Main.projFrames[Projectile.type] = 8;
            Projectile.width = 46;
            Projectile.height = 38;
            Projectile.aiStyle = -1; // ������� ����������� AI
            Projectile.friendly = true;
            Projectile.penetrate = -1; // ������� �� ������������
            Projectile.timeLeft = 2; // ��������� �����������
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true; // ������� ����� ������������ � ��������
        }

        public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Gurd Pet");
            Main.projPet[Projectile.type] = true; // ������� ��� �������
		}

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // ���������, ��� �� �����
            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<Buffs.GurdPetBuff>());
            }

            // ���������, ���� �� ���� �������
            if (player.HasBuff(ModContent.BuffType<Buffs.GurdPetBuff>()))
            {
                Projectile.timeLeft = 2; // ������������ �������������
            }

            // �������� � ������
            Vector2 playerPosition = player.Center + new Vector2(-50f, 0f); // �������� ������������ ������
            float distanceToPlayer = Vector2.Distance(Projectile.Center, playerPosition);

            if (distanceToPlayer > 1000f) // ���� ������� ������� ������, ������������� ���
            {
                Projectile.Center = playerPosition;
            }

            // ������ �� �������
            float speed = 2f; // �������� ��������
            float inertia = 20f;

            if (Projectile.Center.X < player.Center.X - 60f) // ���� ������
            {
                Projectile.velocity.X = (Projectile.velocity.X * (inertia - 1) + speed) / inertia;
            }
            else if (Projectile.Center.X > player.Center.X + 60f) // ���� �����
            {
                Projectile.velocity.X = (Projectile.velocity.X * (inertia - 1) - speed) / inertia;
            }
            else // ���� ����� � �������, �����������
            {
                Projectile.velocity.X *= 0.9f;
            }

            // �������� �� �����
            Point tileBelowPosition = (Projectile.Bottom / 16).ToPoint() + new Point(0, 1); // ���������� ������ ��� ��������
            Tile tileBelow = Framing.GetTileSafely(tileBelowPosition.X, tileBelowPosition.Y);

            if (Projectile.velocity.Y == 0f) // ���� �� �����
            {
                if (!tileBelow.HasTile || !Main.tileSolid[tileBelow.TileType]) // ���� ������ ���, ������
                {
                    Projectile.velocity.Y += 0.4f;
                }
            }
            else // ���� � �������, �������� �������
            {
                Projectile.velocity.Y += 0.4f;
            }

            // ����������� ������������ ��������
            if (Projectile.velocity.Y > 10f)
            {
                Projectile.velocity.Y = 10f;
            }

            // ������������� ����������� �������
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;

            // ��������
            if (Projectile.velocity.X != 0)
            {
                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 8) // �������� ����� ������
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
                }
            }
            else
            {
                Projectile.frame = 0; // ���� �����, ���������� ������ ����
            }
        }
    }
}
