using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Items.Materials;
using TremorMod.Content.Projectiles;

namespace TremorMod.Content.Items.Tools
{
	public class Toothpick : ModItem
	{
		public override void SetDefaults()
		{

			Item.damage = 8;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 20;
			Item.height = 12;
			Item.useTime = 15;
			Item.useAnimation = 25;
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.pick = 65;
			Item.tileBoost++;
			Item.useStyle = 5;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(0, 1, 50, 0);
			Item.rare = 3;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<ToothpickPro>();
			Item.shootSpeed = 26f;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Toothpick");
			// Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CrimtaneBar, 10);
			recipe.AddIngredient(ModContent.ItemType<SharpenedTooth>(), 5);
			//recipe.SetResult(this);
			recipe.AddTile(16);
			recipe.Register();

			Recipe recipe1 = CreateRecipe();
			recipe1.AddIngredient(ItemID.DemoniteBar, 10);
			recipe1.AddIngredient(ModContent.ItemType<SharpenedTooth>(), 5);
			//recipe.SetResult(this);
			recipe1.AddTile(16);
			recipe1.Register();
		}
	}
}
