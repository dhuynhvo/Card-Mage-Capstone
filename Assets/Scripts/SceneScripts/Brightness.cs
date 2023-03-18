using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

// Worked on by Abida
// takes aspects of multiple online sources

public class Brightness : MonoBehaviour
{
    public Slider brightnessSlider;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    AutoExposure exposure;

    [SerializeField]
    public BrightnessObject bright_value;

    // change brightness slider
    // Start is called before the first frame update
    void Start()
    {
        
        brightness.TryGetSettings(out exposure);
        brightnessSlider.value = bright_value.brightnessSliderValue; 

    }

    public void AdjustBrightness(float value)
    {
        if(value != 0)
        {
            brightnessSlider.value = exposure.keyValue.value = value;
            bright_value.brightnessSliderValue = brightnessSlider.value;
            
        }
        else
        {
            exposure.keyValue.value = 0.5f;

        }
    }
}
