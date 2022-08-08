using System.Collections;
using System.Collections.Generic;
using CollsionSelectionManager;
using EventManagement;
using UnityEngine;

namespace Games.FlappyBird.CollisionSelectable
{
    public class ObstacleCenterScoreCollisionSelectable : MonoBehaviour,ITriggerObject2D
    {
        [SerializeField] private CollisionObjectName collisionObjectName;
        [SerializeField] private Collider2D collider;
        [SerializeField] private GameEvent updateScore;

        public CollisionObjectName CollisionObjectName
        {
            get => collisionObjectName;
            set => collisionObjectName = value;
        }

        public Collider2D Collider => collider;

        public void OnObjectTriggered(Transform collidedObject)
        {
            updateScore.Invoke(new GameEventData<int>(1));
        }
    }
}