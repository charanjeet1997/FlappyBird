using System.Collections;
using System.Collections.Generic;
using CollsionSelectionManager;
using UnityEngine;

namespace Games.FlappyBird.CollisionSelectable
{
    public class ObstacleCollisionSelectable : MonoBehaviour, ITriggerObject2D
    {
        [SerializeField] private CollisionObjectName collisionObjectName;
        [SerializeField] private Collider2D obstacleCollider;

        public CollisionObjectName CollisionObjectName
        {
            get => collisionObjectName;
            set => collisionObjectName = value;
        }

        public Collider2D Collider => obstacleCollider;

        private void OnEnable()
        {
            CollisionSelectionManager2D.Instance.Add(this);
        }

        private void OnDisable()
        {
            CollisionSelectionManager2D.Instance.Remove(this);
        }

        public void OnObjectTriggered(Transform collidedObject)
        {
            Debug.Log("Game Over");
        }
    }
}