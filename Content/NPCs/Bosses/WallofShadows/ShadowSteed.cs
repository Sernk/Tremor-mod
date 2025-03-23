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

namespace TremorMod.Content.NPCs.Bosses.WallofShadows
{

    public class ShadowSteed : ModNPC
    {
        private const int MaxShadowSteeds = 5;
        private float speed = 15f; 

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shadow Steed");
            Main.npcFrameCount[NPC.type] = 6;
        }

        public override void SetDefaults()
        {
            NPC.width = NPC.height = 48;
            NPC.lifeMax = 940;
            NPC.damage = 75;
            NPC.defense = 30;
            NPC.knockBackResist = 0f; 
            NPC.HitSound = SoundID.NPCHit31;
            NPC.noGravity = true;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.noTileCollide = true;
            NPC.value = 500; 
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            NPC.life = 0;
            NPC.checkDead();

            if (Main.expertMode)
                target.AddBuff(153, 180); 
        }

        private int GetActiveShadowSteedCount()
        {
            int count = 0;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<ShadowSteed>())
                {
                    count++;
                }
            }
            return count;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 99, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 99, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 99, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 99, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 99, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 99, 1f);

                if (Main.rand.NextBool(2)) 
                {
                    NPC.NewNPC(Entity.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<ShadowHandTwo>());
                }
                if (Main.rand.NextBool(2)) 
                {
                    NPC.NewNPC(Entity.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<ShadowHandTwo>());
                }
                if (Main.rand.NextBool(2)) 
                {
                    NPC.NewNPC(Entity.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<ShadowHandTwo>());
                }
            }
        }

        public override bool PreAI()
        {
            NPC.TargetClosest(true);
            Player targetPlayer = Main.player[NPC.target];

            if (!targetPlayer.active || targetPlayer.dead)
            {
                NPC.velocity.Y += 0.1f; 
                NPC.EncourageDespawn(10);
                return false;
            }

            Vector2 direction = targetPlayer.Center - NPC.Center;
            direction.Normalize();
            NPC.velocity = direction * speed;

            NPC.rotation = NPC.velocity.ToRotation();

            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 5)
            {
                NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
                NPC.frameCounter = 0;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Vector2 origin = new Vector2(texture.Width * 0.5F, (texture.Height / Main.npcFrameCount[NPC.type]) * 0.5F);
            SpriteEffects effects = NPC.velocity.X < 0 ? SpriteEffects.FlipVertically : SpriteEffects.None;
            spriteBatch.Draw(texture, NPC.Center - Main.screenPosition, NPC.frame, drawColor, NPC.rotation, origin, 1, effects, 0);
            return false;
        }
    }
}
