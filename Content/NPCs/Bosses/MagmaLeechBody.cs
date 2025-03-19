using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TremorMod.Content.NPCs.Bosses
{
	public class MagmaLeechBody : ModNPC
	{
		/*public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magma Leech");
		}*/

		public override void SetDefaults()
		{
            NPC.lifeMax = 250;
            NPC.damage = 15;
            NPC.defense = 10;
            NPC.width = 26;
            NPC.height = 48;
            NPC.noTileCollide = true;
            NPC.behindTiles = true;
            NPC.friendly = false;
            NPC.noGravity = true;
            NPC.aiStyle = 6;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.buffImmune[24] = true;
            NPC.buffImmune[67] = true;
            NPC.lavaImmune = true;
		}

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Main.rand.NextBool())
            {
                target.AddBuff(BuffID.OnFire, 180); // �������� ������ "�������" �� 180 ������
            }
        }


        public override void AI()
		{
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, 0f, 0f, 200, NPC.color, 1f);
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, 0f, 0f, 200, NPC.color, 1f);
			}

			if (!Main.npc[(int)NPC.ai[1]].active)
			{
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.active = false;
			}
		}

		public override bool CheckActive()
		{
			return false;
		}
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}

        /*public override bool PreDraw(ref Color drawColor)
        {
            Texture2D drawTexture = Main.npcTexture[NPC.type];
            Vector2 origin = new Vector2((drawTexture.Width / 2), (drawTexture.Height / Main.npcFrameCount[NPC.type]) * 0.5f);

            Vector2 drawPos = NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY);
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Main.spriteBatch.Draw(drawTexture, drawPos, NPC.frame, drawColor, NPC.rotation, origin, NPC.scale, effects, 0f);
            return false; // ������ ����������� ���������
        }*/
    }
}