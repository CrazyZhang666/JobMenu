using JobMenu.Native;
using JobMenu.Offsets;

namespace JobMenu.Features;

public static class Weapon
{
    public static void FillAllAmmo()
    {
        var pCPedInventory = Game.GetCPedInventory();
        if (!Memory.IsValid(pCPedInventory))
            return;

        var pWeapon = Memory.Read<long>(pCPedInventory + 0x48);
        if (!Memory.IsValid(pWeapon))
            return;

        var count = 0;
        var offset_1 = Memory.Read<long>(pWeapon + count * 0x08);
        var offset_2 = Memory.Read<long>(offset_1 + 0x08);

        while (Memory.IsValid(offset_1) && Memory.IsValid(offset_2))
        {
            var ammo_1 = Memory.Read<int>(offset_2 + 0x28);
            var ammo_2 = Memory.Read<int>(offset_2 + 0x34);

            var max_ammo = Math.Max(ammo_1, ammo_2);
            Memory.Write(offset_1 + 0x20, max_ammo);

            count++;
            offset_1 = Memory.Read<long>(pWeapon + count * 0x08);
            offset_2 = Memory.Read<long>(offset_1 + 0x08);
        }
    }

    public static void AmmoModifier(AmmoFlag flag)
    {
        var pCPedInventory = Game.GetCPedInventory();
        if (!Memory.IsValid(pCPedInventory))
            return;

        Memory.Write(pCPedInventory + CPedInventory.AmmoModifier, (byte)flag);
    }
}
