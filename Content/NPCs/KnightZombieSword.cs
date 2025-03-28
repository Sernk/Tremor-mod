using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Utilities;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using TremorMod.Content.Items.Armor.King;
using TremorMod.Content.Items.Materials;
using TremorMod.Content.Items.Weapons.Magic;
using TremorMod.Content.Items.Placeable.Banners;
using TremorMod.Content.Items.Vanity;

namespace TremorMod.Content.NPCs
{
	public class KnightZombieSword : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Knight Zombie");
			Main.npcFrameCount[NPC.type] = 7;
		}

		public override void SetDefaults()
		{
			NPC.width = 30;
			NPC.height = 40;
			NPC.damage = 16;
			NPC.defense = 6;
			NPC.lifeMax = 140;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = Item.buyPrice(0, 0, 4, 7);
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 3;
			AIType = 434;
			NPC.npcSlots = 0.5f;
			AnimationType = 434;
		}

		public override void AI()
		{
			if (NPC.frame.Y <= 204)
				NPC.width = 30;
			if (NPC.frame.Y > 204)
				NPC.width = 70;
		}


        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<KnightHelmet>(), 25));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            int hitDirection = hit.HitDirection;

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 151, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZombieGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZombieGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("KnightZombieGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZombieGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZombieGore2").Type, 1f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
			=> spawnInfo.SpawnTileY < Main.rockLayer && !Main.dayTime ? 0.01f : 0f;
	}
}
