using System;
using System.Collections;
using System.Collections.Generic;
using CommanTickManager;
using EventManagement;
using UnityEngine;
using VariableEvents;

namespace Games.FlappyBird
{
    public class PoolObject : MonoBehaviour,ITick
    {
        public BoxCollider2D boxCollider;
        public float xDistance;
        public float maxDistance;
        public int PoolIndex
        {
            get => poolIndex;
            set => poolIndex = value;
        }
        public bool InitiatePool
        {
            get => initiatePool;
            set => initiatePool = value;
        }
        [SerializeField]private ObstacleSpawner obstacleSpawner;
        [SerializeField] private GameEvent getPlayerGameObject,initiatePoolSequence;
        private bool initiatePool = false;
        private int poolIndex;
        private GameObject player;
        
        private void OnEnable()
        {
            PoolObjectManager.Instance.RegisterPoolobject(this);
            getPlayerGameObject.Add<GameEventData<GameObject>>(OnGetPlayerGameObject);
            ProcessingUpdate.Instance.Add(this);
        }

        private void OnDisable()
        {
            PoolObjectManager.Instance.DeRegisterPoolObject(this);
            getPlayerGameObject.Remove<GameEventData<GameObject>>(OnGetPlayerGameObject);
            ProcessingUpdate.Instance.Remove(this);
        }

        private void OnGetPlayerGameObject(GameEventData<GameObject> gameEventData)
        {
            this.player = gameEventData.data;
        }
        public Bounds GetObjectBounds()
        {
            return boxCollider.bounds;
        }

        public Vector3 GetObjectPosition()
        {
            return transform.position;
        }

        public void Tick()
        {
            if (player != null)
            {
                if(initiatePool)
                {
                    xDistance = transform.position.x - player.transform.position.x;
                    if(xDistance < maxDistance)
                    {
                        initiatePoolSequence.Invoke(new GameData());
                        initiatePool = false;
                    }
                }
            }
            else
            {
                GameManager.Instance.SetPlayer();
            }
        }
    }
}