using System;
using UnityEngine;

namespace Ganes.FlappyBird
{
	[CreateAssetMenu(fileName = "NumberDataContainer" , menuName = "Containers/NumberDataContainer")]
	public class NumberDataContainer : ScriptableObject
	{

		#region PUBLIC_VARS

		public NumberData[] numberDatas;

		#endregion

		#region PRIVATE_VARS

		#endregion

		#region UNITY_CALLBACKS

		public NumberData GetNumberData(string number)
		{
			return Array.Find(numberDatas, x => x.numberInString == number);
		}
		#endregion

		#region PUBLIC_METHODS

		#endregion

		#region PRIVATE_METHODS

		#endregion

	}
}