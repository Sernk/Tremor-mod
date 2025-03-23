using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Tiles;

namespace TremorMod.Content.Items.Placeable
{
	public class Tent : ModItem
	{
		public override void SetDefaults()
		{

			Item.width = 62;
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
			Item.createTile = ModContent.TileType<TentTile>();
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tent");
			// Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Wood, 4);
			recipe.AddIngredient(ItemID.Cobweb, 30);
			//recipe.SetResult(this);
			recipe.AddTile(86);
			recipe.Register();
		}
	}
}
