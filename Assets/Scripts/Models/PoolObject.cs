using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VariableEvents;

namespace Games.FlappyBird
{
    public class PoolObject : MonoBehaviour
    {
        public BoxCollider2D boxCollider;
        public float xDistance;
        public float maxDistance;
        public bool initiatePool = false;
        public ObstacleSwaper obstacleSwaper;
        public int PoolIndex
        {
            get => poolIndex;
            set => poolIndex = value;
        }

        private int poolIndex;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObjectVariable playerObject;
        private void OnEnable()
        {
            PoolObjectManager.Instance.RegisterPoolobject(this);
            playerObject.BindVariable(OnGetPlayerGameObject);
        }

        private void OnDisable()
        {
            PoolObjectManager.Instance.DeRegisterPoolObject(this);
            playerObject.UnbindVariable(OnGetPlayerGameObject);
        }

        private void OnGetPlayerGameObject(GameObject player)
        {
            this.player = player;
        }
        
        private void Update()
        {
            if (player != null)
            {
                if(initiatePool)
                {
                    xDistance = transform.position.x - player.transform.position.x;
                    if(xDistance < maxDistance)
                    {
                        PoolObjectManager.OnPoolInitiated();
                        initiatePool = false;
                    }
                }
            }
            else
            {
                GameManager.Instance.SetPlayer();
            }
        }
        public Bounds GetObjectBounds()
        {
            return boxCollider.bounds;
        }

        public Vector3 GetObjectPosition()
        {
            return transform.position;
        }
    }
}