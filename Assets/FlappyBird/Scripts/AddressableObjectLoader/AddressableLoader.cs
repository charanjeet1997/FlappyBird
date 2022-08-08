using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableLoader<T> : MonoBehaviour
{
    public T LoadedObject { get; private set; }
    public GameObject InstantiatedObject { get; private set; }
    public AssetReference objectToLoad;
    private AsyncOperationHandle<T> onObjectLoad;
    private AsyncOperationHandle<GameObject> onObjectInstantiate;

    public virtual void LoadAddressableObject()
    {
        onObjectLoad = Addressables.LoadAssetAsync<T>(objectToLoad);
        onObjectLoad.Completed += OnObjectLoad;
    }

    private void OnObjectLoad(AsyncOperationHandle<T> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                AddressableObjectLoaded(obj.Result);
                break;
            case AsyncOperationStatus.Failed:
                Debug.LogError("can't load object "+obj.OperationException.Message);
                break;
        }
    }

    public virtual void AddressableObjectLoaded(T loadedObject)
    {
        LoadedObject = loadedObject;
    }
    

    public virtual void InstantiateLoadedObject(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        onObjectInstantiate = Addressables.InstantiateAsync(objectToLoad, position, rotation);
        onObjectInstantiate.Completed += OnObjectInstantiate;
    }

    private void OnObjectInstantiate(AsyncOperationHandle<GameObject> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                AddressableObjectInstantiated(obj.Result);
                break;
            case AsyncOperationStatus.Failed:
                Debug.LogError("can't instantiate object "+obj.OperationException.Message);
                break;
        }
    }

    public virtual void AddressableObjectInstantiated(GameObject instantiatedObject)
    {
        InstantiatedObject = instantiatedObject;
    }
}