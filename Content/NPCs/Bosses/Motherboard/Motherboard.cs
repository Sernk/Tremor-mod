﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using TremorMod.Content.Items.Accessories;
using TremorMod.Content.Items.Materials;
using TremorMod.Content.Items.Placeable;
using TremorMod.Content.Items.Vanity;
using TremorMod.Content.Items.Bag;
using TremorMod.Utilities;
using TremorMod.Content.Projectiles;

namespace TremorMod.Content.NPCs.Bosses.Motherboard
{
	// has two stages
	// some other bosses do
	// the possibility to factor that exists
	// each stage has different appearing, disappearing, and following times

	public class Stage
	{
		public int followPlayerTime; // Time of following player in 1st stage
		public int disappearingTime; // Time of disappearing in 1st stage
		public int appearingTime; // Time of appearing in 1st stage
		public int stateTime; // Stage time
		public int appearTime;

		public int GetStateTime => appearingTime + disappearingTime + followPlayerTime;

		protected int _currentFrame; // Current frame
		protected int _timeToAnimation = 6; // Animation rate
		protected const int AnimationRate = 6; // Animation rate

		protected const int LaserYOffset = 95; // Laser spawn offset by Y value
		protected const int LaserDamage = 25; // Laser damage
		protected const float LaserKb = 1; // Laser knockback

		protected const int SecondShootCount = 3;
		protected const float SecondShootSpeed = 15f;
		protected const int SecondShootDamage = 35;
		protected const float SecondShootKn = 1.0f;
		protected const int SecondShootRate = 60; // The rate of fire of the motherboard's 3 laser shot
		protected const int SecondShootSpread = 35; // The random spread of motherboard's 3 laser shot
		protected const float SecondShootSpreadMult = 0.05f;

		protected int _secondShootTime = 60;

		public Stage(int followPlayerTime, int disappearingTime, int appearingTime)
		{
			this.followPlayerTime = followPlayerTime;
			this.disappearingTime = disappearingTime;
			this.appearingTime = appearingTime;
			this.stateTime = appearingTime + disappearingTime + followPlayerTime;
		}

		public virtual int FrameOffset => 0;

		public void Animate(Motherboard boss)
		{
			--_timeToAnimation;
			if (_timeToAnimation == 0)
			{
				_currentFrame = (_currentFrame + 1) % 3;
				_timeToAnimation = AnimationRate;
				boss.NPC.frame = boss.GetFrame(_currentFrame + FrameOffset);
			}
		}

		public virtual void AI(Motherboard motherboard) { }
		public virtual void AdjustHead(Motherboard boss) { }
		public virtual void Start(Motherboard boss) { }
	}

	// Phase 1

	// In the first phase after she spawns, she will also spawn in with
	// several Signal Drones. The drones will fly around her and will
	// occasionally charge at the player. During this time she will chase
	// after the player slowly and is completely immune to damage. After
	// the drones pass beams between each other, she will fire 3 shadow
	// laser toward the player. Every few seconds after destroying some of
	// the drones, she will spawn new ones. The phase ends when all of the
	// drones are destroyed.

	public class Stage1 : Stage
	{
		public Stage1(int followPlayerTime, int disappearingTime, int appearingTime) : base(followPlayerTime, disappearingTime, appearingTime) { }

		private List<int> _signalDrones = new List<int>(); // ID of Signal Drones

		private const int DroneSpawnAreaX = 300; // Area size in which Drone can spawn by X value
		private const int DroneSpawnAreaY = 300; // Area size in which Drone can spawn by Y value
		private const int StartDroneCount = 8; // How many drones she starts with
		private const int MaxDrones = 20; // How many drones there can be at maximum
		private const int ShootRate = 150; // Fire rate in ticks
		private const int TimeToLaserRate = 3; // Fire rate (From drones to player)

		private int _timeToNextDrone = 1; // Time for spawning next Drone
		private int _timeToShoot = 60; // Time for next shoot
		private int _timeToLaser = 3; // Time for next shoot (Drones lasers)
		private int _lastSignalDrone = -1; // Last Drone

		//------------------------------------------------
		// private methods
		//------------------------------------------------

