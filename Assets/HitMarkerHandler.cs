using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitMarkerHandler : MonoBehaviour
{
    public float timeToReduce;
    public AnimationCurve curve;

    private Color tempColor;
    private float lastHitTime;
    // Start is called before the first frame update
    void Start()
    {
        tempColor = this.GetComponent<Image>().color;
        tempColor.a = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        tempColor.a = curve.Evaluate((Time.time - lastHitTime) / timeToReduce);
        this.GetComponent<Image>().color = tempColor;
    }

    public void hit()
    {
        lastHitTime = Time.time;
    }
}
