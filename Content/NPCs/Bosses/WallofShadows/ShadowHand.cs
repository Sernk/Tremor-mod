using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace TremorMod.Content.NPCs.Bosses.WallofShadows
{
    public class ShadowHand : ModNPC
    {
        public int wallOfShadowIndex = -1;
        private int attackTimer = 0; 
        private float lungeCooldown = 120; 
        private float lungeTimer = 0;
        private bool isLunging = false;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2; 
        }

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 30;
            NPC.damage = 60;
            NPC.defense = 25;
            NPC.lifeMax = 780;
            NPC.knockBackResist = 0.8f;
            NPC.noGravity = true;
            NPC.behindTiles = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.DeathSound = SoundID.NPCDeath11;
            NPC.aiStyle = -1; 
            NPC.value = 500; 
        }

        public override void AI()
        {
            //Romert.romertActive = true; 

            if (wallOfShadowIndex == -1)
            {
                FindWallOfShadow();
            }

            if (wallOfShadowIndex == -1 || !Main.npc[wallOfShadowIndex].active)
            {
                //Romert.romertActive = false; 
                NPC.active = false;
                return;
            }

            Player target = Main.player[NPC.target];
            if (!target.active || target.dead)
            {
                NPC.TargetClosest();
                target = Main.player[NPC.target];
            }

            attackTimer++;
            lungeTimer++;

            float speed = 4f;
            Vector2 direction = target.Center - NPC.Center;
            direction.Normalize();
            NPC.velocity = Vector2.Lerp(NPC.velocity, direction * speed, 0.05f);

            if (lungeTimer >= lungeCooldown && Vector2.Distance(NPC.Center, target.Center) < 300f)
            {
                isLunging = true;
                lungeTimer = 0;
                NPC.velocity = direction * 12f;
                attackTimer = 0;
            }

            if (isLunging && attackTimer > 15)
            {
                isLunging = false;
            }

            if (attackTimer >= 90 && !isLunging)
            {
                attackTimer = 0;
                //if (Main.netMode != NetmodeID.MultiplayerClient)
                //{
                //    Vector2 projDirection = (target.Center - NPC.Center).SafeNormalize(Vector2.Zero);
                //    int proj = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, projDirection * 8f,
                //        ProjectileID.ShadowFlame, 20, 2f, Main.myPlayer);
                //    Main.projectile[proj].timeLeft = 300;
                //}
            }

            NPC.spriteDirection = (target.Center.X > NPC.Center.X) ? 1 : -1;
        }

        private void FindWallOfShadow()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].type == ModContent.NPCType<WallOfShadow>() && Main.npc[i].active)
                {
                    wallOfShadowIndex = i;
                    break;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 15) 
            {
                NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
                NPC.frameCounter = 0;
            }
        }

        public override void OnKill()
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, 0f, 0f);
            }
        }
    }
}