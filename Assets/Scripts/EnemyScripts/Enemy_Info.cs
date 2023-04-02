//Dan Huynhvo
//UNR
//CS 425

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Info : MonoBehaviour
{
    [SerializeField]
    public float health;
    [SerializeField]
    public float DropChance;
    [SerializeField]
    public bool SappingHealth;


    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Spell" && collision.gameObject.GetComponent<Spell_Info>())
        {
            health -= collision.gameObject.GetComponent<Spell_Info>().damage;
        }

        else if(collision.gameObject.tag == "Spell" && collision.gameObject.GetComponent<Connected_Spell>())
        {
            health -= collision.gameObject.GetComponent<Connected_Spell>().SpellInfo.AOEdamage;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Spell" && collision.gameObject.GetComponent<Spell_Info>())
        {
            health -= collision.gameObject.GetComponent<Spell_Info>().damage;
        }

        else if (collision.gameObject.tag == "Spell" && collision.gameObject.GetComponent<Connected_Spell>())
        {
            health -= collision.gameObject.GetComponent<Connected_Spell>().SpellInfo.AOEdamage;
        }
    }

    public IEnumerator SapHealth(int SapNumber, float ActiveDuration, float SapDamage)
    {
        for (int i = 0; i < SapNumber; i++)
        {
            yield return new WaitForSeconds(ActiveDuration / SapNumber - 1);
            health -= SapDamage;
        };
    }


}
