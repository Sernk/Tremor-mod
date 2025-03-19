using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;  // ��������� ��� ������ ��� ������ � List<T>

namespace TremorMod.Content.Tiles
{
    public class AngeliteOreTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            // ������������� ������� �������� ������
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            // ����� ��� ����, ������� ����� �������������� ��� ���������� ������
            DustType = 57;

            // ����� ���� ��� ���������� ������
            HitSound = SoundID.Tink;  // ��������� ���� ����������

            // ����� ������������� ���������� � ����������� �����
            MineResist = 15f;          // ������� ������������� ���������� ������
            MinPick = 250;             // ����������� ������� ����� ��� ���������� ������

            // ��������� ������ �� ����� � ��������� ������
            AddMapEntry(new Color(0, 191, 255), CreateMapEntryName());
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0f;
            g = 0.3f;
            b = 0.9f;
        }
    }
}
