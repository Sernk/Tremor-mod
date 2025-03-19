using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using TremorMod.Content.Items.Materials.OreAndBar;
using TremorMod.Content.Items.Weapons.Summon;
using TremorMod.Content.Items.Weapons.Ranged;
using TremorMod.Content.Items.Weapons.Magic;
using TremorMod.Content.Items.Weapons.Melee;
using TremorMod.Content.Items.Accessories;
using TremorMod.Content.Items.Placeable;
using TremorMod.Content.Items.Vanity;
using TremorMod.Content.Items.Bag;
using TremorMod.Content.Projectiles;
using TremorMod.Utilities;

namespace TremorMod.Content.NPCs.Bosses.Rukh.Romert
{
	[AutoloadBossHead]
	public class Rukh : ModNPC
	{
        #region "Константы"
        const int AnimationRate = 8; // Частота смены кадров (То, сколько кадров не будет сменятся кадр)
        const int FrameCount = 4; // Кол-во кадров

        const int ShootRate = 50; // Частота выстрела. Будет производить 60/ShootRate выстрелов в секунду
        const int ShootDamage = 15; // Урон от выстрела
        const float ShootKnockback = 1; // Отбрасование от выстрела
        const float ShootSpeed = 10; // Скорость выстрела

        const int ShootRate2 = 400; // Частота выстрела. Будет производить 60/ShootRate выстрелов в секунду
        const int ShootDamage2 = 15; // Урон от выстрела
        const float ShootKnockback2 = 1; // Отбрасование от выстрела
        const float ShootSpeed2 = 0; // Скорость выстрела		

        const float DistortPercent = 0.15f; // Процент деформации статов (неточности) (1.0 == 100%)

        const int MinionsID = 61; // ID вуртулек
        const int MinionsCount = 4; // Кол-во вуртулек которых заспавнит

        const int StateTime_Flying = 600; // Сколько будет летать в воздухе до призыва миньонов
        const int StateTime_Minions = 120; // Сколько времени будет спавнить вуртулек

        const int FlyingAI = 2;
        const int MinionsAI = 0;

        const float MinionsState_XDeaccelerationPower = 0.05f; // Скорость замедления по X
        const float MinionsState_YMaxSpeed = 2.80f; // Макс. скорость взлёта во время спавна миньонов
        const float MinionsStete_YSpeedStep = 0.02f; // Скорость увеличения скорости по Y во время спавна миньонов

        const int States = 2;

        // New AI
        private int actionTimer = 0;
        private bool dashing = false;
        private bool shooting = false;
        private Vector2 dashDirection;
        private int shotCount = 0;
        private const float DashDistance = 25f * 16f; // 40 блоков в пикселях
        private const float EndDashDistance = 20f * 16f; // 41 блок в пикселях
        #endregion

        #region "Переменные"
        int TimeToAnimation = AnimationRate;
        int Frame;
        #endregion

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rukh");
			Main.npcFrameCount[NPC.type] = 20;
		}

		public override void SetDefaults()
		{
			NPC.lifeMax = 2500;
			NPC.damage = 22;
			NPC.defense = 8;
			NPC.knockBackResist = 0f;
			NPC.width = 160;
			NPC.height = 210;
            NPC.aiStyle = 2; 
            NPC.noGravity = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = Item.buyPrice(0, 0, 75, 0);
			NPC.boss = true;
			//bossBag = mod.ItemType("VultureKingBag");
			NPC.noTileCollide = true;
		}

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float bossLifeScale, float balance)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.625f * bossLifeScale);
            NPC.damage = (int)(NPC.damage * 0.6f);
        }

        public override void AI()
        {
            PlayAnimation();

            Player player = Main.player[NPC.target];
            if (player == null || !player.active || player.dead)
            {
                NPC.TargetClosest(false);
                return;
            }

            float distanceToPlayer = Vector2.Distance(NPC.Center, player.Center);

            if (!dashing && !shooting)
            {
                NPC.velocity = (player.Center - NPC.Center).SafeNormalize(Vector2.Zero) * 2f; // Обычное передвижение
                actionTimer++;

                if (actionTimer > 120 && distanceToPlayer <= DashDistance) // Рывок только если <= 40 блоков
                {
                    dashing = true;
                    actionTimer = 0;
                    dashDirection = (player.Center - NPC.Center).SafeNormalize(Vector2.Zero) * 10f; // Устанавливаем направление рывка
                }
            }
            else if (dashing)
            {
                NPC.velocity = dashDirection; // Выполняем рывок
                actionTimer++;

                if (actionTimer > 20 || distanceToPlayer > EndDashDistance) // Длительность рывка или если дистанция > 41 блока
                {
                    dashing = false;
                    shooting = true;
                    actionTimer = 0;
                    shotCount = 0;
                }
            }
            else if (shooting)
            {
                if (actionTimer % 60 == 0 && shotCount < 3)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 shootDirection = (player.Center - NPC.Center).SafeNormalize(Vector2.Zero) * 8f;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shootDirection, ModContent.ProjectileType<projVultureFeather>(), 15, 1f, Main.myPlayer);
                    }
                    shotCount++;
                }

                actionTimer++;

                if (shotCount >= 3 && actionTimer > 180) // После 3 выстрелов (60 * 3) = 180 тиков снова рывок или ожидание
                {
                    shooting = false;
                    actionTimer = 0;
                }
            }
        }

        void PlayAnimation()
        {
            if (--TimeToAnimation <= 0)
            {
                TimeToAnimation = (int)Helper.DistortFloat(AnimationRate, DistortPercent);
                if (++Frame >= FrameCount)
                    Frame = 0;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects Direction = (NPC.target == -1) ? SpriteEffects.None : ((Main.player[NPC.target].position.X < NPC.position.X) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, new Rectangle((int)(NPC.position.X - Main.screenPosition.X), (int)(NPC.position.Y - Main.screenPosition.Y), 240, 160), new Rectangle(0, Frame * 160, 240, 160), drawColor, 0, new Vector2(0, 0), Direction, 0);
            return false;
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
            int hitDirection = hit.HitDirection;

            if (NPC.life <= 0)
			{
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TRGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TRGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TRGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TRGore2").Type, 1f);
			}
		}

        public override void OnKill()
        {
            TremorSpawnEnemys.downedRukh = true;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VultureKingMask>(), 7));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VultureFeather>(), 4));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CactusBow>(), 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SandKnife>(), 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SandstoneBar>(), 1, 10, 18));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VultureKingTrophy>(), 10));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<VultureKingBag>(), 1));
        }
    }
}