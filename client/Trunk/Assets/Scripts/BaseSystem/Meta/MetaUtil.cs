using System.Collections;
using UnityEngine;

public static class MetaUtil
{
    public static string GetStringValue(Hashtable properties, string propertyName)
    {
        string val = properties[propertyName] as string;
        return val;
    }

    public static int GetIntValue(Hashtable properties, string propertyName, int defaultVal = -1)
    {
        string val = properties[propertyName] as string;
        int result = defaultVal;
        if (val != null && int.TryParse(val, out result))
        {
            return result;
        }
        return defaultVal;
    }

    public static int GetIntValue(Hashtable properties, string propertyName, int minVal, int maxVal, int defaultVal)
    {
        int val = GetIntValue(properties, propertyName, defaultVal);
        val = Mathf.Clamp(val, minVal, maxVal);
        return val;
    }

    public static float GetFloatValue(Hashtable properties, string propertyName, float defaultVal = 0)
    {
        string val = properties[propertyName] as string;
        float result = defaultVal;
        if (val != null && float.TryParse(val, out result))
        {
            return result;
        }
        return defaultVal;
    }
}
