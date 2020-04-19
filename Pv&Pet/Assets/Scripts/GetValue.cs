using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetValue : MonoBehaviour
{
    private float sliderValue;
    private Text sliderText;

    public void Start()
    {
        sliderValue = GetComponent<Slider>().value;
        sliderText = GetComponentInChildren<Text>();
        sliderText.text = sliderValue.ToString();
    }

    public void UpdateValue()
    {
        sliderValue = GetComponent<Slider>().value;
        sliderText = GetComponentInChildren<Text>();
        sliderText.text = sliderValue.ToString();
    }
}
