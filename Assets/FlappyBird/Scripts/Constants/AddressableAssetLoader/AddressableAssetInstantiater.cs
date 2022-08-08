using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public static class AddressableAssetInstantiater
{
    static bool isAssetInstantiated = false;
    public static async Task<AsyncOperationHandle<GameObject>> InstantiateAddressableAssetAsync(AssetReference assetReference,Vector3 position,Quaternion rotation,Transform parent, Action<AsyncOperationHandle<GameObject>> onAssetInstantiate, params Action[] onInstantiationComplete)
    {
        await Task.Yield();
        AsyncOperationHandle<GameObject> operationHandle = Addressables.InstantiateAsync(assetReference, position, rotation, parent); 
        operationHandle.Completed += delegate(AsyncOperationHandle<GameObject> handle)
        {
            onAssetInstantiate?.Invoke(handle);
            isAssetInstantiated = true;
        };
        return operationHandle;
    }

    public static async Task<AsyncOperationHandle<GameObject>> InstantiateAddressableAssetAsync(IResourceLocation resourceLocation,Vector3 position,Quaternion rotation,Transform parent, Action<AsyncOperationHandle<GameObject>> onAssetInstantiate, params Action[] onInstantiationComplete)
    {
        await Task.Yield();
        AsyncOperationHandle<GameObject> operationHandle = Addressables.InstantiateAsync(resourceLocation, position, rotation, parent);
        operationHandle.Completed +=
        delegate(AsyncOperationHandle<GameObject> handle)
        {
            onAssetInstantiate?.Invoke(handle);
            isAssetInstantiated = true;
        };
        return operationHandle;
    }

    public static async Task UnloadAddressableAssetAsync(AsyncOperationHandle<GameObject> operationHandle)
    {
        await Task.Yield();
        Addressables.ReleaseInstance(operationHandle);
    }
}
