using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject cameraRorator = null;

    [SerializeField]
    RotationDirection targetDirection = RotationDirection.forward;

    [SerializeField]
    RotationDirection exitDirection = RotationDirection.forward;

    CameraRotation cameraRotation = null;

    // Start is called before the first frame update
    void Start()
    {
        cameraRotation = cameraRorator.GetComponent<CameraRotation>();
        GetComponent<MeshRenderer>().enabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("trigged");
        if (other.tag == "Player")
        {
            cameraRotation.RotateTo(CameraRotationDirection.ToVector(targetDirection));
            Debug.Log("rotated");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            cameraRotation.RotateTo(CameraRotationDirection.ToVector(exitDirection));
        }
    }
}
