using JobMenu.Utils;

namespace JobMenu;

/// <summary>
/// App.xaml 的交互逻辑
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        if (CoreUtil.IsAppRun())
        {
            base.OnStartup(e);
        }
        else
        {
            MsgBoxUtil.Warning("未发现GTA5进程，请先运行GTA5游戏");
            Current.Shutdown();
        }
    }
}
