using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void_Area_Behavior : MonoBehaviour
{
    [SerializeField]
    private Enemy_Info info;
    [SerializeField]
    private int SapNumber = 5;
    [SerializeField]
    private Spell_Info SpellInfo;
    [SerializeField]
    private float SapDamage;

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy_Info>().SappingHealth != true)
        {
            info = collision.gameObject.GetComponent<Enemy_Info>();
            info.SappingHealth = true;
            StartCoroutine(info.SapHealth(SapNumber, SpellInfo.ActiveDuration, SapDamage));
            //Debug.Log("TESTING VOID AREA SPELL");
        }
    }
}
