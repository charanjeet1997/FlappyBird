using System;
using System.Collections;
using System.Collections.Generic;
using CollsionSelectionManager;
using Games.FlappyBird;
using UnityEngine;

namespace CollsionSelectionManager
{
    public class CollisionSelectionManager2D : MonoBehaviour
    {
        public static CollisionSelectionManager2D Instance { get; private set; }
        private List<IColliderObject2D> colliderObject2Ds;
        private List<ITriggerObject2D> triggerObject2Ds;

        private void Awake()
        {
            Instance = this;
        }

        public void Add(object colliderObject)
        {
            var colliderObject2D = colliderObject as IColliderObject2D;
            if (colliderObject2D != null)
            {
                if(colliderObject2Ds == null) colliderObject2Ds = new List<IColliderObject2D>();
                colliderObject2Ds.Add(colliderObject2D);
            }
            
            var triggerObject2D = colliderObject as ITriggerObject2D;
            if (triggerObject2D != null)
            {
                if(triggerObject2Ds == null) triggerObject2Ds = new List<ITriggerObject2D>();
                triggerObject2Ds.Add(triggerObject2D);
            }
            
        }

        public void Remove(object colliderObject)
        {
            if (colliderObject.GetType() == typeof(IColliderObject2D))
            {
                colliderObject2Ds.Remove(colliderObject as IColliderObject2D);
                Debug.Log("Collider Removed");
            }

            if (colliderObject.GetType() == typeof(ITriggerObject2D))
            {
                triggerObject2Ds.Remove(colliderObject as ITriggerObject2D);
                Debug.Log("Trigger Removed");
            }
        }

        public IColliderObject2D OnEnterCollision(Transform collidedTransform,ContactPoint2D[] contactPoints,CollisionObjectName objectName,Collision2D collision,params Action[] onEnterCollision)
        {
            for (int indexOfCollision2Dobjects = 0; indexOfCollision2Dobjects < colliderObject2Ds.Count; indexOfCollision2Dobjects++)
            {
                if (colliderObject2Ds[indexOfCollision2Dobjects].Collision == collision && colliderObject2Ds[indexOfCollision2Dobjects].CollisionObjectName == objectName)
                {
                    colliderObject2Ds[indexOfCollision2Dobjects].OnObjectCollided(collidedTransform,contactPoints);
                    for (int i = 0; i < onEnterCollision.Length; i++)
                    {
                        onEnterCollision[i]?.Invoke();
                    }
                    return colliderObject2Ds[indexOfCollision2Dobjects];
                }
            }
            return null;
        }
        
        public ITriggerObject2D OnEnterTrigger(Transform collidedTransform,CollisionObjectName objectName,Collider2D collider,params Action[] onEnterCollision)
        {
            for (int indexOfCollision2Dobjects = 0; indexOfCollision2Dobjects < triggerObject2Ds.Count; indexOfCollision2Dobjects++)
            {
                if (triggerObject2Ds[indexOfCollision2Dobjects].Collider == collider && triggerObject2Ds[indexOfCollision2Dobjects].CollisionObjectName == objectName)
                {
                    triggerObject2Ds[indexOfCollision2Dobjects].OnObjectTriggered(collidedTransform);
                    for (int i = 0; i < onEnterCollision.Length; i++)
                    {
                        onEnterCollision[i]?.Invoke();
                    }
                    return triggerObject2Ds[indexOfCollision2Dobjects];
                }
            }
            return null;
        }
    }
}