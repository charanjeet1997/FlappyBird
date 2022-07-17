using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class LoadAddressableAsset : MonoBehaviour
{
    //public AssetReference tempAssetReference;
    public AssetLabelReference tempAssetLableReference;
    private AsyncOperationHandle<GameObject> instantiateAssetReference;
    private AsyncOperationHandle<IList<IResourceLocation>> loadAssetLabel;
    private GameObject assetReferenceResult;
    

    [ContextMenu("Instantiate addressable")]
    public void InstantiateAddressable()
    {
        instantiateAssetReference = Addressables.InstantiateAsync(tempAssetLableReference, Vector3.zero, quaternion.identity);
        instantiateAssetReference.Completed += OnObjectInstantiated;
    }
    [ContextMenu("Load Asset Label")]
    public void LoadAssetLabel()
    {
        loadAssetLabel = Addressables.LoadResourceLocationsAsync(tempAssetLableReference);
        loadAssetLabel.Completed += OnResourceLoaded;
    }

    private void OnResourceLoaded(AsyncOperationHandle<IList<IResourceLocation>> obj)
    {
        int count = 0;
        foreach (var resourceLoacation in obj.Result)
        {
            instantiateAssetReference = Addressables.InstantiateAsync(resourceLoacation, new Vector3(count,0,0), quaternion.identity);
            instantiateAssetReference.Completed += OnObjectInstantiated;
            count = count + 2;
        }
    }

    private void OnObjectInstantiated(AsyncOperationHandle<GameObject> obj)
    {
        assetReferenceResult = obj.Result;
    }
    [ContextMenu("Release addressable")]
    private void ReleaseAddressables()
    {
        //tempAssetReference.ReleaseInstance(instantiateAssetReference.Result);
        //Destroy(instantiateAssetReference.Result);
        Addressables.ReleaseInstance(instantiateAssetReference);
        Addressables.Release(loadAssetLabel);
        Debug.Log("Addressable memory release");
    }
}
