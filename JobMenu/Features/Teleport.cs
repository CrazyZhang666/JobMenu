using JobMenu.Native;
using JobMenu.Offsets;

namespace JobMenu.Features;

public static class Teleport
{
    public static Vector3 GetCrossHairPosition()
    {
        var pCPlayerInfo = Game.GetCPlayerInfo();
        var vector3 = Memory.Read<Vector3>(pCPlayerInfo + CPlayerInfo.CrossHairX);
        vector3.Z += 1.0f;
        return vector3;
    }

    public static Vector3 GetWaypointPosition()
    {
        return GetBlipPosition(new int[] { 8 }, new byte[] { 84 });
    }

    public static Vector3 GetObjectivePosition()
    {
        return GetBlipPosition(new int[] { 1 }, new byte[] { 5, 60, 66 });
    }

    public static Vector3 GetBlipPosition(int[] blipIds, byte[] blipColors)
    {
        if (blipIds is null || blipIds.Length == 0)
            return Vector3.Zero;

        var isIgnoreColor = false;
        if (blipColors is null || blipColors.Length == 0)
            isIgnoreColor = true;

        for (var i = 1; i <= 2000; i++)
        {
            var pBlip = Memory.Read<long>(Pointers.BlipPTR + i * 0x08);
            if (!Memory.IsValid(pBlip))
                continue;

            var dwIcon = Memory.Read<int>(pBlip + 0x40);
            var dwColor = Memory.Read<byte>(pBlip + 0x48);

            if (isIgnoreColor)
            {
                if (!blipIds.Contains(dwIcon))
                    continue;
            }
            else
            {
                if (!blipIds.Contains(dwIcon) ||
                    !blipColors.Contains(dwColor))
                    continue;
            }

            var vector3 = Memory.Read<Vector3>(pBlip + 0x10);
            vector3.Z = vector3.Z == 20.0f ? -225.0f : vector3.Z + 1.0f;

            return vector3;
        }

        return Vector3.Zero;
    }

    public static void SetTeleportPosition(Vector3 vector3)
    {
        if (vector3 == Vector3.Zero)
            return;

        var pCPed = Game.GetCPed();

        if (Vehicle.IsInVehicle(pCPed))
        {
            // 玩家在载具
            var pCVehicle = Memory.Read<long>(pCPed + CPed.CVehicle);
            Memory.Write(pCVehicle + CVehicle.VisualX, vector3);

            var pCNavigation = Memory.Read<long>(pCVehicle + CVehicle.CNavigation);
            Memory.Write(pCNavigation + CNavigation.PositionX, vector3);
        }
        else
        {
            // 玩家不在载具
            var pCNavigation = Memory.Read<long>(pCPed + CPed.CNavigation);

            Memory.Write(pCPed + CPed.VisualX, vector3);
            Memory.Write(pCNavigation + CNavigation.PositionX, vector3);
        }
    }

    public static void ToWaypoint()
    {
        SetTeleportPosition(GetWaypointPosition());
    }

    public static void ToObjective()
    {
        SetTeleportPosition(GetObjectivePosition());
    }

    public static void ToCrossHair()
    {
        SetTeleportPosition(GetCrossHairPosition());
    }

    public static void ToBlips(int blipId)
    {
        SetTeleportPosition(GetBlipPosition(new int[] { blipId }, null));
    }

    public static void MoveFoward(float distance)
    {
        var pCPed = Game.GetCPed();
        var pCNavigation = Memory.Read<long>(pCPed + CPed.CNavigation);

        var head = Memory.Read<float>(pCNavigation + CNavigation.RightX);
        var head2 = Memory.Read<float>(pCNavigation + CNavigation.RightY);

        var vector3 = Memory.Read<Vector3>(pCNavigation + CNavigation.PositionX);

        vector3.X -= head2 * distance;
        vector3.Y += head * distance;

        SetTeleportPosition(vector3);
    }
}
