using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Projectiles
{
    public class CogLordGunBullet : ModProjectile
    {
        public override void SetDefaults()
        {
           
            Projectile.width = 10; 
            Projectile.height = 10; 
            Projectile.aiStyle = 14;
            Projectile.friendly = false; 
            Projectile.hostile = true; 
            Projectile.timeLeft = 300; 
            Projectile.penetrate = 1; 
            //Projectile.tileCollide = true; 
        }
    }
}