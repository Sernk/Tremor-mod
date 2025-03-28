using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Items.Materials;

namespace TremorMod.Content.Items.Tools
{
	public class SteelAxe : ModItem
	{
		public override void SetDefaults()
		{

			Item.damage = 10;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 17;
			Item.useAnimation = 27;
			Item.axe = 9;
			Item.useStyle = 1;
			Item.knockBack = 4;
			Item.value = 600;
			Item.rare = 1;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Steel Axe");
			// Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<SteelBar>(), 11);
			recipe.AddIngredient(ItemID.Wood, 3);
			recipe.AddTile(16);
			recipe.Register();
		}
	}
}
