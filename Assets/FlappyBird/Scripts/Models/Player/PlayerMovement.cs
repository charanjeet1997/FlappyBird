using System.Collections;
using System.Collections.Generic;
using CommanTickManager;
using CommonGameStateManager;
using UnityEngine;

namespace Games.FlappyBird
{
    public class PlayerMovement : MonoBehaviour,ITick,IGameOver
    {
        public float movementSpeed;
        private bool isGameOver;
        [SerializeField]private Rigidbody2D playerBody2D;
        [SerializeField] private Collider2D playerCollider;
        private void OnEnable()
        {
            ProcessingUpdate.Instance.Add(this);
            GameStateManager.Instance.Add(this);
        }

        private void OnDisable()
        {
            ProcessingUpdate.Instance.Remove(this);
            GameStateManager.Instance.Remove(this);
        }

        public void Tick()
        {
            if(!isGameOver)
                transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
        }

        public void GameOver()
        {
            isGameOver = true;
            playerBody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            playerCollider.enabled = false;
        }
    }
}