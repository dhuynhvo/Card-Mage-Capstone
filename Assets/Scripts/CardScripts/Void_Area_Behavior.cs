using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Void_Area_Behavior : MonoBehaviour
{
    [SerializeField]
    private Enemy_Info info;
    private float baseSpeed;
    private NavMeshPathing agent;
    [SerializeField]
    private NavMeshAgent speed;
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
            speed = collision.gameObject.transform.parent.gameObject.GetComponent<NavMeshAgent>();
            //agent = collision.gameObject.transform.parent.gameObject.GetComponent<NavMeshPathing>();
            baseSpeed = speed.speed;
            StartCoroutine(collision.gameObject.transform.parent.gameObject.GetComponent<NavMeshPathing>().ResetSpeedForEnemy(baseSpeed, speed, SpellInfo.ActiveDuration));
            speed.velocity = Vector3.zero;
            speed.speed = 0f;
            info.SappingHealth = true;
            StartCoroutine(info.SapHealth(SapNumber, SpellInfo.ActiveDuration, SapDamage));
        }

        else if (collision.gameObject.tag == "Boss" && collision.gameObject.GetComponent<Enemy_Info>().SappingHealth != true)
        {
            info = collision.gameObject.GetComponent<Enemy_Info>();
            //agent = collision.gameObject.transform.parent.gameObject.GetComponent<NavMeshPathing>();
            info.SappingHealth = true;
            StartCoroutine(info.SapHealth(SapNumber, SpellInfo.ActiveDuration, SapDamage));
        };
    }

    


}