		// Removes all dead Drones from the list
		private void RemoveDeadDrones(Motherboard boss)
		{
			int lastKnownDrone = _lastSignalDrone != -1 ? _signalDrones[_lastSignalDrone] : -1;

			_signalDrones = _signalDrones.Where(x =>
				{
					NPC npc = Main.npc[x];
					return npc.active && npc.type == ModContent.NPCType<SignalDrone>();
				}).ToList();

			_lastSignalDrone = _signalDrones.FindIndex(x => x == lastKnownDrone);
		}

		private void TrySpawnOneDrone(Motherboard boss)
		{
			if (_signalDrones.Count < MaxDrones)
			{
				--_timeToNextDrone;
				if (_timeToNextDrone < 0)
				{
					_timeToNextDrone = 60 * Main.rand.Next(3, 6);

					Vector2 spawnPosition =
						Helper.RandomPointInArea(
							new Vector2(boss.NPC.Center.X - DroneSpawnAreaX * .5f, boss.NPC.Center.Y - DroneSpawnAreaY * .5f),
							new Vector2(boss.NPC.Center.X + DroneSpawnAreaX * .5f, boss.NPC.Center.Y + DroneSpawnAreaY * .5f));

					_signalDrones.Add(NPC.NewNPC(boss.NPC.GetSource_FromThis(), (int)spawnPosition.X, (int)spawnPosition.Y + LaserYOffset, ModContent.NPCType<SignalDrone>(), 0, 0, 0, 0, boss.NPC.whoAmI));
				}
			}
		}

		private void ShootOneLaser(Motherboard boss)
		{
			// this code is still poop
			try
			{
				int ai0 = _lastSignalDrone == -1 ? boss.NPC.whoAmI : _signalDrones[_lastSignalDrone];
				++_lastSignalDrone;
				//var zapSound = new LegacySoundStyle(SoundID.Trackable, TremorUtils.GetIdForSoundName($"dd2_lightning_aura_zap_{Main.rand.Next(4)}"));
				//SoundEngine.PlaySound(zapSound.WithPitchVariance(Main.rand.NextFloat() * .5f).WithVolume(Main.soundVolume * 1.5f));
				int newProj = Projectile.NewProjectile(boss.NPC.GetSource_FromThis(), boss.NPC.Center.X, boss.NPC.Center.Y, 0, 0,
					boss.Mod.Find<ModProjectile>("projMotherboardLaser").Type,
					LaserDamage, LaserKb, 0, ai0, _signalDrones[_lastSignalDrone]);
				if (_lastSignalDrone == 0)
				{
					Main.projectile[newProj].localAI[1] = 1;
				}
			}
			catch
			{
				// POOP I TELL YOU
			}
		}

		// shoot a beam at target player
		private void ShootOneSecondShot(Motherboard boss)
		{
			Player target = Main.player[boss.NPC.target];

			// Calculate velocity DIRECTION
			float targetAngle = boss.NPC.AngleTo(target.Center);
			Vector2 velocity = new Vector2((float)Math.Cos(targetAngle), (float)Math.Sin(targetAngle)) * SecondShootSpeed;
			velocity += new Vector2(Main.rand.Next(-(int)SecondShootSpeed / 2, (int)SecondShootSpeed / 2 + 1) * SecondShootSpreadMult);

			// Shoot proj
			Projectile.NewProjectile(boss.NPC.GetSource_FromThis(), boss.NPC.Center.X, boss.NPC.Center.Y, velocity.X, velocity.Y, ProjectileID.ShadowBeamHostile, SecondShootDamage, SecondShootKn);
		}

		private void ShootDroneLasers(Motherboard boss) // If it is time to shoot
		{
			// if there's no current drone AND we are not moving, return
			// only shoot lasers if there are any and we are in moving phase
			--_timeToLaser;
			if (_timeToLaser <= 0)
			{
				// Set new shoot time
				_timeToLaser = TimeToLaserRate;
				ShootOneLaser(boss);

				// If we shot all interdrone lasers, shoot at the player
				if (_lastSignalDrone + 1 >= _signalDrones.Count)
				{
					// shoot N SecondShoots
					for (int i = 0; i < SecondShootCount; i++)
					{
						ShootOneSecondShot(boss);
					}

					// setting last Drone to -1 and ending the cycle of shooting
					_lastSignalDrone = -1;
					_timeToShoot = ShootRate;
				}
			}
		}

