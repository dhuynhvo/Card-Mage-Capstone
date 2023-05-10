// Dan Huynhvo and Grant Davis
// UNR
// CS 425
// Enemy_Info.cs
// Original code by Dan added functions with sounds, levels, and dmg numbers.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    // damage and death sounds to Enemy_Info - Grant Davis
    [SerializeField]
    private AudioClip damageSound;
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private Level_Counter levelCounter;

    // Add death sound repeat count -Grant Davis
    [SerializeField]
    private int deathSoundRepeat = 3;

    // Reference to TextMesh prefab -Grant Davis
    [SerializeField]
    private GameObject damageNumberPrefab;

    // Reference to SlimeSound -Grant Davis
    private SlimeSound slimeSound;
    private bool isTakingDamage;
    private const string DamageAnimationName = "Damage";

    private bool alternateSpawnPosition = false;
    // Add the serialized field toggle for damage numbers -Grant Davis
    [SerializeField] private bool showDamageNumbers = true;

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
        // Update the enemy health based on the current level
        health += levelCounter.Level*2;
        maxHealth = health;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void HandleCollision(GameObject collision)
    {
        float damage = 0;

        if (collision.tag == "Spell" && collision.GetComponent<Spell_Info>())
        {
            damage = collision.GetComponent<Spell_Info>().damage;
        }
        else if (collision.tag == "Spell" && collision.GetComponent<Connected_Spell>())
        {
            damage = collision.GetComponent<Connected_Spell>().SpellInfo.AOEdamage;
        }

        if (damage > 0)
        {
            health -= damage;
            StartCoroutine(PlayDamageAnimation());
            ShowDamageNumber(damage);
        }

        if (health <= 0)
        {
            if (gameObject.CompareTag("Boss"))
            {
                StartCoroutine(PlayDeathSoundMultipleTimes());
            }
            else
            {
                slimeSound.PlaySound(slimeSound.deathSound);
            }
        }
    }

    //Spawns damage number on both one side of the enemy then the other side.
    private void ShowDamageNumber(float damage)
    {
        if (showDamageNumbers)
        {
            // Calculate spawn position based on the value of alternateSpawnPosition
            Vector3 spawnPosition = alternateSpawnPosition
                ? transform.position + new Vector3(0.4f, 0.1f, 0)
                : transform.position + new Vector3(-0.4f, 0.1f, 0);

            GameObject damageNumber = Instantiate(damageNumberPrefab, spawnPosition, Quaternion.Euler(90, 0, 0));
            TextMeshPro textMeshPro = damageNumber.GetComponent<TextMeshPro>();

            if (textMeshPro != null)
            {
                textMeshPro.text = damage.ToString("F0");
                StartCoroutine(AnimateDamageNumber(damageNumber));
            }
            else
            {
                Debug.LogError("TextMeshPro component not found on the damage number prefab.");
            }

            // Toggle alternateSpawnPosition for the next spawn
            alternateSpawnPosition = !alternateSpawnPosition;
        }
    }

    private IEnumerator AnimateDamageNumber(GameObject damageNumber)
    {
        float duration = 1f;
        float t = 0;
        Vector3 originalPosition = damageNumber.transform.position;

        while (t < duration)
        {
            t += Time.deltaTime;
            damageNumber.transform.position = new Vector3(originalPosition.x, originalPosition.y + t * 0.5f, originalPosition.z);
            yield return null;
        }

        Destroy(damageNumber);
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
    
    private IEnumerator PlayDeathSoundMultipleTimes()
    {
        for (int i = 0; i < deathSoundRepeat; i++)
        {
            slimeSound.PlaySound(slimeSound.deathSound);
            yield return new WaitForSeconds(slimeSound.deathSound.length);
        }
    }
}
