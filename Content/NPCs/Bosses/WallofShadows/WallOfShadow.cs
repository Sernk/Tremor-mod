using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using TremorMod.Content.Items.Weapons.Magic;
using TremorMod.Content.Items.Weapons.Ranged;
using TremorMod.Content.Items.Weapons.Melee;
using TremorMod.Content.Items.Vanity;
using TremorMod.Content.Items.Placeable;
using TremorMod.Content.Items.Materials;
using TremorMod.Content.Items.Bag;
using TremorMod.Content.Tiles;
using TremorMod;
using TremorMod.Utilities;

// wallBottom = Wall of Flesh bottom
// wallTop = Wall of flesh Top

namespace TremorMod.Content.NPCs.Bosses.WallofShadows
{
	// todo: REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
	[AutoloadBossHead]
	public class WallOfShadow : ModNPC
	{
		//private const int AnimationRate = 8;
		//private const int FrameCount = 4;

		private const int ShootRate = 70;
		private const int ShootDamage = 15;
		//private int _shootType;
		private const float ShootKnockback = 1;
		private float _shootSpeed = 20;

		private const float DistortPercent = 0.15f; // 1 == 100%

		//private const int MinionsId = 61;
		//private const int MinionsCount = 4; 

		//private const int StateTimeFlying = 600;
		//private const int StateTimeMinions = 120;

		//private const int FlyingAi = 2;
		//private const int MinionsAi = 0;

		//private const float MinionsStateXDeaccelerationPower = 0.05f;
		//private const float MinionsStateYMaxSpeed = 2.80f;
		//private const float MinionsSteteYSpeedStep = 0.02f;

		//private const int States = 2;

		//private int _timeToAnimation = AnimationRate;
		//private int _frame = 0;
		private const bool Shoots = true;

		private int _timeToShoot = ShootRate;
        //private int _state = 0;
        //private int _timeToState = StateTimeFlying;
        //private bool _runAway = false;
        private int wallBottom = -1;
        private int wallTop = -1;
        private static int wallIndex = -1;
        private int wallFrameCounter = 0;
        public int phase = 1;
        private int frameCounter = 0;
        private int frame = 0;

        private int MagicBoltCooldown
		{
			get { return (int)NPC.ai[2]; }
			set { NPC.ai[2] = value; }
		}

		private int LaserCooldown
		{
			get { return (int)NPC.ai[0]; }
			set { NPC.ai[0] = value; }
		}

		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Wall of Shadows");
            Main.npcFrameCount[NPC.type] = 4;
        }

