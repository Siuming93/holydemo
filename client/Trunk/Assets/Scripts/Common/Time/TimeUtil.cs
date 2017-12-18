
using System;
using System.Diagnostics;

public enum TimeStyle
{
    HHMMSS =1,
    YYMMDD =2,
}
public static class TimeUtil
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string ToLongTimeString(long milliseconds)
    {
        DateTime now = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified).AddMilliseconds(milliseconds).ToLocalTime();
        //return now.ToLongTimeString();
        return string.Format("{0:yy-MM-dd HH:mm:ss}", now);
    }

    public static string ToShortTimeString(long milliseconds)
    {
        DateTime now = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified).AddMilliseconds(milliseconds).ToLocalTime();
        return now.ToShortTimeString();
    }

    public static string ToTimeString(long milliseconds, TimeStyle style = TimeStyle.HHMMSS)
    {
        return "";
    }
}

