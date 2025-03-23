using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using TremorMod;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using TremorMod.Content.Biomes.Ice;
using TremorMod.Content.Biomes.Ice.Items.Furniture;
using TremorMod.Content.Biomes.Ruins.Tiles;
using StructureHelper.API;
using static StructureHelper.API.Generator;

namespace TremorMod.Utilities
{
    public class Ruin : ModSystem
    {
        private List<Point16> placedRuins = new List<Point16>();

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex == -1)
            {
                // Shinies pass removed by some other mod.
                return;
            }
            tasks.Insert(ShiniesIndex + 1, new PassLegacy("Generating dungeon", (progress, configuration) =>
            {
                progress.Message = "Generating ruins...";

                CreateRuin();
            }));
        }

        private void CreateRuin()
        {
            int worldWidth = Main.maxTilesX;
            int worldHeight = Main.maxTilesY;

            for (int i = 0; i < 4; i++)
            {
                PlaceRuin("Structures/Ruin3", worldWidth, worldHeight, 1000);
            }

            for (int i = 0; i < 10; i++)
            {
                PlaceRuin("Structures/Ruin2", worldWidth, worldHeight, 650);
            }

            for (int i = 0; i < 15; i++)
            {
                PlaceRuin("Structures/Ruin1", worldWidth, worldHeight, 500);
            }
        }

        private void PlaceRuin(string structurePath, int worldWidth, int worldHeight, int minDistance)
        {
            int attempts = 0;
            while (attempts < 100)
            {
                int ruinX = WorldGen.genRand.Next(100, worldWidth - 100);
                int ruinY = WorldGen.genRand.Next(worldHeight - 1000, worldHeight - 500);
                Point16 newRuin = new Point16(ruinX, ruinY);

                bool tooClose = false;
                foreach (var ruin in placedRuins)
                {
                    if (Vector2.Distance(new Vector2(ruinX, ruinY), new Vector2(ruin.X, ruin.Y)) < minDistance)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    GenerateStructure(structurePath, newRuin, TremorMod.Instance);
                    placedRuins.Add(newRuin);
                    break;
                }
                attempts++;
            }
        }
    }
}