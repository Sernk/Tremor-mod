using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Tiles;

namespace TremorMod.Content.Items.Materials
{
	public class NightmareOre : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 12;
			Item.value = 10000;
			Item.height = 12;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.rare = 11;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<NightmareOreTile>();
		}

		/*public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightmare Ore");
			Tooltip.SetDefault("'Concentrated nightmares.'");
		}*/
	}
}
