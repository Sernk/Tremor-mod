using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Buffs
{
	public class MaximumCharge : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Maximum Charge");
			//Description.SetDefault("Maximum mana increased by 100");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statManaMax2 += 100;
		}
	}
}
