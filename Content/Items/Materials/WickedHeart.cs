using Terraria.ModLoader;

namespace TremorMod.Content.Items.Materials
{
	public class WickedHeart : ModItem
	{
		public override void SetDefaults()
		{

			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 9999;
			Item.value = 1000;
			Item.rare = 3;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Wicked Heart");
			// Tooltip.SetDefault("");
		}
	}
}
