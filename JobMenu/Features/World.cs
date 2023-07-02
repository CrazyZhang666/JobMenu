using JobMenu.Native;
using JobMenu.Offsets;

namespace JobMenu.Features;

public static class World
{
    private static readonly List<uint> cctvCamObjects = new();

    static World()
    {
        cctvCamObjects.Add(168901740);
        cctvCamObjects.Add(3199670845);
        cctvCamObjects.Add(4121760380);
        cctvCamObjects.Add(3135545872);
        cctvCamObjects.Add(548760764);
        cctvCamObjects.Add(2954561821);
        cctvCamObjects.Add(3940745496);
        cctvCamObjects.Add(1919058329);
        cctvCamObjects.Add(1449155105);
        cctvCamObjects.Add(2410265639);
    }

    public static void KillAllEnemy()
    {
        var pCPedList = Game.GetCPedList();

        for (var i = 0; i < Base.oMaxPeds; i++)
        {
            var pCPed = Memory.Read<long>(pCPedList + i * 0x10);
            if (!Memory.IsValid(pCPed))
                continue;

            // 跳过玩家
            var pCPlayerInfo = Memory.Read<long>(pCPed + CPed.CPlayerInfo);
            if (Memory.IsValid(pCPlayerInfo))
                continue;

            var oHostility = Memory.Read<byte>(pCPed + CPed.Hostility);
            if (oHostility > 0x01)
            {
                Memory.Write(pCPed + CPed.Health, 0.0f);

                var pCVehicle = Memory.Read<long>(pCPed + CPed.CVehicle);
                if (!Memory.IsValid(pCVehicle))
                    continue;

                Memory.Write(pCVehicle + CVehicle.Health, -1.0f);
                Memory.Write(pCVehicle + CVehicle.HealthBody, -1.0f);
                Memory.Write(pCVehicle + CVehicle.HealthPetrolTank, -1.0f);
                Memory.Write(pCVehicle + CVehicle.HealthEngine, -1.0f);
            }
        }
    }

    public static void KillAllPolice()
    {
        var pCPedList = Game.GetCPedList();

        for (var i = 0; i < Base.oMaxPeds; i++)
        {
            var pCPed = Memory.Read<long>(pCPedList + i * 0x10);
            if (!Memory.IsValid(pCPed))
                continue;

            // 跳过玩家
            var pCPlayerInfo = Memory.Read<long>(pCPed + CPed.CPlayerInfo);
            if (Memory.IsValid(pCPlayerInfo))
                continue;

            var ped_type = Memory.Read<int>(pCPed + CPed.Ragdoll);
            ped_type = ped_type << 11 >> 25;

            if (ped_type == (int)PedType.COP ||
                ped_type == (int)PedType.SWAT ||
                ped_type == (int)PedType.ARMY)
            {
                Memory.Write(pCPed + CPed.Health, 0.0f);

                var pCVehicle = Memory.Read<long>(pCPed + CPed.CVehicle);
                if (!Memory.IsValid(pCVehicle))
                    continue;

                Memory.Write(pCVehicle + CVehicle.Health, -1.0f);
                Memory.Write(pCVehicle + CVehicle.HealthBody, -1.0f);
                Memory.Write(pCVehicle + CVehicle.HealthPetrolTank, -1.0f);
                Memory.Write(pCVehicle + CVehicle.HealthEngine, -1.0f);
            }
        }
    }

    public static void RemoveAllCCTV()
    {
        var pCObjectList = Game.GetCObjectList();

        for (var i = 0; i < Base.oMaxObjects; i++)
        {
            var pCObject = Memory.Read<long>(pCObjectList + i * 0x10);
            if (!Memory.IsValid(pCObject))
                continue;

            var pCBaseModelInfo = Memory.Read<long>(pCObject + CPed.CBaseModelInfo);
            var hash = Memory.Read<uint>(pCBaseModelInfo + CBaseModelInfo.Hash);

            if (cctvCamObjects.Contains(hash))
            {
                var pCNavigation = Memory.Read<long>(pCObject + CPed.CNavigation);

                var vector3 = new Vector3(0, 0, 1024);

                Memory.Write(pCObject + CPed.VisualX, vector3);
                Memory.Write(pCNavigation + CNavigation.PositionX, vector3);
            }
        }
    }
}
