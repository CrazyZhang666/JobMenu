using JobMenu.Utils;
using JobMenu.HotKey;
using JobMenu.Native;
using JobMenu.Offsets;
using JobMenu.Features;
using System.Media;

namespace JobMenu;

/// <summary>
/// MainWindow.xaml 的交互逻辑
/// </summary>
public partial class MainWindow : Window
{
    private IntPtr thisWindowHandle = IntPtr.Zero;
    private Native.Point thisWindowPoint = new();

    private readonly HotKeys hotKeys = new();

    private bool isShowMenu = true;
    private bool isRunning = true;

    private readonly Vector3 position1 = new(-365.500f, -131.501f, 38.673f);
    private readonly Vector3 position2 = new(167.922f, -1770.150f, 29.087f);
    private readonly Vector3 position3 = new(-494.637f, 5191.791f, 89.161f);

    public static readonly SoundPlayer SP_Click03 = new(Properties.Resources.Click_03);

    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        this.DragMove();
    }

    private void PlayClickSound()
    {
        SP_Click03.Play();
    }

    ////////////////////////////////////////////////

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Game.Init();

        thisWindowHandle = new WindowInteropHelper(this).Handle;

        hotKeys.AddKey(Keys.Oem3);
        hotKeys.AddKey(Keys.F3);
        hotKeys.AddKey(Keys.F4);
        hotKeys.AddKey(Keys.F5);
        hotKeys.AddKey(Keys.F6);
        hotKeys.AddKey(Keys.F7);
        hotKeys.KeyDownEvent += HotKeys_KeyDownEvent;

        new Thread(UpdateGameStateThread)
        {
            Name = "UpdateGameState",
            IsBackground = true
        }.Start();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        isRunning = false;

        hotKeys.ClearKeys();
        hotKeys.KeyDownEvent -= HotKeys_KeyDownEvent;
    }

    ////////////////////////////////////////////////

    private void HotKeys_KeyDownEvent(Keys key)
    {
        PlayClickSound();

        switch (key)
        {
            case Keys.Oem3:
                ShowWindow();
                break;
            case Keys.F3:
                Teleport.ToCrossHair();
                break;
            case Keys.F4:
                Teleport.MoveFoward(1.5f);
                break;
            case Keys.F5:
                Teleport.ToWaypoint();
                break;
            case Keys.F6:
                Teleport.ToObjective();
                break;
            case Keys.F7:
                Vehicle.FillHealth();
                break;
        }
    }

    private void UpdateGameStateThread()
    {
        while (isRunning)
        {
            if (!CoreUtil.IsAppRun())
            {
                this.Dispatcher.Invoke(() => this.Close());
                return;
            }

            var pCPed = Game.GetCPed();

            // 玩家无敌
            var oGod = Memory.Read<byte>(pCPed + CPed.God);
            if (oGod != (byte)GodMode.ON)
                Memory.Write(pCPed + CPed.God, (byte)GodMode.ON);

            var pCPedInventory = Memory.Read<long>(pCPed + CPed.CPedInventory);

            // 弹药编辑
            var oAmmoModifier = Memory.Read<byte>(pCPedInventory + CPedInventory.AmmoModifier);
            if (oAmmoModifier != (byte)AmmoFlag.UnlimitedClip)
                Memory.Write(pCPedInventory + CPedInventory.AmmoModifier, (byte)AmmoFlag.UnlimitedClip);

            ////////////////////////

            Thread.Sleep(1000);
        }
    }

    ////////////////////////////////////////////////

    private void ShowWindow()
    {
        isShowMenu = !isShowMenu;

        this.Dispatcher.Invoke(() =>
        {
            if (isShowMenu)
            {
                var thisWindowThreadId = Win32.GetWindowThreadProcessId(thisWindowHandle, IntPtr.Zero);
                var currentForegroundWindow = Win32.GetForegroundWindow();
                var currentForegroundWindowThreadId = Win32.GetWindowThreadProcessId(currentForegroundWindow, IntPtr.Zero);

                Win32.AttachThreadInput(currentForegroundWindowThreadId, thisWindowThreadId, true);

                this.Show();

                Win32.AttachThreadInput(currentForegroundWindowThreadId, thisWindowThreadId, false);

                Win32.SetCursorPos(thisWindowPoint.X, thisWindowPoint.Y);
            }
            else
            {
                Win32.GetCursorPos(out thisWindowPoint);

                this.Hide();
            }
        });
    }

    ////////////////////////////////////////////////

    private void Button_FillAllAmmo_Click(object sender, RoutedEventArgs e)
    {
        PlayClickSound();

        Weapon.FillAllAmmo();
    }

    private void Button_StopCutscene_Click(object sender, RoutedEventArgs e)
    {
        PlayClickSound();

        Online.StopCutscene();
    }

    private void Button_Suicide_Click(object sender, RoutedEventArgs e)
    {
        PlayClickSound();

        Player.Suicide();
    }

    private void Button_GetInOnlinePV_Click(object sender, RoutedEventArgs e)
    {
        PlayClickSound();

        Online.GetInOnlinePV();
    }

    private void Button_VehicleExtra_0x40_Click(object sender, RoutedEventArgs e)
    {
        PlayClickSound();

        Vehicle.Extras(0x40);
    }

    private void Button_KillEnemy_Click(object sender, RoutedEventArgs e)
    {
        PlayClickSound();

        World.KillEnemy();
    }

    private void Button_KillPolice_Click(object sender, RoutedEventArgs e)
    {
        PlayClickSound();

        World.KillPolice();
    }

    private void Button_RemoveCCTV_Click(object sender, RoutedEventArgs e)
    {
        PlayClickSound();

        World.RemoveCCTV();
    }

    private void Button_Telport_Click(object sender, RoutedEventArgs e)
    {
        PlayClickSound();

        if (sender is Button button)
        {
            var btntext = button.Content.ToString();

            switch (btntext)
            {
                case "事务所":
                    Teleport.ToBlips(826);
                    break;
                case "夜总会":
                    Teleport.ToBlips(614);
                    break;
                case "会所":
                    Teleport.ToBlips(492);
                    break;
                case "改装铺":
                    Teleport.ToBlips(779);
                    break;
                case "办公室":
                    Teleport.ToBlips(475);
                    break;
                //////////////////////////
                case "虎鲸":
                    Teleport.ToBlips(760);
                    break;
                case "恐霸":
                    Teleport.ToBlips(632);
                    break;
                case "复仇者":
                    Teleport.ToBlips(589);
                    break;
                case "机动作战中心":
                    Teleport.ToBlips(564);
                    break;
                //////////////////////////
                case "洛圣都改车王":
                    Teleport.SetTeleportPosition(position1);
                    break;
                case "地铁隧道":
                    Teleport.SetTeleportPosition(position2);
                    break;
                case "火车隧道":
                    Teleport.SetTeleportPosition(position3);
                    break;
            }
        }
    }

    private void Button_ExitApp_Click(object sender, RoutedEventArgs e)
    {
        PlayClickSound();

        this.Close();
    }
}
