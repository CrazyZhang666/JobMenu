namespace JobMenu.Offsets;

public enum GodMode : byte
{
    OFF,
    ON
}

public enum AmmoFlag : byte
{
    Default,
    UnlimitedAmmo,
    UnlimitedClip,
    UnlimitedAmmoAndClip
}

public enum PedType
{
    PLAYER_0,                   //  0 麦克
    PLAYER_1,                   //  1 富兰克林
    NETWORK_PLAYER,             //  2 线上角色
    PLAYER_2,                   //  3 崔佛
    CIVMALE,                    //  4 文明的男性
    CIVFEMALE,                  //  5 文明的女性
    COP,                        //  6 警察
    GANG_ALBANIAN,              //  7 帮派...
    GANG_BIKER_1,               //
    GANG_BIKER_2,               //
    GANG_ITALIAN,               //
    GANG_RUSSIAN,               //
    GANG_RUSSIAN_2,             //
    GANG_IRISH,                 //
    GANG_JAMAICAN,              //
    GANG_AFRICAN_AMERICAN,      // 
    GANG_KOREAN,                // 
    GANG_CHINESE_JAPANESE,      // 
    GANG_PUERTO_RICAN,          //
    DEALER,                     // 19 商人
    MEDIC,                      // 20 医生
    FIREMAN,                    // 21 消防员
    CRIMINAL,                   // 22 罪犯
    BUM,                        // 23 流浪汉
    PROSTITUTE,                 // 24 妓女
    SPECIAL,                    // 25 特殊
    MISSION,                    // 26 任务
    SWAT,                       // 27 特警
    ANIMAL,                     // 28 动物
    ARMY                        // 29 军队
}

public enum ModelType
{
    Invalid,
    Object,
    MLO,
    Time,
    Weapon,
    Vehicle,
    Ped,
    Destructable,
    WorldObject = 33,
    Sprinkler = 35,
    Unk65 = 65,
    EmissiveLOD = 67,
    Plant = 129,
    LOD = 131,
    Unk132 = 132,
    Unk133 = 133,
    OnlineOnlyPed = 134,
    Building = 161,
    Unk193 = 193
}
