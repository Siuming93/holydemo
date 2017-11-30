
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
    public static string ToLongTimeString(double seconds)
    {
        DateTime now = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified).AddMilliseconds(seconds * 1000).ToLocalTime();
        return now.ToLongTimeString();
        //return string.Format("{0:yy-MM-dd hh:mm:ss zz}", now);
    }

    public static string ToShortTimeString(double seconds)
    {
        DateTime now = new DateTime(1970, 1, 1).AddMilliseconds(seconds * 1000);
        return now.ToShortTimeString();
    }

    public static string ToTimeString(double seconds, TimeStyle style = TimeStyle.HHMMSS)
    {
        return "";
    }
}

