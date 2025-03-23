using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Tiles.Stone;

namespace TremorMod.Content.Items.Placeable.Stone
{
	public class StoneSink : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 32;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useStyle = 1;
			Item.rare = 1;
			Item.consumable = true;
			Item.value = 2000;
			Item.createTile = ModContent.TileType<StoneSinkTile>();
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stone Sink");
			// Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(3, 6);
			recipe.AddIngredient(206);
			//recipe.SetResult(this);
			recipe.AddTile(17);
			recipe.Register();
		}
	}
}
