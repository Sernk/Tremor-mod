using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Projectiles;

namespace TremorMod.Content.Items.Weapons.Melee
{
	public class IchorCleaver : ModItem
	{
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.Arkhalis);

			Item.damage = 36;

		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ichor Cleaver");
			// Tooltip.SetDefault("Inflicts Ichor on enemies");
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<IchorCleaverPro>(), damage, knockback, player.whoAmI);
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(69, 120);
		}
	}
}
