﻿
using System;
using System.Diagnostics;
using Monster.BaseSystem;

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
    public static string ToLongTimeString(double milliseconds)
    {
        DateTime now = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(milliseconds).ToLocalTime();
        //return now.ToLongTimeString();
        return string.Format("{0:yy-MM-dd HH:mm:ss}", now);
    }

    public static string ToShortTimeString(double milliseconds)
    {
        DateTime now = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified).AddMilliseconds(milliseconds).ToLocalTime();
        return now.ToShortTimeString();
    }

    public static string ToTimeString(double milliseconds, TimeStyle style = TimeStyle.HHMMSS)
    {
        return "";
    }
}

