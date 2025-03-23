using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace TremorMod.Content.Event
{
    public class CyberWrathProgressBar : UIState
    {
        private Texture2D[] systemTextures; 
        private UIText progressText;

        public override void OnInitialize()
        {
            systemTextures = new Texture2D[11];
            for (int i = 0; i <= 10; i++)
            {
                systemTextures[i] = ModContent.Request<Texture2D>($"TremorMod/Assets/Textures/System{(10 - i)}").Value;
            }

            progressText = new UIText("System10");
            progressText.Top.Set(5, 0);
            progressText.Left.Set(10, 0);
            Append(progressText);
        }

        public void UpdateProgress(int progress)
        {
            string systemLevel = GetSystemLevel(progress);
            progressText.SetText(systemLevel);
        }

        private string GetSystemLevel(int progress)
        {
            if (progress >= 98) return "System";
            if (progress >= 90) return "System1";
            if (progress >= 80) return "System2";
            if (progress >= 70) return "System3";
            if (progress >= 60) return "System4";
            if (progress >= 50) return "System5";
            if (progress >= 40) return "System6";
            if (progress >= 30) return "System7";
            if (progress >= 20) return "System8";
            if (progress >= 10) return "System9";
            return "System10";
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            Texture2D currentTexture = GetCurrentTexture(InvasionWorld.CyberWrathPoints1);

            if (currentTexture != null)
            {
                Vector2 position = new Vector2(Main.screenWidth / 2 - currentTexture.Width / 2, 50); 
                spriteBatch.Draw(currentTexture, position, Color.White);
            }
        }

        private Texture2D GetCurrentTexture(int progress)
        {
            if (progress >= 98) return systemTextures[0]; // System
            if (progress >= 90) return systemTextures[1];  // System1
            if (progress >= 80) return systemTextures[2];  // System2
            if (progress >= 70) return systemTextures[3];  // System3
            if (progress >= 60) return systemTextures[4];  // System4
            if (progress >= 50) return systemTextures[5];  // System5
            if (progress >= 40) return systemTextures[6];  // System6
            if (progress >= 30) return systemTextures[7];  // System7
            if (progress >= 20) return systemTextures[8];  // System8
            if (progress >= 10) return systemTextures[9];  // System9
            return systemTextures[10]; // System10
        }
    }
}