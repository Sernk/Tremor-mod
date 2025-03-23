using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Items.Weapons.Melee
{
	public class RipperKnife : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 6;
			Item.DamageType = DamageClass.Melee;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 37;
			Item.useAnimation = 21;
			Item.useStyle = 1;
			Item.knockBack = 2;
			Item.value = 600;
			Item.rare = 1;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.useTurn = true;
        }

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ripper Knife");
			//Tooltip.SetDefault("");
		}
	}
}