using Terraria;
using Terraria.ModLoader;

namespace TremorMod.Content.Items.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class GrayKnightHelmet : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 22;
			Item.value = 10000;
			Item.rare = 2;
			Item.vanity = true;
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Gray Knight Helmet");
			//Tooltip.SetDefault("Can be colored with gems");
		}
	}
}