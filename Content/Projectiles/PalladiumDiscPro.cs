using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Projectiles
{
    public class PalladiumDiscPro : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(106);

            AIType = 106;
        }

        /*public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("PalladiumDiscPro");
		}*/
    }
}