using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Tiles.Bottled;

namespace TremorMod.Content.Items.Placeable.Bottled
{
	public class BottledSoulOfFlight : ModItem
	{
		public override void SetDefaults()
		{

			Item.width = 20;
			Item.height = 36;
			Item.maxStack = 1;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;

			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.value = 150;
			Item.rare = 5;
			Item.createTile = ModContent.TileType<BottledSoulOfFlightTile>();
			Item.accessory = true;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bottled Soul of Flight");
			/* Tooltip.SetDefault("Grants Shiny Red Baloon effect if worn\n" +
"20% increased jump height if placed"); */
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(575, 5);
			recipe.AddIngredient(ItemID.Bottle, 1);
			recipe.AddTile(114);
			recipe.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.jumpBoost = true;
		}
	}
}
