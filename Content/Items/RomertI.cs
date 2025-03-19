////+----------------------------------------------------------------------------------------+
////|standard AI boss too simple but not everyone may like it and this is a new AI switchable|
////|romert = dokimasíai                                                                     |
////+----------------------------------------------------------------------------------------+
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Terraria.DataStructures;
//using Terraria.Localization;
//using TremorMod.Utilities;

//namespace TremorMod.Content.Items
//{
//    public class RomertI : ModItem
//    {
//        public override void SetDefaults()
//        {
//            Item.width = 54;
//            Item.height = 64;
//            Item.maxStack = 99;
//            Item.value = 10000;
//            Item.rare = ItemRarityID.Red;
//            Item.useStyle = ItemUseStyleID.HoldUp;
//            Item.useTime = 20;
//            Item.useAnimation = 20;
//            Item.consumable = false;
//        }

//        public override void SetStaticDefaults()
//        {
//            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(8, 3));
//            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
//        }

//        public override bool? UseItem(Player player)
//        {
//            if (!Romert.romertActive)
//            {
//                Romert.romertActive = true;
//                Main.NewText("dokimasíai activated!", 75, 0, 130);
//            }
//            else
//            {
//                Romert.romertActive = false;
//                Main.NewText("dokimasíai deactivated!", 50, 205, 50);
//            }

//            return true;
//        }
//    }
//}
