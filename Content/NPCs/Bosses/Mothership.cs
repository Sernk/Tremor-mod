using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using TremorMod.Content.Projectiles;
using TremorMod.Content.NPCs.Bosses;
using Terraria.Audio;

namespace TremorMod.Content.NPCs.Bosses
{
    [AutoloadBossHead]
    public class Mothership : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mothership");
            Main.npcFrameCount[NPC.type] = 8;
        }

        private float timeToNextFrame;
        public int frame;

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 45000;
            NPC.damage = 125;
            NPC.defense = 55 * 0;
            NPC.knockBackResist = 0f;
            NPC.width = 162;
            NPC.height = 122;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.npcSlots = 1;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath6;
            Music = MusicID.Boss2;
        }

        public float timeToShoot = 2;
        //private float vel = 2.5f;
        private float lifeTime;
        private bool Rage;

        public Vector2 bossCenter
        {
            get { return NPC.Center; }
            set { NPC.position = value - new Vector2(NPC.width / 2, NPC.height / 2); }
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float bossLifeScale, float balance)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.625f * bossLifeScale);
            NPC.damage = (int)(NPC.damage * 0.6f);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CKMotherGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CKMotherGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CKMotherGore3").Type, 1f);
            }
        }

        public override void AI()
        {
            bool allDead = false;
            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i].dead) allDead = true;
            }

            if (Main.dayTime || allDead)
            {
                if (NPC.velocity.X > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X + 0.75f;
                }
                else
                {
                    NPC.velocity.X = NPC.velocity.X - 0.75f;
                }
                NPC.velocity.Y = NPC.velocity.Y - 0.1f;
                NPC.rotation = NPC.velocity.X * 0.05f;
            }

            lifeTime += 0.016f;
            Player player = Main.player[NPC.target];
            Vector2 targetPos = player.Center - new Vector2(0, 250) + new Vector2((float)Math.Sin(lifeTime) * 200, (float)Math.Cos(lifeTime) * 50);
            bossCenter = Vector2.Lerp(bossCenter, targetPos, 0.01f);
            Lighting.AddLight(bossCenter, 0.3f, 0.3f, 1f);
            if (NPC.life < NPC.lifeMax / 2)
            {
                Rage = true;
            }
            if (timeToNextFrame > 0)
            {
                timeToNextFrame -= 0.016f;
            }
            else
            {
                timeToNextFrame = 0.1f;
                if (frame < 3)
                {
                    frame++;
                }
                else
                {
                    frame = 0;
                }
                if (Rage)
                {
                    frame += 4;
                }
            }
            if (timeToShoot > 0)
            {
                timeToShoot -= 0.016f;
            }
            else
            {
                Shoot(player);
            }
        }

        private void Shoot(Player player)
        {
            if (!Rage)
            {
                float angle = Main.rand.Next(0, (int)Math.PI * 200) / 100f;
                Vector2 vel = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 3 * Main.rand.Next(5);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), bossCenter.X, bossCenter.Y + 15, vel.X + 15, vel.Y, Mod.Find<ModProjectile>("PurplePulsePro").Type, 30, 5f);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), bossCenter.X, bossCenter.Y, vel.X, vel.Y, Mod.Find<ModProjectile>("PurplePulsePro").Type, 30, 5f);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), bossCenter.X, bossCenter.Y - 15, vel.X - 15, vel.Y, Mod.Find<ModProjectile>("PurplePulsePro").Type, 30, 5f);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), bossCenter.X, bossCenter.Y + 30, vel.X + 30, vel.Y, Mod.Find<ModProjectile>("PurplePulsePro").Type, 30, 5f);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), bossCenter.X, bossCenter.Y - 30, vel.X - 30, vel.Y, Mod.Find<ModProjectile>("PurplePulsePro").Type, 30, 5f);
                timeToShoot = 1;
            }
            else
            {
                float angle = (float)Math.Atan2(player.Center.Y - bossCenter.Y, player.Center.X - bossCenter.X);
                Vector2 vel = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 5;
                Projectile.NewProjectile(NPC.GetSource_FromThis(), bossCenter.X, bossCenter.Y, vel.X, vel.Y, 465, 25, 5f);
                timeToShoot = 8;
            }
        }

        public override bool PreKill()
        {
            Player player = Main.player[NPC.target];
            NPC.NewNPC(NPC.GetSource_FromThis(), (int)bossCenter.X, (int)bossCenter.Y, Mod.Find<ModNPC>("CyberKing").Type, 0, 0, 0, 0, 0, NPC.target);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return false;
        }

        private float clamp(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameHeight * frame + 2;
        }
    }
}