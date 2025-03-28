using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TremorMod.Content.Projectiles;

namespace TremorMod.Content.Buffs
{
    public class BallnChainBuff : ModBuff
    {
        int MinionType = -1;
        int MinionID = -1;

        const int Damage = 30;
        const float KB = 1;

        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            // DisplayName.SetDefault("Third True Blade");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (MinionType == -1)
                MinionType = ModContent.ProjectileType<BallChainPro>();
            if (MinionID == -1 || Main.projectile[MinionID].type != MinionType || !Main.projectile[MinionID].active || Main.projectile[MinionID].owner != player.whoAmI)
            {
                IEntitySource source = player.GetSource_Buff(buffIndex);
                MinionID = Projectile.NewProjectile(source, player.Center, Vector2.Zero, MinionType, (int)(Damage * player.GetDamage(DamageClass.Melee).ApplyTo(1f)), KB, player.whoAmI);
            }
            else
            {
                Main.projectile[MinionID].timeLeft = 5;
            }
        }
    }
}