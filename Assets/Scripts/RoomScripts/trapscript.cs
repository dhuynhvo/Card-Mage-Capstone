using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class trapscript : MonoBehaviour
{
    public int basedamage = 5; // amount of damage the trap deals
    private int damage;
    public float armDelay = 1.0f; // delay in seconds before the trap becomes armed
    public float disarmDelay = 2.0f; // delay in seconds before the trap becomes disarmed after being armed
    public float damageDelay = 0.2f; // delay in seconds before the trap can deal damage again
    public Animator animator; // animator component for the trap
    bool isArmed = false; // whether or not the trap is currently armed
    float disarmTime; // time at which the trap should be disarmed
    float nextDamageTime; // time at which the trap can deal damage again
    public AudioClip TrapNoise;
    [SerializeField]
    private Level_Counter levels;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isArmed)
        {
            // if the player enters the collider and the trap isn't armed, arm the trap after the delay
            animator.SetBool("IsActive", false);
            Invoke("ArmTrap", armDelay);
        }
    }

    void ArmTrap()
    {
        isArmed = true;
        animator.SetBool("IsActive", true);
        damage = basedamage + levels.Level * 2;
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.clip = TrapNoise;
        audio.Play();
        disarmTime = Time.time + disarmDelay; // set the time at which the trap should be disarmed
    }
    void playAudio()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.clip = TrapNoise;
        audio.Play();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isArmed && Time.time >= nextDamageTime)
        {
            // if the player is still in the collider and the trap is armed and can deal damage again, deal damage
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.clip = TrapNoise;
            audio.Play();
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            nextDamageTime = Time.time + damageDelay; // set the time at which the trap can deal damage again
        }
    }

    void Update()
    {
        if (isArmed && Time.time >= disarmTime)
        {
            Invoke("playAudio", 0);
            
            // if the trap has been active for the specified time and hasn't dealt damage, disarm the trap
            isArmed = false;
            animator.SetBool("IsActive", false);
            disarmTime = 0; // reset the disarm time
            nextDamageTime = 0; // reset the damage delay
        }
    }
}