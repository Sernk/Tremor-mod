using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Items.Weapons.Melee
{
	public class OrcishKnife : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Melee;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 28;
			Item.useAnimation = 21;
			Item.useStyle = 1;
			Item.knockBack = 2;
			Item.value = 2200;
			Item.rare = 1;
			Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

		/*public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Orcish Knife");
			Tooltip.SetDefault("");
		}*/

	}
}
