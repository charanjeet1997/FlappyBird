using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public static class AddressableSceneLoader
{
    public static async Task<AsyncOperationHandle<SceneInstance>> LoadAddressableScene(AssetReference assetReference,LoadSceneMode loadSceneMode,Action<AsyncOperationHandle<SceneInstance>> onSceneLoad,List<Action> beforeSceneLoad,List<Action> afterSceneLoad)
    {
        await Task.Yield();
        AsyncOperationHandle<SceneInstance> operationHandle = Addressables.LoadSceneAsync(assetReference,loadSceneMode);
        for (int indexOfAction = 0; indexOfAction < beforeSceneLoad.Count; indexOfAction++)
        {
            beforeSceneLoad[indexOfAction]?.Invoke();
        }
        operationHandle.Completed += delegate(AsyncOperationHandle<SceneInstance> handle)
        {
            onSceneLoad?.Invoke(handle);
            SceneManager.SetActiveScene(handle.Result.Scene);
            for (int indexOfAction = 0; indexOfAction < afterSceneLoad.Count; indexOfAction++)
            {
                afterSceneLoad[indexOfAction]?.Invoke();
            }
        };
        return operationHandle;
    }
    
    public static async Task<AsyncOperationHandle<SceneInstance>> LoadAddressableSceneAsync(AssetLabelReference sceneLabelReference,LoadSceneMode loadSceneMode,Action<AsyncOperationHandle<SceneInstance>> onSceneLoad,List<Action> beforeSceneLoad,List<Action> afterSceneLoad)
    {
        await Task.Yield();
        for (int indexOfAction = 0; indexOfAction < beforeSceneLoad.Count; indexOfAction++)
        {
            beforeSceneLoad[indexOfAction]?.Invoke();
        }

        AsyncOperationHandle<SceneInstance> operationHandle = Addressables.LoadSceneAsync(sceneLabelReference,loadSceneMode);
        operationHandle.Completed += delegate(AsyncOperationHandle<SceneInstance> handle)
        {
            onSceneLoad?.Invoke(handle);
            SceneManager.SetActiveScene(handle.Result.Scene);
            for (int indexOfAction = 0; indexOfAction < afterSceneLoad.Count; indexOfAction++)
            {
                afterSceneLoad[indexOfAction]?.Invoke();
            }
        };
        return operationHandle;
    }

    public static async Task UnloadAddressableSceneAsync(AsyncOperationHandle<SceneInstance> operationHandle,List<Action> beforeSceneUnLoad,List<Action> afterSceneUnLoad)
    {
        await Task.Yield();
        for (int indexOfAction = 0; indexOfAction < beforeSceneUnLoad.Count; indexOfAction++)
        {
            beforeSceneUnLoad[indexOfAction]?.Invoke();
        }
        Addressables.UnloadSceneAsync(operationHandle,true).Completed += delegate(AsyncOperationHandle<SceneInstance> handle)
        {
            for (int indexOfAction = 0; indexOfAction < afterSceneUnLoad.Count; indexOfAction++)
            {
                afterSceneUnLoad[indexOfAction]?.Invoke();
            }
        };
    }
}
