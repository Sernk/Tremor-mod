using Terraria;
using Terraria.ModLoader;

namespace TremorMod.Content.Items.Materials
{
	public class AlienTongue : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 14;
			Item.height = 22;
			Item.maxStack = 9999;
			Item.rare = 11;
			Item.value = Item.buyPrice(0, 10, 0, 0);
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Alien Tongue");
			//Tooltip.SetDefault("'Extraterrestrial abomination'");
		}
	}
}