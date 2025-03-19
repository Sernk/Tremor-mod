using Terraria.ModLoader;

namespace TremorMod.Content.Items.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class WhiteTurban : ModItem
	{

		public override void SetDefaults()
		{

			Item.width = 30;
			Item.height = 28;

			Item.value = 10000;
			Item.rare = 2;
			Item.vanity = true;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("White Turban");
			// Tooltip.SetDefault("");
		}

	}
}
