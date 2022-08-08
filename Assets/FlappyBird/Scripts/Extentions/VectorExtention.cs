using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtention
{
	public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max)
	{
		vector.x = Mathf.Clamp(vector.x, min.x, max.x);
		vector.y = Mathf.Clamp(vector.y, min.y, max.y);
		vector.z = Mathf.Clamp(vector.z, min.z, max.z);
		return vector;
	}

	public static Vector2 Clamp(this Vector2 vector, Vector3 min, Vector3 max)
	{
		float x = vector.x;
		float y = vector.y;
		x = Mathf.Clamp(x, min.x, max.x);
		y = Mathf.Clamp(y, min.y, max.y);
		vector.SetVectorValue(x,y);
		return vector;
	}

	public static void SetVectorValue(this Vector3 vector, float x, float y, float z)
	{
		vector.x = x;
		vector.y = y;
		vector.z = z;
	}

	public static void SetVectorValue(this Vector2 vector, float x, float y)
	{
		vector.x = x;
		vector.y = y;
	}
}