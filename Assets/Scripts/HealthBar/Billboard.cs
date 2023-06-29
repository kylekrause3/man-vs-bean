using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = transform;        
    }

    void Update()
    {
        if (GameObject.Find("Main Camera") != null) {
            cameraTransform = GameObject.Find("Main Camera").transform;
        }
        transform.LookAt(cameraTransform.transform);
        transform.Rotate(Vector3.up * 180);
    }
}