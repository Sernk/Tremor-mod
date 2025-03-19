using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using TremorMod.Content.Items.CyberKing;
using TremorMod.Content.NPCs.Bosses;
using TremorMod.Content.Items.Materials.OreAndBar;
using TremorMod.Content.NPCs.Bosses.AncienDragon;
using TremorMod.Content.NPCs.Bosses.Alchemaster;
using TremorMod.Content.Items.BossSumonItems;
using TremorMod.Content.Items.Vanity;
using TremorMod.Content.Items.Placeable;
using TremorMod.Content.Items.Weapons.Alchemical;
using TremorMod.Content.Items.Weapons.Melee;
using TremorMod.Content.Items.Accessories;
using TremorMod.Content.Items.Bag;
using TremorMod.Content.Items;
using TremorMod.Content.Items.Weapons.Magic;
using TremorMod.Content.NPCs.Invasion.ParadoxTitan;
using TremorMod.Content.Items.Materials;
using TremorMod.Content.Items.Weapons.Ranged;
using TremorMod.Content.Items.Weapons.Summon;
using TremorMod.Content.Items.Weapons.Throwing;
using TremorMod.Content.NPCs.Bosses.CogLord;
using TremorMod.Content.Items.CogLordItems;
using TremorMod.Content.NPCs.Bosses.SpaceWhale;
using TremorMod.Content.Items.SpaceWhaleItems;
using TremorMod.Content.Items.EvilCornItems;
using TremorMod.Content.NPCs.Bosses.EvilCorn;
using TremorMod.Content.NPCs.Bosses.Rukh;
using TremorMod.Content.NPCs.Bosses.TheDarkEmperor;
using TremorMod.Content.Items.BossLoot.TheDarkEmperor;
using TremorMod.Content.NPCs.Bosses.FrostKing;
using TremorMod.Content.NPCs.Bosses.AndasBoss;
using TremorMod.Content.Items.AndasItems;
using TremorMod.Content.NPCs.Bosses.Motherboard;
using TremorMod.Content.NPCs.Bosses.FungusBeetle;
using TremorMod.Content.NPCs.Bosses;
using TremorMod.Content.Items.Fungus;
using TremorMod.Content.Items.HeaterOfWorldsItems;
using TremorMod.Content.NPCs.Bosses.TikiTotem;
using TremorMod.Content.NPCs.Bosses.PixieQueen;
using TremorMod.Content.Items.BossLoot.TikiTotem;
using TremorMod.Content.NPCs.Bosses.Brutallisk;
using TremorMod.Content.NPCs.Bosses.WallofShadows;
using TremorMod.Content.NPCs.Bosses.Trinity;
using TremorMod.Content.NPCs.Bosses.Jellyfish;

namespace TremorMod.Utilities
{
    public class ModSupport : ModSystem
    {
        public override void PostSetupContent()
        {
            DoBossChecklistIntegration();
        }

