using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;

namespace Games.FlappyBird
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField]private float obstacleCenterDifference;
        [SerializeField]private Transform obstacleParent;
        [SerializeField]private float maxcameraOrthographicSizeScale,mincameraOrthographicSizeScale;
        [SerializeField]List<GameObject> obstacles;
        [SerializeField] private GameObject obstacleCenterCollider;
        Camera mainCamera;
        private float cameraOrthographicSize;
        
        public AssetReference[] obstacleReferences;

        public List<GameObject> Obstacles
        {
            get => obstacles;
            private set => obstacles = value;
        }

        public Transform ObstacleParent
        {
            get => obstacleParent;
            set => obstacleParent = value;
        }

        private async void Start()
        {
            CameraData cameraData = await CameraManager.Instance.GetCameraData(CameraFor.MainCamera);
            mainCamera = cameraData.camera;
            Init();
        }

        private async void Init()
        {
            if(obstacles.IsNull()) obstacles = new List<GameObject>();
            await SetObstacleHolderRandomPosition();
            List<Task> tasks = new List<Task>();
            for (int referenceAssetIndex = 0; referenceAssetIndex < obstacleReferences.Length; referenceAssetIndex++)
            {
                tasks.Add(InstantiateObstacleAsync(referenceAssetIndex));
                Debug.Log(referenceAssetIndex);
            }
            await Task.WhenAll(tasks);
            await SetObstacleCenterColliderPosition();
        }

        private async Task InstantiateObstacleAsync(int index)
        {
            int randomObstacleIndex = Random.Range(0,obstacleReferences.Length);
            float obstacleYPos = obstacleCenterDifference/2;
            Vector3 obstacleParentPosition = obstacleParent.position;
            Vector3 obstaclePosition = obstacleParentPosition + (index % 2 != 0 ? new Vector3(0, -obstacleYPos, 0) : new Vector3(0, obstacleYPos, 0));
            Quaternion rotation = quaternion.LookRotation((obstacleParentPosition - obstaclePosition),Vector3.forward);
            Vector3 obstacleAngle = rotation.eulerAngles;
            rotation.eulerAngles = new Vector3(0,0,obstacleAngle.z);
            await AddressableAssetInstantiater.InstantiateAddressableAssetAsync(obstacleReferences[randomObstacleIndex], Vector3.zero, rotation, obstacleParent, (handle =>
            {
                handle.Result.transform.localPosition = Vector3.zero + (index % 2 != 0 ? new Vector3(0, -obstacleYPos, 0) : new Vector3(0, obstacleYPos, 0)); 
                obstacles.Add(handle.Result);
            }));
        }

        private async Task SetObstacleCenterColliderPosition()
        {
            await Task.Delay(500);
            Vector3 obstacleOnePosition = obstacles[0].transform.position;
            Vector3 obstacleTwoPosition = obstacles[1].transform.position;
            float centerYPos = Mathf.Sqrt(Vector3.Distance(obstacleOnePosition, obstacleTwoPosition)) * 0.4f;
            Debug.Log($"Distance {centerYPos}");
            Vector3 centerPosition = Vector3.Lerp(obstacleOnePosition, obstacleTwoPosition, 0.5f);
            obstacleCenterCollider.transform.position = centerPosition;
            float obstacleXScale = obstacles[0].transform.localScale.x;
            obstacleCenterCollider.transform.localScale = new Vector3(obstacleXScale/2,centerYPos,0);
        }

        private async Task SetObstacleHolderRandomPosition()
        {
            await Task.Yield();
            cameraOrthographicSize = mainCamera.orthographicSize;
            obstacleParent.position = Vector3.zero + new Vector3(transform.position.x,Random.Range(-cameraOrthographicSize * mincameraOrthographicSizeScale,cameraOrthographicSize * maxcameraOrthographicSizeScale),0);
        }
    }
}