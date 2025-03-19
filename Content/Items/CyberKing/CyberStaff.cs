using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Projectiles.Minions;
using TremorMod.Content.Buffs;

namespace TremorMod.Content.Items.CyberKing
{
    public class CyberStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 62;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 15;
            Item.width = 26;
            Item.height = 28;
            Item.expert = true;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = 1;
            Item.knockBack = 3;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<CyberStaffPro>();
            Item.shootSpeed = 2f;
            Item.buffType = ModContent.BuffType<CyberSawBuff>();
            Item.buffTime = 3600;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true; // ���������� true, ���� ����� ���������� �������������� ������
        }

        public override bool? UseItem(Player player)
        {
            // ������ ��� ��������� ��������������� ���������
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim(false); // ������ ���� ��� �������, �� ���������, ���� ���� �� ����������
            }
            return base.UseItem(player); // ���������� ����������� ���������, �������� ��� bool?
        }


        public override void HoldItem(Player player)
        {
            // ������ ��� �������� �������������
            if (player.altFunctionUse != 2)
            {
                base.HoldItem(player);
            }
        }
    }
}
