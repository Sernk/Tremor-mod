using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Items.Weapons.Melee
{
	public class CursedKnife : ModItem
	{
		public override void SetDefaults()
		{

			Item.damage = 22;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 11;
			Item.useAnimation = 21;
			Item.useStyle = 1;
			Item.knockBack = 2;
			Item.value = 2800;
			Item.rare = 4;
			Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cursed Knife");
			// Tooltip.SetDefault("");
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(39, 60);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<RipperKnife>());
			recipe.AddIngredient(ItemID.CursedFlame, 20);
			recipe.AddTile(134);
			recipe.Register();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 74);
			}
		}
	}
}
