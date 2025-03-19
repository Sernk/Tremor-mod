using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TremorMod.Content.Tiles
{
	public class SandstoneBookcaseTile : ModTile
	{
        public override void SetStaticDefaults()
        {
            // ��������� ������
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;

            // ������������ TileObjectData
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4); // ������ 3x4
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16 }; // ������ ������ ������ � ��������
            TileObjectData.addTile(Type);

            // ��������, ��� ��� ������ ���������� �������� �����
            AdjTiles = new int[] { TileID.Bookcases };

            // �����
            AddMapEntry(new Color(233, 211, 123), CreateMapEntryName());
        }
    }
}