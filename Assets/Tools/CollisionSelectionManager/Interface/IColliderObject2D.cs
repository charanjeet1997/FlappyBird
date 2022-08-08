using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollsionSelectionManager
{
    public interface IColliderObject2D
    {
        CollisionObjectName CollisionObjectName { get; set; }
        Collision2D Collision { get; }

        void OnObjectCollided(Transform collidedObject,ContactPoint2D[] contactPoints);
    }
}