		//------------------------------------------------
		// hooks
		//------------------------------------------------

		public override void Start(Motherboard boss)
		{
			for (int i = 0; i < StartDroneCount; i++)
			{
				_timeToNextDrone = -1; // forcefully spawn
				TrySpawnOneDrone(boss);
			}
		}

		public override void AdjustHead(Motherboard boss)
		{
            TextureAssets.NpcHeadBoss[boss.headTexture] = ModContent.Request<Texture2D>("TremorMod/Content/NPCs/Bosses/Motherboard/Motherboard_Head_Boss");
        }

		public override void AI(Motherboard boss)
		{
			RemoveDeadDrones(boss);
			TrySpawnOneDrone(boss);

			if (_signalDrones.Count > 0) // we have drones, we can shoot
			{
				--_timeToShoot;
				if (_timeToShoot < 0)
				{
					// only shoot lasers if there are any and we are in moving phase
					if (boss.NPC.ai[0] == -1)
					{
						ShootDroneLasers(boss);
					}
				}
			}
			else // no drones, advance stage
			{
				boss.stage = boss.stage2;
				boss.stage.Start(boss);
			}
		}
	}

	// Phase 2

	// Once all of the drones are destroyed, she will be vulnerable to
	// attacks. She will replace the drones with 4 new minions called
	// Clampers, which will be attached to her. They will chase after the
	// player while Motherboard moves aimlessly around you and
	// teleporting. After about 80% of her health is gone she will detach
	// the Clampers and begin to aggressively chase you.

	public class Stage2 : Stage
	{
		public Stage2(int followPlayerTime, int disappearingTime, int appearingTime) : base(followPlayerTime, disappearingTime, appearingTime) { }

		private List<int> _clampers = new List<int>(); // Clampers list

		public override int FrameOffset => 3;

        public override void AdjustHead(Motherboard boss)
        {
            TextureAssets.NpcHeadBoss[boss.headTexture] = ModContent.Request<Texture2D>("TremorMod/Content/NPCs/Bosses/Motherboard/Motherboard_Head_Boss");
        }

        public override void Start(Motherboard boss)
		{
			_clampers = new List<int>
			{
				NPC.NewNPC(boss.NPC.GetSource_FromThis(),(int) boss.NPC.Center.X - 15, (int) boss.NPC.Center.Y + 25, boss.Mod.Find<ModNPC>("Clamper").Type, 0, 0, 0, 0, boss.NPC.whoAmI),
				NPC.NewNPC(boss.NPC.GetSource_FromThis(),(int) boss.NPC.Center.X - 10, (int) boss.NPC.Center.Y + 25, boss.Mod.Find<ModNPC>("Clamper").Type, 0, 0, 0, 0, boss.NPC.whoAmI),
				NPC.NewNPC(boss.NPC.GetSource_FromThis(),(int) boss.NPC.Center.X + 10, (int) boss.NPC.Center.Y + 25, boss.Mod.Find<ModNPC>("Clamper").Type, 0, 0, 0, 0, boss.NPC.whoAmI),
				NPC.NewNPC(boss.NPC.GetSource_FromThis(),(int) boss.NPC.Center.X + 15, (int) boss.NPC.Center.Y + 25, boss.Mod.Find<ModNPC>("Clamper").Type, 0, 0, 0, 0, boss.NPC.whoAmI)
			};

			for (int i = 0; i <= 3; i++)
			{
				Main.npc[_clampers[i]].localAI[1] = i + 1;
			}

			boss.NPC.dontTakeDamage = false;
			boss.NPC.aiStyle = 2;

			SoundEngine.PlaySound(SoundID.ScaryScream.WithPitchOffset(Main.rand.NextFloat()), boss.NPC.position); // high tonal boss screech
			//SoundEngine.PlaySound(SoundID.DD2_LightningBugDeath.AsSound().WithPitchVariance(Main.rand.NextFloat()).WithVolume(Main.soundVolume * 1.5f), boss.NPC.position);
		}

