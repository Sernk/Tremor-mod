using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Tiles;

namespace TremorMod.Content.Items.Materials
{
	public class Jellyxir : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 14;
			Item.height = 22;
			Item.maxStack = 9999;
			Item.rare = 5;
			Item.value = Item.buyPrice(0, 0, 3, 0);
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Jellyxir");
			//Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater, 1);
			//recipe.SetResult(this);
			recipe.AddTile(ModContent.TileType<AlchematorTile>());
			recipe.Register();
		}
	}
}
