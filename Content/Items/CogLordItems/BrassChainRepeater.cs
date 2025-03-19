using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Buffs;

namespace TremorMod.Content.Items.CogLordItems
{
    public class BrassChainRepeater : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.width = 36;
            Item.height = 24;
            Item.useTime = 11;
            Item.useAnimation = 11;
            Item.shoot = ProjectileID.WoodenArrowFriendly; // ������������� ������
            Item.useAmmo = AmmoID.Arrow;
            Item.shootSpeed = 30f;
            Item.useStyle = 5; // ����� �������������, ��� � ������ �������� ���
            Item.damage = 30;
            Item.knockBack = 4;
            Item.value = 30000;
            Item.rare = 5;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
        }

        /*public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Brass Chain Repeater");
			Tooltip.SetDefault("Quickly launches arrows\n" +
			"25% to shoot a heat ray");
		}*/

        public override void UpdateInventory(Player player)
        {
            if (player.HasBuff(ModContent.BuffType<SteamRangerBuff>()))
            {
                Item.damage = 45;
            }
            else
            {
                Item.damage = 30;
            }
        }

        // ������������� ����� ��� ������������ ��������
        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // �������� ������� ��� ��������
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            position += muzzleOffset;

            // 25% ���� �� �������� ��������� �������
            if (Main.rand.NextBool(4)) // 25% �����������
            {
                // ������� �������� ������
                Projectile.NewProjectile(source, position, velocity, ProjectileID.PurificationPowder, damage, knockback, player.whoAmI);
            }
            else
            {
                // ������� ����������� ������
                Projectile.NewProjectile(source, position, velocity, ProjectileID.WoodenArrowFriendly, damage, knockback, player.whoAmI);
            }

            return false; // ��������� ����������� �������
        }
    }
}
