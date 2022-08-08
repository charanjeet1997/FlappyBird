using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenericExtention
{
    public static bool IsNull<T>(this T data)
    {
        return data == null;
    }
    
    public static bool IsNull<T>(this T[] array)
    {
        return array == null;
    }

    public static bool IsLengthGreaterThanZero<T>(this T[] array)
    {
        return array.Length > 0;
    }
    
    public static float Remap(this float value, float minOld, float maxOld, float minNew, float maxNew)
    {
        float t = Mathf.InverseLerp(minOld, maxOld, value);
        float remapedValue = Mathf.Lerp(minNew, maxNew, t);
        return remapedValue;
    }

}