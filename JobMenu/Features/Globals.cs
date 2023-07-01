using JobMenu.Native;
using JobMenu.Offsets;

namespace JobMenu.Features;

public static class Globals
{
    public static long GlobalAddress(int index)
    {
        return Memory.Read<long>(Pointers.GlobalPTR + 0x8 * (index >> 0x12 & 0x3F)) + 8 * (index & 0x3FFFF);
    }

    public static T ReadGA<T>(int index) where T : struct
    {
        return Memory.Read<T>(GlobalAddress(index));
    }

    public static void WriteGA<T>(int index, T vaule) where T : struct
    {
        Memory.Write(GlobalAddress(index), vaule);
    }
}
