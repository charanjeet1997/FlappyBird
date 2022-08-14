using System;
using UnityEngine;
using UISystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Games.FlappyBird.UI
{
	public class GameplayScreen : UISystem.Screen
	{

		#region PUBLIC_VARS

		#endregion

		#region PRIVATE_VARS

		[SerializeField] private Sprite[] backgrounds;
		[SerializeField] private Image backgroundImage;
		#endregion

		#region UNITY_CALLBACKS

		#endregion

		#region PUBLIC_METHODS

		#endregion

		#region PRIVATE_METHODS

		#endregion

		#region UISystem_Callbacks

		public override void Enable()
		{

		}
		public override void Disable()
		{

		}
		
		public override void Show()
		{
			int randomBackgroundSpriteIndex = Random.Range(0, backgrounds.Length);
			backgroundImage.sprite = backgrounds[randomBackgroundSpriteIndex];
		}
		
		public override void Hide()
		{

		}

		#endregion

	}
}