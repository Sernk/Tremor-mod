using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TremorMod.Utilities
{
    public static partial class TownNPCsUtilities
    {
        public static NPCShop AddWithCustomValue(this NPCShop shop, int itemType, int customValue, params Condition[] conditions)
        {
            var item = new Item(itemType)
            {
                shopCustomPrice = customValue
            };
            return shop.Add(item, conditions);
        }

        public static NPCShop AddWithCustomValue<T>(this NPCShop shop, int customValue, params Condition[] conditions) where T : ModItem
        {
            return shop.AddWithCustomValue(ItemType<T>(), customValue, conditions);
        }
    }

    public class NPCUtil : ModSystem
    {     
        public override void Load()
        {

        }
    }

    public static class PlayerExtensions
    {
        public static bool InventoryHas(this Player player, int itemType)
        {
            foreach (Item item in player.inventory)
            {
                if (item.type == itemType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}