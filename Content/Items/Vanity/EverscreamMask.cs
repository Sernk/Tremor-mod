using Terraria;
using Terraria.ModLoader;

namespace TremorMod.Content.Items.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class EverscreamMask : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 32;
			Item.rare = 1;
			Item.vanity = true;
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Everscream Mask");
			//Tooltip.SetDefault("");
		}
	}
}