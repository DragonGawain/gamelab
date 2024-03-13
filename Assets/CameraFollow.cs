using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 playerVelocity = Vector3.zero;

    [SerializeField] [Range(5, 20)] private float smoothSpeed = 10f;

    [SerializeField] private Vector3 offset;
    

    void LateUpdate()
    {
        
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref playerVelocity,
            smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
