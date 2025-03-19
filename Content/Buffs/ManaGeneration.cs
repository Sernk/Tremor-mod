using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Buffs
{
	public class ManaGeneration : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			//DisplayName.SetDefault("Mana Generation");
			//Description.SetDefault("Lowered mana cost for magic weapons");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.manaCost -= 0.70f;
		}
	}
}
