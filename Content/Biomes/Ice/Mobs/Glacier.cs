using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TremorMod.Content.Biomes.Ice.Items;
using Terraria.GameContent.ItemDropRules;
using TremorMod.Utilities;

namespace TremorMod.Content.Biomes.Ice.Mobs
{
	public class Glacier : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Glacier");
		}

		public override void SetDefaults()
		{
			NPC.lifeMax = Main.hardMode ? 100 : 20;
			NPC.damage = Main.hardMode ? 32 : 5;
			NPC.defense = Main.hardMode ? 12 : 2;
			NPC.knockBackResist = 0f;
			NPC.width = 68;
			NPC.height = 78;
			//animationType = 156;
			NPC.aiStyle = 2;
			AIType = 180;
			NPC.npcSlots = 15f;
			NPC.noTileCollide = true;
			NPC.noGravity = true;
			NPC.HitSound = SoundID.NPCHit3;
			NPC.noGravity = true;
			NPC.DeathSound = SoundID.NPCDeath5;
			NPC.value = Item.buyPrice(0, 0, 1, 0);
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
		{
			NPC.lifeMax += 10 * numPlayers;
			NPC.damage += 2 * numPlayers;
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Frostex>(), 30));

            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),
                ModContent.ItemType<IceBlockB>(), 10, 1, 4));

            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),
                ModContent.ItemType<Icicle>(), 20, 1, 3));
        }

        public override void AI()
		{
			Dust.NewDust(NPC.position, NPC.width, NPC.height, 80, 0, -1f, 0, default(Color), 0.7f);
		}

		public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
		{
			if (Main.hardMode || Main.expertMode)
			{
				target.AddBuff(BuffID.Frostburn, Main.rand.Next(1, 3) * 60);
			}
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!spawnInfo.Player.InModBiome<IceBiome>())
            {
                if (!NPC.AnyNPCs(NPCID.LunarTowerVortex) && !NPC.AnyNPCs(NPCID.LunarTowerStardust) && !NPC.AnyNPCs(NPCID.LunarTowerNebula) && !NPC.AnyNPCs(NPCID.LunarTowerSolar))
                {
                    return 0f;
                }
            }

            return 15f;
        }


        public override void HitEffect(NPC.HitInfo hitInfo)
        {
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 151, 2.5f * hitInfo.HitDirection, -2.5f, 0, default(Color), 0.7f);
                }
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GlacierGore").Type, 1f);
            }
        }
    }
}