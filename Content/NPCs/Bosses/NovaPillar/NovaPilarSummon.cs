using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.NPCs.Bosses.NovaPillar
{
    public class NovaPilarSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32; // ������ �������
            Item.height = 32; // ������ �������
            Item.useStyle = ItemUseStyleID.HoldUp; // �������� �������������
            Item.useTime = 20; // ����� �������������
            Item.useAnimation = 20; // �������� �������������
            Item.rare = ItemRarityID.Red; // �������� ��������
            Item.value = Item.sellPrice(0, 5, 0, 0); // ���� ��������
            Item.consumable = true; // ������������ �� ������� (�����������/�������� ����� �������������)
            Item.maxStack = 20; // ������������ ���������� � �����
        }

        public override bool CanUseItem(Player player)
        {
            // ���������, ����� �� ������������ �������
            if (NPC.AnyNPCs(ModContent.NPCType<NovaPillar>()))
            {
                Main.NewText("A Nova Pillar already exists in this world!", Color.Red);
                return false; // ���� NovaPillar ��� ����������, ������������� �������� ���������
            }
            return true;
        }

        public override bool? UseItem(Player player)
        {
            // ���������� ���������� ��� ������ NovaPillar
            Vector2 spawnPos = player.Center + new Vector2(Main.rand.Next(-1600, 1600), -100); // ������ ������� ��� �������

            int spawnNPC = NPC.NewNPC(new Terraria.DataStructures.EntitySource_ItemUse(player, Item),
                (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<NovaPillar>());

            if (spawnNPC < 200)
            {
                Main.NewText("The Nova Pillar has been summoned!", Color.Orange);
                return true; // ������� ������� �����������
            }
            else
            {
                Main.NewText("Failed to summon the Nova Pillar.", Color.Red);
                return false; // ������ ��� �������������
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(3456, 10);
            recipe.AddIngredient(3457, 10);
            recipe.AddIngredient(3458, 10);
            //recipe.AddIngredient(3459, 1);
            //recipe.SetResult(this);
            recipe.AddTile(412);
            recipe.Register();

            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(3456, 10);
            recipe1.AddIngredient(3457, 10);
            recipe1.AddIngredient(3459, 10);
            recipe1.AddTile(412);
            recipe1.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(3456, 10);
            recipe2.AddIngredient(3459, 10);
            recipe2.AddIngredient(3458, 10);
            recipe2.AddTile(412);
            recipe2.Register();

            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient(3459, 10);
            recipe3.AddIngredient(3457, 10);
            recipe3.AddIngredient(3458, 10);
            recipe3.AddTile(412);
            recipe3.Register();
        }
    }
}