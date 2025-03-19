using Terraria;
using Terraria.ModLoader;

namespace TremorMod.Content.Items.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class IceQueenMask : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.rare = 1;
			Item.vanity = true;
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ice Queen Mask");
			//Tooltip.SetDefault("");
		}
	}
}