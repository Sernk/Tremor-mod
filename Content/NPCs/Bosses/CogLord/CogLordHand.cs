using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Content;
using TremorMod.Content.NPCs.Bosses.CogLord;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace TremorMod.Content.NPCs.Bosses.CogLord
{
    /*
	 * npc.ai[0] = Index of the Cog Lord boss in the Main.npc array.
	 * npc.ai[1] = State manager.
	 * npc.ai[2] = Timer.
	 */

    public class CogLordHand : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cog Lord Hand");
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 20000;
            NPC.damage = 80;
            NPC.defense = 20;
            NPC.knockBackResist = 0f;
            NPC.width = 44;
            NPC.height = 84;
            NPC.aiStyle = 12;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = Item.buyPrice(0, 0, 5, 0);
        }

        private const float MaxDist = 250f;
        private bool _firstAi = true;
        private int _timer;

        public override void AI()
        {
            _timer++;
            if (_firstAi)
            {
                _firstAi = false;
                MakeArms();
            }
            if (NPC.AnyNPCs(Mod.Find<ModNPC>("CogLordProbe").Type))
            {
                NPC.dontTakeDamage = true;
            }
            else
                NPC.dontTakeDamage = false;
            if (_timer < 1000)
            {
                NPC.frame = GetFrame(1);
                NPC.damage = 80;
            }
            if (_timer >= 1000 && _timer < 1500)
            {
                NPC.frame = GetFrame(2);
                NPC.dontTakeDamage = true;
                NPC.damage = 120;
            }
            if (_timer > 1500)
            {
                _timer = 0;
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

        private Rectangle GetFrame(int number)
        {
            return new Rectangle(0, NPC.frame.Height * (number - 1), NPC.frame.Width, NPC.frame.Height);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                //Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Gores/CogLordHand").Type, 1f);
            }
        }

        private void MakeArms()
        {
            int arm = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("CogLordArm").Type, 0, 9999, 1, 1, NPC.ai[1]);
            int arm2 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("CogLordArmSecond").Type, 0, NPC.whoAmI, 0, 1, arm);
            Main.npc[arm].ai[0] = arm2;
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