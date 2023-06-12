using System.Collections;
using System.Collections.Generic;

using Unity.Netcode;

using UnityEngine;

public class CamMovement : NetworkBehaviour
{
    public float sensitivity = 100;
    float actualsens;
    public Transform player;

    float xRotation = 0;


    // Start is called before the first frame update
    void Awake()
    {
        actualsens = sensitivity * 10f;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * actualsens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * actualsens * Time.deltaTime;

        //up and down looking stuff
        xRotation -= mouseY; //+= does opposite for some reason
        xRotation = Mathf.Clamp(xRotation, -89.99f, 89.99f); //restricts rotation inside of 180 degress

        //up and down
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //ROTATING JUST CAMERA ALONG X AXIS
        //side to side
        player.Rotate(Vector3.up * mouseX); // Vector3.up is the same as Vector3(0, 1, 0), ROTATING ALONG Y AXIS
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.1f, 0.1f, 0.1f));
    }
}