		public override void SetDefaults()
		{
			NPC.width = 100;
			NPC.height = 100;
			NPC.value = Item.buyPrice(0, 17, 0, 0);
			NPC.damage = 64;
			NPC.defense = 57;
			NPC.lifeMax = 36000;
			NPC.knockBackResist = 0f;
			NPC.npcSlots = 10;
			NPC.boss = true;
			NPC.scale = 1.2f;
			NPC.noGravity = true;
			NPC.lavaImmune = true;
			NPC.behindTiles = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit8;
			NPC.DeathSound = SoundID.NPCDeath10;
			Music = MusicID.Boss4;
		}

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float bossLifeScale, float balance)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.625f * bossLifeScale);
            NPC.damage = (int)(NPC.damage * 0.6f);
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.ShadowFlame, 240);
			}
		}

		private void ShootBall()
		{
			MagicBoltCooldown--;
			if (MagicBoltCooldown <= 60 && MagicBoltCooldown % ((Main.expertMode) ? 12 : 20) == 0 && Main.netMode != 1)
			{
				var targetPos = NPC.HasPlayerTarget ? Main.player[NPC.target].Center : Main.npc[NPC.target].Center;
				var shootPos = (NPC.Top + new Vector2(0, 60)).RotatedBy(NPC.rotation, NPC.Center);
				float inaccuracy = 3f * (NPC.life / NPC.lifeMax);
				var shootVel = targetPos - shootPos + new Vector2(Main.rand.NextFloat(-inaccuracy, inaccuracy), Main.rand.NextFloat(-inaccuracy, inaccuracy));
				shootVel.Normalize();
				shootVel *= 10f;
				Projectile.NewProjectile(NPC.GetSource_FromThis(), shootPos, shootVel, 290, NPC.damage, 5f, Main.myPlayer);
			}
			if (MagicBoltCooldown <= 0)
			{
				MagicBoltCooldown = 100 + (int)(60 * (float)NPC.life / NPC.lifeMax);
			}
		}

		private void Shoot()
		{
			if (--_timeToShoot > 0) //если таймер меньше нуля, то вырубаем автоматом
				return;
			_timeToShoot = (int)Helper.DistortFloat(ShootRate, DistortPercent); //устанавливаем частоту выстрела
			for (int i = 0; i < ((Main.expertMode) ? 3 : 1); i++) //в цикле указываем кол-во перьев при выстреле
			{
				if (Main.expertMode)
				{
					_shootSpeed = 25;
				}
				Vector2 velocity = Helper.VelocityToPoint(NPC.Center, Helper.RandomPointInArea(new Vector2(Main.player[NPC.target].Center.X - 10, Main.player[NPC.target].Center.Y - 10), new Vector2(Main.player[NPC.target].Center.X + 20, Main.player[NPC.target].Center.Y + 20)), _shootSpeed); //здесь устанавливаем позиции (здесь от перса в плеера)
				int proj = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, velocity.X, velocity.Y, 83, (int)Helper.DistortFloat(ShootDamage, DistortPercent), Helper.DistortFloat(ShootKnockback, DistortPercent)); //подтверждаем все выше действие: от перса к мобу, от моба к персу (второе выстрел)
				Main.projectile[proj].Center = NPC.Center;
			}
		}

		private void ShootSuper()
		{
			LaserCooldown--;
			if (LaserCooldown <= 60 && LaserCooldown % ((Main.expertMode) ? 4 : 7) == 0 && Main.netMode != 1)
			{
				Vector2 velocity = Helper.VelocityToPoint(NPC.Center, Helper.RandomPointInArea(new Vector2(Main.player[NPC.target].Center.X - 100, Main.player[NPC.target].Center.Y - 100), new Vector2(Main.player[NPC.target].Center.X + 20, Main.player[NPC.target].Center.Y + 20)), ((Main.expertMode) ? 20 : 15)); //здесь устанавливаем позиции (здесь от перса в плеера)
				int proj = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, velocity.X, velocity.Y, 83, (int)Helper.DistortFloat(ShootDamage, DistortPercent), Helper.DistortFloat(ShootKnockback, DistortPercent)); //подтверждаем все выше действие: от перса к мобу, от моба к персу (второе выстрел)
				Main.projectile[proj].Center = NPC.Center;
			}
			if (LaserCooldown <= 0)
			{
				LaserCooldown = 100 + (int)(600 * (float)NPC.life / NPC.lifeMax);
			}
		}

		public override bool PreAI()
		{
			NPC.TargetClosest(false);
			if (NPC.target != -1)
			{
				Player player = Main.player[NPC.target];
				NPC.position.Y = player.position.Y;
				player.AddBuff(22, 1);
				if (player.dead)
				{
					NPC.TargetClosest(false);
					NPC.velocity.Y = NPC.velocity.Y + 1f;
					if (NPC.position.Y > Main.worldSurface * 16.0)
					{
						NPC.velocity.Y = NPC.velocity.Y + 1f;
					}
					if (NPC.position.Y > Main.rockLayer * 16.0)
					{
						for (int num957 = 0; num957 < 200; num957++)
						{
							if (Main.npc[num957].aiStyle == NPC.aiStyle)
							{
								Main.npc[num957].active = false;
							}
						}
					}
				}
			}

			ShootBall();
			ShootSuper();
            HandleAnimation();
            if (NPC.life < NPC.lifeMax * 0.5f)
			{
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[NPC.type]] = ModContent.Request<Texture2D>("TremorMod/Content/NPCs/Bosses/WallofShadows/WallOfShadow_Head_Boss1");
				Shoot();

                if ((int)(Main.time % 360) == 0) 
                {
                    int shadowSteedCount = NPC.CountNPCS(ModContent.NPCType<ShadowSteed>());
                    if (shadowSteedCount < 1)
                    {
                        int index = NPC.NewNPC(NPC.GetSource_FromThis(),
                            (int)(NPC.position.X + (NPC.width / 2)),
                            (int)(NPC.position.Y + (NPC.height / 2) + 20),
                            ModContent.NPCType<ShadowSteed>());
                        Main.npc[index].velocity.X = NPC.direction * 6;
                    }
                }

                if (NPC.localAI[0] == 0.0)
				{
					NPC.localAI[0] = 1f;
					wallBottom = -1;
					wallTop = -1;
				}

				NPC.ai[1]++;
				if (NPC.ai[2] == 0)
				{
					if (NPC.life < NPC.lifeMax * 0.5F)
						NPC.ai[1]++;
					if (NPC.life < NPC.lifeMax * 0.2F)
						NPC.ai[1]++;
					if (NPC.ai[1] > 2700.0)
						NPC.ai[2] = 1f;
				}
				if (NPC.ai[2] > 0 && NPC.ai[1] > 60)
				{
					int spawnCooldown = 3;
					if (NPC.life < NPC.lifeMax * 0.3)
						++spawnCooldown;
					NPC.ai[2]++;
					NPC.ai[1] = 0;
					if (NPC.ai[2] > spawnCooldown)
						NPC.ai[2] = 0;

					if (Main.netMode != 1)
					{
						// Spawn... a Shadow Steed?
						//int index = NPC.NewNPC((int)(npc.position.X + (npc.width / 2)), (int)(npc.position.Y + (npc.height / 2) + 20.0), mod.NPCType("ShadowSteed"), 1, 0.0f, 0.0f, 0.0f, 0.0f, (int)byte.MaxValue);
						//int index2 = NPC.NewNPC((int)(npc.position.X + (npc.width / 2)), (int)(npc.position.Y + (npc.height / 2) + 20.0), mod.NPCType("ShadowSteed"), 1, 0.0f, 0.0f, 0.0f, 0.0f, (int)byte.MaxValue);
						//NPC.NewNPC((int)(npc.position.X + (npc.width / 2)), (int)(npc.position.Y + (npc.height / 2) + 10.0), mod.NPCType("ShadowSteed"), 1, 0.0f, 0.0f, 0.0f, 0.0f, (int)byte.MaxValue);
						//NPC.NewNPC((int)(npc.position.X + (npc.width / 2)), (int)(npc.position.Y + (npc.height / 2) + 30.0), mod.NPCType("ShadowSteed"), 1, 0.0f, 0.0f, 0.0f, 0.0f, (int)byte.MaxValue);
						//Main.npc[index].velocity.X = npc.direction * 6;
						//Main.npc[index2].velocity.X = npc.direction * 6;
					}
				}

                WallOfShadow.wallIndex = NPC.whoAmI;
                int npcTileX = (int)(NPC.position.X / 16);
				int npcRightXTile = (int)((NPC.position.X + NPC.width) / 16);
				int npcCenterYTile = (int)((NPC.position.Y + (NPC.height / 2)) / 16);
				int solidTiles = 0;
				int npcBottom = npcCenterYTile + 7;
				while (solidTiles < 15 && npcBottom > Main.maxTilesY - 200)
				{
					++npcBottom;
					for (int i = npcTileX; i <= npcRightXTile; ++i)
					{
						try
						{
							if (!WorldGen.SolidTile(i, npcBottom))
							{
								if (Main.tile[i, npcBottom].LiquidAmount <= 0)
									continue;
							}
							++solidTiles;
						}
						catch
						{
							solidTiles += 15;
						}
					}
				}
				int num5 = npcBottom + 4;
				if (wallBottom == -1)
					wallBottom = num5 * 16;
				else if (wallBottom > num5 * 16)
				{
					--wallBottom;
					if (wallBottom < num5 * 16)
						wallBottom = num5 * 16;
				}
				else if (wallBottom < num5 * 16)
				{
					++wallBottom;
					if (wallBottom > num5 * 16)
						wallBottom = num5 * 16;
				}

				int num6 = 0;
				int j2 = npcCenterYTile - 7;
				while (num6 < 15 && j2 < Main.maxTilesY - 10)
				{
					--j2;
					for (int i = npcTileX; i <= npcRightXTile; ++i)
					{
						try
						{
							if (!WorldGen.SolidTile(i, j2))
							{
								if (Main.tile[i, j2].LiquidAmount <= 0)
									continue;
							}
							++num6;
						}
						catch
						{
							num6 += 15;
						}
					}
				}
				int num7 = j2 - 4;
				if (wallTop == -1)
					wallTop = num7 * 16;
				else if (wallTop > num7 * 16)
				{
					--wallTop;
					if (wallTop < num7 * 16)
						wallTop = num7 * 16;
				}
				else if (wallTop < num7 * 16)
				{
					++wallTop;
					if (wallTop > num7 * 16)
						wallTop = num7 * 16;
				}

				#region Movement

				float num8 = ((wallBottom + wallTop) / 2 - NPC.height / 2);
				if (NPC.position.Y > num8 + 1.0)
					NPC.velocity.Y = -1f;
				else if (NPC.position.Y < num8 - 1.0)
					NPC.velocity.Y = 1f;
				NPC.velocity.Y = 0.0f;
				NPC.position.Y = num8;
				float speed = 1.5f;
				if (NPC.life < NPC.lifeMax * 0.75)
					speed += 0.25f;
				if (NPC.life < NPC.lifeMax * 0.5)
					speed += 0.4f;
				if (NPC.life < NPC.lifeMax * 0.25)
					speed += 0.5f;
				if (NPC.life < NPC.lifeMax * 0.1)
					speed += 0.6f;
				if (NPC.life < NPC.lifeMax * 0.66 && Main.expertMode)
					speed += 0.3f;
				if (NPC.life < NPC.lifeMax * 0.33 && Main.expertMode)
					speed += 0.3f;
				if (NPC.life < NPC.lifeMax * 0.05 && Main.expertMode)
					speed += 0.6f;
				if (NPC.life < NPC.lifeMax * 0.035 && Main.expertMode)
					speed += 0.6f;
				if (NPC.life < NPC.lifeMax * 0.025 && Main.expertMode)
					speed += 0.6f;
				if (Main.expertMode)
					speed = speed * 1.35f + 0.35f;
				if (NPC.velocity.X == 0.0)
				{
					NPC.TargetClosest(true);
					NPC.velocity.X = NPC.direction;
				}
				if (NPC.velocity.X < 0.0)
				{
					NPC.velocity.X = -speed;
					NPC.direction = -1;
				}
				else
				{
					NPC.velocity.X = speed;
					NPC.direction = 1;
				}

				#endregion

				#region Mouth Rotation

				NPC.spriteDirection = NPC.direction;
				Vector2 vector2 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
				float num10 = Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2) - vector2.X;
				float num11 = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - vector2.Y;
				float num12 = (float)Math.Sqrt(num10 * num10 + num11 * num11);
				float num13 = num10 * num12;
				float num14 = num11 * num12;
				NPC.rotation = NPC.direction <= 0
					? (Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2) >= NPC.position.X + (NPC.width / 2)
						? 0
						: (float)Math.Atan2(num14, num13) + 3.14f)
					: (Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2) <= NPC.position.X + (NPC.width / 2)
						? 0
						: (float)Math.Atan2(-num14, -num13) + 3.14f);

				#endregion

				for (int i = 0; i < 255; ++i)
				{
					if (Main.player[i].active && !Main.player[i].dead)
					{
						if (Main.player[i].Center.X > NPC.Center.X && Main.player[i].direction == -1 && NPC.direction == -1 &&
							Vector2.Distance(Main.player[i].Center, NPC.Center) <= 480f)
						{
							Main.player[i].AddBuff(BuffID.TheTongue, 600);
						}
					}
				}

				for (int i = 0; i < 255; ++i)
				{
					if (Main.player[i].active && !Main.player[i].dead)
					{
						if (Main.player[i].Center.X < NPC.Center.X && Main.player[i].direction == 1 && NPC.direction == 1 &&
							Vector2.Distance(Main.player[i].Center, NPC.Center) <= 480f)
						{
							Main.player[i].AddBuff(BuffID.TheTongue, 600);
						}
					}
				}

				if (NPC.localAI[0] != 1.0 || Main.netMode == 1)
					return false;
				NPC.localAI[0] = 2f;
			}

			if (NPC.life > NPC.lifeMax * 0.5f)
			{
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[NPC.type]] = ModContent.Request<Texture2D>("TremorMod/Content/NPCs/Bosses/WallofShadows/WallOfShadow_Head_Boss");
				Shoot();
				if (NPC.position.X < 160 || NPC.position.X > (Main.maxTilesX - 10) * 16)
					NPC.active = false;
				if (NPC.localAI[0] == 0.0)
				{
					NPC.localAI[0] = 1f;
					wallBottom = -1;
					wallTop = -1;
				}

				NPC.ai[1]++;
				if (NPC.ai[2] == 0)
				{
					if (NPC.life < NPC.lifeMax * 0.5F)
						NPC.ai[1]++;
					if (NPC.life < NPC.lifeMax * 0.2F)
						NPC.ai[1]++;
					if (NPC.ai[1] > 2700.0)
						NPC.ai[2] = 1f;
				}
				if (NPC.ai[2] > 0 && NPC.ai[1] > 60)
				{
					int spawnCooldown = 3;
					if (NPC.life < NPC.lifeMax * 0.3)
						++spawnCooldown;
					NPC.ai[2]++;
					NPC.ai[1] = 0;
					if (NPC.ai[2] > spawnCooldown)
						NPC.ai[2] = 0;

					if (Main.netMode != 1)
					{

					}
				}

				wallIndex = NPC.whoAmI;
				int npcTileX = (int)(NPC.position.X / 16);
				int npcRightXTile = (int)((NPC.position.X + NPC.width) / 16);
				int npcCenterYTile = (int)((NPC.position.Y + (NPC.height / 2)) / 16);
				int solidTiles = 0;
				int npcBottom = npcCenterYTile + 7;
				while (solidTiles < 15 && npcBottom > Main.maxTilesY - 200)
				{
					++npcBottom;
					for (int i = npcTileX; i <= npcRightXTile; ++i)
					{
						try
						{
							if (!WorldGen.SolidTile(i, npcBottom))
							{
								if (Main.tile[i, npcBottom].LiquidAmount <= 0)
									continue;
							}
							++solidTiles;
						}
						catch
						{
							solidTiles += 15;
						}
					}
				}
				int num5 = npcBottom + 4;
				if (wallBottom == -1)
					wallBottom = num5 * 16;
				else if (wallBottom > num5 * 16)
				{
					--wallBottom;
					if (wallBottom < num5 * 16)
						wallBottom = num5 * 16;
				}
				else if (wallBottom < num5 * 16)
				{
					++wallBottom;
					if (wallBottom > num5 * 16)
						wallBottom = num5 * 16;
				}

				int num6 = 0;
				int j2 = npcCenterYTile - 7;
				while (num6 < 15 && j2 < Main.maxTilesY - 10)
				{
					--j2;
					for (int i = npcTileX; i <= npcRightXTile; ++i)
					{
						try
						{
							if (!WorldGen.SolidTile(i, j2))
							{
								if (Main.tile[i, j2].LiquidAmount <= 0)
									continue;
							}
							++num6;
						}
						catch
						{
							num6 += 15;
						}
					}
				}
				int num7 = j2 - 4;
				if (wallTop == -1)
					wallTop = num7 * 16;
				else if (wallTop > num7 * 16)
				{
					--wallTop;
					if (wallTop < num7 * 16)
						wallTop = num7 * 16;
				}
				else if (wallTop < num7 * 16)
				{
					++wallTop;
					if (wallTop > num7 * 16)
						wallTop = num7 * 16;
				}

				#region Movement

				float num8 = ((wallBottom + wallTop) / 2 - NPC.height / 2);
				if (NPC.position.Y > num8 + 1.0)
					NPC.velocity.Y = -1f;
				else if (NPC.position.Y < num8 - 1.0)
					NPC.velocity.Y = 1f;
				NPC.velocity.Y = 0.0f;
				NPC.position.Y = num8;
				float speed = 1.5f;
				if (NPC.life < NPC.lifeMax * 0.75)
					speed += 0.25f;
				if (NPC.life < NPC.lifeMax * 0.5)
					speed += 0.4f;
				if (NPC.life < NPC.lifeMax * 0.25)
					speed += 0.5f;
				if (NPC.life < NPC.lifeMax * 0.1)
					speed += 0.6f;
				if (NPC.life < NPC.lifeMax * 0.66 && Main.expertMode)
					speed += 0.3f;
				if (NPC.life < NPC.lifeMax * 0.33 && Main.expertMode)
					speed += 0.3f;
				if (NPC.life < NPC.lifeMax * 0.05 && Main.expertMode)
					speed += 0.6f;
				if (NPC.life < NPC.lifeMax * 0.035 && Main.expertMode)
					speed += 0.6f;
				if (NPC.life < NPC.lifeMax * 0.025 && Main.expertMode)
					speed += 0.6f;
				if (Main.expertMode)
					speed = speed * 1.35f + 0.35f;
				if (NPC.velocity.X == 0.0)
				{
					NPC.TargetClosest(true);
					NPC.velocity.X = NPC.direction;
				}
				if (NPC.velocity.X < 0.0)
				{
					NPC.velocity.X = -speed;
					NPC.direction = -1;
				}
				else
				{
					NPC.velocity.X = speed;
					NPC.direction = 1;
				}

				#endregion

				#region Mouth Rotation

				NPC.spriteDirection = NPC.direction;
				Vector2 vector2 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
				float num10 = Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2) - vector2.X;
				float num11 = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - vector2.Y;
				float num12 = (float)Math.Sqrt(num10 * num10 + num11 * num11);
				float num13 = num10 * num12;
				float num14 = num11 * num12;
				NPC.rotation = NPC.direction <= 0
					? (Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2) >= NPC.position.X + (NPC.width / 2)
						? 0
						: (float)Math.Atan2(num14, num13) + 3.14f)
					: (Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2) <= NPC.position.X + (NPC.width / 2)
						? 0
						: (float)Math.Atan2(-num14, -num13) + 3.14f);

				#endregion

				for (int i = 0; i < 255; ++i)
				{
					if (Main.player[i].active && !Main.player[i].dead)
					{
						if (Main.player[i].Center.X > NPC.Center.X && Main.player[i].direction == -1 && NPC.direction == -1 &&
							Vector2.Distance(Main.player[i].Center, NPC.Center) <= 480f)
						{
							Main.player[i].AddBuff(BuffID.TheTongue, 600);
						}
					}
				}

				for (int i = 0; i < 255; ++i)
				{
					if (Main.player[i].active && !Main.player[i].dead)
					{
						if (Main.player[i].Center.X < NPC.Center.X && Main.player[i].direction == 1 && NPC.direction == 1 &&
							Vector2.Distance(Main.player[i].Center, NPC.Center) <= 480f)
						{
							Main.player[i].AddBuff(BuffID.TheTongue, 600);
						}
					}
				}



				//if (Main.expertMode && Main.netMode != 1)
				//{
				int num15 = (int)(1.0 + NPC.life / NPC.lifeMax * 10.0);
				int num16 = num15 * num15;
				if (num16 < 400)
					num16 = (num16 * 19 + 400) / 20;
				if (num16 < 60)
					num16 = (num16 * 3 + 60) / 4;
				if (num16 < 20)
					num16 = (num16 + 20) / 2;
				int maxValue1 = (int)(num16 * 0.7);
				if (Main.rand.Next(maxValue1) == 0)
				{
					int index1 = 0;
					float[] numArray = new float[10];
					for (int index2 = 0; index2 < 200; ++index2)
					{
						if (index1 < 10 && Main.npc[index2].active && Main.npc[index2].type == Mod.Find<ModNPC>("ShadowHand").Type)
						{
							numArray[index1] = Main.npc[index2].ai[0];
							++index1;
						}
					}
					int maxValue2 = 1 + index1 * 2;
					if (index1 < 10 && Main.rand.Next(maxValue2) <= 1)
					{
						int num17 = -1;
						for (int index2 = 0; index2 < 1000; ++index2)
						{
							int num18 = Main.rand.Next(20);
							float num19 = (float)(num18 * 0.100000001490116 - 0.0500000007450581);
							bool flag = true;
							for (int index3 = 0; index3 < index1; ++index3)
							{
								if (num19 == (double)numArray[index3])
								{
									flag = false;
									break;
								}
							}
							if (flag)
							{
								num17 = num18;
								break;
							}
						}
						if (num17 >= 0)
						{
							int index2 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)num8, Mod.Find<ModNPC>("ShadowHand").Type, NPC.whoAmI, 0.0f, 0.0f,
								0.0f, 0.0f, 255);
							Main.npc[index2].ai[0] = (num17 * 0.100000001490116F - 0.0500000007450581F);
						}
					}
				}
				//}
				if (NPC.localAI[0] != 1.0 || Main.netMode == 1)
					return false;
				NPC.localAI[0] = 2f;

				float num20 = ((((wallBottom + wallTop) / 2) + wallBottom) / 2.0F);
				for (int index1 = 0; index1 < 11; ++index1)
				{
					int index2 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)num20, Mod.Find<ModNPC>("ShadowHand").Type, NPC.whoAmI, 0.0f, 0.0f,
						0.0f, 0.0f, 255);
					Main.npc[index2].ai[0] = (index1 * 0.100000001490116F - 0.0500000007450581F);
				}

			}

			return false;
		}

		public override void FindFrame(int frameHeight)
		{
			int frameWidth = 96; // I'm just hardcoding this, since this is the frame width of one frame along the X axis.
			NPC.spriteDirection = NPC.direction;

			// Now if you want to animate, you can do:
			NPC.frameCounter++;
			if (NPC.frameCounter >= 12)
			{

				//if (NPC.life > NPC.lifeMax * 0.5f)
				//{
				//	NPC.frame.Y += 98;
				//	if (NPC.frame.Y >= 196)
				//	{
				//		NPC.frame.Y = 0;
				//		NPC.frame.X = 0;
				//	}
				//}

				//if (NPC.life < NPC.lifeMax * 0.5f)
				//{
				//	NPC.frame.Y += 98;
				//	if (NPC.frame.Y >= 196)
				//	{
				//		NPC.frame.Y = 0;
				//		NPC.frame.X = 96;
				//	}

				//}

				//NPC.frameCounter = 0;
			}

			NPC.frame.Width = frameWidth;
            NPC.frame.Y = frame * frameHeight;
        }

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				for (int i = 1; i <= 27; i++)
				{
					float x =
						  i <= 2 ? 1f
						: i <= 8 ? 2f
						: i <= 18 ? 3f
						: i <= 26 ? 4f
						: 5f;
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 99, x);
                }

				for (int i = 1; i <= 13; i++)
				{
					int x = i <= 2 ? 1 : 2;
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WallOfShadowGore1").Type, x);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WallOfShadowGore2").Type, x);
                }
			}
		}

        public override void OnKill()
        {
            TremorSpawnEnemys.downedWallOfShadow = true;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                SpawnLootBox();
            }

            IEntitySource source = NPC.GetSource_Death();
        }

        private void SpawnLootBox()
        {

            int width = 20;
            int height = 20;
            int centerX = (int)(NPC.Center.X / 16);
            int centerY = (int)(NPC.Center.Y / 16);

            int halfWidth = width / 2;
            int halfHeight = height / 2;

            for (int x = -halfWidth; x <= halfWidth; x++)
            {
                for (int y = -halfHeight; y <= halfHeight; y++)
                {
                    int tileX = centerX + x;
                    int tileY = centerY + y;

                    Tile tile = Main.tile[tileX, tileY];
                    if (tile != null)
                    {
                        tile.LiquidAmount = 0;
                        WorldGen.SquareTileFrame(tileX, tileY);
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendTileSquare(-1, tileX, tileY, 1);
                        }
                    }

                    if (x == -halfWidth || x == halfWidth || y == -halfHeight || y == halfHeight)
                    {
                        WorldGen.PlaceTile(tileX, tileY, TileID.Demonite);
                    }
                    else
                    {
                        WorldGen.KillTile(tileX, tileY);
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
            Texture2D shadowChain = ModContent.Request<Texture2D>("TremorMod/Content/NPCs/Bosses/WallofShadows/WallOfShadowChain").Value;
            Texture2D shadowWall = ModContent.Request<Texture2D>("TremorMod/Content/NPCs/Bosses/WallofShadows/WallOfShadow_Wall").Value;
            for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active && Main.player[i].tongued && !Main.player[i].dead)
				{
					float num = NPC.position.X + (NPC.width / 2);
					float num2 = NPC.position.Y + (NPC.height / 2);
					Vector2 vector = new Vector2(Main.player[i].position.X + Main.player[i].width * 0.5f, Main.player[i].position.Y + Main.player[i].height * 0.5f);
					float num3 = num - vector.X;
					float num4 = num2 - vector.Y;
					float rotation = (float)Math.Atan2(num4, num3) - 1.57f;
					bool flag = true;
					while (flag)
					{
						float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
						if (num5 < 40f)
						{
							flag = false;
						}
						else
						{
							num5 = shadowChain.Height / num5;
							num3 *= num5;
							num4 *= num5;
							vector.X += num3;
							vector.Y += num4;
							num3 = num - vector.X;
							num4 = num2 - vector.Y;
							Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f));
							spriteBatch.Draw(shadowChain, new Vector2(vector.X - Main.screenPosition.X, vector.Y - Main.screenPosition.Y), new Rectangle(0, 0, shadowChain.Width, shadowChain.Height), color, rotation, new Vector2(shadowChain.Width * 0.5f, shadowChain.Height * 0.5f), 1f, SpriteEffects.None, 0f);
						}
					}
				}
			}
			for (int j = 0; j < 200; j++)
			{
				if (Main.npc[j].active && Main.npc[j].type == Mod.Find<ModNPC>("ShadowHand").Type)
				{
					float num6 = NPC.position.X + (NPC.width / 2);
					float num7 = NPC.position.Y;
					float num8 = (wallBottom - wallTop);
					bool flag2 = Main.npc[j].frameCounter > 7.0;
					num7 = wallTop + num8 * Main.npc[j].ai[0];
					Vector2 vector2 = new Vector2(Main.npc[j].position.X + (Main.npc[j].width / 2), Main.npc[j].position.Y + (Main.npc[j].height / 2));
					float num9 = num6 - vector2.X;
					float num10 = num7 - vector2.Y;
					float rotation2 = (float)Math.Atan2(num10, num9) - 1.57f;
					bool flag3 = true;
					while (flag3)
					{
						SpriteEffects effects1 = SpriteEffects.None;
						if (flag2)
						{
							effects1 = SpriteEffects.FlipHorizontally;
							flag2 = false;
						}
						else
						{
							flag2 = true;
						}
						int height = 28;
						float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
						if (num11 < 40f)
						{
							height = (int)num11 - 40 + 28;
							flag3 = false;
						}
						num11 = 28f / num11;
						num9 *= num11;
						num10 *= num11;
						vector2.X += num9;
						vector2.Y += num10;
						num9 = num6 - vector2.X;
						num10 = num7 - vector2.Y;
						Color color2 = Lighting.GetColor((int)vector2.X / 16, (int)(vector2.Y / 16f));
						spriteBatch.Draw(shadowChain, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle(0, 0, shadowChain.Width, height), color2, rotation2, new Vector2(shadowChain.Width * 0.5f, shadowChain.Height * 0.5f), 1f, effects1, 0f);
					}
				}
			}
			int num12 = 140;
			float num13 = wallTop;
			float num14 = wallBottom;
			num14 = Main.screenPosition.Y + Main.screenHeight;
			float num15 = (int)((num13 - Main.screenPosition.Y) / num12) + 1;
			num15 *= num12;
			if (num15 > 0f)
			{
				num13 -= num15;
			}
			float num16 = num13;
			float num17 = NPC.position.X;
			float num18 = num14 - num13;
			bool flag4 = true;
			SpriteEffects effects2 = SpriteEffects.None;
			if (NPC.spriteDirection == 1)
			{
				effects2 = SpriteEffects.FlipHorizontally;
			}
			if (NPC.direction > 0)
			{
				num17 -= 80f;
			}
			int num19 = 0;
			if (!Main.gamePaused)
			{
                wallFrameCounter++;
			}
			if (wallFrameCounter > 12)
			{
				num19 = 280;
				if (wallFrameCounter > 17)
				{
                    wallFrameCounter = 0;
				}
			}
			else if (wallFrameCounter > 6)
			{
				num19 = 140;
			}
			while (flag4)
			{
				num18 = num14 - num16;
				if (num18 > num12)
				{
					num18 = num12;
				}
				bool flag5 = true;
				int num20 = 0;
				while (flag5)
				{
					int x = (int)(num17 + shadowWall.Width / 2) / 16;
					int y = (int)(num16 + num20) / 16;
					Main.spriteBatch.Draw(shadowWall, new Vector2(num17 - Main.screenPosition.X, num16 + num20 - Main.screenPosition.Y), new Rectangle(0, num19 + num20, shadowWall.Width, 16), Lighting.GetColor(x, y), 0f, default(Vector2), 1f, effects2, 0f);
					num20 += 16;
					if (num20 >= num18)
					{
						flag5 = false;
					}
				}
				num16 += num12;
				if (num16 >= num14)
				{
					flag4 = false;
				}
			}

			Texture2D drawTexture = TextureAssets.Npc[NPC.type].Value;
			Vector2 origin = new Vector2((drawTexture.Width / 2) * 0.5F, (drawTexture.Height / Main.npcFrameCount[NPC.type]) * 0.5F);

			Vector2 drawPos = new Vector2(
			NPC.position.X - Main.screenPosition.X + (NPC.width / 2) - (TextureAssets.Npc[NPC.type].Value.Width / 2) * NPC.scale / 2f + origin.X * NPC.scale,
			NPC.position.Y - Main.screenPosition.Y + NPC.height - TextureAssets.Npc[NPC.type].Value.Height * NPC.scale / Main.npcFrameCount[NPC.type] + 4f + origin.Y * NPC.scale + NPC.gfxOffY);

			SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(drawTexture, drawPos, NPC.frame, drawColor, NPC.rotation, origin, NPC.scale, effects, 0);

			return false;
		}

        private void HandleAnimation()
        {
            int previousPhase = phase;

            if (NPC.life < NPC.lifeMax * 0.5)
            {
                phase = 2;
            }

            if (previousPhase != phase)
            {
                frameCounter = 0;
                frame = (phase == 1) ? 0 : 2;
            }

            AnimateNPC();
        }

        private void AnimateNPC()
        {
            frameCounter++;
            if (frameCounter >= 30)
            {
                frameCounter = 0;

                if (phase == 1)
                {
                    frame = (frame == 0) ? 1 : 0;
                }
                else if (phase == 2)
                {
                    frame = (frame == 2) ? 3 : 2;
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WallofShadowTrophy>(), 10));
            npcLoot.Add(ItemDropRule.Common(ItemID.GreaterHealingPotion, 1, 5, 15));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WallofShadowMask>(), 7));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarknessCloth>(), 1, 8, 15));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<WallofShadowBag>(), 1));

            // Гарантированное выпадение одного из трёх предметов.
            npcLoot.Add(ItemDropRule.OneFromOptions(1,
                ModContent.ItemType<HeavyBeamCannon>(),
                ModContent.ItemType<Bolter>(),
                ModContent.ItemType<StrikerBlade>()));
        }
    }
}
