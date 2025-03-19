using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Projectiles
{
	public class AxeofExecutionerPro : ModProjectile
	{
        public override void SetDefaults()
        {
            // �������� ��������� ��������� ������� � ID 182
            Projectile.CloneDefaults(182);

            // ������ ������� �������
            Projectile.width = 29;
            Projectile.height = 29;

            // ��������� AIType (������, �� ������� ������������� ������ ���������)
            AIType = 182; // ���������, ��� 182 � ��� ������������ ID �������, � ������� �� ������ ����������
        }


        /*public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("AxeofExecutioner");
		}*/
    }
}
