//Author: Grant Davis and Abida Mim

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    // creates slider
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;
    private void Start()
    {
        
    }

    // slider max
    public void SetMaxHealth(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }

}
