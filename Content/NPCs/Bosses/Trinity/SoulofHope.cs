﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using TremorMod.Content.Items.BossLoot.TikiTotem;
using TremorMod.Content.Items.Vanity;
using TremorMod.Content.Items.Materials;
using TremorMod.Utilities;
using System.Linq;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;
using TremorMod.Content.Tiles;
using TremorMod;
using ReLogic.Content;
using TremorMod.Content.Items.Weapons.Melee;
using TremorMod.Content.Items.Weapons.Throwing;
using TremorMod.Content.Items.Bag;

namespace TremorMod.Content.NPCs.Bosses.Trinity
{
	[AutoloadBossHead]
	public class SoulofHope : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Hope");
		}

		const int NormalShootRate = 60;
		const int NormalLaserDamage = 10;
		const float NormalLaserKB = 1.0f;
		const float NormalLaserSpeed = 15.0f;
		const int NormalAiStyle = 2;

		const int PowerShootRate = 80;
		const int PowerLaserDamage = 24;
		const float PowerLaserKB = 2.0f;
		const float PowerLaserSpeed = 10.0f;
		const int PowerAiStyle = 5;

		const float DistantionToPower = 300f;
		const int BonusDefenseInPower = 5;
		const int BonusDamageInPower = 10;
		const int ShootType = 435;
		const float RotationSpeed = 0.3f;

		bool Power;
		bool OnlyPower;
		int TimeToShoot = NormalShootRate;
		float Rotation;
		Color TextColor = Color.Orange;
		bool StateFlag = true;

		public override void SetDefaults()
		{
			NPC.lifeMax = 75000;
			NPC.damage = 37;
			NPC.defense = 150;
			NPC.knockBackResist = 0.0f;
			NPC.width = 200;
			NPC.height = 194;
			NPC.aiStyle = 5;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath10;
			//npc.boss = true;
			NPC.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float bossLifeScale, float balance)
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.625f * bossLifeScale);
			NPC.damage = (int)(NPC.damage * 0.6f);
		}

		bool RunAway;
		public override void AI()
		{
			NPC.position += NPC.velocity * 1.7f;
			if (Main.rand.Next(500) == 0 && Main.expertMode)
			{
				for (int i = 0; i < 50; i++)
				{
					int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 5);
					Main.dust[dust].scale = 1.5f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 0f;
					Main.dust[dust].velocity *= 0f;
				}
				NPC.position.X = (Main.player[NPC.target].position.X - 250) + Main.rand.Next(500);
				NPC.position.Y = (Main.player[NPC.target].position.Y - 250) + Main.rand.Next(500);
			}

			if (Main.rand.Next(500) == 0 && !Main.expertMode)
			{
				NPC.TargetClosest(true);
				Vector2 vector142 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num1243 = Main.player[NPC.target].Center.X - vector142.X;
				float num1244 = Main.player[NPC.target].Center.Y - vector142.Y;
				float num1245 = (float)Math.Sqrt(num1243 * num1243 + num1244 * num1244);
				if (NPC.ai[1] == 0f)
				{
					if (Main.netMode != 1)
					{
						NPC.localAI[1] += 1f;
						if (NPC.localAI[1] >= 120 + Main.rand.Next(200))
						{
							NPC.localAI[1] = 0f;
							NPC.TargetClosest(true);
							int num1249 = 0;
							int num1250;
							int num1251;
							while (true)
							{
								num1249++;
								num1250 = (int)Main.player[NPC.target].Center.X / 16;
								num1251 = (int)Main.player[NPC.target].Center.Y / 16;
								num1250 += Main.rand.Next(-50, 51);
								num1251 += Main.rand.Next(-50, 51);
								if (!WorldGen.SolidTile(num1250, num1251) && Collision.CanHit(new Vector2(num1250 * 16, num1251 * 16), 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
								{
									break;
								}
								if (num1249 > 100)
								{
									//return;
								}
							}
							NPC.ai[1] = 1f;
							NPC.ai[2] = num1250;
							NPC.ai[3] = num1251;
							NPC.netUpdate = true;
							//return;
						}
					}
				}
				else if (NPC.ai[1] == 1f)
				{
					NPC.alpha += 3;
					if (NPC.alpha >= 255)
					{
						NPC.alpha = 255;
						NPC.position.X = NPC.ai[2] * 16f - NPC.width / 2;
						NPC.position.Y = NPC.ai[3] * 16f - NPC.height / 2;
						NPC.ai[1] = 2f;
						//return;
					}
				}
				else if (NPC.ai[1] == 2f)
				{
					NPC.alpha -= 3;
					if (NPC.alpha <= 0)
					{
						NPC.alpha = 0;
						NPC.ai[1] = 0f;
						//return;
					}
				}
			}

			if (Main.rand.Next(2500) == 0)
			{
				NPC.NewNPC(Entity.GetSource_FromThis(), (int)NPC.position.X - 150, (int)NPC.position.Y - 50, 420);
				NPC.NewNPC(Entity.GetSource_FromThis(), (int)NPC.position.X + 150, (int)NPC.position.Y - 50, 420);
			}

			if (NPC.target != -1 && !RunAway)
				if (!Main.player[NPC.target].active)
				{
					if (Helper.GetNearestAlivePlayer(NPC) == -1)
						RunAway = true;
					else
					{
						if (Main.player[Helper.GetNearestAlivePlayer(NPC)].Distance(NPC.Center) > 2500f)
							RunAway = true;
						else
							NPC.target = Helper.GetNearestAlivePlayer(NPC);
					}
				}
			if (Main.dayTime || RunAway || NPC.localAI[3] == 1)
			{
				NPC.localAI[3] = 1;
				if (Main.npc[(int)NPC.ai[2]].type == ModContent.NPCType<SoulofTrust>() && Main.npc[(int)NPC.ai[2]].active)
					Main.npc[(int)NPC.ai[2]].localAI[3] = 1;
				if (Main.npc[(int)NPC.ai[3]].type == ModContent.NPCType<SoulofTrust>() && Main.npc[(int)NPC.ai[3]].active)
					Main.npc[(int)NPC.ai[3]].localAI[3] = 1;
				NPC.life += 11;
				NPC.aiStyle = 0;
				NPC.rotation = 0;
				NPC.velocity = Helper.VelocityFPTP(NPC.Center, new Vector2(NPC.Center.X, NPC.Center.Y - 4815162342), 30.0f);
				CreateDust();
				return;
			}
			if (StateFlag)
				if (
					!((Main.npc[(int)NPC.ai[2]].type == ModContent.NPCType<SoulofTrust>() && Main.npc[(int)NPC.ai[2]].active)) ||
					!((Main.npc[(int)NPC.ai[3]].type == ModContent.NPCType<SoulofTrust>() && Main.npc[(int)NPC.ai[3]].active))
				   )
				{
					StateFlag = false;
					OnlyPower = true;
				}
			if (OnlyPower)
				SetStage(true);
			else
				SetStage(Main.player[Helper.GetNearestPlayer(NPC)].Distance(NPC.Center) <= DistantionToPower);
			SetRotation();
			CreateDust();
			TimeToShoot--;
			if (TimeToShoot <= 0)
			{
				TimeToShoot = (Power) ? PowerShootRate : NormalShootRate;
				Shoot();
			}
		}

		void CreateDust()
		{
			if (Main.rand.NextBool(3))
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 62);
		}

		void SetRotation()
		{
			if (Power)
			{
				Rotation += RotationSpeed;
				NPC.rotation = Rotation;
			}
			else
			{
				while (Rotation - 36.0f >= 0.0f)
					Rotation -= 36.0f;
				if (Rotation != 0.0f)
				{
					Rotation -= (RotationSpeed * 2.0f);
					Vector2 velocity = Helper.VelocityFPTP(NPC.Center, Helper.RandomPosition(new Vector2(NPC.Center.X - 250, NPC.Center.Y - 250), new Vector2(NPC.Center.X + 250, NPC.Center.Y + 250)), PowerLaserSpeed);
					int i = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, velocity.X, velocity.Y, ShootType, PowerLaserDamage, PowerLaserKB);
					Main.projectile[i].tileCollide = false;
				}
				if (Rotation < 0.0f)
					Rotation = 0.0f;
				NPC.rotation = Rotation;
			}
		}

		void SetStage(bool stage)
		{
			if (Power == stage)
				return;
			Power = stage;
			if (Power)
			{
				NPC.defense += BonusDefenseInPower;
				NPC.damage += BonusDamageInPower;
				NPC.aiStyle = PowerAiStyle;
			}
			else
			{
				NPC.defense -= BonusDefenseInPower;
				NPC.damage -= BonusDamageInPower;
				NPC.aiStyle = NormalAiStyle;
			}
		}

		void Shoot()
		{
			if (Helper.GetNearestPlayer(NPC) != -1)
			{
				NPC.target = Helper.GetNearestPlayer(NPC);
				Vector2 velocity = Helper.VelocityFPTP(NPC.Center, Main.player[NPC.target].Center, (Power) ? PowerLaserSpeed : NormalLaserSpeed);
				int i = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, velocity.X, velocity.Y, ShootType, (Power) ? PowerLaserDamage : NormalLaserDamage, (Power) ? PowerLaserKB : NormalLaserKB);
				Main.projectile[i].tileCollide = false;
				Main.projectile[i].friendly = false;
			}
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			int hitDirection = hit.HitDirection;

			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);
				}
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HopeGore1").Type, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HopeGore2").Type, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HopeGore2").Type, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HopeGore2").Type, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HopeGore2").Type, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HopeGore3").Type, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HopeGore3").Type, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HopeGore3").Type, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HopeGore3").Type, 1f);

				if (!NPC.AnyNPCs(ModContent.NPCType<SoulofTruth>()) && !NPC.AnyNPCs(ModContent.NPCType<SoulofTrust>()))
				{
					Main.NewText("The Trinity has been defeated! (The Hope)", 175, 75, 255);
				}
			}
		}

        public override void OnKill()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<SoulofTruth>()) && !NPC.AnyNPCs(ModContent.NPCType<SoulofTrust>()))
            {
                if (Main.rand.NextFloat() < 0.3f)
                {
                    Item.NewItem(NPC.GetSource_Loot(), NPC.position, ModContent.ItemType<Banhammer>());
                    Item.NewItem(NPC.GetSource_Loot(), NPC.position, ModContent.ItemType<BestNightmare>());
                }

                if (Main.rand.NextFloat() < 0.7f)
                {
                    Item.NewItem(NPC.GetSource_Loot(), NPC.position, ModContent.ItemType<TrinityBag3>());
                }

                TremorSpawnEnemys.TrinityKillCount++;

                bool spawnAngelite = !TremorSpawnEnemys.spawnedAngelite;
                bool spawnCollapsium = !TremorSpawnEnemys.spawnedCollapsium;

                if (spawnAngelite || spawnCollapsium)
                {
                    if (spawnAngelite)
                    {
                        SpawnOre(ModContent.TileType<AngeliteOreTile>(), "This world has been enlightened with Angelite!", new Color(0, 191, 255));
                        TremorSpawnEnemys.spawnedAngelite = true;
                    }

                    if (spawnCollapsium)
                    {
                        SpawnOre(ModContent.TileType<CollapsiumOreTile>(), "This world has been attacked with Collapsium!", new Color(255, 20, 147));
                        TremorSpawnEnemys.spawnedCollapsium = true;
                    }
                }

                TremorSpawnEnemys.downedTrinity = true;

                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData);
                }
            }
        }

        private void SpawnOre(int oreType, string message, Color messageColor)
        {
            Main.NewText(message, messageColor);

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), messageColor);
            }

            for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
            {
                WorldGen.TileRunner(
                    WorldGen.genRand.Next(0, Main.maxTilesX),
                    WorldGen.genRand.Next((int)(Main.maxTilesY * .3f), (int)(Main.maxTilesY * .65f)),
                    WorldGen.genRand.Next(9, 15),
                    WorldGen.genRand.Next(9, 15),
                    oreType,
                    false,
                    0f, 0f, false, true);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<SoulofTruth>()) && !NPC.AnyNPCs(ModContent.NPCType<SoulofTrust>()))
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<TrinityBag3>(), 1));
			}          
        }
    }
}