		private void CheckClampers(Motherboard boss)
		{
			_clampers = _clampers.Where(x =>
			{
				NPC npc = Main.npc[x];
				return npc.active && npc.type == boss.Mod.Find<ModNPC>("Clamper").Type;
			}).ToList();

			// Spawn clamper strings (laser)
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				foreach (int clamper in _clampers)
				{
					int id = Projectile.NewProjectile(boss.NPC.GetSource_FromThis(), boss.NPC.Center.X, boss.NPC.Center.Y + LaserYOffset, 0, 0,
						boss.Mod.Find<ModProjectile>("projClamperLaser").Type, LaserDamage, LaserKb, 0, boss.NPC.whoAmI, clamper);
					Main.projectile[id].localAI[1] = stateTime;
				}
			}
		}

		protected void SecondShoot(Motherboard boss)
		{
			if (!boss.isInsideTerrain())
			{
				--_secondShootTime;
			}

			if (_secondShootTime <= 0)
			{
				_secondShootTime = SecondShootRate;
				for (int i = 0; i < 2; i++)
				{
					//SoundEngine.PlaySound(SoundID.Item113.WithPitchVariance(Main.rand.NextFloat()), boss.NPC.position);
					if (Main.netMode != NetmodeID.MultiplayerClient)
						Projectile.NewProjectile(boss.NPC.GetSource_FromThis(), boss.NPC.Center.X, boss.NPC.Center.Y + 95, 0, 0,
							boss.Mod.Find<ModProjectile>("projMotherboardSuperLaser").Type, SecondShootDamage, SecondShootKn, 0, boss.NPC.whoAmI, i);
				}
			}
		}

		protected void ChangeAlpha(Motherboard boss, float difference)
		{
			boss.NPC.alpha = (int)MathHelper.Clamp(boss.NPC.alpha + difference, 0, 255);
		}

