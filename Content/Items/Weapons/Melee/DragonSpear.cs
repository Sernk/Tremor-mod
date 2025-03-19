using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Projectiles;
using TremorMod.Content.Items.Materials;

namespace TremorMod.Content.Items.Weapons.Melee  
{
	public class DragonSpear : ModItem // Projectiles not working 
    {
		public override void SetDefaults()
		{
			Item.damage = 278;
            Item.width = 72;
            Item.height = 72;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useTime = 30;
            Item.shoot = ModContent.ProjectileType<DragonSpearPro>();
            Item.shootSpeed = 3f;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.knockBack = 4;
            Item.value = 210000;
            Item.rare = 11;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Dragon Spear");
			//Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DragonCapsule>(), 10);
			recipe.AddIngredient(ModContent.ItemType<EarthFragment>(), 15);
			recipe.AddTile(412);
			//recipe.SetResult(this);
			recipe.Register();
		}
	}
}