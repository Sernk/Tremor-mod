using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Projectiles;

namespace TremorMod.Content.Items.Weapons.Melee
{
	public class LeadSpear : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.width = 14;
			Item.height = 84;
			Item.noUseGraphic = true;
			Item.DamageType = DamageClass.Melee;
			Item.useTime = 30;
			Item.shoot = ModContent.ProjectileType<LeadSpearPro>();
			Item.shootSpeed = 3f;
			Item.useAnimation = 30;
			Item.useStyle = 5;
			Item.knockBack = 4;
			Item.value = 1000;
			Item.rare = 0;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Lead Spear");
			//Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LeadBar, 9);
			recipe.AddTile(16);
			//recipe.SetResult(this);
			recipe.Register();
		}
	}
}