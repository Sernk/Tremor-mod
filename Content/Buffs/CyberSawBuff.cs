using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content.Projectiles.Minions;
using TremorMod.Content;

namespace TremorMod.Content.Buffs
{
    public class CyberSawBuff : ModBuff
    {
        public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cyber Saw");
			//Description.SetDefault("The cyber saw will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            //TremorPlayer modPlayer = player.GetModPlayer<TremorPlayer>();

            /*if (player.ownedProjectileCounts[ModContent.ProjectileType<CyberStaffPro>()] > 0)
            {
                modPlayer.cyberMinion = true; 
            }*/

            /*if (!modPlayer.cyberMinion)
            {
                player.DelBuff(buffIndex);
                buffIndex = -1;
            }*/
            
            {
                player.buffTime[buffIndex] = 18000;
            }
        }

    }
}