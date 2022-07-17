using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public static class AddressableAssetLoader
{
    public static async Task<AsyncOperationHandle<T>> LoadAddressableAssetAsync<T>(AssetReference assetReference,Action<AsyncOperationHandle<T>> onAssetLoaded,params Action[] onAssetLoadCompleted)
    {
        await Task.Yield();
        AsyncOperationHandle<T> operationHandle = Addressables.LoadAssetAsync<T>(assetReference);
        operationHandle.Completed += delegate(AsyncOperationHandle<T> handle)
        {
            onAssetLoaded?.Invoke(handle);
            foreach (var action in onAssetLoadCompleted)
            {
                action?.Invoke();
            }
        };
        return operationHandle;
    }
    
    public static async Task<AsyncOperationHandle<IList<IResourceLocation>>> LoadAddressableAssetAsync(AssetLabelReference labelReference,Action<AsyncOperationHandle<IList<IResourceLocation>>> onAssetLoaded,params Action[] onAssetLoadCompleted)
    {
        await Task.Yield();
        AsyncOperationHandle<IList<IResourceLocation>> operationHandle = Addressables.LoadResourceLocationsAsync(labelReference);
        operationHandle.Completed += delegate(AsyncOperationHandle<IList<IResourceLocation>> handle)
        {
            onAssetLoaded?.Invoke(handle);
            foreach (var action in onAssetLoadCompleted)
            {
                action?.Invoke();
            }
        };
        return operationHandle;
    }

    public static async Task UnloadAddressableAsset<T>(AsyncOperationHandle<T> operationHandle)
    {
        await Task.Yield();
        Addressables.Release(operationHandle);
    }
}
