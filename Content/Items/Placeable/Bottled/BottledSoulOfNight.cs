using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Tiles.Bottled;
using TremorMod.Content.Items.Materials;

namespace TremorMod.Content.Items.Placeable.Bottled
{
	public class BottledSoulOfNight : ModItem
	{
		public override void SetDefaults()
		{

			Item.width = 24;
			Item.height = 28;
			Item.maxStack = 1;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;

			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.value = 150;
			Item.rare = 5;
			Item.createTile = ModContent.TileType<BottledSoulOfNightTile>();
			Item.accessory = true;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bottled Soul of Night");
			/* Tooltip.SetDefault("15% increased melee speed if worn\n" +
"8% increased melee speed if placed"); */
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(521, 5);
			recipe.AddIngredient(ItemID.Bottle, 1);
			recipe.AddTile(114);
			recipe.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)

		{
			player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
		}
	}
}
