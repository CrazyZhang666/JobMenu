namespace JobMenu.Utils;

public static class CoreUtil
{
    public static bool IsAppRun(string appName = "GTA5")
    {
        return Process.GetProcessesByName(appName).Length > 0;
    }
}
