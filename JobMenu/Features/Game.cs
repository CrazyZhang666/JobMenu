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

        Pointers.ReplayInterfacePTR = Memory.FindPattern(Mask.ReplayInterface);
        Pointers.ReplayInterfacePTR = Memory.Rip_37(Pointers.ReplayInterfacePTR);
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

    public static bool GetCVehicle(out long pCVehicle)
    {
        pCVehicle = 0;

        var pCPed = GetCPed();
        var mInVehicle = Memory.Read<byte>(pCPed + CPed.InVehicle);

        if (mInVehicle == 0x01)
        {
            pCVehicle = Memory.Read<long>(pCPed + CPed.CVehicle);
            return true;
        }

        return false;
    }

    public static long GetCPedInventory()
    {
        var pCPed = GetCPed();
        return Memory.Read<long>(pCPed + CPed.CPedInventory);
    }

    public static long GetCReplayInterface()
    {
        return Memory.Read<long>(Pointers.ReplayInterfacePTR);
    }

    public static long GetCPedInterface()
    {
        var pCReplayInterface = GetCReplayInterface();
        return Memory.Read<long>(pCReplayInterface + CReplayInterface.CPedInterface);
    }

    public static long GetCObjectInterface()
    {
        var pCReplayInterface = GetCReplayInterface();
        return Memory.Read<long>(pCReplayInterface + CReplayInterface.CObjectInterface);
    }

    public static long GetCPedList()
    {
        var pCPedInterface = GetCPedInterface();
        return Memory.Read<long>(pCPedInterface + CPedInterface.CPedList);
    }

    public static long GetCObjectList()
    {
        var pCObjectInterface = GetCObjectInterface();
        return Memory.Read<long>(pCObjectInterface + CObjectInterface.CObjectList);
    }
}
