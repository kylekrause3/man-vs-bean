using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MyUtils : MonoBehaviour
{
    public static Quaternion randomRot()
    {
        Quaternion rot = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        return rot;
    }

    public static Quaternion randomYRot()
    {
        Quaternion rot = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        return rot;
    }

    public static Vector3 randomDir()
    {
        Vector3 pos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        return pos;
    }
}
