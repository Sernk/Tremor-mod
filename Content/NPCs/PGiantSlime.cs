using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TremorMod.Utilities;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using TremorMod.Content.Items.Materials.OreAndBar;
using TremorMod.Content.Projectiles;
using TremorMod.Content.Items.Materials;
using TremorMod.Content.Items.Weapons.Magic;
using TremorMod.Content.Items.Placeable.Banners;
using TremorMod.Content.Items.Vanity;

namespace TremorMod.Content.NPCs
{
	public class PGiantSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Purple Slime");
			Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetDefaults()
		{
			NPC.lifeMax = 700;
			NPC.damage = 100;
			NPC.defense = 30;
			NPC.knockBackResist = 0.3f;
			NPC.width = 70;
			NPC.alpha = 175;
			NPC.color = new Color(200, 0, 255, 150);
			NPC.height = 46;
			AnimationType = 244;
			NPC.aiStyle = 1;
			AIType = 138;
			NPC.npcSlots = 0.5f;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath23;
			NPC.value = Item.buyPrice(0, 0, 12, 15);
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 23, 23));
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
            int hitDirection = hit.HitDirection;

            if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				}
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 1, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
			=> spawnInfo.SpawnTileY < Main.rockLayer && Main.hardMode && Helper.NoInvasion(spawnInfo) && NPC.downedMoonlord && Main.dayTime ? 0.02f : 0f;
	}
}