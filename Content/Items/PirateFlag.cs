using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Mounts;

namespace TremorMod.Content.Items
{
	public class PirateFlag : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.value = 30000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item79;
			Item.noMelee = true;
			Item.mountType = ModContent.MountType<FlyingDutchman>();
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Pirate Flag");
			//Tooltip.SetDefault("Summons a rideable Flying Dutchman");
		}
	}
}