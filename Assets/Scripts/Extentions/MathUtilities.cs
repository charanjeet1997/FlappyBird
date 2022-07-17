/// <summary>
/// This property always returns a value &lt; 1.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtilities
{
	public const float TAU = 6.28318530718f;

	public static Vector3 AngleRadToVectorZAxis(float angleRad)
	{
		return new Vector3(Mathf.Cos(angleRad),0,Mathf.Sin(angleRad));
	}
	
	public static Vector3 AngleRadToVectorYAxis(float angleRad)
	{
		return new Vector3(Mathf.Cos(angleRad),Mathf.Sin(angleRad),0);
	}

	public static Vector3 LocalToWorldPoint(this Vector3 localPos, Quaternion rotation, Vector3 position)
	{
		return position + rotation * localPos;
	}
	
	public static Vector3 LocalToWorldDirection(this Vector3 localPos, Quaternion rotation)
	{
		return rotation * localPos;
	}
}
