using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Tiles;

namespace TremorMod.Content.Items.Placeable
{
	public class EbonstoneChest : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 18;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useStyle = 1;
			Item.rare = 1;
			Item.consumable = true;
			Item.value = 2000;
			Item.createTile = ModContent.TileType<EbonstoneChestTile>();
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ebonstone Chest");
			//Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(61, 8);
			recipe.AddIngredient(57, 1);
			recipe.AddIngredient(ItemID.IronBar, 2);
			//recipe.SetResult(this);
			recipe.AddTile(17);
			recipe.Register();

			Recipe recipe1 = CreateRecipe();
			recipe1.AddIngredient(61, 8);
			recipe1.AddIngredient(57, 1);
			recipe1.AddIngredient(ItemID.LeadBar, 2);
			//recipe.SetResult(this);
			recipe1.AddTile(17);
			recipe1.Register();
		}
	}
}