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
    public float maxHealth;
    [SerializeField]
    public float DropChance;
    [SerializeField]
    public bool SappingHealth;
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        maxHealth = health;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Spell" && collision.gameObject.GetComponent<Spell_Info>())
        {
            health -= collision.gameObject.GetComponent<Spell_Info>().damage;
            StartCoroutine(PlayDamageAnimation());
        }

        else if(collision.gameObject.tag == "Spell" && collision.gameObject.GetComponent<Connected_Spell>())
        {
            health -= collision.gameObject.GetComponent<Connected_Spell>().SpellInfo.AOEdamage;
            StartCoroutine(PlayDamageAnimation());
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Spell" && collision.gameObject.GetComponent<Spell_Info>())
        {
            health -= collision.gameObject.GetComponent<Spell_Info>().damage;
            StartCoroutine(PlayDamageAnimation());
        }

        else if (collision.gameObject.tag == "Spell" && collision.gameObject.GetComponent<Connected_Spell>())
        {
            health -= collision.gameObject.GetComponent<Connected_Spell>().SpellInfo.AOEdamage;
            StartCoroutine(PlayDamageAnimation());
        }
    }

    public IEnumerator SapHealth(int SapNumber, float ActiveDuration, float SapDamage)
    {
        for (int i = 0; i < SapNumber; i++)
        {
            //Debug.Log("Sapping HP");
            yield return new WaitForSeconds(ActiveDuration / SapNumber - 1);
            health -= SapDamage;
            StartCoroutine(PlayDamageAnimation());
        };
        SappingHealth = false;
    }
    
    //Play Damage Animation for enemies -Grant Davis
    private IEnumerator PlayDamageAnimation()
    {
        anim.SetBool("Damage", true);
        //Damage animation timer (change if needed)
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Damage", false);
    }


}
