using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace TremorMod.Content.Items.SpaceWhaleItems
{
	public class SpaceWhaleTreasureBag : ModItem
	{
		public override void SetDefaults()
		{
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.rare = 9;
			Item.expert = true;
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Treasure Bag");
			//Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override bool CanRightClick()
		{
			return true;
		}

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ItemID.PlatinumCoin, 1, 1, 7)); 
            itemLoot.Add(ItemDropRule.Common(ItemID.GoldCoin, 1, 25, 60)); 

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SpaceWhaleTrophy>(), 10));

			itemLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SpaceWhaleMask>(), 7));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CosmicFuel>(), 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StarLantern>(), 4));

			itemLoot.Add(ItemDropRule.OneFromOptions(1,
				ModContent.ItemType<BlackHoleCannon>(),
				ModContent.ItemType<HornedWarHammer>(),
				ModContent.ItemType<SDL>(),
				ModContent.ItemType<WhaleFlippers>()));

            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<LasCannon>(), 1));    
        }
	}
}
