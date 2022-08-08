using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Games.FlappyBird
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }
        public List<CameraData> CameraDatas => cameraDatas;
        [SerializeField] private List<CameraData> cameraDatas;

        private void Awake()
        {
            Instance = this;
        }

        public async Task<CameraData> GetCameraData(CameraFor cameraFor)
        {
            await Task.Yield();
            return cameraDatas.Find(x => x.cameraFor == cameraFor);
        }
    }

    [Serializable]
    public class CameraData
    {
        public Camera camera;
        public CameraFor cameraFor;
    }

    public enum CameraFor
    {
        MainCamera,
        UICamera
    }
}