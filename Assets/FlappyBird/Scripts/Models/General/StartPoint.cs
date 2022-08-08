using System;
using System.Collections;
using System.Collections.Generic;
using CollsionSelectionManager;
using EventManagement;
using Games.FlappyBird;
using UnityEngine;

namespace Games.FlappyBird
{
    public class StartPoint : MonoBehaviour
    {
        public GameEvent sendObstacleOffsetPosition;
        [SerializeField] private Transform offsetTransform;

        private void OnTriggerEnter2D(Collider2D other)
        {
            CollisionSelectionManager2D.Instance.OnEnterTrigger(transform, CollisionObjectName.Player, other,
                () => sendObstacleOffsetPosition.Invoke(new GameEventData<Vector3>(offsetTransform.position)),
                () => Debug.Log("On trigger enter"));
        }
    }
}