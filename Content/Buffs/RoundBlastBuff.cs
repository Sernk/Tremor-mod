using Terraria;
using Terraria.ModLoader;

namespace TremorMod.Content.Buffs
{
	public class RoundBlastBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Round Blast");
			//Description.SetDefault("Alchemical projectiles leave explosions in the shape of round");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
	}
}