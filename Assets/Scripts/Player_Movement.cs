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
        if (isFalling)
        {
            if (transform.position.y <= targetFallHeight)
            {
                float x = Mathf.Round(transform.position.x);
                float y = Mathf.Round(targetFallHeight);
                float z = Mathf.Round(transform.position.z);

                transform.position = new Vector3(x, y, z);

                isFalling = false;

                return;
            }

            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            return;
        }
        else if (isMoving)
        {
            if (Vector3.Distance(startPos, transform.position) > 1f)
            {
                float x = Mathf.Round(targetPos.x);
                float y = Mathf.Round(targetPos.y);
                float z = Mathf.Round(targetPos.z);

                transform.position = new Vector3(x, y, z);

                isMoving = false;

                return;
            }

            transform.position += (targetPos - startPos) * speed * Time.deltaTime;
            return;
        }
        else
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up * 0.5f,
                Vector3.down, maxFallCastDistance, walkableMask);

            if (hits.Length > 0)
            {
                int topCollider = 0;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[topCollider].collider.bounds.max.y < hits[i].collider.bounds.max.y)
                    {
                        topCollider = i;
                    }
                }
                if (hits[topCollider].distance > 1f)
                {
                    targetFallHeight = transform.position.y - hits[topCollider].distance + 0.5f;
                    isFalling = true;
                }
            }
            else
            {
                targetFallHeight = -Mathf.Infinity;
                isFalling = true;

            }
        }


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
