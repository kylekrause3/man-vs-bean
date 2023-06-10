using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamera : MonoBehaviour
{
    static CinemachineVirtualCamera cam;
    static float camdistance;
    
    // Start is called before the first frame update
    void Awake()
    {
        cam = this.GetComponent<CinemachineVirtualCamera>();
        camdistance = cam.transform.position.z * -1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float getCamDistance()
    {
        return camdistance;
    }

    public static Vector3 getCamDistanceAsV3()
    {
        return new Vector3(0, 0, camdistance);
    }
}
