using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TremorMod.Content.Tiles
{
	public class GloomstoneTile : ModTile
	{
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true; // ������ �������� �������.
            Main.tileMergeDirt[Type] = true; // ��������� � ������.
            Main.tileBlockLight[Type] = true; // ��������� ����.
            Main.tileLighted[Type] = true; // ����������.
            //ItemDrop = ModContent.ItemType<GloomstoneItem>(); // ���������� �������.
            HitSound = SoundID.Tink; // ���� ��� ����������.
            AddMapEntry(new Color(36, 118, 174));
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}
