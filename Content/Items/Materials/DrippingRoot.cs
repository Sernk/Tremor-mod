using Terraria;
using Terraria.ModLoader;

namespace TremorMod.Content.Items.Materials
{
	public class DrippingRoot : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 9999;
			Item.value = 250;
			Item.rare = 4;
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Dripping Root");
			//Tooltip.SetDefault("");
		}
	}
}