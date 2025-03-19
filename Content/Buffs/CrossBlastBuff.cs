using Terraria;
using Terraria.ModLoader;

namespace TremorMod.Content.Buffs
{
	public class CrossBlastBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cross Blast");
			//Description.SetDefault("Alchemical projectiles leave explosions in the shape of cross");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
	}
}