		public override void AI(Motherboard boss)
		{
			boss.Move();
			boss.NPC.TargetClosest(true);

			// this was never actually executed
			// not sure what is meant to do, or where it is supposed to go
			// for (int i = 0; i < _clampers.Count; i++)
			//	Main.npc[_clampers[i]].ai[2] = 1;

			// following
			if (boss.NPC.ai[1] == 0f)
			{
				// runs only SP/server side
				if (Main.netMode != 1)
				{
					// increment the something timer
					boss.NPC.localAI[1] += 1f;

					// if the timer is due, plus some random amount of ticks
					if (boss.NPC.localAI[1] >= 120 + Main.rand.Next(200))
					{
						boss.NPC.localAI[1] = 0f;
						boss.NPC.TargetClosest(true);

						// attempt to find coords somewhere around the target (max 100 tries)
						// break as soon as we find a place around the player that we can move to
						for (int attempts = 0; attempts < 100; attempts++)
						{
							Player target = Main.player[boss.NPC.target];
							int coordX = (int)target.Center.X / 16 + Main.rand.Next(-50, 51);
							int coordY = (int)target.Center.Y / 16 + Main.rand.Next(-50, 51);

							if (!WorldGen.SolidTile(coordX, coordY)
								&& Collision.CanHit(new Vector2(coordX, coordY).ToWorldCoordinates(), 1, 1,
									target.position,
									target.width,
									target.height))
							{
								boss.NPC.teleportTime = 1f;
								boss.NPC.ai[1] = 1f;
								boss.NPC.ai[2] = coordX;
								boss.NPC.ai[3] = coordY;
								boss.NPC.netUpdate = true;
								break;
							}
						}

						return;
					}
				}
			}
			// disappearing
			else if (boss.NPC.ai[1] == 1f)
			{
				ChangeAlpha(boss, 3);

				// finished disappearing
				if (boss.NPC.alpha >= 255)
				{
					boss.NPC.teleportTime = 0f;
					boss.NPC.position.X = boss.NPC.ai[2] * 16f - boss.NPC.width / 2;
					boss.NPC.position.Y = boss.NPC.ai[3] * 16f - boss.NPC.height / 2;
					boss.NPC.ai[1] = 2f;
					// Motherboard screech
					//var screech = new LegacySoundStyle(SoundID.Trackable, TremorUtils.GetIdForSoundName($"dd2_lightning_bug_death_{Main.rand.Next(3)}"));
					//SoundEngine.PlaySound(screech.WithPitchVariance(Main.rand.NextFloat()));
					////Main.PlaySound(SoundID.DD2_LightningBugDeath.WithPitchVariance(Main.rand.NextFloat()).WithVolume(Main.soundVolume * 2.5f), boss.npc.position);
					//SoundEngine.PlaySound(SoundID.Item78.WithVolume(Main.soundVolume * 1.15f), boss.NPC.position); // tp
					return;
				}
			}
			// appearing
			else if (boss.NPC.ai[1] == 2f)
			{
				ChangeAlpha(boss, -3);

				// finished appearing
				if (boss.NPC.alpha <= 0)
				{
					boss.NPC.ai[1] = 0f;
					return;
				}
			}

			// not finished appearing, disappearing, or didn't find a new place to move to....?
			CheckClampers(boss);
			SecondShoot(boss);
		}
	}

	// BOSS CODE

	[AutoloadBossHead]
	public class Motherboard : ModNPC
	{
		public Stage stage0 = new Stage(120, 30, 30);
		public Stage stage1 = new Stage1(120, 30, 30);
		public Stage stage2 = new Stage2(90, 30, 30);

		public Stage stage;

		public int headTexture = 0;

		// private int _stateTime = StateOneAppearingTime + StateOneDisappearingTime + StateOneFollowPlayerTime; // Stage time

		// private int GetStateTime => GetAppearingTimeNow + GetDisappearingTimeNow + GetFollowPlayerTimeNow;

		// private int GetFollowPlayerTimeNow => (stage == stage1) ? stage1.followPlayerTime : stage2.followPlayerTime;
		private int GetDisappearingTimeNow => (stage == stage1) ? stage1.disappearingTime : stage2.disappearingTime;
		private int GetAppearingTimeNow => (stage == stage1) ? stage1.appearingTime : stage2.appearingTime;

		private float _teleportTime;

		public bool IsTeleporting
			=> _teleportTime > 0f;

		private float teleportStyle = 0f;

		// public override bool UsesPartyHat() => false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Motherboard");
			Main.npcFrameCount[NPC.type] = 6;

			NPCID.Sets.MustAlwaysDraw[NPC.type] = true;
			NPCID.Sets.NeedsExpertScaling[NPC.type] = true;
		}

		public override void SetDefaults()
		{
			NPC.lifeMax = 45000;
			NPC.damage = 30;
			NPC.knockBackResist = 0f;
			NPC.defense = 30;
			NPC.width = 170;
			NPC.height = 160;
			NPC.aiStyle = 2;
			NPC.npcSlots = 50f;
			Music = MusicID.Boss3;

			NPC.dontTakeDamage = true;
			NPC.noTileCollide = true;
			NPC.noGravity = true;
			NPC.boss = true;
			NPC.lavaImmune = true;

			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;

			//bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.ItemType<MotherboardBag>();
			headTexture = NPCID.Sets.BossHeadTextures[NPC.type];

			stage = stage0;
		}

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float bossLifeScale, float balance)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.625f * bossLifeScale);
            NPC.damage = (int)(NPC.damage * 0.6f);
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
            int hitDirection = hit.HitDirection;
            if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 151, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);
				}

				const int goreAmount = 4;
				for (int i = 0; i < goreAmount; i++)
				{
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MotherboardGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MotherboardGore2").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MotherboardGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MotherboardGore4").Type, 1f);
                }
			}
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MotherboardMask>(), 7));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MotherboardTrophy>(), 10));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FlaskCore>(), 10));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulofMind>(),1, 20, 40));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CarbonSteel>(),1, 5, 12));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BenderLegs>(), 3));
			npcLoot.Add(ItemDropRule.Common(ItemID.GreaterHealingPotion, 1, 5, 15));
			npcLoot.Add(ItemDropRule.Common(ItemID.HallowedBar, 1, 15, 35));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<MotherboardBag>(), 1));
		}

		// AI
		public void Move()
		{
			if (!IsTeleporting)
				NPC.position += NPC.velocity * 2;
		}

		private void UpdateTeleportVisuals()
		{
			if (_teleportTime > 0f)
			{
				if (teleportStyle == 0f)
				{
					_teleportTime -= 0.02f;
					if (Main.rand.Next(100) >= 25f * _teleportTime)
					{
						Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.CrystalPulse);
						dust.noGravity = true;
						dust.scale = 1.2f;
						dust.fadeIn = 0.4f;
					}
				}
				_teleportTime -= 0.005f;
			}
		}

		public Rectangle GetFrame(int number)
		{
			return new Rectangle(0, NPC.frame.Height * number, NPC.frame.Width, NPC.frame.Height);
		}

		public bool isInsideTerrain()
		{
			for (int i = (int)NPC.position.X - 8; i < (NPC.position.X + 8 + NPC.width); i += 8)
				for (int l = (int)NPC.Center.Y + 90; l < (NPC.Center.Y + 106); l += 8)
					if (WorldGen.SolidTile(i / 16, l / 16))
						return true;
			return false;
		}

		// Changes phase (Following/disappearing/appearing)
		// this code should be revised
		private void CyclePhases()
		{
			--(stage.stateTime); // Lowering states time

			if (stage.stateTime <= 0) // If state time < or = 0 then update a variable
				stage.stateTime = stage.GetStateTime; // Updating

			// If it is time to appear
			if (stage.stateTime <= GetAppearingTimeNow)
			{
				NPC.ai[0] = -3; // Then appear
				return; // Ending the method
			}

			// If it is time to disappear
			if (stage.stateTime <= GetAppearingTimeNow + GetDisappearingTimeNow)
			{
				NPC.ai[0] = -2; // Then disappear
				return; // Ending the method
			}

			// Otherwise it's time to follow..... maybe? stage2?
			if (NPC.ai[0] == -2)
				stage.appearTime = GetAppearingTimeNow;

			--(stage.appearTime);
			if (stage.appearTime > 0)
			{
				NPC.ai[0] = -3;
				return;
			}

			// else
			NPC.ai[0] = -1; // Follow the player
		}

		public override void AI()
		{
			stage.Animate(this);
			stage.AdjustHead(this);

			// fly away/enrage/whatever if needed (skeletron aiStyle)
			if (Helper.GetNearestPlayer(NPC.position, true) == -1 || Main.dayTime)
			{
				NPC.aiStyle = 11;
				NPC.damage = 1000;
				NPC.ai[0] = 2;
			}

			// if flying away
			if (NPC.aiStyle == 11)
			{
				NPC.rotation = 0;
				return;
			}

			// ini initialising
			if (stage == stage0)
			{
				stage = stage1;
				stage.Start(this);
			}

			UpdateTeleportVisuals();

			// move between phases
			CyclePhases();

			// execute the stage's AI
			stage.AI(this);
		}

        public override void OnKill()
        {
            TremorSpawnEnemys.downedMotherboard = true;
        }

        // // ?? Doesn't seem to fix much
        // public override void SendExtraAI(BinaryWriter writer)
        // {
        //	writer.Write(_appearTime);
        //	writer.Write(AIStage);
        //	writer.Write(_signalDrones.Count);
        //	foreach (int drone in _signalDrones)
        //	{
        //		writer.Write(drone);
        //	}
        //	writer.Write(_lastSignalDrone);
        //	writer.Write(_shootNow);
        //	writer.Write(_timeToNextDrone);
        //	writer.Write(_timeToShoot);
        //	writer.Write(_timeToLaser);
        //	writer.Write(_currentFrame);
        //	writer.Write(_timeToAnimation);
        //	writer.Write(_clampers.Count);
        //	foreach (int clamper in _clampers)
        //	{
        //		writer.Write(clamper);
        //	}
        //	writer.Write(_secondShootTime);
        // }

        // public override void ReceiveExtraAI(BinaryReader reader)
        // {
        //	_appearTime = reader.ReadInt32();
        //	AIStage = reader.ReadInt32();
        //	int c = reader.ReadInt32();
        //	_signalDrones = new List<int>();
        //	for (int i = 0; i < c; i++)
        //	{
        //		_signalDrones[i] = reader.ReadInt32();
        //	}
        //	_lastSignalDrone = reader.ReadInt32();
        //	_shootNow = reader.ReadBoolean();
        //	_timeToNextDrone = reader.ReadInt32();
        //	_timeToShoot = reader.ReadInt32();
        //	_timeToLaser = reader.ReadInt32();
        //	_currentFrame = reader.ReadInt32();
        //	_timeToAnimation = reader.ReadInt32();
        //	c = reader.ReadInt32();
        //	for (int i = 0; i < c; i++)
        //	{
        //		_clampers[i] = reader.ReadInt32();
        //	}
        //	_secondShootTime = reader.ReadInt32();
        // }
    }
}
