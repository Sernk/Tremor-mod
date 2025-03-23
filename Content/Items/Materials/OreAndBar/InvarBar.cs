using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Items.Materials;

namespace TremorMod.Content.Items.Materials.OreAndBar
{
	public class InvarBar : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 9999;
			Item.value = Item.sellPrice(silver: 1, copper: 25);
			Item.rare = 1;
		}

		/*public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Invar Bar");
			Tooltip.SetDefault("Can be used to make Invar equipment at an anvil");
		}*/

		public override void AddRecipes()
		{
			Recipe recipe1 = Recipe.Create(ModContent.ItemType<InvarBar>(), 3); // ���������� ����������� InvarBar
			recipe1.AddIngredient(ModContent.ItemType<BrokenInvarShield>()); // �������� BrokenInvarShield
			recipe1.AddTile(TileID.Furnaces); // ���������
			recipe1.Register();

			// ������ ������: BrokenInvarSword -> 1 InvarBar
			Recipe recipe2 = Recipe.Create(ModContent.ItemType<InvarBar>(), 2); // ���������� ����������� InvarBar
			recipe2.AddIngredient(ModContent.ItemType<BrokenInvarSword>()); // �������� BrokenInvarSword
			recipe2.AddTile(TileID.Furnaces); 
			recipe2.Register();

			Recipe recipe3 = Recipe.Create(ModContent.ItemType<InvarBar>(), 4);
			recipe3.AddIngredient(ModContent.ItemType<OldInvarPlate>());
            recipe3.AddTile(TileID.Furnaces);
			recipe3.Register();
        }
	}
}
