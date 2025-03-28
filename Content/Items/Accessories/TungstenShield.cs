using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Items.Accessories
{
	[AutoloadEquip(EquipType.Shield)]
	public class TungstenShield : ModItem
	{
		public override void SetDefaults()
		{

			Item.width = 30;
			Item.height = 30;
			Item.value = 110;
			Item.rare = 0;
			Item.accessory = true;
			Item.defense = 3;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tungsten Shield");
			// Tooltip.SetDefault("");
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed -= 0.30f;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TungstenBar, 15);
			//recipe.SetResult(this);
			recipe.AddTile(16);
			recipe.Register();
		}
	}
}
