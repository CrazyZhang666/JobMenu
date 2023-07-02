using JobMenu.Native;
using JobMenu.Offsets;

namespace JobMenu.Features;

public static class World
{
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
            }
        }
    }
}
