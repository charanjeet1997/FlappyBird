using System.Collections;
using System.Collections.Generic;
using CommanTickManager;
using CommonGameStateManager;
using UnityEngine;

namespace Games.FlappyBird
{
    public class PlayerJump : MonoBehaviour,ITick,IGameOver
    {
        public float jumpForce;
        public Rigidbody2D rb_player;
        public SpriteAnimation spriteAnimation;

        private bool isGameOver = false;
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
            if(Input.GetMouseButtonDown(0) && !isGameOver)
            {
                rb_player.velocity = Vector3.zero;
                rb_player.AddForce(new Vector2(0,1) * jumpForce);
                spriteAnimation.StartAnimate();
            }
        }

        public void GameOver()
        {
            isGameOver = true;
        }
    }
}