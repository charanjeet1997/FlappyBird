using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VariableEvents;

public class CameraMovement : MonoBehaviour
{
    public GameObjectVariable playerObject;
    [SerializeField]private GameObject player;
    [SerializeField] private Vector3 cameraOffest;
    private void OnEnable()
    {
        playerObject.BindVariable(OnGetPlayer);
    }

    private void OnDisable()
    {
        playerObject.UnbindVariable(OnGetPlayer);
    }

    private void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + cameraOffest;
        }
    }

    public void OnGetPlayer(GameObject player)
    {
        this.player = player;
    }
}
