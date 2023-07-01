using JobMenu.Native;
using JobMenu.Offsets;

namespace JobMenu.Features;

public class Vehicle
{
    public static bool IsInVehicle(long pCPed)
    {
        return Memory.Read<byte>(pCPed + CPed.InVehicle) == 0x01;
    }
}
