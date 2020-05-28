using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField]
    RotationDirection rotationDirection;

    [SerializeField]
    float speed = 360f;

    Quaternion targetRotation = Quaternion.identity;

    
    void Update()
    {
        if (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

    public void RotateTo(Vector3 newRotation)
    {
        Vector3 relativePos = transform.position + newRotation;
        

        targetRotation = Quaternion.LookRotation(relativePos - transform.position, Vector3.up);
    }
}
