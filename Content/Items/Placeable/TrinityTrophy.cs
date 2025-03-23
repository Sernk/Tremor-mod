using Terraria;
using Terraria.ModLoader;
using TremorMod.Content.Tiles;

namespace TremorMod.Content.Items.Placeable
{
	public class TrinityTrophy : ModItem
	{
		public override void SetDefaults()
		{

			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.value = 50000;
			Item.createTile = ModContent.TileType<TrinityTrophyTile>();
			Item.placeStyle = 0;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Trinity Trophy");
			// Tooltip.SetDefault("");
		}

	}
}
