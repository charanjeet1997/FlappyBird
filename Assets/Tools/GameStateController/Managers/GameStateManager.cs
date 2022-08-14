using System;
using System.Collections.Generic;
using Ganes.FlappyBird;
using UnityEngine;

namespace CommonGameStateManager
{
	public class GameStateManager : MonoBehaviour
	{

		#region PUBLIC_VARS
		public static GameStateManager Instance { get; private set; }
		#endregion

		#region PRIVATE_VARS

		private List<IGameStart> gameStarts;
		private List<IGameOver> gameOvers;

		#endregion

		#region UNITY_CALLBACKS

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
		}
		#endregion

		#region PUBLIC_METHODS

		public void Add(object gameState)
		{
			if(gameStarts == null) gameStarts = new List<IGameStart>();
			if(gameOvers == null) gameOvers = new List<IGameOver>();
			IGameStart gameStart = gameState as IGameStart;
			if (gameStart != null)
			{
				gameStarts.Add(gameStart);
			}
			
			IGameOver gameOver = gameState as IGameOver;
			if (gameOver != null)
			{
				gameOvers.Add(gameOver);
			}
		}

		public void Remove(object gameState)
		{
			gameStarts.Remove(gameState as IGameStart);
			gameOvers.Remove(gameState as IGameOver);
		}

		public void StartGame(params Action[] onGameStart)
		{
			for (int indexOfGameStart = 0; indexOfGameStart < gameStarts.Count; indexOfGameStart++)
			{
				gameStarts[indexOfGameStart].GameStart();
			}

			for (int indexOfAction = 0; indexOfAction < onGameStart.Length; indexOfAction++)
			{
				onGameStart[indexOfAction]?.Invoke();
			}
		}
		
		public void EndGame(params Action[] onGameOver)
		{
			for (int indexOfGameOver = 0; indexOfGameOver < gameOvers.Count; indexOfGameOver++)
			{
				gameOvers[indexOfGameOver].GameOver();
			}

			for (int indexOfAction = 0; indexOfAction < onGameOver.Length; indexOfAction++)
			{
				onGameOver[indexOfAction]?.Invoke();
			}
		}
		#endregion

		#region PRIVATE_METHODS

		#endregion

	}
}