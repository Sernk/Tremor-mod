//using Terraria;
//using Terraria.ID;
//using Terraria.DataStructures;
//using Terraria.ModLoader;
//using TremorMod.Content.Items.Materials;

//namespace TremorMod.Content.Items.Accessories.Sparks
//{
//	public class ThrowerSpark : ModItem
//	{
//		public override void SetStaticDefaults()
//		{
//			// DisplayName.SetDefault("Thrower Spark");
//			/* Tooltip.SetDefault("3% increased thrown damage\n" +
//							   "8% increased thrown critical strike chance"); */
//			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
//		}

//		public override void SetDefaults()
//		{
//			Item.width = 22;
//			Item.height = 22;
//			Item.rare = 1;
//			Item.accessory = true;
//			Item.value = Item.buyPrice(silver: 1);
//		}

//		public override void UpdateAccessory(Player player, bool hideVisual)
//		{
//			player.GetDamage(DamageClass.Throwing) += 0.03f;
//			player.GetCritChance(DamageClass.Throwing) += 8;
//		}

//		public override void AddRecipes()
//		{
//			Recipe recipe = CreateRecipe();
//			recipe.AddIngredient(ModContent.ItemType<AdventurerSpark>());
//			//recipe.SetResult(this);
//			recipe.Register();
//		}
//	}
//}