using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtention
{
	public static bool ContainsData<T>(this List<T> list, T data)
	{
		if (list.Find(x => x.Equals(data)) != null)
		{
			return true;
		}

		return false;
	}
	
	public static bool IsNull<T>(this List<T> list)
	{
		return list == null;
	}
	
	public static void CreateList<T>(this List<T> list)
	{
		if(list.IsNull()) list.CreateList();
	}
	
	public static int CountActiveListElements<T>(this List<T> list) where T : MonoBehaviour
	{
		int count = 0;
		for (int i = 0; i < list.Count; i++)
		{
			count += list[i].gameObject.activeSelf ? 1 : 0;
		}

		return count;
	}
}