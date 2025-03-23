using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using TremorMod.Content.Biomes.Ice.Items;
using TremorMod.Utilities;

namespace TremorMod.Content.Biomes.Ice.Mobs
{
	public class Frostbyte : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Frostbyte");
			Main.npcFrameCount[NPC.type] = 3;
		}

		public override void SetDefaults()
		{
			NPC.lifeMax = Main.hardMode ? 80 : 20;
			NPC.damage = Main.hardMode ? 26 : 4;
			NPC.defense = Main.hardMode ? 12 : 3;
			NPC.knockBackResist = 0.2f;
			NPC.width = 25;
			NPC.height = 20;
			AnimationType = 3;
			NPC.aiStyle = -1;
			NPC.npcSlots = 1f;
			NPC.value = Item.buyPrice(silver: 10, copper : 5);
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax += 5 * numPlayers;
			NPC.damage += 2 * numPlayers;
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

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrostByteEye>(), 28));

            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),
                ModContent.ItemType<IceBlockB>(), 10, 1, 4));

            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),
                ModContent.ItemType<Icicle>(), 20, 1, 3));
		}

		public override void AI()
		{
			int tileX = (int)(NPC.Bottom.Y / 16f), tileY = (int)(NPC.Bottom.Y / 16f);
			int height = Math.Min(10, tileX);
			float velX = MathHelper.Lerp(5f, 3f, height / 10f), velY = MathHelper.Lerp(-3f, -6f, height / 10f);
			NPC.aiStyle = 1;
			if (NPC.target >= 0 && NPC.target <= 255 && Main.player[NPC.target].Bottom.Y < NPC.Center.Y && NPC.collideX && NPC.velocity.Y != 0)
			{
				NPC.netUpdate = true;
				if (Main.netMode != 2)
				{
					for (int m = 0; m < 2; m++)
					{
						float xPos = NPC.velocity.X > 0 ? NPC.Right.X : NPC.Left.X;
						int dustID = Dust.NewDust(new Vector2(xPos, NPC.Center.Y), 1, 1, 80, xPos < NPC.Center.X ? -4f : 4f, Math.Abs(NPC.velocity.Y) * 0.2f, 100, Color.White, 1.5f);
						Main.dust[dustID].noGravity = true;
					}
				}
				NPC.direction *= -1;
				NPC.velocity.Y = -5f; NPC.velocity.X = 5f * NPC.direction;
			}
			else
			if (NPC.velocity.Y == 0) NPC.TargetClosest(true);
			if (NPC.velocity.Y == 0 && NPC.ai[0] < -20) NPC.ai[0] = -20;
			if (NPC.velocity.X != 0) NPC.spriteDirection = NPC.velocity.X > 0 ? 1 : -1;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Main.hardMode || Main.expertMode)
            {
                target.AddBuff(BuffID.Frostburn, 180);
            }
        }
    }
}