using JobMenu.Utils;

namespace JobMenu;

/// <summary>
/// App.xaml 的交互逻辑
/// </summary>
public partial class App : Application
{
    private static Mutex mutex;

    protected override void OnStartup(StartupEventArgs e)
    {
        mutex = new Mutex(true, "OnlyRun_JobMenu", out bool isCreatedNew);

        if (!isCreatedNew)
        {
            MsgBoxUtil.Warning("程序已经在运行，请勿重复允许");
            Current.Shutdown(); 
            return;
        }

        if (!CoreUtil.IsAppRun())
        {
            MsgBoxUtil.Warning("未发现GTA5进程，请先运行GTA5游戏");
            Current.Shutdown();
            return;
        }

        base.OnStartup(e);
    }
}
