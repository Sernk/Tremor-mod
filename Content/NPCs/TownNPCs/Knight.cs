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
using TremorMod.Content.Items.Armor.Chain;

namespace TremorMod.Content.NPCs.TownNPCs
{
	[AutoloadHead]
	public class Knight : ModNPC
	{
		public override string Texture => $"{typeof(Knight).NamespaceToPath()}/Knight";

        public override bool IsLoadingEnabled(Mod mod) => true;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Knight");
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
                if (!player.dead)
                {
                    return true;
                }
            }

            return false;
        }


        private readonly WeightedRandom<string> _names = new[]
		{
			"Wheatly",
			"Daniel:3",
			"Crox",
			"Geralt:2",
			"Roland",
			"Hodor:4"
		}.ToWeightedCollectionWithWeight();

        public override List<string> SetNPCNameList() => new List<string> { _names.Get() };

        private readonly WeightedRandom<string> _chats = new[]
		{
			"Well met, brave adventurer.",
			"A balanced weapon can mean the difference between victory and defeat.",
			"I am not overly fond of the bovine hordes. Best to leave them alone, really.",
			"Do you have a weapon? Needs about 20% more coolness!",
			"Hail and good morrow my Liege!",
			"I was in a strange castle one day. There were mechanical things saying EXTERMINATE. Were they your minions?",
			"Have you ever met a knight whose name is Sir Uncle Slime? He is a good friend of mine."
		}.ToWeightedCollection();

		public override string GetChat()
			=> Name == "Hodor" ? Name : _chats.Get();

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Lang.inter[28].Value;
		}

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
                shopName = "Knight";
        }

        public override void AddShops()
        {
            var downedBoss1Condition = new Condition("DownedBoss1", () => NPC.downedBoss1);
            var downedBoss2Condition = new Condition("DownedBoss2", () => NPC.downedBoss2);
            var downedBoss3Condition = new Condition("DownedBoss3", () => NPC.downedBoss3);
            var hardmodeCondition = new Condition("Hardmode", () => Main.hardMode);
            var bloodMoonCondition = new Condition("BloodMoon", () => Main.bloodMoon);
            var downedMechBossAnyCondition = new Condition("DownedMechBossAny", () => NPC.downedMechBossAny);

            NPCShop shop = new(Type, "Knight");

            shop.Add(ModContent.ItemType<ThrowingAxe>())
                .Add(ModContent.ItemType<RustySword>())
                .Add(ModContent.ItemType<RipperKnife>());

            shop.Add(ModContent.ItemType<TombRaider>(), downedBoss1Condition)
                .Add(ModContent.ItemType<SpikeShield>(), downedBoss1Condition)
                .Add(ModContent.ItemType<ChainCoif>(), downedBoss1Condition)
                .Add(ModContent.ItemType<Chainmail>(), downedBoss1Condition)
                .Add(ModContent.ItemType<ChainGreaves>(), downedBoss1Condition);

            shop.Add(ModContent.ItemType<TwilightHorns>(), downedBoss2Condition)
                .Add(ModContent.ItemType<ToxicRazorknife>(), downedBoss2Condition);

            shop.Add(ModContent.ItemType<NecromancerClaymore>(), downedBoss3Condition)
                .Add(ModContent.ItemType<Shovel>(), downedBoss3Condition);

            shop.Add(ModContent.ItemType<GoldenThrowingAxe>(), hardmodeCondition);

            shop.Add(ModContent.ItemType<Oppressor>(), hardmodeCondition, bloodMoonCondition);

            shop.Add(ModContent.ItemType<PrizmaticSword>(), downedMechBossAnyCondition);

            shop.Register(); 
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 25;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 10;
			randExtraCooldown = 10;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = ModContent.ProjectileType<ThrowingAxePro>();
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

				for (int i = 0; i < 3; ++i)
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("KnightGore1").Type, 1f);
            }
		}
	}
}