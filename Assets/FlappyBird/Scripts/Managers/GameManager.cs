using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonGameStateManager;
using EventManagement;
using Games.FlappyBird;
using JetBrains.Annotations;
using UISystem;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VariableEvents;

public class GameManager : MonoBehaviour,IGameStart,IGameOver
{
    public static GameManager Instance;
    public AssetReference objectToLoad;
    public AssetReference emptyGroundObjectReference;
    public GameObject Player => player;
    private GameObject player;
    [SerializeField] private GameEvent sendPlayerGameObject;
    [SerializeField] private GameEvent instantiateEmptyGround;
    private AsyncOperationHandle<GameObject> emptyGroundOperationHandel;
    private AsyncOperationHandle<GameObject> playerOperationHandel;
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        instantiateEmptyGround.Add<GameData>(InstantiateEmptyGround);
        GameStateManager.Instance.Add(this);
    }

    private void OnDisable()
    {
        instantiateEmptyGround.Remove<GameData>(InstantiateEmptyGround);
        GameStateManager.Instance.Remove(this);
    }
    
    private void InstantiateEmptyGround(GameData gameData)
    {
        InstantiateEmptyGroundAsync();
    }
    
    private async void InstantiateEmptyGroundAsync(Transform parent = null)
    {
        Vector3 position = Vector3.zero;
        CameraData cameraData = await CameraManager.Instance.GetCameraData(CameraFor.MainCamera);
        Camera mainCamera = cameraData.camera;
        float cameraOrthographicSize = mainCamera.orthographicSize;
        position = new Vector3(0,-cameraOrthographicSize * 0.85f,0);
        await AddressableAssetInstantiater.InstantiateAddressableAssetAsync(emptyGroundObjectReference, position, Quaternion.identity, parent, OnEmptyGroundInstantiate);
    }

    private void OnEmptyGroundInstantiate(AsyncOperationHandle<GameObject> operationHandle)
    {
        emptyGroundOperationHandel = operationHandle;
        InstantiatePlayer();
    }

    
    private async void InstantiatePlayerAsync(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        await AddressableAssetInstantiater.InstantiateAddressableAssetAsync(objectToLoad, position, rotation, parent, OnPlayerObjectInstantiated);
    }

    private void OnPlayerObjectInstantiated(AsyncOperationHandle<GameObject> operationHandle)
    {
        playerOperationHandel = operationHandle;
        player = operationHandle.Result;
        sendPlayerGameObject.Invoke(new GameEventData<GameObject>(player));
    }
    
    
    [ContextMenu("Instantiate Player")]
    public void InstantiatePlayer()
    {
        InstantiatePlayerAsync(Vector3.zero, quaternion.identity);
    }
    public void SetPlayer()
    {
        sendPlayerGameObject.Invoke(new GameEventData<GameObject>(player));
    }

    public void GameStart()
    {
        InstantiateEmptyGroundAsync();
    }

    public async void GameOver()
    {
        await Task.Delay(3000);
        Addressables.ReleaseInstance(emptyGroundOperationHandel);
        Addressables.ReleaseInstance(playerOperationHandel);
        ViewController.Instance.ChangeScreen(ScreenName.GameoverScreen);
    }
}