using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Games.FlappyBird
{
    public class PoolObjectManager : MonoBehaviour
    {
        public static Action OnPoolInitiated;
        public static PoolObjectManager Instance { get; private set; }
        public List<PoolObject> PoolObjects => poolObjects;
        [SerializeField]private float numberOfObjectToSpawn;
        [SerializeField]private int poolIndex;
        [SerializeField]private AssetReference objectToLoad;
        private List<PoolObject> poolObjects;

        private void Awake() {
            if(PoolObjectManager.Instance != null)
                Destroy(PoolObjectManager.Instance.gameObject);
            Instance = this;
        }
        private void OnEnable() {
            OnPoolInitiated += InitiatePoolSequence;   
        }
        private void OnDisable() {
            OnPoolInitiated -= InitiatePoolSequence;
        }
        private void Start() {
            for (int poolObjectIndex = 0; poolObjectIndex < numberOfObjectToSpawn; poolObjectIndex++)
            {
                InstantiatePoolGameObjectAsync(transform.position,quaternion.identity);
            }
        }

        public void RegisterPoolobject(PoolObject poolObject)
        {
            poolObjects.CreateList();
            if(poolObject != null)
                poolObjects.Add(poolObject);
        }

        public void DeRegisterPoolObject(PoolObject poolObject)
        {
            poolObjects.Remove(poolObject);
        }
        
        [ContextMenu("Set Pool")]
        void InitiatePoolSequence()
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
                    poolObjects[poolIndex].initiatePool = true;
                }
            }   
        }
        
        private async void InstantiatePoolGameObjectAsync(Vector3 position, Quaternion rotation, Transform parent = null)
        {
             await AddressableAssetInstantiater.InstantiateAddressableAssetAsync(objectToLoad, position, rotation, parent, OnPoolObjectInstantiated);
        }

        private void OnPoolObjectInstantiated(AsyncOperationHandle<GameObject> instantiatedObject)
        {
            if(poolObjects.Count-1 == poolIndex)
            {
                poolObjects[poolObjects.Count-1].initiatePool = true;
            }
            if (poolObjects.Count > 1)
            {
                Vector3 groundOffset = poolObjects[poolObjects.Count-2].GetObjectBounds().max - poolObjects[poolObjects.Count-2].GetObjectBounds().min;
                poolObjects[poolObjects.Count-1].transform.position = new Vector3(groundOffset.x,0,0) + poolObjects[poolObjects.Count-2].GetObjectPosition();
            }
        }
    }
}