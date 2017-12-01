
using UnityEngine;

public static class MathUtil
{
    public static Vector2 GetCoordinate(float angle)
    {
        //Debug.Log("angle:" + angle + " dir:" + new Vector2(Mathf.Cos(angle * Mathf.PI / 180f), Mathf.Sin(angle * Mathf.PI / 180f)));
        return new Vector2(Mathf.Cos(angle * Mathf.PI / 180f), Mathf.Sin(angle * Mathf.PI / 180f));
    }
}

