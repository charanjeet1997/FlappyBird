using System;
using System.Collections;
using System.Collections.Generic;
using CommanTickManager;
using EventManagement;
using Games.FlappyBird;
using UnityEngine;
using VariableEvents;

public class CameraMovement : MonoBehaviour,ITick
{
    public GameEvent getPlayerGameObject;
    [SerializeField] private Vector3 cameraOffest;
    private GameObject player;
    private void OnEnable()
    {
        getPlayerGameObject.Add<GameEventData<GameObject>>(OnGetPlayer);
        ProcessingUpdate.Instance.Add(this);
    }

    private void OnDisable()
    {
        getPlayerGameObject.Remove<GameEventData<GameObject>>(OnGetPlayer);
        ProcessingUpdate.Instance.Remove(this);
    }

    private void OnGetPlayer(GameEventData<GameObject> gameEventData)
    {
        player = gameEventData.data;
    }

    public void Tick()
    {
        if (player != null)
        {
            transform.position = player.transform.position + cameraOffest;
        }
    }
}
