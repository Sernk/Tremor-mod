using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Utilities
{
    public class TremorGlobalProjectile : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
        {
            return projectile.type == ProjectileID.ShadowFlame ||
                   projectile.type == ProjectileID.Skull;
        }

        public override void AI(Projectile projectile)
        {
            Player owner = Main.player[projectile.owner];
            MPlayer modPlayer = owner.GetModPlayer<MPlayer>();

            if (projectile.type == ProjectileID.VortexLightning || projectile.type == ProjectileID.VortexVortexLightning)
            {
                if (modPlayer.VortexLightningF)
                {
                    projectile.friendly = true;  
                    projectile.hostile = false;
                }
                else
                {
                    projectile.friendly = false; 
                    projectile.hostile = true;  
                }
            }

            if (projectile.type == ProjectileID.ShadowFlame)
            {
                if (Romert.romertActive) 
                {
                    projectile.friendly = false;
                    projectile.hostile = true;
                }
                else
                {
                    projectile.friendly = true;
                    projectile.hostile = false;
                }
            }

            if (projectile.type == ProjectileID.Skull)
            {
                if (modPlayer.HeatRayF)
                {
                    projectile.friendly = true;
                    projectile.hostile = false;
                }
                else
                {
                    projectile.friendly = false;
                    projectile.hostile = true;
                }
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if (projectile.type == ProjectileID.ShadowFlame)
            {
                target.AddBuff(BuffID.ShadowFlame, 180);
            }
        }
    }
}