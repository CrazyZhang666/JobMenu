using JobMenu.Native;
using JobMenu.Offsets;

namespace JobMenu.Features;

public static class Game
{
    public static void Init()
    {
        Pointers.WorldPTR = Memory.FindPattern(Mask.World);
        Pointers.WorldPTR = Memory.Rip_37(Pointers.WorldPTR);

        Pointers.BlipPTR = Memory.FindPattern(Mask.Blip);
        Pointers.BlipPTR = Memory.Rip_37(Pointers.BlipPTR);

        Pointers.GlobalPTR = Memory.FindPattern(Mask.Global);
        Pointers.GlobalPTR = Memory.Rip_37(Pointers.GlobalPTR);
    }

    public static long GetCPed()
    {
        var pCPedFactory = Memory.Read<long>(Pointers.WorldPTR);
        return Memory.Read<long>(pCPedFactory + CPedFactory.CPed);
    }

    public static long GetCPlayerInfo()
    {
        var pCPed = GetCPed();
        return Memory.Read<long>(pCPed + CPed.CPlayerInfo);
    }

    public static long GetCPedInventory()
    {
        var pCPed = GetCPed();
        return Memory.Read<long>(pCPed + CPed.CPedInventory);
    }
}
