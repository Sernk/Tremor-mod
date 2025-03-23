using Terraria;
using Terraria.ModLoader;

namespace TremorMod.Content.Items.Materials
{
	public class ClusterShard : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 9999;
			Item.value = 12000;
			Item.rare = 11;
		}

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cluster Shard");
			//Tooltip.SetDefault("");
		}
	}
}