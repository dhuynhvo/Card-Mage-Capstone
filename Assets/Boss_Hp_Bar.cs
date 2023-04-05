using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Hp_Bar : MonoBehaviour
{
    public Enemy_Info BossHP;
    public float MaxHP;
    public bool hasSetMax;
    public Slider slider;
    
    public void Update()
    {
        if(BossHP != null && hasSetMax == false)
        {
            MaxHP = BossHP.health;
            hasSetMax= true;
        }

        if (BossHP != null)
        {
            slider.value = BossHP.health / MaxHP;
        }

    }
}
