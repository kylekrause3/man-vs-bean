using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class ReduceTransparencyOverTime : MonoBehaviour
{

    public float timeToReduce;
    public AnimationCurve curve;
    public SpriteRenderer spriteRenderer;

    private Color color;
    private float startTransparency;
    private float timeOnInstantiation;
    void Start()
    {
        color = spriteRenderer.color;
        startTransparency = color.a;
        timeOnInstantiation = Time.time;

    }
    void Update()
    {
        //reduce transparency over time based on curve and timeoninstantiation
        spriteRenderer.color = new Color(color.r, color.g, color.b, startTransparency * curve.Evaluate((Time.time - timeOnInstantiation) / timeToReduce));
        
    }
}
