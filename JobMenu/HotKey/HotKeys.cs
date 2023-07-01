namespace JobMenu.HotKey;

public class HotKeys
{
    [DllImport("user32.dll")]
    private static extern int GetAsyncKeyState(Keys vKey);
    private const int KEY_PRESSED = 0x8000;

    private bool _disposed = false;
    private readonly Dictionary<Keys, KeyInfo> HotKeyDirts;

    public event Action<Keys> KeyUpEvent;
    public event Action<Keys> KeyDownEvent;

    public HotKeys()
    {
        HotKeyDirts = new Dictionary<Keys, KeyInfo>();

        new Thread(UpdateHotKeyStateThread)
        {
            Name = "UpdateHotKeyState",
            IsBackground = true
        }.Start();
    }

    ~HotKeys()
    {
        _disposed = true;
    }

    public void AddKey(Keys key)
    {
        if (!HotKeyDirts.ContainsKey(key))
            HotKeyDirts.Add(key, new KeyInfo() { Key = key });
    }

    public void RemoveKey(Keys key)
    {
        if (HotKeyDirts.ContainsKey(key))
            HotKeyDirts.Remove(key);
    }

    public void ClearKeys()
    {
        HotKeyDirts.Clear();
    }

    private void OnKeyDown(Keys vK)
    {
        KeyDownEvent?.Invoke(vK);
    }

    private void OnKeyUp(Keys vK)
    {
        KeyUpEvent?.Invoke(vK);
    }

    private bool IsKeyPressed(Keys key)
    {
        return Convert.ToBoolean(GetAsyncKeyState(key) & KEY_PRESSED);
    }

    private void UpdateHotKeyStateThread()
    {
        while (!_disposed)
        {
            if (HotKeyDirts.Count > 0)
            {
                var keyInfos = new List<KeyInfo>(HotKeyDirts.Values);
                if (keyInfos != null && keyInfos.Count > 0)
                {
                    foreach (var key in keyInfos)
                    {
                        if (IsKeyPressed(key.Key))
                        {
                            if (!key.IsKeyDown)
                            {
                                key.IsKeyDown = true;
                                OnKeyDown(key.Key);
                            }
                        }
                        else
                        {
                            if (key.IsKeyDown)
                            {
                                key.IsKeyDown = false;
                                OnKeyUp(key.Key);
                            }
                        }
                    }
                }
            }

            Thread.Sleep(20);
        }
    }
}
