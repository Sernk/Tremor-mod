using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TremorMod.Content.Projectiles
{
	public class BrassCog : ModProjectile
	{
		public override void SetDefaults()
		{
            Projectile.CloneDefaults(1);  // ���������� ��������� ������������ ������� (��������, ��� �����)
            Projectile.aiStyle = 1;
        }

		/*public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BrassCog");

		}*/

	}
}
