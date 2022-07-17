using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.FlappyBird
{
    public class BirdDestroy : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == Constants.GameConstants.ObstacleTag)
            {
                //Destroy(this.transform.parent.gameObject);
            }
        }
    }
}