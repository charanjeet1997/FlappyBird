using System;
using UnityEngine;
using UISystem;
namespace Games.FlappyBird.UI
{
	public class GameoverScreen : UISystem.Screen
	{

		#region PUBLIC_VARS

		#endregion

		#region PRIVATE_VARS

		#endregion

		#region UNITY_CALLBACKS

		#endregion

		#region PUBLIC_METHODS

		public void RestartButtonClick()
		{
			ViewController.Instance.ChangeScreen(ScreenName.StartScreen);
		}
		#endregion

		#region PRIVATE_METHODS

		#endregion

		#region UISystem_Callbacks

		public override void Enable()
		{
			base.Enable();
		}
		public override void Disable()
		{
			base.Disable();
		}
		
		public override void Show()
		{
			base.Show();
		}
		
		public override void Hide()
		{
			base.Hide();
		}

		#endregion
	}
}