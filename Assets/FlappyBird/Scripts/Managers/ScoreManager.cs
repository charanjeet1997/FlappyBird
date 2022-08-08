using System;
using System.Collections;
using System.Collections.Generic;
using EventManagement;
using UnityEngine;

namespace Games.FlappyBird
{
    public class ScoreManager : MonoBehaviour
    {
        public GameScoreContainer scoreContainer;
        public GameEvent onScoreUpdate;

        private void OnEnable()
        {
            onScoreUpdate.Add<GameEventData<int>>(UpdateScore);
        }

        private void OnDisable()
        {
            onScoreUpdate.Remove<GameEventData<int>>(UpdateScore);
        }

        private void UpdateScore(GameEventData<int> eventData)
        {
            scoreContainer.UpdateScore(eventData.data);
        }
    }
}