        private void DoBossChecklistIntegration()
        {

            if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
            {
                return;
            }

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalName = "Alchemaster";

            float weight = 7.3f;

            Func<bool> downed = () => !TremorSpawnEnemys.downedAlchemaster;

            int bossType = ModContent.NPCType<Alchemaster>();

            int spawnItem = ModContent.ItemType<AncientMosaic>();

            List<int> collectibles = new List<int>()
            {
                ModContent.ItemType<TheGlorch>(),
                ModContent.ItemType<LongFuse>(),
                ModContent.ItemType<PlagueFlask>(),
                ModContent.ItemType<AlchemasterMask>(),
                ModContent.ItemType<BadApple>(),
                ModContent.ItemType<AlchemasterTreasureBag>(),
                ModContent.ItemType<AlchemasterTrophy>()
            };

            // By default, it draws the first frame of the boss, omit if you don't need custom drawing
            // But we want to draw the bestiary texture instead, so we create the code for that to draw centered on the intended location
            //var customPortrait = (SpriteBatch sb, Rectangle rect, Color color) => {
            //    Texture2D texture = ModContent.Request<Texture2D>("ExampleMod/Assets/Textures/Bestiary/MinionBoss_Preview").Value;
            //    Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
            //    sb.Draw(texture, centered, color);
            //};

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalName,
                weight,
                downed,
                bossType,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItem,
                    ["collectibles"] = collectibles,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameAncientDragon = "AncientDragon";

            float weightAncientDragon = 5.7f;

            Func<bool> downedAncientDragon = () => !TremorSpawnEnemys.downedAncienDragon;

            int bossTypeAncientDragon = ModContent.NPCType<Dragon_HeadB>();

            int spawnItemAncientDragon = ModContent.ItemType<RustyLantern>();

            List<int> collectiblesAncientDragon = new List<int>()
            {
                ModContent.ItemType<AncientDragonTrophy>(),
                ModContent.ItemType<AncientDragonMask>(),
                ModContent.ItemType<Swordstorm>(),
                ModContent.ItemType<DragonHead>(),
                ModContent.ItemType<AncientTimesEdge>(),
                ModContent.ItemType<AncientDragonBag>(),
            };

            //var customPortrait = (SpriteBatch sb, Rectangle rect, Color color) => {
            //    Texture2D texture = ModContent.Request<Texture2D>("ExampleMod/Assets/Textures/Bestiary/MinionBoss_Preview").Value;
            //    Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
            //    sb.Draw(texture, centered, color);
            //};

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameAncientDragon,
                weightAncientDragon,
                downedAncientDragon,
                bossTypeAncientDragon,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemAncientDragon,
                    ["collectibles"] = collectiblesAncientDragon,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameCyberKing = "CyberKing";

            float weightCyberKing = 13.6f;

            Func<bool> downedCyberKing = () => !TremorSpawnEnemys.downedCyberKing;

            int bossTypeCyberKing = ModContent.NPCType<CyberKing>();

            int spawnItemCyberKing = ModContent.ItemType<AdvancedCircuit>();

            List<int> collectiblesCyberKing = new List<int>()
            {
                ModContent.ItemType<CyberKingTrophy>(),
                ModContent.ItemType<CyberKingMask>(),
                ModContent.ItemType<RedStorm>(),
                ModContent.ItemType<ShockwaveClaymore>(),
                ModContent.ItemType<CyberCutter>(),
                ModContent.ItemType<CyberKingBag>(),
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameCyberKing,
                weightCyberKing,
                downedCyberKing,
                bossTypeCyberKing,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemCyberKing,
                    ["collectibles"] = collectiblesCyberKing,
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameTitan = "Titan";

            float weightTitan = 18.6f;

            Func<bool> downedTitan = () => !TremorSpawnEnemys.downedTitan;

            int bossTypeTitan = ModContent.NPCType<Titan>();

            int spawnItemTitan = ModContent.ItemType<AncientWatch>();

            List<int> collectiblesTitan = new List<int>()
            {
                ModContent.ItemType<ParadoxTitanMask>(),
                ModContent.ItemType<TimeTissue>(),
                ModContent.ItemType<RocketWand>(),
                ModContent.ItemType<TheEtherealm>(),
                ModContent.ItemType<SoulFlames>(),
                ModContent.ItemType<ParadoxTitanTrophy>(),
                ModContent.ItemType<ParadoxTitanBag>(),
                ModContent.ItemType<VioleumWings>()
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameTitan,
                weightTitan,
                downedTitan,
                bossTypeTitan,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemTitan,
                    ["collectibles"] = collectiblesTitan,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameCogLord = "CogLord";

            float weightCogLord = 13.3f;

            Func<bool> downedCogLord = () => !TremorSpawnEnemys.downedCogLord;

            int bossTypeCogLord = ModContent.NPCType<CogLord>();

            int spawnItemCogLord = ModContent.ItemType<ArtifactEngine>();

            List<int> collectiblesCogLord = new List<int>()
            {
                ModContent.ItemType<CogLordTrophy>(),
                ModContent.ItemType<CogLordMask>(),
                ModContent.ItemType<BrassChip>(),
                ModContent.ItemType<BrassNugget>(),
                ModContent.ItemType<BrassRapier>(),
                ModContent.ItemType<BrassChainRepeater>(),
                ModContent.ItemType<BrassStave>(),
                ModContent.ItemType<CogLordBag>()
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameCogLord,
                weightCogLord,
                downedCogLord,
                bossTypeCogLord,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemCogLord,
                    ["collectibles"] = collectiblesCogLord,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameSpaceWhale = "SpaceWhale";

            float weightSpaceWhale = 18.4f;

            Func<bool> downedSpaceWhale = () => !TremorSpawnEnemys.downedSpaceWhale;

            int bossTypeSpaceWhale = ModContent.NPCType<SpaceWhale>();

            int spawnItemSpaceWhale = ModContent.ItemType<CosmicKrill>();

            List<int> collectiblesSpaceWhale = new List<int>()
            {
                ModContent.ItemType<SpaceWhaleTrophy>(),
                ModContent.ItemType<CosmicFuel>(),
                ModContent.ItemType<StarLantern>(),
                ModContent.ItemType<SpaceWhaleMask>(),
                ModContent.ItemType<BlackHoleCannon>(),
                ModContent.ItemType<HornedWarHammer>(),
                ModContent.ItemType<SDL>(),
                ModContent.ItemType<SpaceWhaleTreasureBag>(),
                ModContent.ItemType<WhaleFlippers>()
            };


            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameSpaceWhale,
                weightSpaceWhale,
                downedSpaceWhale,
                bossTypeSpaceWhale,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemSpaceWhale,
                    ["collectibles"] = collectiblesSpaceWhale,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameEvilCorn = "EvilCorn";

            float weightEvilCorn = 4.6f;

            Func<bool> downedEvilCorn = () => !TremorSpawnEnemys.downedEvilCorn;

            int bossTypeEvilCorn = ModContent.NPCType<EvilCorn>();

            int spawnItemEvilCorn = ModContent.ItemType<CursedPopcorn>();

            List<int> collectiblesEvilCorn = new List<int>()
            {
                ModContent.ItemType<EvilCornTrophy>(),
                ModContent.ItemType<EvilCornMask>(),
                ModContent.ItemType<GrayKnightHelmet>(),
                ModContent.ItemType<GrayKnightBreastplate>(),
                ModContent.ItemType<KnightGreaves>(),
                ModContent.ItemType<CornSword>(),
                ModContent.ItemType<Corn>(),
                ModContent.ItemType<CornJavelin>(),
                ModContent.ItemType<FarmerShovel>(),
                ModContent.ItemType<EvilCornBag>()
            };


            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameEvilCorn,
                weightEvilCorn,
                downedEvilCorn,
                bossTypeEvilCorn,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemEvilCorn,
                    ["collectibles"] = collectiblesEvilCorn,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameRukh = "Rukh";

            float weightRukh = 2.2f;

            Func<bool> downedRukh = () => !TremorSpawnEnemys.downedRukh;

            int bossTypeRukh = ModContent.NPCType<npcVultureKing>();

            int spawnItemRukh = ModContent.ItemType<DesertCrown>(); 

            List<int> collectiblesRukh = new List<int>()
            {
                ModContent.ItemType<VultureKingMask>(),
                ModContent.ItemType<VultureFeather>(),
                ModContent.ItemType<SandKnife>(),
                ModContent.ItemType<CactusBow>(),
                ModContent.ItemType<SandstoneBar>(),
                ModContent.ItemType<VultureKingTrophy>(),
                ModContent.ItemType<VultureKingBag>()
            };


            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameRukh,
                weightRukh,
                downedRukh,
                bossTypeRukh,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemRukh,
                    ["collectibles"] = collectiblesRukh,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameDarkE = "TheDarkEmperor";

            float weightDarkE = 18.2f;

            Func<bool> downedDarkE = () => !TremorSpawnEnemys.downedTheDarkEmperor;

            int bossTypeDarkE = ModContent.NPCType<TheDarkEmperor>();

            int spawnItemDarkE = ModContent.ItemType<EmperorCrown>();

            List<int> collectiblesDarkE = new List<int>()
            {
                ModContent.ItemType<DarkEmperorMask>(),
                ModContent.ItemType<DarkEmperorTrophy>(),
                ModContent.ItemType<DrippingScythe>(),
                ModContent.ItemType<DelightfulClump>(),
                ModContent.ItemType<NastyJavelin>(),
                ModContent.ItemType<DarkGel>(),
                ModContent.ItemType<DarkEmperorBag>(),
                ModContent.ItemType<SoulofFight>()
            };


            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameDarkE,
                weightDarkE,
                downedDarkE,
                bossTypeDarkE,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemDarkE,
                    ["collectibles"] = collectiblesDarkE,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameFrostK = "FrostKing";

            float weightFrostK = 14.2f;

            Func<bool> downedFrostK = () => !TremorSpawnEnemys.downedFrostKing;

            int bossTypeFrostK = ModContent.NPCType<FrostKing>();

            int spawnItemFrostK = ModContent.ItemType<FrostCrown>();

            List<int> collectiblesFrostK = new List<int>()
            {
                ModContent.ItemType<FrostKingTrophy>(),
                ModContent.ItemType<FrostKingMask>(),
                ModContent.ItemType<FrostoneOre>(),
                ModContent.ItemType<FrostKingBag>()
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameFrostK,
                weightFrostK,
                downedFrostK,
                bossTypeFrostK,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemFrostK,
                    ["collectibles"] = collectiblesFrostK,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameAndas = "TrueAndas";

            float weightAndas = 18.7f;

            Func<bool> downedAndas = () => !TremorSpawnEnemys.downedFrostKing;

            int bossTypeAndas = ModContent.NPCType<TrueAndas>();

            int spawnItemAndas = ModContent.ItemType<InfernoSkulll>();

            List<int> collectiblesAndas = new List<int>()
            {
                ModContent.ItemType<AndasTrophy>(),
                ModContent.ItemType<AndasMask>(),
                ModContent.ItemType<GehennaStaff>(),
                ModContent.ItemType<GehennaStaff>(),
                ModContent.ItemType<VulcanBlade>(),
                ModContent.ItemType<HellStorm>(),
                ModContent.ItemType<Inferno>(),
                ModContent.ItemType<Pandemonium>(),
                ModContent.ItemType<AndasBag>()
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameAndas,
                weightAndas,
                downedAndas,
                bossTypeAndas,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemAndas,
                    ["collectibles"] = collectiblesAndas,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameMotherboard = "Motherboard";

            float weightMotherboard = 13.7f;

            Func<bool> downedMotherboard = () => !TremorSpawnEnemys.downedMotherboard;

            int bossTypeMotherboard = ModContent.NPCType<Motherboard>();

            int spawnItemMotherboard = ModContent.ItemType<MechanicalBrain>();

            List<int> collectiblesMotherboard = new List<int>()
            {
                ModContent.ItemType<MotherboardMask>(),
                ModContent.ItemType<MotherboardTrophy>(),
                ModContent.ItemType<FlaskCore>(),
                ModContent.ItemType<SoulofMind>(),
                ModContent.ItemType<CarbonSteel>(),
                ModContent.ItemType<BenderLegs>(),
                ModContent.ItemType<MotherboardBag>()
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameMotherboard,
                weightMotherboard,
                downedMotherboard,
                bossTypeMotherboard,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemMotherboard,
                    ["collectibles"] = collectiblesMotherboard,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameHOWH = "HeaterOfWorldsHead";

            float weightHOWH = 6.6f;

            Func<bool> downedHOWH = () => !TremorSpawnEnemys.downedHeaterOfWorldsHead; 

            int bossTypeHOWHd = ModContent.NPCType<HeaterOfWorldsHead>();

            int spawnItemHOWH = ModContent.ItemType<MoltenHeart>();

            List<int> collectiblesHOWH = new List<int>()
            {
                ModContent.ItemType<MoltenParts>(),
                ModContent.ItemType<HeaterOfWorldsTrophy>(),
                ModContent.ItemType<HeaterOfWorldsMask>(),
                ModContent.ItemType<HeaterOfWorldsBag>()
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameHOWH,
                weightHOWH,
                downedHOWH,
                bossTypeHOWHd,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemHOWH,
                    ["collectibles"] = collectiblesHOWH,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameFungusB = "FungusBeetle";

            float weightFungusB = 5.6f;

            Func<bool> downedFungusB = () => !TremorSpawnEnemys.downedFungusBeetle;

            int bossTypeFungusB = ModContent.NPCType<FungusBeetle>();

            int spawnItemFungusB = ModContent.ItemType<MushroomCrystal>();

            List<int> collectiblesFungusB = new List<int>()
            {
                ModContent.ItemType<FungusBeetleMask>(),
                ModContent.ItemType<FungusElement>(),
                ModContent.ItemType<FungusBeetleTrophy>(),
                ModContent.ItemType<FungusBeetleBag>()
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameFungusB,
                weightFungusB,
                downedFungusB,
                bossTypeFungusB,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemFungusB,
                    ["collectibles"] = collectiblesFungusB,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );


            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameTikiTotem = "TikiTotem";

            float weightTikiTotem = 5.2f;

            Func<bool> downedTikiTotem = () => !TremorSpawnEnemys.downedTikiTotem;

            int bossTypeTikiTotem = ModContent.NPCType<TikiTotem>();

            int spawnItemTikiTotem = ModContent.ItemType<MysteriousDrum>();

            List<int> collectiblesTikiTotem = new List<int>()
            {
                ModContent.ItemType<ToxicBlade>(),
                ModContent.ItemType<JungleAlloy>(),
                ModContent.ItemType<PickaxeofBloom>(),
                ModContent.ItemType<AngryTotemMask>(),
                ModContent.ItemType<HappyTotemMask>(),
                ModContent.ItemType<IndifferentTotemMask>(),
                ModContent.ItemType<TikiTotemBag>(),
                ModContent.ItemType<ToxicHilt>()
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameTikiTotem,
                weightTikiTotem,
                downedTikiTotem,
                bossTypeTikiTotem,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemTikiTotem,
                    ["collectibles"] = collectiblesTikiTotem,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNamePixieQueen = "PixieQueen";

            float weightPixieQueen = 14.8f;

            Func<bool> downedPixieQueen = () => !TremorSpawnEnemys.downedPixieQueen;

            int bossTypePixieQueen = ModContent.NPCType<PixieQueen>();

            int spawnItemPixieQueen = ModContent.ItemType<PixieinaJar>();

            List<int> collectiblesPixieQueen = new List<int>()
            {
                ModContent.ItemType<PixieQueenMask>(),
                ModContent.ItemType<EtherealFeather>(),
                ModContent.ItemType<PixiePulse>(),
                ModContent.ItemType<HeartMagnet>(),
                ModContent.ItemType<PixieQueenTrophy>(),
                ModContent.ItemType<ChaosBar>(),
                ModContent.ItemType<PixieQueenBag>()
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNamePixieQueen,
                weightPixieQueen,
                downedPixieQueen,
                bossTypePixieQueen,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemPixieQueen,
                    ["collectibles"] = collectiblesPixieQueen,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameBrutallisk = "Brutallisk";

            float weightBrutallisk = 18.3f;

            Func<bool> downedBrutalliskn = () => !TremorSpawnEnemys.downedBrutallisk;

            int bossTypeBrutallisk = ModContent.NPCType<Brutallisk>();

            int spawnItemBrutallisk = ModContent.ItemType<RoyalEgg>();

            List<int> collectiblesBrutallisk = new List<int>()
            {
                ModContent.ItemType<BrutalliskMask>(),
                ModContent.ItemType<LightningStaff>(),
                ModContent.ItemType<Awakening>(),
                ModContent.ItemType<SnakeDevourer>(),
                ModContent.ItemType<QuetzalcoatlStave>(),
                ModContent.ItemType<TreasureGlaive>(),
                ModContent.ItemType<FallenSnake>(),
                ModContent.ItemType<FallenSnake>(),
                ModContent.ItemType<StrangeEgg>(),
                ModContent.ItemType<BrutalliskTrophy>(),
                ModContent.ItemType<Aquamarine>(),
                ModContent.ItemType<BrutalliskBag>()
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameBrutallisk,
                weightBrutallisk,
                downedBrutalliskn,
                bossTypeBrutallisk,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemBrutallisk,
                    ["collectibles"] = collectiblesBrutallisk,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNamePixieQueen,
                weightPixieQueen,
                downedPixieQueen,
                bossTypePixieQueen,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemPixieQueen,
                    ["collectibles"] = collectiblesPixieQueen,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameWOF = "WallofShadow";

            float weightWOF = 14.9f;

            Func<bool> downedWOF = () => !TremorSpawnEnemys.downedWallOfShadow;

            int bossTypeWOF = ModContent.NPCType<WallOfShadow>();

            int spawnItemWOF = ModContent.ItemType<ShadowRelic>();

            List<int> collectiblesWOF = new List<int>()
            {
                ModContent.ItemType<WallofShadowTrophy>(),
                ModContent.ItemType<WallofShadowMask>(),
                ModContent.ItemType<DarknessCloth>(),
                ModContent.ItemType<WallofShadowBag>(),
                ModContent.ItemType<HeavyBeamCannon>(),
                ModContent.ItemType<Bolter>(),
                ModContent.ItemType<StrikerBlade>()
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameWOF,
                weightWOF,
                downedWOF,
                bossTypeWOF,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemWOF,
                    ["collectibles"] = collectiblesWOF,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameTrinity = "Trinity";

            float weightTrinity = 18.5f;

            Func<bool> downedTrinity = () => !TremorSpawnEnemys.downedTrinity;

            int bossTypeTrinity = ModContent.NPCType<SoulofHope>();

            int spawnItemTrinity = ModContent.ItemType<StoneofKnowledge>();

            List<int> collectiblesTrinity = new List<int>()
            {
                ModContent.ItemType<HopeMask>(),
                ModContent.ItemType<Banhammer>(),
                ModContent.ItemType<BestNightmare>(),
                ModContent.ItemType<TrustMask>(),
                ModContent.ItemType<Volcannon>(),
                ModContent.ItemType<HonestBlade>(),
                ModContent.ItemType<TruthMask>(),
                ModContent.ItemType<TrebleClef>(),
                ModContent.ItemType<Revolwar>(),
                ModContent.ItemType<TrinityBag3>()
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameTrinity,
                weightTrinity,
                downedTrinity,
                bossTypeTrinity,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemTrinity,
                    ["collectibles"] = collectiblesTrinity,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalNameSJ = "StormJelly";

            float weightSJ = 4.5f;

            Func<bool> downedSJ = () => !TremorSpawnEnemys.downedBrutallisk;

            int bossTypeSJ = ModContent.NPCType<StormJellyfish>();

            int spawnItemSJ = ModContent.ItemType<StormJelly>();

            List<int> collectiblesSJ = new List<int>()
            {
                ModContent.ItemType<StormJellyfishMask>(),
                ModContent.ItemType<StormBlade>(),
                ModContent.ItemType<Poseidon>(),
                ModContent.ItemType<JellyfishStaff>(),
                ModContent.ItemType<BoltTome>(),
                ModContent.ItemType<StormJellyfishTrophy>(),
                ModContent.ItemType<StormJellyfishBag>()
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalNameSJ,
                weightSJ,
                downedSJ,
                bossTypeSJ,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItemSJ,
                    ["collectibles"] = collectiblesSJ,
                    //["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );
        }
    }       
}