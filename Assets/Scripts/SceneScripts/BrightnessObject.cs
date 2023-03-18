using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Brightness_Value", menuName = "ScriptableObjects/ExposureValue", order = 1)]
public class BrightnessObject : ScriptableObject
{

    [SerializeField]
    public float brightnessSliderValue;
    [SerializeField]
    public float volume;
}
