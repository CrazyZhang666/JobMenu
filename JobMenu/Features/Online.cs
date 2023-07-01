using JobMenu.Offsets;

namespace JobMenu.Features;

public static class Online
{
    public static async void StopCutscene()
    {
        Globals.WriteGA(2766640 + 3, 1);
        Globals.WriteGA(1575063, 1);
        await Task.Delay(2000);
        Globals.WriteGA(2766640 + 3, 0);
        Globals.WriteGA(1575063, 0);
    }

    public static void GetInOnlinePV()
    {
        Globals.WriteGA(Base.oVGETIn + 8, 1);
    }
}
