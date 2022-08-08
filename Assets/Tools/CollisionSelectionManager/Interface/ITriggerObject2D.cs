using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollsionSelectionManager
{
    public interface ITriggerObject2D
    {
        CollisionObjectName CollisionObjectName { get; set; }
        Collider2D Collider { get; }

        void OnObjectTriggered(Transform collidedObject);
    }
}