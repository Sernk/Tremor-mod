using Terraria;
using Terraria.ModLoader;

namespace TremorMod.Content.Items.Materials
{
	public class ToothofAbraxas : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 9999;
			Item.value = 19000;
			Item.rare = 11;
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Tooth of Abraxas");
			//Tooltip.SetDefault("");
		}
	}
}