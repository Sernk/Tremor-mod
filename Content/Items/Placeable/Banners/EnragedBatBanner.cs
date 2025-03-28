using TremorMod.Content.Tiles.Banners;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria;
using Terraria.ObjectData;

namespace TremorMod.Content.Items.Placeable.Banners
{
    public class EnragedBatBanner : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<EnemyBanner>(), (int)EnemyBanner.StyleID.EnragedBat);
            Item.width = 10;
            Item.height = 24;
            Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(silver: 10));
            Item.maxStack = 9999;
        }

        public override void SetStaticDefaults()
        {
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
        }
    }
}