using Terraria;
using Terraria.ModLoader;

namespace TremorMod.Content.Items.Materials
{
	public class RedPuzzleFragment : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 9999;
			Item.value = 50000;
			Item.rare = 3;
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Red Puzzle Fragment");
			//Tooltip.SetDefault("");
		}
	}
}