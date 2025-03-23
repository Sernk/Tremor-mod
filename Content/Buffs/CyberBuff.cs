using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TremorMod.Content.Projectiles;

namespace TremorMod.Content.Buffs
{
	public class CyberBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cyber Stray");
			// Description.SetDefault("Cyber stray fights for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            TremorPlayer modPlayer = player.GetModPlayer<TremorPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Mini_Cyber>()] > 0)
            {
                modPlayer.CyberStray = true;
            }
            if (!modPlayer.CyberStray)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}