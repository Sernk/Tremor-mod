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
using TremorMod.Content.Items.Armor.Sniper;
using TremorMod.Content.Items.BossLoot.TheDarkEmperor;
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
using TremorMod.Content.Items.Buffs;
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
using TremorMod.Content.Items.Armor.Chain;

namespace TremorMod.Content.NPCs.TownNPCs
{
	[AutoloadHead]
	public class Startrooper : ModNPC
	{
		public override string Texture => $"{typeof(Startrooper).NamespaceToPath()}/Startrooper";

        public override bool IsLoadingEnabled(Mod mod) => true;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Startrooper");
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
            foreach (Player player in Main.ActivePlayers)
            {
                if (!TremorSpawnEnemys.downedSpaceWhale)
                {
                    return true;
                }
            }
            return false;
        }


        public override List<string> SetNPCNameList() => new List<string>()
        {
            this.GetLocalizedValue("Name.Ripley"),
            this.GetLocalizedValue("Name.Dallas"),
            this.GetLocalizedValue("Name.Brett"),
            this.GetLocalizedValue("Name.Kane"),
            this.GetLocalizedValue("Name.Ash"),
            this.GetLocalizedValue("Name.Parker"),
            this.GetLocalizedValue("Name.Lambert")
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
                shopName = "Startrooper";
        }

        public override void AddShops()
        {
            var nightCondition = new Condition("NightTime", () => !Main.dayTime);
            var dayCondition = new Condition("DayTime", () => Main.dayTime);
            var notDownedTrinityCondition = new Condition("NotDownedTrinity", () => !TremorSpawnEnemys.downedTrinity);
            var bloodMoonCondition = new Condition("BloodMoon", () => Main.bloodMoon);
            var hasSuperBigCannonCondition = new Condition("HasSuperBigCannon", () => Main.LocalPlayer.HasItem(ModContent.ItemType<SuperBigCannon>()));

            NPCShop shop = new(Type, "Startrooper");

            shop.Add(ModContent.ItemType<Starmine>())
                .Add(ModContent.ItemType<ChainBow>())
                .Add(ModContent.ItemType<EnforcerShield>());

            shop.Add(ModContent.ItemType<SniperHelmet>(), nightCondition)
                .Add(ModContent.ItemType<SniperBreastplate>(), nightCondition)
                .Add(ModContent.ItemType<SniperBoots>(), nightCondition);

            shop.Add(ModContent.ItemType<ParatrooperLens>(), dayCondition)
                .Add(ModContent.ItemType<StartrooperFlameburstPistol>(), dayCondition);

            shop.Add(ModContent.ItemType<CosmicAssaultRifle>(), notDownedTrinityCondition, nightCondition)
                .Add(ModContent.ItemType<WartimeRocketLauncher>(), notDownedTrinityCondition);

            shop.Add(ModContent.ItemType<ParatrooperShotgun>(), bloodMoonCondition);

            shop.Add(ModContent.ItemType<SBCCannonballAmmo>(), hasSuperBigCannonCondition);

            shop.Register();
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 310;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 15;
			randExtraCooldown = 15;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = ModContent.ProjectileType<StarminePro>();
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

				for (int i = 0; i < 3; i++)
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("StartrooperNGore1").Type, 1f);
            }
		}
	}
}