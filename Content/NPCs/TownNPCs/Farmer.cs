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
	public class Farmer : ModNPC
	{
		public override string Texture => $"{typeof(Farmer).NamespaceToPath()}/Farmer";

        public override bool IsLoadingEnabled(Mod mod) => true;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Farmer");
			Main.npcFrameCount[NPC.type] = 23;
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
			NPC.height = 48;
			NPC.aiStyle = 7;
			NPC.damage = 20;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;
			AnimationType = NPCID.Nurse;
		}

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            foreach (Player player in Main.ActivePlayers)
            {
                if (player.InventoryHas(ModContent.ItemType<FarmerShovel>()))
                {
                    return true; 
                }
            }
            return false; 
        }

        private readonly WeightedRandom<string> _names = new[]
		{
			"Trillian:2",
			"Penelope:2",
			"Emily",
			"Abigail",
			"Alma",
			"Alexandra",
			"Peg"
		}.ToWeightedCollectionWithWeight();

        public override List<string> SetNPCNameList() => new List<string> { _names.Get() };

        private readonly WeightedRandom<string> _chats = new[]
		{
			"I wonder who had the idea of growing such an evil corn? Don't look at me like this, I have nothing to do with.",
			"There are so many wonderful and amazing plants in this world but there is nothing more amazing like a corn!",
			"Uh... Oh... Did you came to buy a corn? I'm afraid that it can become evil too.",
			"Don't use chemicals on your plants! Chemicals make them being evil and crazy!",
			"Don't you dare to offer me to eat popcorn! After those bad events I just can't eat anything that contains corn!",
			"Take some water... Add ebonkoi... Wallow some deathweed dust... Mix everything... Oh! Hello! Want to buy something?"
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
                shopName = "Farmer";
        }

        public override void AddShops()
        {
            var notDownedBoss1Condition = new Condition("NotDownedBoss1", () => !NPC.downedBoss1);
            var dayCondition = new Condition("DayTime", () => Main.dayTime);
            var nightCondition = new Condition("NightTime", () => !Main.dayTime);
            var downedSlimeKingCondition = new Condition("DownedSlimeKing", () => NPC.downedSlimeKing);
            var downedBoss2Condition = new Condition("DownedBoss2", () => NPC.downedBoss2);
            var hardmodeCondition = new Condition("Hardmode", () => Main.hardMode);
            var hasCarrowCondition = new Condition("HasCarrow", () => Main.LocalPlayer.HasItem(ModContent.ItemType<Carrow>()));
            var bloodMoonCondition = new Condition("BloodMoon", () => Main.bloodMoon);

            NPCShop shop = new(Type, "Farmer");

            shop.Add(ModContent.ItemType<CornSeed>());

            shop.Add(ModContent.ItemType<Pitchfork>(), notDownedBoss1Condition);

            shop.Add(ItemID.DaybloomSeeds, dayCondition);
            shop.Add(ItemID.MoonglowSeeds, nightCondition);

            shop.Add(ItemID.WaterleafSeeds, downedSlimeKingCondition);

            shop.Add(ItemID.BlinkrootSeeds, downedBoss2Condition);

            shop.Add(ItemID.FireblossomSeeds, hardmodeCondition);

            shop.Add(ModContent.ItemType<Carrow>(), hasCarrowCondition);

            shop.Add(ItemID.DeathweedSeeds, bloodMoonCondition);

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
			projType = ModContent.ProjectileType<TomatoPro>();
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

				for(int i = 0; i < 3; ++i)
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FarmerGore1").Type, 1f);
            }
		}
	}
}