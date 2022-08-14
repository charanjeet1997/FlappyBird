using System;
using System.Threading.Tasks;
using CommonGameStateManager;
using EventManagement;
using UnityEngine;
using UISystem;
namespace Games.FlappyBird.UI
{
	public class StartScreen : UISystem.Screen
	{

		#region PUBLIC_VARS
	
		#endregion

		#region PRIVATE_VARS

		[SerializeField] private Camera mainCamera;
		#endregion

		#region UNITY_CALLBACKS

		#endregion

		#region PUBLIC_METHODS
		public async void OnStartButtonClick()
		{
			ViewController.Instance.ChangeScreen(ScreenName.GameScreen);
			await Task.Delay(500);
			GameStateManager.Instance.StartGame();
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
			mainCamera.transform.position = Vector3.zero;
			base.Show();
		}
		
		public override void Hide()
		{
			base.Hide();
		}

		#endregion

	}
}