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
using Terraria.GameContent.ItemDropRules;
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
	public class ForgeMaster : ModNPC
	{
		public override string Texture => $"{typeof(ForgeMaster).NamespaceToPath()}/ForgeMaster";

        public override bool IsLoadingEnabled(Mod mod) => true;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Forge Master");
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
			NPC.width = 28;
			NPC.height = 48;
			NPC.aiStyle = 7;
			NPC.damage = 10;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;
			AnimationType = NPCID.Guide;
		}

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            foreach (Player player in Main.ActivePlayers)
            {
                if (player.InventoryHas(ModContent.ItemType<JungleAlloy>()))
                {
                    return true;
                }
            }

            return false; 
        }

        public override List<string> SetNPCNameList() => new List<string>()
        {
            this.GetLocalizedValue("Name.Gefest"),
            this.GetLocalizedValue("Name.Aule"),
            this.GetLocalizedValue("Name.Agarorn"),
            this.GetLocalizedValue("Name.Treak"),
            this.GetLocalizedValue("Name.Haymer"),
            this.GetLocalizedValue("Name.Golan")
        };

        public override string GetChat()
        {
            WeightedRandom<string> dialogue = new WeightedRandom<string>();

            dialogue.Add(this.GetLocalizedValue("Chat.Normal1"));
            dialogue.Add(this.GetLocalizedValue("Chat.Normal2"));
            dialogue.Add(this.GetLocalizedValue("Chat.Normal3"));
            dialogue.Add(this.GetLocalizedValue("Chat.Normal4"));
            dialogue.Add(this.GetLocalizedValue("Chat.Normal5"));
            dialogue.Add(this.GetLocalizedValue("Chat.Normal6"));
            dialogue.Add(this.GetLocalizedValue("Chat.Normal7"));

            return dialogue;
        }

        public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Lang.inter[28].Value;
		}

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
                shopName = "ForgeMaster";
        }

        public override void AddShops()
        {
            var dayCondition = new Condition("DayTime", () => Main.dayTime);
            var nightCondition = new Condition("NightTime", () => !Main.dayTime);
            var downedBoss2Condition = new Condition("DownedBoss2", () => NPC.downedBoss2);
            var downedBoss3Condition = new Condition("DownedBoss3", () => NPC.downedBoss3);
            var downedMechBossAnyCondition = new Condition("DownedMechBossAny", () => NPC.downedMechBossAny);
            var downedPlantBossCondition = new Condition("DownedPlantBoss", () => NPC.downedPlantBoss);
            var downedGolemBossCondition = new Condition("DownedGolemBoss", () => NPC.downedGolemBoss);
            var downedAncientCultistCondition = new Condition("DownedAncientCultist", () => NPC.downedAncientCultist);
            var hardmodeCondition = new Condition("Hardmode", () => Main.hardMode);

            NPCShop shop = new(Type, "ForgeMaster");

            shop.Add(ModContent.ItemType<GreatAnvil>());

            shop.Add(ItemID.CopperBar, dayCondition)
                .Add(ItemID.IronBar, dayCondition)
                .Add(ItemID.SilverBar, dayCondition);

            shop.Add(ItemID.GoldBar, dayCondition, downedBoss2Condition);
            shop.Add(ItemID.DemoniteBar, dayCondition, downedBoss3Condition);
            shop.Add(ItemID.CobaltBar, dayCondition, downedMechBossAnyCondition)
                .Add(ItemID.MythrilBar, dayCondition, downedMechBossAnyCondition)
                .Add(ItemID.AdamantiteBar, dayCondition, downedMechBossAnyCondition);

            shop.Add(ItemID.TinBar, nightCondition)
                .Add(ItemID.LeadBar, nightCondition)
                .Add(ItemID.TungstenBar, nightCondition);

            shop.Add(ItemID.PlatinumBar, nightCondition, downedBoss2Condition);
            shop.Add(ItemID.CrimtaneBar, nightCondition, downedBoss3Condition);
            shop.Add(ItemID.PalladiumBar, nightCondition, downedMechBossAnyCondition)
                .Add(ItemID.OrichalcumBar, nightCondition, downedMechBossAnyCondition)
                .Add(ItemID.TitaniumBar, nightCondition, downedMechBossAnyCondition);

            shop.Add(ModContent.ItemType<PoisonRod>(), downedBoss2Condition);

            shop.Add(ModContent.ItemType<BurningHammer>(), downedBoss3Condition)
                .Add(ModContent.ItemType<PerfectBehemoth>(), downedBoss3Condition);

            shop.Add(ItemID.HallowedBar, downedPlantBossCondition);

            shop.Add(ItemID.ChlorophyteBar, downedGolemBossCondition);

            shop.Add(ItemID.SpectreBar, downedAncientCultistCondition);

            shop.Add(ModContent.ItemType<GoldenMace>(), hardmodeCondition)
                .Add(ItemID.HellstoneBar, hardmodeCondition);

            shop.Register(); 
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 22;
			knockback = 3f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 10;
			randExtraCooldown = 10;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = ModContent.ProjectileType<BurningHammerPro>();
			attackDelay = 4;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 2f;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
            int hitDirection = hit.HitDirection;

            if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 151, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);

				for (int i = 0; i < 4; ++i)
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FarmerGore1").Type, 1f);
            }
		}
	}
}