using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    //NOTE
    //The script is turned off by default 
    //because there is no player being referenced
    //to until a network player is spawned
    //which enables the script and tells camera to follow it
    
    [SerializeField] private Transform player;
    private Vector3 playerVelocity = Vector3.zero;

    [SerializeField] [Range(5, 20)] private float smoothSpeed = 10f;

    [SerializeField] private Vector3 offset;

    public void SetPlayer(Transform _player)
    {
        player = _player;
    }

    void LateUpdate()
    {
        if (player == null) {return;}
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref playerVelocity,
            smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
