using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject sparks;
    public GameObject smoke;

    private float startTime;

    private void Awake()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime >= 4.75f) {
            sparks.SetActive(true);
            smoke.SetActive(true);
        }
    }
}
