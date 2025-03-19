using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Utilities;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using TremorMod.Content.Items.Materials;
using TremorMod.Content.Items.Weapons.Magic;
using TremorMod.Content.Items.Placeable.Banners;
using TremorMod.Content.Items.Vanity;

namespace TremorMod.Content.NPCs
{
	public class Grood : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grood");
			Main.npcFrameCount[NPC.type] = 3;
		}

		public override void SetDefaults()
		{
			NPC.lifeMax = 1000;
			NPC.damage = 70;
			NPC.defense = 28;
			NPC.knockBackResist = 0.3f;
			NPC.width = 38;
			NPC.height = 44;
			AnimationType = 141;
			NPC.aiStyle = 41;
			NPC.npcSlots = 1f;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath17;
			NPC.value = Item.buyPrice(0, 0, 5, 24);
			// banner = npc.type;
			// Todo: bannerItem = mod.ItemType("GroodBanner");
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<YellowPuzzleFragment>(), 23));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RedFeather>(), 30));
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
            int hitDirection = hit.HitDirection;

            if (NPC.life <= 0)
			{
				for (int k = 0; k < 60; k++)
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GeerdGore").Type, 1f);
                //Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GroodGore2").Type, 1f);
                //Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GroodGore2").Type, 1f);
                //Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GroodGore3").Type, 1f);
                //Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GroodGore3").Type, 1f);
            }
			else
			{
				for (int k = 0; k < hit.Damage / NPC.lifeMax * 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hitDirection, -1f, 0, default(Color), 0.7f);
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hitDirection, -1f, 0, default(Color), 0.7f);
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hitDirection, -1f, 0, default(Color), 0.7f);
				}
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
			=> spawnInfo.SpawnTileY < Main.rockLayer && Main.hardMode && Main.dayTime ? 0.001f : 0f;
	}
}