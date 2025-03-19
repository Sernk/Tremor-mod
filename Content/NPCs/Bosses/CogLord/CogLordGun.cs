using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using TremorMod.Content.Projectiles;
using TremorMod.Utilities;

namespace TremorMod.Content.NPCs.Bosses.CogLord
{
    /*
	 * npc.ai[0] = Index of the Cog Lord boss in the Main.npc array.
	 * npc.ai[1] = State manager.
	 * npc.ai[2] = (Shoot) timer.
	 */
    public class CogLordGun : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cog Lord Gun");
        }

        private const int ShootRate = 120;
        private const int ShootDamage = 20;
        private const float ShootKn = 1.0f;
        private const int ShootType = 88;
        private const float ShootSpeed = 5;
        private const int ShootCount = 10;
        private const int Spread = 45;
        private const float SpreadMult = 0.045f;
        private const float MaxDist = 250f;

        private int _timeToShoot = ShootRate;

        public override void SetDefaults()
        {
            NPC.lifeMax = 20000;
            NPC.damage = 80;
            NPC.defense = 20;
            NPC.knockBackResist = 0f;
            NPC.width = 88;
            NPC.height = 46;
            NPC.aiStyle = 12;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = Item.buyPrice(0, 0, 5, 0);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                //Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gores/CogLordGun").Type, 1f);

            }
        }

        private bool _firstAi = true;
        public override void AI()
        {
            if (_firstAi)
            {
                _firstAi = false;
                MakeArms();
            }
            if (Main.npc[(int)NPC.ai[1]].type == Mod.Find<ModNPC>("CogLord").Type && Main.npc[(int)NPC.ai[1]].active)
                if (Main.player[Main.npc[(int)NPC.ai[1]].target].active)
                {
                    if (NPC.localAI[3] == 0f)
                    {
                        NPC.rotation = Helper.rotateBetween2Points(NPC.Center, Main.player[Main.npc[(int)NPC.ai[1]].target].Center);
                        if (--_timeToShoot <= 0) Shoot();
                    }
                }
            if (NPC.AnyNPCs(Mod.Find<ModNPC>("CogLordProbe").Type))
            {
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.dontTakeDamage = false;
            }
            Vector2 cogLordCenter = Main.npc[(int)NPC.ai[1]].Center;
            Vector2 distance = NPC.Center - cogLordCenter;
            if (distance.Length() >= MaxDist)
            {
                distance.Normalize();
                distance *= MaxDist;
                NPC.Center = cogLordCenter + distance;
            }
        }

        private void MakeArms()
        {
            int arm = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("CogLordArm").Type, 0, 9999, 1, 1, NPC.ai[1]);
            int arm2 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("CogLordArmSecond").Type, 0, NPC.whoAmI, 0, 1, arm);
            Main.npc[arm].ai[0] = arm2;
        }

        private void Shoot()
        {
            _timeToShoot = ShootRate;
            if (Main.npc[(int)NPC.ai[1]].target != -1)
            {
                Vector2 velocity = Helper.VelocityToPoint(NPC.Center, Main.player[Main.npc[(int)NPC.ai[1]].target].Center, ShootSpeed);
                for (int l = 0; l < 2; l++)
                {
                    velocity.X = velocity.X + Main.rand.Next(-Spread, Spread + 1) * SpreadMult;
                    velocity.Y = velocity.Y + Main.rand.Next(-Spread, Spread + 1) * SpreadMult;
                    int i = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, velocity.X, velocity.Y, ShootType, ShootDamage, ShootKn);
                    Main.projectile[i].hostile = true;
                    Main.projectile[i].friendly = false;
                }
            }
        }

        public override bool PreKill()
        {
            NPC.aiStyle = -1;
            NPC.ai[1] = -1;
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D drawTexture = TextureAssets.Npc[NPC.type].Value;
            Vector2 origin = new Vector2((drawTexture.Width / 2) * 0.5F, (drawTexture.Height / Main.npcFrameCount[NPC.type]) * 0.5F);
            Vector2 drawPos = new Vector2(
                NPC.position.X - Main.screenPosition.X + (NPC.width / 2) - (TextureAssets.Npc[NPC.type].Value.Width / 2) * NPC.scale / 2f + origin.X * NPC.scale,
                NPC.position.Y - Main.screenPosition.Y + NPC.height - TextureAssets.Npc[NPC.type].Value.Height * NPC.scale / Main.npcFrameCount[NPC.type] + 4f + origin.Y * NPC.scale + NPC.gfxOffY);
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(drawTexture, drawPos, NPC.frame, Color.White, NPC.rotation, origin, NPC.scale, effects, 0);
            return false;
        }
    }
}