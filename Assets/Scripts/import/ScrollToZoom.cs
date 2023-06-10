using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScrollToZoom : MonoBehaviour
{
    public CinemachineFreeLook cam;
    float[] minrad = new float[3];
    float[] minhgt = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            minrad[i] = cam.m_Orbits[i].m_Radius;
            minhgt[i] = cam.m_Orbits[i].m_Height;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.PageDown))
        {
            for(int i = 0; i < 3; i++)
            {
                
                if(cam.m_Orbits[i].m_Radius <= minrad[i] + 5f)
                    cam.m_Orbits[i].m_Radius += 40f * Time.deltaTime;
                if (cam.m_Orbits[i].m_Height <= minhgt[i] + 5f)
                    cam.m_Orbits[i].m_Height += 40f * Time.deltaTime;
            }
        }
        else if(Input.GetKey(KeyCode.PageUp))
        {
            for (int i = 0; i < 3; i++)
            {
                if (cam.m_Orbits[i].m_Radius >= minrad[i])
                    cam.m_Orbits[i].m_Radius -= 40f * Time.deltaTime;
                if (cam.m_Orbits[i].m_Height >= minhgt[i])
                    cam.m_Orbits[i].m_Height -= 40f * Time.deltaTime;
            }
        }
    }
}
