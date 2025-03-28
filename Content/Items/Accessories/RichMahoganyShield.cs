using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Items.Accessories
{
	[AutoloadEquip(EquipType.Shield)]
	public class RichMahoganyShield : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.value = 110;
			Item.rare = 0;
			Item.accessory = true;
			Item.defense = 1;
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Rich Mahogany Shield");
			//Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.RichMahogany, 15);
			//recipe.SetResult(this);
			recipe.AddTile(18);
			recipe.Register();
		}
	}
}
