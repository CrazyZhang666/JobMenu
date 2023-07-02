using JobMenu.Native;
using JobMenu.Offsets;

namespace JobMenu.Features;

public class Vehicle
{
    public static bool IsInVehicle(long pCPed)
    {
        return Memory.Read<byte>(pCPed + CPed.InVehicle) == 0x01;
    }

    public static bool IsInVehicle()
    {
        var pCPed = Game.GetCPed();
        if (!Memory.IsValid(pCPed))
            return false;

        return IsInVehicle(pCPed);
    }

    public static void Extras(short flag)
    {
        if (Game.GetCVehicle(out long pCVehicle))
        {
            var pCModelInfo = Memory.Read<long>(pCVehicle + CVehicle.CModelInfo);
            Memory.Write(pCModelInfo + CModelInfo.Extras, flag);
        }
    }

    public static void FillHealth()
    {
        if (Game.GetCVehicle(out long pCVehicle))
        {
            var oVHealth = Memory.Read<float>(pCVehicle + CVehicle.Health);
            var oVHealthMax = Memory.Read<float>(pCVehicle + CVehicle.HealthMax);

            if (oVHealth <= oVHealthMax)
            {
                Memory.Write(pCVehicle + CVehicle.Health, oVHealthMax);
                Memory.Write(pCVehicle + CVehicle.HealthBody, oVHealthMax);
                Memory.Write(pCVehicle + CVehicle.HealthPetrolTank, oVHealthMax);
                Memory.Write(pCVehicle + CVehicle.HealthEngine, oVHealthMax);
            }
            else
            {
                Memory.Write(pCVehicle + CVehicle.Health, 1000.0f);
                Memory.Write(pCVehicle + CVehicle.HealthBody, 1000.0f);
                Memory.Write(pCVehicle + CVehicle.HealthPetrolTank, 1000.0f);
                Memory.Write(pCVehicle + CVehicle.HealthEngine, 1000.0f);
            }
        }
    }
}
