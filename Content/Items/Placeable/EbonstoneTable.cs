using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Tiles;

namespace TremorMod.Content.Items.Placeable
{
	public class EbonstoneTable : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 32;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useStyle = 1;
			Item.rare = 1;
			Item.consumable = true;
			Item.value = 2000;
			Item.createTile = ModContent.TileType<EbonstoneTableTile>();
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ebonstone Table");
			//Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(61, 8);
			recipe.AddIngredient(57, 1);
			//recipe.SetResult(this);
			recipe.AddTile(17);
			recipe.Register();
		}
	}
}