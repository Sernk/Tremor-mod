using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Items.Materials;

namespace TremorMod.Content.Items.Weapons.Magic
{
    public class CrystalWhirlwind : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Starfury);
            Item.damage = 85;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 55;
            Item.useTime = 7;
            Item.mana = 20;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 30f;
            Item.knockBack = 3;
            Item.value = 30000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item4;
            Item.autoReuse = true;
        }

        /*public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Hail");
            Tooltip.SetDefault("Causes crystals to fall from the sky\n" +
            "'Made of pure friendship'");
        }*/

        /*public override bool? UseItem(Player player)
        {
            // ���������� ������� �������
            Vector2 position = player.Center + new Vector2(Main.rand.Next(-200, 200), -400); // ��������� ��� �������
            Vector2 velocity = new Vector2(0, Item.shootSpeed); // �������� ����

            // ������� ������
            Projectile.NewProjectile(
                position,
                velocity,
                521, // ��� �������
                Item.damage,
                Item.knockBack,
                player.whoAmI
            );

            return true; // �������� ������������� ��������
        }*/

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrystalStorm, 1);
            recipe.AddIngredient(ModContent.ItemType<NightmareBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<Phantaplasm>(), 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
