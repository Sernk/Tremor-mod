using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TremorMod.Content.Items.Placeable.Banners;
using TremorMod.Utilities;

namespace TremorMod.Content.NPCs
{
	public class Flayer : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flayer");
			Main.npcFrameCount[NPC.type] = 7;
		}

		public override void SetDefaults()
		{
			NPC.width = 40;
			NPC.height = 60;
			NPC.damage = 100;
			NPC.defense = 7;
			NPC.lifeMax = 1000;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = Item.buyPrice(0, 0, 85, 7);
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 3;
			AIType = 434;
			NPC.aiStyle = 3;
			AnimationType = 434;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<FlayerBanner>();
            ItemID.Sets.KillsToBanner[BannerItem] = 50;
        }

		public override void HitEffect(NPC.HitInfo hit)
		{
            int hitDirection = hit.HitDirection;

            if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 151, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FlayerGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FlayerGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FlayerGore4").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FlayerGore4").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FlayerGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FlayerGore3").Type, 1f);
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
			=> Helper.NormalSpawn(spawnInfo) && Helper.NoZoneAllowWater(spawnInfo) && NPC.downedPlantBoss && Main.bloodMoon && spawnInfo.SpawnTileY < Main.worldSurface ? 0.01f : 0f;
	}
}
