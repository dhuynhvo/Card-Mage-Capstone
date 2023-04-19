//Dan Huynhvo and Grant Davis
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

    // Add damage and death sounds to Enemy_Info
    [SerializeField]
    private AudioClip damageSound;
    [SerializeField]
    private AudioClip deathSound;

    //reference to SlimeSound
    private SlimeSound slimeSound;
    private bool isTakingDamage;
    private const string DamageAnimationName = "Damage";    

    void Start()
    {
        anim = GetComponent<Animator>();
        maxHealth = health;
        isTakingDamage = false;

        // Get reference to SlimeSound
        slimeSound = GetComponent<SlimeSound>();

        // Assign damage and death sounds
        if (slimeSound != null)
        {
            slimeSound.damageSound = damageSound;
            slimeSound.deathSound = deathSound;
        }
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
    
    private IEnumerator PlayDamageAnimation()
    {
        // Check if the damage animation is already playing
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName(DamageAnimationName))
        {
            anim.SetBool("Damage", true);

            // Play damage sound
            slimeSound.PlaySound(slimeSound.damageSound);

            yield return new WaitForSeconds(0.5f);
            anim.SetBool("Damage", false);
        }
    }
}
