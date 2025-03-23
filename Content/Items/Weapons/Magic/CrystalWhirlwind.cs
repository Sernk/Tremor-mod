using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Items.Materials;
using Terraria.DataStructures;

namespace TremorMod.Content.Items.Weapons.Magic
{
    public class CrystalWhirlwind : ModItem
    {
        public override void SetDefaults()
        {
            //Item.CloneDefaults(ItemID.Starfury);
            Item.damage = 85;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 55;
            Item.useTime = 7;
            Item.mana = 20;
            Item.useAnimation = 25;
            Item.useStyle = 5;
            Item.shootSpeed = 30f;
            Item.knockBack = 3;
            Item.value = 30000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item4;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.SuperStar; 
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spawnHeight = 400f; 
            Vector2 mousePosition = Main.MouseWorld; 

            Vector2 spawnPosition = new Vector2(mousePosition.X, mousePosition.Y - spawnHeight);

            Vector2 baseDirection = (mousePosition - spawnPosition).SafeNormalize(Vector2.UnitY) * 16f;

            float angleOffset1 = MathHelper.ToRadians(Main.rand.Next(10, 20));
            Vector2 firstVelocity = baseDirection.RotatedBy(mousePosition.X > Main.screenPosition.X + Main.screenWidth / 2 ? -angleOffset1 : angleOffset1);
            Projectile.NewProjectile(source, spawnPosition, firstVelocity, type, damage, knockback, player.whoAmI);

            float angleOffset2 = MathHelper.ToRadians(Main.rand.Next(25, 35)); 
            Vector2 secondVelocity = baseDirection.RotatedBy(mousePosition.X > Main.screenPosition.X + Main.screenWidth / 2 ? -angleOffset2 : angleOffset2);
            Projectile.NewProjectile(source, spawnPosition, secondVelocity, type, damage, knockback, player.whoAmI);

            return false;
        }



        /*public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Hail");
            Tooltip.SetDefault("Causes crystals to fall from the sky\n" +
            "'Made of pure friendship'");
        }*/

        /*public override bool? UseItem(Player player)
        {
            Vector2 position = player.Center + new Vector2(Main.rand.Next(-200, 200), -400); 
            Vector2 velocity = new Vector2(0, Item.shootSpeed); 

            // Создаем снаряд
            Projectile.NewProjectile(
                position,
                velocity,
                521, // Тип снаряда
                Item.damage,
                Item.knockBack,
                player.whoAmI
            );

            return true; 
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
