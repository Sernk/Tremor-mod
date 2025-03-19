using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Terraria.ModLoader.IO;

public class Romert : ModSystem
{
    public static bool romertActive = false;

    public override void OnWorldLoad()
    {
        romertActive = false;
    }

    public override void OnWorldUnload()
    {
        romertActive = false;
    }

    public override void SaveWorldData(TagCompound tag)
    {
        tag["romertActive"] = romertActive;
    }

    public override void LoadWorldData(TagCompound tag)
    {
        romertActive = tag.GetBool("romertActive");
    }
}