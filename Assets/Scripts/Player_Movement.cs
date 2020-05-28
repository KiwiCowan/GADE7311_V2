using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField]
    float speed = 0.25f;
    [SerializeField]
    float raylength = 1.4f;
    [SerializeField]
    float rayOffsetX = 0.5f;
    [SerializeField]
    float rayOffsetY = 0.5f;
    [SerializeField]
    float rayOffsetZ = 0.5f;
    //[SerializeField]
    //float snapDistance = 0.25f;

    Vector3 targetPos;
    Vector3 startPos;
    //idk
    Vector3 xOffset;
    Vector3 yOffset;
    Vector3 zOffset;
    Vector3 zAxisOriginA;
    Vector3 zAxisOriginB;
    Vector3 xAxisOriginA;
    Vector3 xAxisOriginB;

    bool isMoving;

    [SerializeField]
    Transform cameraRotator = null;

    [SerializeField]
    LayerMask walkableMask = 0;
    [SerializeField]
    LayerMask collidableMask = 0;

    [SerializeField]
    float maxFallCastDistance = 100f;
    [SerializeField]
    float fallSpeed = 30f;

    bool isFalling;
    float targetFallHeight;

    void Update()
    {
        if (isMoving)
        {
            if (Vector3.Distance(startPos, transform.position) > 1f)
            {
                transform.position = targetPos;
                isMoving = false;
                return;
            }

            transform.position += (targetPos - startPos) * speed * Time.deltaTime;
            return;
        }
        //!Physics.Raycast(transform.position, Vector3.forward, raylength

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (CanMove(Vector3.forward))
            {
                targetPos = transform.position + cameraRotator.transform.forward;
                startPos = transform.position;
                isMoving = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (CanMove(Vector3.back))
            {
                targetPos = transform.position - cameraRotator.transform.forward;
                startPos = transform.position;
                isMoving = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (CanMove(Vector3.left))
            {
                targetPos = transform.position - cameraRotator.transform.right;
                startPos = transform.position;
                isMoving = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (CanMove(Vector3.right))
            {
                targetPos = transform.position + cameraRotator.transform.right;
                startPos = transform.position;
                isMoving = true;
            }
        }
    }

    bool CanMove(Vector3 direction)
    {
        if (Vector3.Equals(Vector3.forward, direction) || Vector3.Equals(Vector3.back, direction))
        {
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.right * rayOffsetX, direction, raylength))
            {
                return false;
            }
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY - Vector3.right * rayOffsetX, direction, raylength))
            {
                return false;
            }
        }
        else if (Vector3.Equals(Vector3.left, direction) || Vector3.Equals(Vector3.right, direction))
        {
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.forward * rayOffsetZ, direction, raylength))
            {
                return false;
            }
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY - Vector3.forward * rayOffsetZ, direction, raylength))
            {
                return false;
            }
        }
        return true;
    }
}
