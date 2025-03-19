using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Utilities;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using TremorMod.Content.Items.Materials;
using TremorMod.Content.Items.Placeable.Banners;
using TremorMod.Content.Items.Vanity;

namespace TremorMod.Content.NPCs
{
	public class GraniteBowman : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Granite Bowman");
			Main.npcFrameCount[NPC.type] = 20;
		}

		public override void SetDefaults()
		{
			NPC.width = 24;
			NPC.height = 44;
			NPC.damage = 26;
			NPC.defense = 20;
			NPC.lifeMax = 95;
			NPC.HitSound = SoundID.NPCHit41;
			NPC.DeathSound = SoundID.NPCDeath44;
			NPC.value = Item.buyPrice(0, 0, 3, 7);
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 3;
			AIType = 111;
			AnimationType = 110;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[31] = false;
			NPC.buffImmune[24] = true;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<GraniteBowmanBanner>();
            ItemID.Sets.KillsToBanner[BannerItem] = 50;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StoneofLife>(), 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Granite, 2, 1, 10));
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
            int hitDirection = hit.HitDirection;

            if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 31, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GBGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GBGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GBGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GBGore3").Type, 1f);
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
			=> Helper.NoZoneAllowWater(spawnInfo) && spawnInfo.SpawnTileType == TileID.Granite && spawnInfo.SpawnTileY > Main.rockLayer ? 0.01f : 0f;
	}
}
