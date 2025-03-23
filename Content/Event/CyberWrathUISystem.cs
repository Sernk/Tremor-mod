using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace TremorMod.Content.Event
{
    public class CyberWrathUISystem : ModSystem
    {
        private UserInterface cyberWrathInterface;
        private CyberWrathProgressBar progressBar;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                progressBar = new CyberWrathProgressBar();
                cyberWrathInterface = new UserInterface();
                cyberWrathInterface.SetState(progressBar);
            }
        }

        public void UpdateProgress(int progress)
        {
            progressBar.UpdateProgress(progress);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (InvasionWorld.CyberWrath)
            {
                cyberWrathInterface?.Update(gameTime);
                progressBar.UpdateProgress(InvasionWorld.CyberWrathPoints1);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "TremorMod: Cyber Wrath Progress Bar",
                    delegate
                    {
                        if (InvasionWorld.CyberWrath)
                        {
                            cyberWrathInterface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}