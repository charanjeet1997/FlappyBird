using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Games.FlappyBird
{
    public class ObstacleSwaper : AddressableLoader<GameObject>
    {
        public AssetReference[] obstacleReferences;
        public Transform obstacleParent;
        public float obstacleCenterDifference;
        [SerializeField]Camera mainCamera;
        [SerializeField]List<GameObject> obstacles = new List<GameObject>();
        [SerializeField]float cameraOrthographicSize,maxcameraOrthographicSizeScale,mincameraOrthographicSizeScale;
        private async void Start()
        {
            mainCamera = Camera.main;
            Init();
        }

        private void Init()
        {
            for (int referenceAssetIndex = 0; referenceAssetIndex < obstacleReferences.Length; referenceAssetIndex++)
            {
                int randomObstacleIndex = Random.Range(0,obstacleReferences.Length);
                Addressables.InstantiateAsync(obstacleReferences[randomObstacleIndex]).Completed += op =>
                {
                    op.Result.transform.parent = obstacleParent;
                    obstacles.Add(op.Result);
                    SetObstacleposition(op.Result,referenceAssetIndex);
                };
            }
           SetObstacleHolderRandomPosition();
        }
        

        public void SetObstacleposition(GameObject obstacle,int obstacleIndex)
        {
            float obstacleYPos = obstacleCenterDifference/2;
            Vector3 obstaclePosition = Vector3.zero;
            if (obstacleIndex % 2 != 0)
            {
                obstaclePosition = obstacleParent.transform.position + new Vector3(0, -obstacleYPos, 0);
            }
            else
            {
                obstaclePosition = obstacleParent.transform.position + new Vector3(0, obstacleYPos, 0);
            }
            obstacle.transform.position = obstaclePosition;
            obstacle.transform.rotation = Quaternion.Euler(0,0,0);
        }

        public void SetObstacleHolderRandomPosition()
        {
            cameraOrthographicSize = mainCamera.orthographicSize;
            obstacleParent.transform.position = Vector3.zero + new Vector3(transform.position.x,Random.Range(-cameraOrthographicSize * mincameraOrthographicSizeScale,cameraOrthographicSize * maxcameraOrthographicSizeScale),0);
        }
    }
}