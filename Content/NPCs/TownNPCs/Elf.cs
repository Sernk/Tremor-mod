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
	public class Elf : ModNPC
	{
		public override string Texture => $"{typeof(Elf).NamespaceToPath()}/Elf";

        public override bool IsLoadingEnabled(Mod mod) => true;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Elf");
			Main.npcFrameCount[NPC.type] = 26;
			NPCID.Sets.ExtraFramesCount[NPC.type] = 5;
			NPCID.Sets.AttackFrameCount[NPC.type] = 6;
			NPCID.Sets.DangerDetectRange[NPC.type] = 1000;
			NPCID.Sets.AttackType[NPC.type] = 0;
			NPCID.Sets.AttackTime[NPC.type] = 30;
			NPCID.Sets.AttackAverageChance[NPC.type] = 30;
		}

		public override void SetDefaults()
		{
			NPC.townNPC = true;
			NPC.friendly = true;
			NPC.width = 36;
			NPC.height = 44;
			NPC.aiStyle = 7;
			NPC.damage = 20;
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
                if (player.InventoryHas(ModContent.ItemType<SuspiciousLookingPresent>()))
                {
                    return true; 
                }
            }

            return false; 
        }


        private readonly WeightedRandom<string> _names = new[]
		{
			"Nick",
			"Elfie:2",
			"Jingle",
			"Sparkle:2",
			"Twinkle",
			"Elvis",
			"Peppermint",
			"Snowflake:2"
		}.ToWeightedCollectionWithWeight();

        public override List<string> SetNPCNameList() => new List<string> { _names.Get() };

        private readonly WeightedRandom<string> _chats = new[]
		{
			"What do you know about reindeers?",
			"I could give you some presents, but... You've been naughty this year.",
			"I am Santa's favorite elf!",
			"Jingle bells, jingle bells, jingle all the way!",
			"Someone threw a snowball at me. I don't know who did it but I will find him and throw a snowball at him too.",
			"I licked an icicle one fine day. It resulted in not a lot of pain."
		}.ToWeightedCollection();

		public override string GetChat()
			=> _chats.Get();

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Lang.inter[28].Value;
		}

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
                shopName = "Elf";
        }

        public override void AddShops()
        {
            var downedBoss1Condition = new Condition("DownedBoss1", () => NPC.downedBoss1);
            var downedBoss3Condition = new Condition("DownedBoss3", () => NPC.downedBoss3);
            var hardmodeCondition = new Condition("Hardmode", () => Main.hardMode);

            NPCShop shop = new(Type, "Elf");

            shop.Add(ModContent.ItemType<CandyCane>())
                .Add(ModContent.ItemType<RedChristmasStocking>())
                .Add(ModContent.ItemType<BlueChristmasStocking>())
                .Add(ModContent.ItemType<GreenChristmasStocking>());

            shop.Add(ModContent.ItemType<SnowShotgun>(), downedBoss1Condition)
                .Add(ModContent.ItemType<CandyBow>(), downedBoss1Condition);

            shop.Add(ModContent.ItemType<TheSnowBall>(), downedBoss3Condition);

            shop.Add(ModContent.ItemType<Blizzard>(), hardmodeCondition);

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
			projType = 1;
			attackDelay = 2;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 2f;
		}     
    } 
}