using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSound : MonoBehaviour
{
    [SerializeField]
    public AudioClip damageSound;
    [SerializeField]
    public AudioClip deathSound;
    public AudioSource audioSource;

    private Enemy_Info enemyInfo;

    private bool deathSoundPlayed;

    void Start()
    {
        // Initialize the audioSource variable
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        enemyInfo = GetComponent<Enemy_Info>();
        deathSoundPlayed = false;
    }

    void Update()
    {
        if (enemyInfo.health <= 0 && !deathSoundPlayed)
        {
            PlaySound(deathSound);
            deathSoundPlayed = true;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}