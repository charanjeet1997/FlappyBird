using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VariableEvents;

public class GameManager : MonoBehaviour
{
    public AssetReference objectToLoad;
    public static GameManager Instance;
    public GameObject Player => player;
    private GameObject player;
    [SerializeField]private GameObjectVariable playerObject;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InstantiatePlayerAsync(Vector3.zero, quaternion.identity);
    }

    private async void InstantiatePlayerAsync(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        await AddressableAssetInstantiater.InstantiateAddressableAssetAsync(objectToLoad, position, rotation, parent, OnPlayerObjectInstantiated);
    }

    private void OnPlayerObjectInstantiated(AsyncOperationHandle<GameObject> operationHandle)
    {
        player = operationHandle.Result;
        playerObject.CallAction(player);
    }

    public void SetPlayer()
    {
        playerObject.CallAction(player);
    }
}
