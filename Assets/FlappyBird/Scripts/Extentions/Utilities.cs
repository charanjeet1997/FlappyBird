/// <summary>
/// This property always returns a value &lt; 1.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
	public static void LogError(this string errorValue)
	{
		Debug.LogError(errorValue);
	}
	
	public static void Log(this string value)
	{
		Debug.Log(value);
	}
	
	public static void LogWarning(this string warningValue)
	{
		Debug.Log(warningValue);
	}
}
