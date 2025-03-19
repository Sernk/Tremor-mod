using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Items.Materials.OreAndBar;

namespace TremorMod.Content.Items.Weapons.Melee
{
	public class InvarBroadsword : ModItem
	{
		public override void SetDefaults()
		{

			Item.damage = 11;
            Item.DamageType = DamageClass.Melee;
            Item.width = 36;
			Item.height = 36;
			Item.useTime = 21;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 3;
			Item.value = 100;
			Item.rare = 1;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		/*public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Invar Broadsword");
			Tooltip.SetDefault("");
		}*/

		public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<InvarBar>(), 9); // ��������� ��� �������� � ����������
            //recipe.SetResult(this); // ������������� ��������� �������
            recipe.AddTile(TileID.Anvils); // ��������� ������ ��� �������� ��������
            recipe.Register();
        }
	}
}
