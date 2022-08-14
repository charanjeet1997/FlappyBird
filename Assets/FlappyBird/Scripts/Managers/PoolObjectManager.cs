using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonGameStateManager;
using EventManagement;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Games.FlappyBird
{
    public class PoolObjectManager : MonoBehaviour,IGameOver
    {
        public static PoolObjectManager Instance { get; private set; }
        public List<PoolObject> PoolObjects => poolObjects;
        [SerializeField]private float numberOfObjectToSpawn;
        [SerializeField]private int poolIndex;
        [SerializeField]private AssetReference objectToLoad;
        [SerializeField] private Transform poolObjectParent;
        [SerializeField] private GameEvent onPoolInitiated,getOffsetTransformPosition;
        [SerializeField]private List<PoolObject> poolObjects;
        private Vector3 poolObjectOffsetPosition;
        private List<AsyncOperationHandle<GameObject>> poolObjectOperationHandels;
        private void Awake() {
            if(PoolObjectManager.Instance != null)
                Destroy(PoolObjectManager.Instance.gameObject);
            Instance = this;
        }
        private void OnEnable() {
            onPoolInitiated.Add<GameData>(InitiatePoolSequence);
            getOffsetTransformPosition.Add<GameEventData<Vector3>>(GetPoolObjectoffsetPosition);
            GameStateManager.Instance.Add(this);
        }
        private void OnDisable() {
            onPoolInitiated.Remove<GameData>(InitiatePoolSequence);
            getOffsetTransformPosition.Remove<GameEventData<Vector3>>(GetPoolObjectoffsetPosition);
            GameStateManager.Instance.Remove(this);
        }
        
        public void RegisterPoolobject(PoolObject poolObject)
        {
            if(poolObjects.IsNull()) poolObjects = new List<PoolObject>();
            if(poolObject != null)
                poolObjects.Add(poolObject);
        }

        public void DeRegisterPoolObject(PoolObject poolObject)
        {
            poolObjects.Remove(poolObject);
        }
        
        public async void GameOver()
        {
            await Task.Delay(3000);
            for (int indexOfPoolObjectOperationalHandel = 0; indexOfPoolObjectOperationalHandel < poolObjectOperationHandels.Count; indexOfPoolObjectOperationalHandel++)
            {
                AsyncOperationHandle<GameObject> operationHandle = poolObjectOperationHandels[indexOfPoolObjectOperationalHandel];
                Addressables.ReleaseInstance(operationHandle);
            }
            poolObjectOperationHandels.Clear();
        }
        
        [ContextMenu("Set Pool")]
        void InitiatePoolSequence(GameData gameData)
        {
            Debug.Log("Initiating pool sequence");
            List<PoolObject> tempPoolObjects = new List<PoolObject>();
            for (int poolObjectIndex = 0; poolObjectIndex < poolIndex; poolObjectIndex++)
            {
                tempPoolObjects.Add(poolObjects[0]);
                poolObjects.RemoveAt(0);
            }
            poolObjects.AddRange(tempPoolObjects);
            int startIndex = poolObjects.Count - poolIndex;
            for (int poolObjectIndex = startIndex; poolObjectIndex < poolObjects.Count; poolObjectIndex++)
            {
                Vector3 poolOffset = poolObjects[poolObjectIndex-1].GetObjectBounds().max - poolObjects[poolObjectIndex-1].GetObjectBounds().min;
                poolObjects[poolObjectIndex].transform.position = new Vector3(poolOffset.x,0,0) + poolObjects[poolObjectIndex-1].GetObjectPosition();
            }
            for (int poolObjectIndex = 0; poolObjectIndex < poolObjects.Count; poolObjectIndex++)
            {
                if(poolObjectIndex == poolIndex)
                {
                    poolObjects[poolIndex].InitiatePool = true;
                }
            }   
        }
        
        private async void InstantiatePoolGameObjectAsync(Vector3 position, Quaternion rotation, Transform parent = null)
        {
             await AddressableAssetInstantiater.InstantiateAddressableAssetAsync(objectToLoad, position, rotation, parent, OnPoolObjectInstantiated);
        }

        private void OnPoolObjectInstantiated(AsyncOperationHandle<GameObject> instantiatedObject)
        {
            if (poolObjectOperationHandels == null) poolObjectOperationHandels = new List<AsyncOperationHandle<GameObject>>();
            poolObjectOperationHandels.Add(instantiatedObject);
            if(poolObjects.Count-1 == poolIndex)
            {
                poolObjects[poolObjects.Count-1].InitiatePool = true;
            }
            if (poolObjects.Count > 1)
            {
                Vector3 groundOffset = poolObjects[poolObjects.Count-2].GetObjectBounds().max - poolObjects[poolObjects.Count-2].GetObjectBounds().min;
                poolObjects[poolObjects.Count-1].transform.position = new Vector3(groundOffset.x,0,0) + poolObjects[poolObjects.Count-2].GetObjectPosition();
            }
        }
        
        private void GetPoolObjectoffsetPosition(GameEventData<Vector3> eventData)
        {
            Debug.Log(eventData.data);
            poolObjectOffsetPosition = eventData.data;
            for (int poolObjectIndex = 0; poolObjectIndex < numberOfObjectToSpawn; poolObjectIndex++)
            {
                InstantiatePoolGameObjectAsync(poolObjectOffsetPosition + new Vector3(0,0,0),quaternion.identity,poolObjectParent);
            }
        }
    }
}