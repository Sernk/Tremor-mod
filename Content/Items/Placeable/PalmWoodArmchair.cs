using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Tiles;

namespace TremorMod.Content.Items.Placeable
{
	public class PalmWoodArmchair : ModItem
	{
		public override void SetDefaults()
		{

			Item.width = 42;
			Item.height = 32;
			Item.maxStack = 9999;
			Item.value = 100;
			Item.rare = 1;
			Item.createTile = ModContent.TileType<PalmWoodArmchairTile>();
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Palm Wood Armchair");
			// Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.PalmWood, 15);
			recipe.AddIngredient(ItemID.Silk, 6);
			recipe.AddTile(106);
			recipe.Register();
		}
	}
}
