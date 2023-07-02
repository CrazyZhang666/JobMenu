﻿namespace JobMenu.Offsets;

public struct Mask
{
    public const string World = "48 8B 05 ?? ?? ?? ?? 45 ?? ?? ?? ?? 48 8B 48 08 48 85 C9 74 07";
    public const string Blip = "4C 8D 05 ?? ?? ?? ?? 0F B7 C1";
    public const string Global = "4C 8D 05 ?? ?? ?? ?? 4D 8B 08 4D 85 C9 74 11";

    public const string ReplayInterface = "48 8D 0D ?? ?? ?? ?? 48 8B D7 E8 ?? ?? ?? ?? 48 8D 0D ?? ?? ?? ?? 8A D8 E8";
}
