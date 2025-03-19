using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Tiles.Crimstone;

namespace TremorMod.Content.Items.Placeable.Crimstone
{
	public class CrimstoneBench : ModItem
	{
		public override void SetDefaults()
		{

			Item.width = 42;
			Item.height = 16;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useStyle = 1;
			Item.rare = 1;
			Item.consumable = true;
			Item.value = 2000;
			Item.createTile = ModContent.TileType<CrimstoneBenchTile>();
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crimstone Bench");
			// Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(836, 8);
			recipe.AddIngredient(1257, 1);
			recipe.AddTile(17);
			recipe.Register();
		}
	}
}
