using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.FlappyBird
{
    public class PlayerMovement : MonoBehaviour
    {
        public float movementSpeed;
        void Update()
        {
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime); 
        }
        
    }
}