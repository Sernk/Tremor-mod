using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Buffs;
using TremorMod.Content.Projectiles.Minions;
using TremorMod.Content.Items.Materials;

namespace TremorMod.Content.Items.Weapons.Summon
{
	public class JellyfishStaff : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 15;
			Item.width = 26;
			Item.height = 28;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = 1;
			//Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = Item.buyPrice(0, 3, 0, 0);
			Item.rare = 3;
			Item.UseSound = SoundID.Item44;
			Item.shoot = ModContent.ProjectileType<JellyfishStaffPro>();
			Item.shootSpeed = 2f;
			Item.buffType = ModContent.BuffType<JellyBuff>();
			Item.buffTime = 3600;
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Jellyfish Staff");
			//Tooltip.SetDefault("Summons a jellyfish to fight for you.");
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return player.altFunctionUse != 2;
        }
    }
}