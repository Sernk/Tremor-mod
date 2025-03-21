using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Items.Materials;
using TremorMod.Content.Tiles;

namespace TremorMod.Content.Items.Tools
{
	public class ParaxydePickaxe : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 42;
            Item.DamageType = DamageClass.Melee;
            Item.width = 36;
			Item.height = 36;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.pick = 200;
			Item.useStyle = 1;
			Item.knockBack = 5;
			Item.value = 216000;
			Item.rare = 5;
			Item.scale = 1.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Paraxyde Pickaxe");
			//Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ParaxydeShard>(), 12);
            //recipe.SetResult(this);
            recipe.AddTile(ModContent.TileType<AlchematorTile>());
            recipe.Register();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 27);
			}
		}
	}
}
