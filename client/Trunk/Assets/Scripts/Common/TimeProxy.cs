
public class TimeProxy : BaseProxy
{
    public new const string NAME = "TimeProxy";

    /// <summary>
    /// Server Millisecond
    /// </summary>
    public static int Time
    {
        get { return System.DateTime.Now.Millisecond - _lastLocaleTime + _lastServerMillisecond; }
    }
    public TimeProxy(): base(NAME)
    {
        OnAsyncServerTime(null);
    }

    private static int _lastLocaleTime;
    private static int _lastServerMillisecond;

    private void OnAsyncServerTime(object data)
    {
        _lastLocaleTime = System.DateTime.Now.Millisecond;
        _lastServerMillisecond = _lastLocaleTime;
    }
}

