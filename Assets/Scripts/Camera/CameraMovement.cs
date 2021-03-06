﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //[SerializeField]
    //Transform player1Trans = null;
    //[SerializeField]
    //Transform player2Trans = null;
    [SerializeField]
    Transform target = null;
    [SerializeField]
    float speed = 1.0f;
    [SerializeField]
    float innerBuffer = 0.1f;
    [SerializeField]
    float outerBuffer = 1.5f;

    bool isMoving;

    Vector3 offset;

    //public Turn turn;

    void Start()
    {
        
        //target = player1Trans;
        offset = target.position + transform.position;
    }


    void Update()
    {
        //if(turn == Turn.P1)
        //{
        //    target = player1Trans;
        //}
        //else 
        //{
        //    target = player2Trans;
        //}
        Vector3 cameraTargetPos = target.position + offset;
        Vector3 heading = cameraTargetPos - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        if (distance > outerBuffer)
        {
            isMoving = true;
        }
        if (isMoving)
        {
            if (distance > innerBuffer)
            {
                transform.position += direction * Time.deltaTime * speed * Mathf.Max(distance, 1f);
            }
            else
            {
                transform.position = cameraTargetPos;
                isMoving = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.position + offset, innerBuffer);
        Gizmos.DrawWireSphere(target.position + offset, outerBuffer);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, innerBuffer);
    }
}
