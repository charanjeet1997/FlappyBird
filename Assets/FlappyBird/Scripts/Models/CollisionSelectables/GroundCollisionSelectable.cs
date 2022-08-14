using System.Collections;
using System.Collections.Generic;
using CollsionSelectionManager;
using CommonGameStateManager;
using UnityEngine;

namespace Games.FlappyBird.CollisionSelectable
{
    public class GroundCollisionSelectable : MonoBehaviour, ITriggerObject2D
    {
        [SerializeField] private CollisionObjectName _collisionObjectName;
        [SerializeField] private Collider2D _collider;

        public CollisionObjectName CollisionObjectName
        {
            get => _collisionObjectName;
            set => _collisionObjectName = value;
        }

        public Collider2D Collider => _collider;

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
            GameStateManager.Instance.EndGame();
        }
    }
}