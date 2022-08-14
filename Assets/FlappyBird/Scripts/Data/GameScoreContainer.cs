using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.FlappyBird
{
    [CreateAssetMenu(fileName = "GameScoreContainer",menuName = "Containers/GameScoreContainer")]
    public class GameScoreContainer : ScriptableObject
    {
        [SerializeField] private int score;
        [SerializeField] private int highScore;

        public int Score => score;
        public int HighScore => highScore;

        public void UpdateScore(int scoreToAdd)
        {
            score += scoreToAdd;
            if (score > highScore)
            {
                highScore = score;
            }
        }

        public void Reset()
        {
            score = 0;
        }
    }
}