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
    
    // Update is called once per frame
    void Update()
    {
        if (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

    public void RotateTo(Vector3 to)// change "to" to a better name
    {
        Vector3 relativePos = transform.position + to;

        targetRotation = Quaternion.LookRotation(relativePos - transform.position, Vector3.up);
    }
}
