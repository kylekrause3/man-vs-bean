using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateWithCamera : MonoBehaviour
{
    public Transform cam;

    void Update()
    {
        gameObject.transform.rotation = cam.transform.rotation;
    }
}
