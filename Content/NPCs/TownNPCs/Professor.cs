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
	public class Professor : ModNPC
	{
		public override string Texture => $"{typeof(Professor).NamespaceToPath()}/Professor";

        public override bool IsLoadingEnabled(Mod mod) => true;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Professor");
			Main.npcFrameCount[NPC.type] = 25;
			NPCID.Sets.ExtraFramesCount[NPC.type] = 5;
			NPCID.Sets.AttackFrameCount[NPC.type] = 4;
			NPCID.Sets.DangerDetectRange[NPC.type] = 1000;
			NPCID.Sets.AttackType[NPC.type] = 1;
			NPCID.Sets.AttackTime[NPC.type] = 30;
			NPCID.Sets.AttackAverageChance[NPC.type] = 30;
		}

		public override void SetDefaults()
		{
			NPC.townNPC = true;
			NPC.friendly = true;
			NPC.width = 24;
			NPC.height = 46;
			NPC.aiStyle = 7;
			NPC.damage = 40;
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
                if (!Main.hardMode)
                {
                    return true;
                }
            }
            return false;
        }

        public override List<string> SetNPCNameList() => new List<string>()
        {
            this.GetLocalizedValue("Name.James"),
            this.GetLocalizedValue("Name.Harold"),
            this.GetLocalizedValue("Name.Steven"),
            this.GetLocalizedValue("Name.David"),
            this.GetLocalizedValue("Name.John"),
            this.GetLocalizedValue("Name.BrusBunner"),
            this.GetLocalizedValue("Name.Alfred")
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
                shopName = "Professor";
        }

        public override void AddShops()
        {
            var downedAncientCultistCondition = new Condition("DownedAncientCultist", () => NPC.downedAncientCultist);
            var downedMechBossAnyCondition = new Condition("DownedMechBossAny", () => NPC.downedMechBossAny);

            NPCShop shop = new(Type, "Professor");

            shop.Add(ModContent.ItemType<DimensionalTopHat>())
                .Add(ItemID.HangingSkeleton)
                .Add(ModContent.ItemType<LifeMachine>())
                .Add(ModContent.ItemType<AncientTechnology>())
                .Add(ModContent.ItemType<BagofDust>())
                .Add(ModContent.ItemType<UnchargedBand>());

            shop.Add(ModContent.ItemType<ManaGenerator>(), downedAncientCultistCondition);

            shop.Add(ModContent.ItemType<ChaoticAmplifier>(), downedMechBossAnyCondition);

            shop.Register(); 
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 40;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 5;
			randExtraCooldown = 5;
		}

        public override void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)/* tModPorter Note: closeness is now horizontalHoldoutOffset, use 'horizontalHoldoutOffset = Main.DrawPlayerItemPos(1f, itemtype) - originalClosenessValue' to adjust to the change. See docs for how to use hook with an item type. */ //Allows you to customize how this town NPC's weapon is drawn when this NPC is shooting (this NPC must have an attack type of 1). Scale is a multiplier for the item's drawing size, item is the ID of the item to be drawn, and closeness is how close the item should be drawn to the NPC.
        {
			scale = 1f;
            item = TextureAssets.Item[ModContent.ItemType<AlienBlaster>()].Value;
            itemSize = 14;
		}

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)//Allows you to determine the projectile type of this town NPC's attack, and how long it takes for the projectile to actually appear
		{
			projType = 440;
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)//Allows you to determine the speed at which this town NPC throws a projectile when it attacks. Multiplier is the speed of the projectile, gravityCorrection is how much extra the projectile gets thrown upwards, and randomOffset allows you to randomize the projectile's velocity in a square centered around the original velocity
		{
			multiplier = 7f;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
            int hitDirection = hit.HitDirection;

            if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 151, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);

				for (int i = 0; i < 3; ++i)
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ProfessorGore1").Type, 1f);
            }
		}
	}
}