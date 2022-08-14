using System;
using System.Collections;
using System.Collections.Generic;
using CommonGameStateManager;
using EventManagement;
using UnityEngine;

namespace Games.FlappyBird
{
    public class ScoreManager : MonoBehaviour,IGameStart
    {
        public GameScoreContainer scoreContainer;
        [SerializeField] private GameEvent onScoreUpdate,updateScoreInUI;
        
        private void OnEnable()
        {
            onScoreUpdate.Add<GameEventData<int>>(UpdateScore);
            GameStateManager.Instance.Add(this);
        }

        private void OnDisable()
        {
            onScoreUpdate.Remove<GameEventData<int>>(UpdateScore);
            GameStateManager.Instance.Remove(this);
        }

        private void UpdateScore(GameEventData<int> eventData)
        {
            scoreContainer.UpdateScore(eventData.data);
            updateScoreInUI.Invoke(new GameEventData<int>(scoreContainer.Score));
        }
        
        public void GameStart()
        {
            scoreContainer.Reset();
        }
    }
}