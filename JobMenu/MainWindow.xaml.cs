using JobMenu.HotKey;

namespace JobMenu;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly HotKeys hotKeys = new HotKeys();

    private bool isShowMenu = true;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        hotKeys.AddKey(Keys.Oem3);
        hotKeys.AddKey(Keys.F5);
        hotKeys.AddKey(Keys.F6);

        hotKeys.KeyDownEvent += HotKeys_KeyDownEvent;
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        hotKeys.ClearKeys();
        hotKeys.KeyDownEvent -= HotKeys_KeyDownEvent;
    }

    private void HotKeys_KeyDownEvent(Keys key)
    {
        switch (key)
        {
            case Keys.Oem3:
                ShowWindow();
                break;
        }
    }

    private void ShowWindow()
    {
        isShowMenu = !isShowMenu;

        this.Dispatcher.Invoke(() =>
        {
            if (isShowMenu)
            {
                this.Show();
            }
            else
            {
                this.Hide();
            }
        });
    }
}
