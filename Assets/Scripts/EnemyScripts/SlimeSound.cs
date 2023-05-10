// Author: Grant Davis
// CS 426 Senior Project: Card Mage
// SlimeSound.cs
// Sound for all enemies when hit and when killed.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the sound effects for enemies
public class SlimeSound : MonoBehaviour
{
    // Serialize the AudioClip fields to set them in the Unity editor
    [SerializeField]
    public AudioClip damageSound;
    [SerializeField]
    public AudioClip deathSound;
    // Define an AudioSource to play the sound effects
    public AudioSource audioSource;

    // Reference to the Enemy_Info component
    private Enemy_Info enemyInfo;

    // Flag to ensure the death sound is played only once
    private bool deathSoundPlayed;

    // This function is called when the script is first loaded
    void Start()
    {
        // Initialize the audioSource variable
        audioSource = GetComponent<AudioSource>();
        // If there's no AudioSource component, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Get the Enemy_Info component
        enemyInfo = GetComponent<Enemy_Info>();
        // Initialize the deathSoundPlayed flag
        deathSoundPlayed = false;
    }

    // This function is called once per frame
    void Update()
    {
        // If the enemy's health is 0 and the death sound hasn't been played yet, play it
        if (enemyInfo.health <= 0 && !deathSoundPlayed)
        {
            PlaySound(deathSound);
            deathSoundPlayed = true;
        }
    }

    // This function plays the specified AudioClip
    public void PlaySound(AudioClip clip)
    {
        // Set the clip to be played and play it
        audioSource.clip = clip;
        audioSource.Play();
    }
}