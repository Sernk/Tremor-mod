using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using TremorMod.Content.Items.BossLoot.TheDarkEmperor;

namespace TremorMod.Content.NPCs.Bosses.TheDarkEmperor
{
	public class DarkServant : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dark Servant");
			Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetDefaults()
		{
			NPC.lifeMax = 1000;
			NPC.damage = 140;
			NPC.defense = 30;
			NPC.knockBackResist = 0.3f;
			NPC.width = 70;
			NPC.height = 46;
			AnimationType = 244;
			NPC.aiStyle = 41;
			NPC.npcSlots = 0.5f;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
            int hitDirection = hit.HitDirection;

            if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 191, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 191, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
				}

				Dust.NewDust(NPC.position, NPC.width, NPC.height, 191, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 191, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 191, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 191, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 99, 1f); 
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 99, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 99, 1f);
			}
		}

        public override void OnKill()
        {
            if (Main.rand.NextBool(6))
            {
                Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ModContent.ItemType<DarkGel>());
            }
        }
    }
}