using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using TremorMod.Content.Items.Materials.OreAndBar;
using Terraria.Localization;

namespace TremorMod.Content.Items.Armor.Invar
{
    [AutoloadEquip(EquipType.Head)]
    internal class InvarHat : ModItem
	{
        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
            // Setting IsTallHat is the only special thing this item does.
            ArmorIDs.Head.Sets.IsTallHat[Item.headSlot] = true;
            // CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            // ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true;
            SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs("increased melee speed");
        }
  
        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 26;
			Item.value = Item.sellPrice(silver: 9);
			Item.rare = 1;
			Item.defense = 2;
		}

		/*protected sealed override void StaticDefaults()
		{
			DisplayName.SetDefault("Invar Hat");
			Tooltip.SetDefault("10% increased melee speed");
		}*/

		public override void UpdateEquip(Player player)
		{
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == ModContent.ItemType<InvarBreastPlate>() && legs.type == ModContent.ItemType<InvarGreaves>(); //|| body.type == mod.ItemType<ReinforcedInvarBreastplate>() && legs.type == mod.ItemType<ReinforcedInvarGreaves>();
        }

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = SetBonusText.Value;
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
        }

		public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<InvarBar>(), 8);
			//recipe.SetResult(this);
			recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
	}
}