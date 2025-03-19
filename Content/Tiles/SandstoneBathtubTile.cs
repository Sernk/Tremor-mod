using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TremorMod.Content.Items.Placeable;

namespace TremorMod.Content.Tiles
{
    public class SandstoneBathtubTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2); // ���� ����� ������������� ��������� �����������
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
            TileObjectData.addTile(Type);

            // ���������� � ������� ��� ����������� �������
            TileID.Sets.CanBeSleptIn[Type] = true; // ���������, ��� ������ ����� ���� ������������ ��� �������
            TileID.Sets.IsValidSpawnPoint[Type] = true; // ��������� ���������� ����� �����������

            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable); // ���������, ����� ������ ��������� ��� ���� (���� �����)
            AddMapEntry(new Color(233, 211, 123), CreateMapEntryName());
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;

            // ���������, ��� ����� ��������������� � ���������
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<SandstoneBathtub>(); // ��������� ������ ��� �����������
        }

        // �������������� ����� ��� ��������� �������������� � ��������
      
    }
}
