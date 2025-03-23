using Terraria;
using Terraria.ModLoader;

namespace TremorMod.Content.Items.Materials
{
	public class Aquamarine : ModItem
	{
		public override void SetDefaults()
		{

			Item.width = 14;
			Item.height = 22;
			Item.maxStack = 9999;
			Item.rare = 11;
			Item.value = Item.buyPrice(0, 10, 0, 0);
		}

		/*public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aquamarine");
			Tooltip.SetDefault("'A jewel from a snake tail");
		}*/

	}
}