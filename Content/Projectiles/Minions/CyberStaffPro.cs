using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content;
using TremorMod;
using TremorMod.Content.Buffs;

namespace TremorMod.Content.Projectiles.Minions
{
    public class CyberStaffPro : ModProjectile
    {
        public override void SetDefaults()
        {
            // ���������� ��������� ������������ ������� (��������, 533 - ��� ��� ������� �������)
            Projectile.CloneDefaults(533);
            Projectile.aiStyle = 533;
            Projectile.width = 50;
            Projectile.height = 50;
            Main.projFrames[Projectile.type] = 1;
            Projectile.friendly = true;
            Projectile.damage = 60; // ����������� ����
            Projectile.minion = true; // ���������, ��� ��� ������
            Projectile.minionSlots = 1; // ���������� ������ ��� ��������, ������� ����� ����� �����
            Projectile.penetrate = -1; // ������ ����� ��������� �� ������������� (�� ���� ��� "�����������" ������)
            Projectile.timeLeft = 18000; // ����� ����� �������
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false; // ������ �� ������������ � ��������
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; // �������� ����������� ��� ������� ��������� ����
        }

        public override void SetStaticDefaults()
        {
           // DisplayName.SetDefault("CyberStaffPro");
            //Main.projPet[Projectile.type] = true; // ������� ��� �������
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // ��� ������������ � ��������, ��������� ������� ��������
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = oldVelocity.Y;
            }
            return false; // �� ���������� ������� ��� ������������ � ��������
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // ��������, ������� �� �������
            if (!player.active || player.dead || !player.HasBuff(ModContent.BuffType<CyberSawBuff>()))
            {
                Projectile.Kill();
                return;
            }

            // �������� � ������
            Vector2 targetPosition = player.Center + new Vector2(0f, -48f);
            float speed = 10f;
            Vector2 direction = targetPosition - Projectile.Center;
            float distance = direction.Length();

            if (distance > 2000f) // ���� ������� ������� ������, �������������
            {
                Projectile.Center = player.Center;
            }
            else if (distance > 10f)
            {
                direction.Normalize();
                direction *= speed;
                Projectile.velocity = (Projectile.velocity * 20f + direction) / 21f;
            }
            else
            {
                Projectile.velocity *= 0.95f; // ����������
            }

            Projectile.rotation += 0.1f; // ������ ��������

            // ����� ������
            NPC target = FindTarget();
            if (target != null)
            {
                Vector2 attackDirection = target.Center - Projectile.Center;
                attackDirection.Normalize();
                attackDirection *= speed;
                Projectile.velocity = (Projectile.velocity * 10f + attackDirection) / 11f;

                // ��������� ���������� �� ����
                if (Vector2.Distance(Projectile.Center, target.Center) < 50f)
                {
                    int damage = Projectile.damage; // ���� �������
                    //float knockBack = 2f; // ������������
                    //bool crit = Main.rand.Next(100) < player.meleeCrit; // ����������� ����
                    //target.StrikeNPC(damage, knockBack, Projectile.direction, crit); // ������� ����
                }
            }
        }
        private NPC FindTarget()
        {
            NPC closestNPC = null;
            float closestDistance = 500f; // ������ ������ ������

            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy(this) && Vector2.Distance(Projectile.Center, npc.Center) < closestDistance)
                {
                    closestNPC = npc;
                    closestDistance = Vector2.Distance(Projectile.Center, npc.Center);
                }
            }

            return closestNPC;
        }
    }
}