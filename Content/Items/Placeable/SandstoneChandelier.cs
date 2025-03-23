using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Tiles;

namespace TremorMod.Content.Items.Placeable
{
	public class SandstoneChandelier : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 22;
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
			Item.createTile = ModContent.TileType<SandstoneChandelierTile>();
		}

		/*public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstone Chandelier");
			Tooltip.SetDefault("");
		}*/

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(607, 4);
			recipe.AddIngredient(ItemID.Torch, 4);
			recipe.AddIngredient(ItemID.Chain, 1);
			//recipe.SetResult(this);
			recipe.AddTile(17);
			recipe.Register();
		}
	}
}
