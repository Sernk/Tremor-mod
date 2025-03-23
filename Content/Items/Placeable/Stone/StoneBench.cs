using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Tiles.Stone;

namespace TremorMod.Content.Items.Placeable.Stone
{
	public class StoneBench : ModItem
	{
		public override void SetDefaults()
		{

			Item.width = 42;
			Item.height = 16;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useStyle = 1;
			Item.rare = 1;
			Item.consumable = true;
			Item.value = 0;
			Item.createTile = ModContent.TileType<StoneBenchTile>();
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stone Bench");
			// Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(3, 8);
			//recipe.SetResult(this);
			recipe.AddTile(17);
			recipe.Register();
		}
	}
}
