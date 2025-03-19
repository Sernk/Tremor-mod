using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using System;
using System.Collections.Generic;
using TremorMod.Content.Items.Armor.Leather;
using TremorMod.Content.Buffs;
using TremorMod.Content.Items.Armor.Zerokk;
using TremorMod.Content.Items.Armor.Hummer;
using TremorMod.Content.Items.Weapons.Alchemical;
using TremorMod.Content.Items.Accessories;
using TremorMod.Content.Items.BossSumonItems;
using TremorMod.Content.Items.CogLordItems;
using TremorMod.Content.Items;
using TremorMod.Content.Items.CraftingStations;
using TremorMod.Content.Items.Crystal;
using TremorMod.Content.Items.CyberKing;
using TremorMod.Content.Items.EvilCornItems;
using TremorMod.Content.Items.Fish;
using TremorMod.Content.Items.Fungus;
using TremorMod.Content.Items.HeaterOfWorldsItems;
using TremorMod.Content.Items.Key;
using TremorMod.Content.Items.Materials;
using TremorMod.Content.Items.NPCsDrop;
using TremorMod.Content.Items.Placeable;
using TremorMod.Content.Items.SpaceWhaleItems;
using TremorMod.Content.Items.Tools;
using TremorMod.Content.Items.Vanity;
using TremorMod.Content.Items.Weapons;
using TremorMod.Content.Items.Weapons.Magic;
using TremorMod.Content.Items.Weapons.Melee;
using TremorMod.Content.Items.Weapons.Ranged;
using TremorMod.Content.Items.Weapons.Summon;
using TremorMod.Content.Items.Weapons.Throwing;
using TremorMod.Content.Items.Wood;
using TremorMod.Content.Projectiles;
using TremorMod.Utilities;
using TremorMod;

namespace TremorMod.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class Alchemist : ModNPC
    {
        public override string Texture => $"{typeof(Alchemist).NamespaceToPath()}/Alchemist";

        public override bool IsLoadingEnabled(Mod mod) => true;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 25;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 5;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 1000;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 30;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 30;
            NPC.height = 44;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.GoblinTinkerer;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            if (!NPC.downedBoss1)
                return false;

            foreach (Player player in Main.ActivePlayers)
            {
                if (!player.dead)
                {
                    return true;
                }
            }

            return false;
        }



        public override List<string> SetNPCNameList() => new List<string>
        {
            "Rizo",
            "Albert",
            "Bernando",
            "Seefeld",
            "Raymond",
            "Paracelsus",
            "Nerxius"
        };

        public override string GetChat() => "Love is just a chain of chemical reactions.";

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Lang.inter[28].Value;
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
                shopName = "AlchemistShop";
        }

        public override void AddShops()
        {
            var bossCondition = new Condition("DownedEaterOrBrain", () => Condition.DownedEaterOfWorlds.IsMet() || Condition.DownedBrainOfCthulhu.IsMet());
            var dayCondition = new Condition("DayTime", () => Main.dayTime);
            var nightCondition = new Condition("NightTime", () => !Main.dayTime);
            var notHardmodeCondition = new Condition("NotHardmode", () => !Main.hardMode);
            var zoneSnowCondition = new Condition("ZoneSnow", () => Main.LocalPlayer.ZoneSnow);
            var ZoneJungleCondition = new Condition("ZoneJungle", () => Main.LocalPlayer.ZoneJungle);
            var downedBoss3Condition = new Condition("DownedBoss3", () => NPC.downedBoss3);
            var downedGolemBossCondition = new Condition("DownedGolemBoss", () => NPC.downedGolemBoss);
            var downedPlantBossCondition = new Condition("DownedPlantBoss", () => NPC.downedPlantBoss);

            NPCShop shop = new(Type, "AlchemistShop"); 

            shop.Add(ModContent.ItemType<BasicFlask>())
                .Add(ModContent.ItemType<HazardousChemicals>())
                .Add(ItemID.StinkPotion)
                .Add(ItemID.LovePotion);

            if (!TremorSpawnEnemys.downedAlchemaster)
            {
                shop.Add(ModContent.ItemType<Pyro>());
            }

            shop.Add(ModContent.ItemType<BigHealingFlack>(), Condition.Hardmode, dayCondition);
            shop.Add(ModContent.ItemType<BigManaFlask>(), Condition.Hardmode, nightCondition);
            shop.Add(ModContent.ItemType<BlackCauldron>(), Condition.Hardmode)
                .Add(ModContent.ItemType<LesserVenomFlask>(), Condition.Hardmode)
                .Add(ModContent.ItemType<ConcentratedTincture>(), Condition.Hardmode);

            shop.Add(ModContent.ItemType<LesserHealingFlack>(), notHardmodeCondition, dayCondition);
            shop.Add(ModContent.ItemType<LesserManaFlask>(), notHardmodeCondition, nightCondition);

            shop.Add(ModContent.ItemType<HealthSupportFlask>())
                .Add(ModContent.ItemType<ManaSupportFlask>());

            shop.Add(ModContent.ItemType<FreezeFlask>(), zoneSnowCondition);
            shop.Add(ModContent.ItemType<LesserPoisonFlask>(), ZoneJungleCondition);

            shop.Add(ModContent.ItemType<BoomFlask>(), Condition.DownedEyeOfCthulhu);
            shop.Add(ModContent.ItemType<Nitro>(), bossCondition)
                .Add(ModContent.ItemType<BurningFlask>(), bossCondition);
            shop.Add(ModContent.ItemType<GoldFlask>(), downedBoss3Condition);

            shop.Add(ModContent.ItemType<CthulhuBlood>(), downedGolemBossCondition);

            shop.Add(ModContent.ItemType<AlchemistGlove>(), downedPlantBossCondition, Condition.BloodMoon);

            shop.Register(); 
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 10;
            randExtraCooldown = 10;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<BasicFlaskPro>();
            attackDelay = 5;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 151, 2.5f * hit.HitDirection, -2.5f);

                for (int i = 0; i < 3; i++)
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("AlchemistGore1").Type, 1f);
            }
        }
    }
}