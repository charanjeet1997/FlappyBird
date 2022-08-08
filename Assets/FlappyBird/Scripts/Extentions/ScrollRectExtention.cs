/// <summary>
/// This property always returns a value &lt; 1.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ScrollRectExtention
{
	public static void SetScrollValue(this ScrollRect scrollRect, float value)
	{
		if (scrollRect.horizontal)
		{
			scrollRect.horizontalNormalizedPosition = value;
		}
		else
		{
			scrollRect.verticalNormalizedPosition = value;
		}
	}
}
