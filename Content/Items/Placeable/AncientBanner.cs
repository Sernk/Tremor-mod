using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Items.Crystal;
using TremorMod.Content.Items.Materials;
using TremorMod.Content.Tiles;

namespace TremorMod.Content.Items.Placeable
{
	public class AncientBanner : ModItem
	{
		public override void SetDefaults()
		{

			Item.width = 48;
			Item.height = 64;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;

			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.value = 50000;
			Item.rare = 11;
			Item.createTile = ModContent.TileType<AncientBannerTile>();
		}



		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Gloomstone>(), 10);
			recipe.AddIngredient(ModContent.ItemType<UnstableCrystal>(), 2);
			recipe.AddIngredient(ItemID.AncientCloth, 25);
			recipe.AddTile(106);
			recipe.Register();
		}
	}
}
