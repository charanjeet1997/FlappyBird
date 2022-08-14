using System;
using EventManagement;
using Ganes.FlappyBird;
using UnityEngine;
using UISystem;
using UnityEngine.UI;

namespace Games.FlappyBird
{
	public class GameScoreUIUpdateModule : Module
	{

		#region PUBLIC_VARS
		public NumberDataContainer numberDataContainer;
		#endregion

		#region PRIVATE_VARS
		[SerializeField] private GameEvent updateScoreInUI;
		[SerializeField] private Image[] scoreImages;
		#endregion

		#region UNITY_CALLBACKS
		private void OnEnable()
		{
			updateScoreInUI.Add<GameEventData<int>>(UpdateScoreInUI);
		}

		private void OnDisable()
		{
			updateScoreInUI.Remove<GameEventData<int>>(UpdateScoreInUI);			
		}
		#endregion

		#region PUBLIC_METHODS

		#endregion

		#region PRIVATE_METHODS
		private void UpdateScoreInUI(GameEventData<int> eventData)
		{
			EnableScoreImagesBasedOnScore(eventData.data);
		}

		private void EnableScoreImagesBasedOnScore(int score)
		{
			string scoreInString = score.ToString();
			for (int indexOfScoreImage = 0; indexOfScoreImage < scoreImages.Length; indexOfScoreImage++)
			{
				scoreImages[indexOfScoreImage].gameObject.SetActive(indexOfScoreImage < scoreInString.Length);
			}

			for (int indexOfCharInScoreString = 0; indexOfCharInScoreString < scoreInString.Length; indexOfCharInScoreString++)
			{
				NumberData numberData = numberDataContainer.GetNumberData(scoreInString[indexOfCharInScoreString].ToString());
				scoreImages[indexOfCharInScoreString].sprite = numberData.numberSprite;
			}
		}
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
			for (int indexOfScoreImage = 0; indexOfScoreImage < scoreImages.Length; indexOfScoreImage++)
			{
				scoreImages[indexOfScoreImage].gameObject.SetActive(false);
			}
			base.Show();
		}

		public override void Hide()
		{
			base.Hide();
		}

		#endregion

	}
}