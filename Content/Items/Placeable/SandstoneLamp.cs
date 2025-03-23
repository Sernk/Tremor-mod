using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Tiles;

namespace TremorMod.Content.Items.Placeable
{
	public class SandstoneLamp : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 48;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useStyle = 1;
			Item.rare = 1;
			Item.consumable = true;
			Item.value = 2000;
			Item.createTile = ModContent.TileType<SandstoneLampTile>();
		}

		/*public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstone Lamp");
			Tooltip.SetDefault("");
		}*/

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(607, 3);
			recipe.AddIngredient(ItemID.Torch, 1);
			//recipe.SetResult(this);
			recipe.AddTile(17);
			recipe.Register();
		}
	}
}
