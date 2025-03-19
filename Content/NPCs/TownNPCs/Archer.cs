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
	public class Archer : ModNPC
	{
		public override string Texture => $"{typeof(Archer).NamespaceToPath()}/Archer";

        public override bool IsLoadingEnabled(Mod mod) => true;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Archer");
			Main.npcFrameCount[NPC.type] = 26;
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

			NPC.width = 30;
			NPC.height = 48;
			NPC.aiStyle = 7;
			NPC.damage = 10;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.knockBackResist = 0.5f;

			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			AnimationType = NPCID.Guide;
		}

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            foreach (Player player in Main.ActivePlayers)
            {
                if (player.InventoryHas(ItemID.WoodenArrow))
                {
                    return true; 
                }
            }
            return false; 
        }

        private readonly WeightedRandom<string> _names = new[]
		{
			"Richard",
			"Arthur:2",
			"Jack",
			"William:2",
			"Robin",
			"Wales"
		}.ToWeightedCollectionWithWeight();

        public override List<string> SetNPCNameList() => new List<string> { _names.Get() };

        private readonly WeightedRandom<string> _chats = new WeightedRandom<string>(
			"You'd have to be a very good archer in order to shoot an arrow into a knee.".ToWeightedTuple(2),
			"I'd like to get my hands on a goblintech bow. Those things can shoot multiple arrows.".ToWeightedTuple(.5),
			"I deal in long distance death! Have a look at my wares.".ToWeightedTuple(),
			"I will shoot you with my best arrow if you will not buy anything!".ToWeightedTuple(),
			"Guns? Guns are for cowards!".ToWeightedTuple(),
			"You don't need to make arrows. You need to buy them!".ToWeightedTuple()
		);

		public override string GetChat()
			=> _chats.Get();

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Lang.inter[28].Value;
		}

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
                shopName = "Archer";
        }

        public override void AddShops()
        {
            var bossCondition = new Condition("DownedEaterOrBrain", () => Condition.DownedEaterOfWorlds.IsMet() || Condition.DownedBrainOfCthulhu.IsMet());
            NPCShop shop = new(Type, "Archer");

            shop.AddWithCustomValue(ModContent.ItemType<Quiver>(), Item.buyPrice(silver: 6), Condition.DownedEyeOfCthulhu)
                .AddWithCustomValue(ModContent.ItemType<ArcherGlove>(), Item.buyPrice(gold: 1))
                .AddWithCustomValue(ModContent.ItemType<Crossbow>(), Item.buyPrice(gold: 3))
                .AddWithCustomValue(ItemID.WoodenArrow, Item.buyPrice(copper: 5))
                .AddWithCustomValue(ModContent.ItemType<MiniGun>(), Item.buyPrice(gold: 5), Condition.DownedEyeOfCthulhu)
                .AddWithCustomValue(ModContent.ItemType<LeatherHat>(), Item.buyPrice(silver: 50), Condition.DownedEyeOfCthulhu)
                .AddWithCustomValue(ModContent.ItemType<LeatherShirt>(), Item.buyPrice(silver: 75), Condition.DownedEyeOfCthulhu)
                .AddWithCustomValue(ModContent.ItemType<LeatherGreaves>(), Item.buyPrice(silver: 75), Condition.DownedEyeOfCthulhu)
                .AddWithCustomValue(ItemID.JestersArrow, Item.buyPrice(silver: 10), Condition.DownedEyeOfCthulhu)
                .AddWithCustomValue(ItemID.BoneJavelin, Item.buyPrice(silver: 15), bossCondition)
                .AddWithCustomValue(ModContent.ItemType<DragonGem>(), Item.buyPrice(gold: 3), bossCondition)
                .AddWithCustomValue(ItemID.UnholyArrow, Item.buyPrice(silver: 20), bossCondition)
                .AddWithCustomValue(ModContent.ItemType<DesertEagle>(), Item.buyPrice(gold: 10), Condition.Hardmode)
                .AddWithCustomValue(ItemID.HolyArrow, Item.buyPrice(silver: 25), Condition.Hardmode)
                .AddWithCustomValue(ItemID.HellfireArrow, Item.buyPrice(silver: 30), Condition.Hardmode)
                .AddWithCustomValue(ItemID.BoneArrow, Item.buyPrice(silver: 5), Condition.BloodMoon);

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

        public override void DrawTownAttackGun(ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset)
        {
            scale = 1f;
            item = Main.hardMode ? TextureAssets.Item[ItemID.ShadowFlameBow].Value : TextureAssets.Item[ItemID.DemonBow].Value;
            horizontalHoldoutOffset = 20;
        }

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)//Allows you to determine the projectile type of this town NPC's attack, and how long it takes for the projectile to actually appear
		{
			projType = !Main.hardMode ? ProjectileID.FireArrow : ProjectileID.ShadowFlameArrow;
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

				for(int i = 0; i < 3; ++i)
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ArcherGore1").Type, 1f);
            }
		}
	}
}