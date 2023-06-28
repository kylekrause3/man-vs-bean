using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TextMeshProUGUI text;
    public float textSaturation;

    private void Start()
    {
        slider.fillRect = fill.rectTransform;
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue); //SliderValue from 0-1

        if(text != null)
        {
            text.SetText(((int)Mathf.Ceil(health)).ToString());
            float addAmount = 1.0f - textSaturation;
            text.color = fill.color + new Color(addAmount, addAmount, addAmount);
        }
    }
    public float GetHealth()
    {
        return slider.value;
    }
}
