using JobMenu.Native;
using JobMenu.Offsets;

namespace JobMenu.Features;

public static class Player
{
    public static void Suicide()
    {
        var pCPed = Game.GetCPed();
        if (!Memory.IsValid(pCPed))
            return;

        Memory.Write(pCPed + CPed.Health, 1.0f);
    }
}
