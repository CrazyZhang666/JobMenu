using JobMenu.Native;
using JobMenu.Offsets;

namespace JobMenu.Features;

public static class World
{
    public static readonly Vector3 SpacePos = new(9999.0f, 9999.0f, 9999.0f);

    public static readonly List<long> CameraHashs = new()
    {
        299608302,      // prop_cctv_pole_02
        168901740,      // prop_cctv_cam_06a
        3199670845,     // prop_cctv_cam_04a
        4121760380,     // prop_cctv_cam_05a

        3135545872,     // prop_cctv_cam_02a
        548760764,      // prop_cctv_cam_01a
        2954561821,     // prop_cctv_cam_07a
        4287988834,     // prop_cctv_pole_03

        3940745496,     // prop_cctv_cam_01b
        1919058329,     // prop_cctv_cam_04b
        1449155105,     // prop_cctv_cam_03a
        1927491455,     // prop_cctv_pole_01a

        2135655372,     // prop_cctv_pole_04
        2410265639,     // prop_cctv_cam_04c
        2090203758,     // prop_cs_cctv
        3312047777,     // prop_cctv_cont_06
        
        3388315290,     // prop_cctv_01_sm_02
        1079430269,     // prop_cctv_cont_01
        3789885335,     // prop_cctv_cont_03
        383555675,      // prop_dest_cctv_02

        3077936200,     // prop_cctv_01_sm
        262335250,      // prop_cctv_cont_02
        3083131213,     // prop_dest_cctv_03b
        641508,         // v_res_cctv
        
        1295239567,     // prop_cctv_unit_05
        2507445645,     // prop_dest_cctv_01
        2874647165,     // prop_cctv_cont_04
        4253927144,     // prop_cctv_cont_05
        
        1924666731,     // prop_cctv_02_sm
        1517151235,     // prop_cctv_unit_04
        7254050,        // prop_cctv_unit_03
        808554411,      // prop_cctv_unit_01
        
        480355301,      // prop_dest_cctv_03
        4139031726,     // prop_cctv_unit_02
        39380961,       // prop_cctv_mon_02
        3287612635,     // hei_prop_bank_cctv_01
        
        2452560208,     // hei_prop_bank_cctv_02
        289451089,      // p_cctv_s
    };

    public static void KillEnemy()
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

    public static void KillPolice()
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

    public static void RemoveCCTV()
    {
        var pCObjectList = Game.GetCObjectList();

        for (var i = 0; i < Base.oMaxObjects; i++)
        {
            var pCObject = Memory.Read<long>(pCObjectList + i * 0x10);
            if (!Memory.IsValid(pCObject))
                continue;

            var pCBaseModelInfo = Memory.Read<long>(pCObject + CPed.CBaseModelInfo);
            var hash = Memory.Read<uint>(pCBaseModelInfo + CBaseModelInfo.Hash);

            if (!CameraHashs.Contains(hash))
                continue;

            var pCNavigation = Memory.Read<long>(pCObject + CPed.CNavigation);

            Memory.Write(pCObject + CPed.VisualX, SpacePos);
            Memory.Write(pCNavigation + CNavigation.PositionX, SpacePos);
        }
    }
}
