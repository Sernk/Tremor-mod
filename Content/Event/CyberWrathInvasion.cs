using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TremorMod.Content.NPCs.Invasion.ParadoxTitan;
using TremorMod.Content.NPCs.Invasion;

namespace TremorMod.Content.Event
{
	public class CyberWrathInvasion : ModPlayer
	{
		public override void UpdateDead()
		{

		}

        public override void PostUpdateBuffs()
        {
            if (InvasionWorld.CyberWrath)
            {
                InvasionWorld.CyberWrathPoints = InvasionWorld.CyberWrathPoints1;

                ModContent.GetInstance<CyberWrathUISystem>().UpdateProgress(InvasionWorld.CyberWrathPoints1);
            }
        }

        public override void PostUpdate()
		{
			//bool First = true;
			const int XOffset = 400;
			const int YOffset = 400;

			CyberWrathInvasion modPlayer = Player.GetModPlayer<CyberWrathInvasion>();
			if (!InvasionWorld.CyberWrath)
			{
				InvasionWorld.CyberWrathPoints1 = 0;
			}

            if (InvasionWorld.CyberWrath)
            {
                //Main.spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(200, 200, 200) * 0.5f);
                if (Main.rand.Next(700) == 1)
                    NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)Player.Center.X + XOffset, (int)Player.Center.Y, Mod.Find<ModNPC>("InvisibleSoul").Type);
                if (Main.rand.Next(700) == 1)
                    NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)Player.Center.X - XOffset, (int)Player.Center.Y, Mod.Find<ModNPC>("InvisibleSoul").Type);

                if (Main.rand.Next(150) == 1)
                    NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)Player.Center.X + XOffset, (int)Player.Center.Y + YOffset, Mod.Find<ModNPC>("ParadoxBat").Type);
                if (Main.rand.Next(150) == 1)
                    NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)Player.Center.X - XOffset, (int)Player.Center.Y - YOffset, Mod.Find<ModNPC>("ParadoxBat").Type);

                if (Main.rand.Next(500) == 1)
                    NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)Player.Center.X + XOffset, (int)Player.Center.Y + YOffset, Mod.Find<ModNPC>("ParadoxSun").Type);
                if (Main.rand.Next(500) == 1)
                    NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)Player.Center.X - XOffset, (int)Player.Center.Y + YOffset, Mod.Find<ModNPC>("ParadoxSun").Type);
            }

            InvasionWorld.CyberWrathPoints = InvasionWorld.CyberWrathPoints1;

            if (InvasionWorld.CyberWrathPoints1 == 15)
            {
                NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)Player.Center.X, (int)Player.Center.Y - YOffset, Mod.Find<ModNPC>("Violeum").Type);
                InvasionWorld.CyberWrathPoints1 = 16;
            }

            if (InvasionWorld.CyberWrathPoints1 == 35)
            {
                NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)Player.Center.X, (int)Player.Center.Y - YOffset, Mod.Find<ModNPC>("Violeum").Type);
                InvasionWorld.CyberWrathPoints1 = 36;
            }

            if (InvasionWorld.CyberWrathPoints1 == 50)
            {
                NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)Player.Center.X, (int)Player.Center.Y - YOffset, Mod.Find<ModNPC>("Violeum").Type);
                InvasionWorld.CyberWrathPoints1 = 51;
            }

            if (InvasionWorld.CyberWrathPoints1 == 85)
            {
                NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)Player.Center.X, (int)Player.Center.Y - YOffset, Mod.Find<ModNPC>("Violeum").Type);
                InvasionWorld.CyberWrathPoints1 = 86;
            }

            if (InvasionWorld.CyberWrathPoints1 >= 100 && !NPC.AnyNPCs(Mod.Find<ModNPC>("Titan").Type))
            {
                //Main.NewText("Wave 1: Complete!", 255, 255, 0);
                //Main.NewText("Wave 2: Complete 0%", 0, 255, 255);
                InvasionWorld.CyberWrath = false;
            }

            if (InvasionWorld.CyberWrathPoints1 > 100 && !NPC.AnyNPCs(Mod.Find<ModNPC>("Titan").Type))
            {
                InvasionWorld.CyberWrathPoints1 = 100;
            }

            if (InvasionWorld.CyberWrathPoints1 > 98 && NPC.AnyNPCs(Mod.Find<ModNPC>("Titan").Type))
            {
                InvasionWorld.CyberWrathPoints1 = 98;
            }

            if (NPC.AnyNPCs(Mod.Find<ModNPC>("Titan_").Type) && InvasionWorld.CyberWrathPoints1 == 98)
            {
                InvasionWorld.CyberWrathPoints1 = 98;
            }

            if (NPC.AnyNPCs(Mod.Find<ModNPC>("Titan").Type) && InvasionWorld.CyberWrathPoints1 == 98)
            {
                InvasionWorld.CyberWrathPoints1 = 98;
            }

            if (!NPC.AnyNPCs(Mod.Find<ModNPC>("Titan_").Type) && InvasionWorld.CyberWrath && InvasionWorld.CyberWrathPoints1 < 98)
            {
                NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)Player.Center.X, (int)Player.Center.Y - 200, Mod.Find<ModNPC>("Titan_").Type);
            }

            /*if (modPlayer.CyberWrathPoints1 == 94)
            {
            NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)player.position.X, (int)player.position.Y - 200, mod.NPCType("Titan"));
            Main.NewText("Your life happens?", Color.Red.R, Color.Orange.G, Color.Red.B);
            modPlayer.CyberWrathPoints1 = 95;
            } */

            if (NPC.AnyNPCs(Mod.Find<ModNPC>("Titan").Type) && InvasionWorld.CyberWrathPoints1 > 98)
            {
                InvasionWorld.CyberWrathPoints1 = 98;
            }

            //if (NPC.AnyNPCs(Mod.Find<ModNPC>("Zerokk").Type) && InvasionWorld.CyberWrathPoints1 == 98)
            //{
            //    InvasionWorld.CyberWrathPoints1 = 98;
            //}

            if (NPC.AnyNPCs(Mod.Find<ModNPC>("Titan_").Type) && InvasionWorld.CyberWrathPoints1 > 98)
            {
                InvasionWorld.CyberWrathPoints1 = 98;
            }

            //if (NPC.AnyNPCs(Mod.Find<ModNPC>("Zerokk").Type) && InvasionWorld.CyberWrathPoints1 > 98)
            //{
            //    InvasionWorld.CyberWrathPoints1 = 98;
            //}

            if (InvasionWorld.CyberWrathPoints1 == 100 && !NPC.AnyNPCs(Mod.Find<ModNPC>("Titan_").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Titan").Type))
            {
                Main.NewText("Paradox Cohort has been defeated!", 39, 86, 134);
                InvasionWorld.CyberWrathPoints1 = 0;
            }
        }

		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			return true;
		}

		/*public override void UpdateBiomeVisuals()
		{
			bool usePurity = InvasionWorld.CyberWrath;
			Player.ManageSpecialBiomeVisuals("Tremor:Invasion", usePurity);
		}*/
	}
}
