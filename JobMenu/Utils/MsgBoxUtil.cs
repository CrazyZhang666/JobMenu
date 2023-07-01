namespace JobMenu.Utils;

public static class MsgBoxUtil
{
    public static void Information(string content, string title = "提示")
    {
        MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public static void Warning(string content, string title = "警告")
    {
        MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    public static void Error(string content, string title = "错误")
    {
        MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
