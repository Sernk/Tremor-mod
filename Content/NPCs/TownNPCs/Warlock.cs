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
using TremorMod.Content.Items.Armor.Vicious;
using TremorMod.Content.Items.Armor.Vile;

namespace TremorMod.Content.NPCs.TownNPCs
{
	[AutoloadHead]
	public class Warlock : ModNPC
	{
		public override string Texture => $"{typeof(Warlock).NamespaceToPath()}/Warlock";

        public override bool IsLoadingEnabled(Mod mod) => true;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Warlock");
			Main.npcFrameCount[NPC.type] = 26;
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
			NPC.width = 40;
			NPC.height = 52;
			NPC.aiStyle = 7;
			NPC.damage = 10;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit2;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.knockBackResist = 0.5f;
			AnimationType = NPCID.Guide;
		}

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            foreach (Player player in Main.ActivePlayers)
            {
                if (!NPC.downedBoss2)
                {
                    return true;
                }
            }
            return false;
        }

        private readonly WeightedRandom<string> _names = new[]
		{
			"Azazel:2",
			"Baphomet",
			"Vaal:3",
			"Dis",
			"Nisroke",
			"Sabnak"
		}.ToWeightedCollectionWithWeight();

        public override List<string> SetNPCNameList() => new List<string> { _names.Get() };

        private readonly WeightedRandom<string> _chats = new[]
		{
			"..."
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
                shopName = "Warlock";
        }

        public override void AddShops()
        {
            // Создаем условия
            var downedBoss3Condition = new Condition("DownedBoss3", () => NPC.downedBoss3);
            var crimsonWorldCondition = new Condition("CrimsonWorld", () => WorldGen.crimson);
            var corruptionWorldCondition = new Condition("CorruptionWorld", () => !WorldGen.crimson);
            var hardmodeCondition = new Condition("Hardmode", () => Main.hardMode);
            var downedAllMechBossesCondition = new Condition("DownedAllMechBosses", () => NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3);

            NPCShop shop = new(Type, "Warlock");

            shop.Add(ModContent.ItemType<StrongBelt>());

            shop.Add(ModContent.ItemType<BallnChain>(), downedBoss3Condition);

            shop.Add(ModContent.ItemType<ViciousHelmet>(), crimsonWorldCondition)
                .Add(ModContent.ItemType<ViciousChestplate>(), crimsonWorldCondition)
                .Add(ModContent.ItemType<ViciousLeggings>(), crimsonWorldCondition);

            shop.Add(ModContent.ItemType<VileHelmet>(), corruptionWorldCondition)
                .Add(ModContent.ItemType<VileChestplate>(), corruptionWorldCondition)
                .Add(ModContent.ItemType<VileLeggings>(), corruptionWorldCondition);

            shop.Add(ModContent.ItemType<Necronomicon>(), hardmodeCondition)
                .Add(ModContent.ItemType<Zephyrhorn>(), hardmodeCondition);

            shop.Add(ModContent.ItemType<Necronomicon>(), downedAllMechBossesCondition)
                .Add(ModContent.ItemType<Zephyrhorn>(), downedAllMechBossesCondition);

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
			projType = 270;
			attackDelay = 5;
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
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WarlockGore1").Type, 1f);
            }
		}
	}
}