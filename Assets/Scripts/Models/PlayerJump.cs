using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.FlappyBird
{
    public class PlayerJump : MonoBehaviour
    {
        public float jumpForce;
        public Rigidbody2D rb_player;
        public SpriteAnimation spriteAnimation;
        private void Update() {
            if(Input.GetMouseButtonDown(0))
            {
                rb_player.velocity = Vector3.zero;
                rb_player.AddForce(new Vector2(0,1) * jumpForce);
                spriteAnimation.StartAnimate();
            }
        }
    }
}