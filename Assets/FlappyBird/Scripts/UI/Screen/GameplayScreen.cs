using System;
using EventManagement;
using Ganes.FlappyBird;
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

		[SerializeField] private GameScoreUIUpdateModule gameScoreUiUpdateModule;
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

		public override void Disable()
		{
			base.Disable();
		}

		public override void Enable()
		{
			base.Enable();
		}

		public override void Show()
		{
			int randomBackgroundSpriteIndex = Random.Range(0, backgrounds.Length);
			backgroundImage.sprite = backgrounds[randomBackgroundSpriteIndex];
			gameScoreUiUpdateModule.Show();
			base.Show();
		}
		
		public override void Hide()
		{
			gameScoreUiUpdateModule.Hide();
			base.Hide();
		}

		#endregion

	}
}