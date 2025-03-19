using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using TremorMod.Content.NPCs;
using TremorMod.Content.NPCs.Bosses.TikiTotem;
using TremorMod.Content.Items;
using TremorMod.Content.Tiles;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using TremorMod.Content.NPCs.Bosses.SpaceWhale;

namespace TremorMod.Utilities
{
    public partial class TremorSpawnEnemys : ModSystem
    {
        public static bool downedTikiTotem = false;
        public static bool downedAncienDragon = false;
        public static bool downedTrinity = false;
        public static bool downedAlchemaster = false;
        public static bool downedRukh = false;
        public static bool downedSpaceWhale = false;
        public static bool downedMotherboard = false;
        public static bool downedCyberKing = false;
        public static bool downedTitan = false;
        public static bool spawnedAngeliteLast = false;
        public static bool downedCogLord = false;
        public static bool downedEvilCorn = false;
        public static bool downedTheDarkEmperor = false;
        public static bool downedFrostKing = false;
        public static bool downedTrueAndas = false;
        public static bool downedHeaterOfWorldsHead = false;
        public static bool downedFungusBeetle = false;
        public static bool downedPixieQueen = false;
        public static bool downedBrutallisk = false;
        public static bool downedWallOfShadow = false;
        public static bool spawnedOreAlready = false;
        public static bool spawnedCollapsium = false;
        public static bool spawnedAngelite = false;

        public override void OnWorldLoad()
        {
            downedTikiTotem = false; // Изначально босс не убит
            downedAncienDragon = false; 
            downedTrinity = false;
            downedRukh = false;
            downedSpaceWhale = false;
            spawnedAngeliteLast = false;
            downedAlchemaster = false;
            downedMotherboard = false;
            downedCyberKing = false;
            downedTitan = false;
            downedCogLord = false;
            downedEvilCorn = false;
            downedTheDarkEmperor = false;
            downedFrostKing = false;
            downedTrueAndas = false;
            downedHeaterOfWorldsHead = false;
            downedFungusBeetle = false;
            downedPixieQueen = false;
            downedBrutallisk = false;
            downedWallOfShadow = false;
            spawnedOreAlready = false;
            spawnedCollapsium = false;
            spawnedAngelite = false;

        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["downedTikiTotem"] = downedTikiTotem; // Сохраняем состояние в мире
            tag["downedAncienDragon"] = downedAncienDragon; 
            tag["downedTrinity"] = downedTrinity;
            tag["spawnedAngeliteLast"] = spawnedAngeliteLast;
            tag["downedAlchemaster"] = downedAlchemaster;
            tag["downedMotherboard"] = downedMotherboard;
            tag["downedRukh"] = downedRukh;
            tag["downedSpaceWhale"] = downedSpaceWhale;
            tag["downedCyberKing"] = downedCyberKing;
            tag["downedTitan "] = downedTitan;
            tag["downedCogLord "] = downedCogLord;
            tag["downedEvilCorn "] = downedEvilCorn;
            tag["downedTheDarkEmperor "] = downedTheDarkEmperor;
            tag["downedFrostKing"] = downedFrostKing;
            tag["downedTrueAndas"] = downedTrueAndas;
            tag["downedHeaterOfWorldsHead"] = downedHeaterOfWorldsHead;
            tag["downedFungusBeetle"] = downedFungusBeetle;
            tag["downedPixieQueen"] = downedPixieQueen;
            tag["downedBrutallisk"] = downedBrutallisk;
            tag["downedWallOfShadow"] = downedWallOfShadow;
            tag["spawnedOreAlready"] = spawnedOreAlready;
            tag["spawnedCollapsium"] = spawnedCollapsium;
            tag["spawnedAngelite"] = spawnedAngelite;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            downedTikiTotem = tag.GetBool("downedTikiTotem"); // Загружаем состояние из мира
            downedTrinity = tag.GetBool("downedTrinity");
            downedAncienDragon = tag.GetBool("downedAncienDragon");
            spawnedAngeliteLast = tag.GetBool("spawnedAngeliteLast");
            downedAlchemaster = tag.GetBool("downedAlchemaster");
            downedMotherboard = tag.GetBool("downedMotherboard");
            downedRukh = tag.GetBool("downedRukh");
            downedSpaceWhale = tag.GetBool("downedSpaceWhale");
            downedCyberKing = tag.GetBool("downedCyberKing");
            downedTitan = tag.GetBool("downedTitan ");
            downedCogLord = tag.GetBool("downedCogLord ");
            downedEvilCorn = tag.GetBool("downedEvilCorn ");
            downedTheDarkEmperor = tag.GetBool("downedTheDarkEmperor ");
            downedFrostKing = tag.GetBool("downedFrostKing");
            downedTrueAndas = tag.GetBool("downedTrueAndas");
            downedHeaterOfWorldsHead = tag.GetBool("downedHeaterOfWorldsHead");
            downedFungusBeetle = tag.GetBool("downedFungusBeetle");
            downedPixieQueen = tag.GetBool("downedPixieQueen");
            downedBrutallisk = tag.GetBool("downedBrutallisk");
            downedWallOfShadow = tag.GetBool("downedWallOfShadow");
            spawnedOreAlready = tag.GetBool("spawnedOreAlready");
            spawnedCollapsium = tag.GetBool("spawnedCollapsium");
            spawnedAngelite = tag.GetBool("spawnedAngelite");
        }
    }
}