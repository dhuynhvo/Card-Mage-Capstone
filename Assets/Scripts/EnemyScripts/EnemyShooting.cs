// Author: Grant Davis
// CS 426 Senior Project: Card Mage
// EnemyShooting.cs
// Original Enemy shooting script (unused for EnemyShootingRed.cs)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the enemy shooting behavior
public class EnemyShooting : MonoBehaviour
{
    // Prefab for the bullet to be shot
    public GameObject bullet;
    // Position from where the bullet will be shot
    public Transform bulletPos;
    // Timer to manage shooting cooldown
    private float timer;
    // Cooldown time between each shot
    public float cooldown;
    // Range within which the enemy can shoot
    public float enemyRange;
    // Reference to the player game object
    private GameObject player;
    // Reference to the Enemy_Info component
    private Enemy_Info ei;
    // Reference to the Animator component
    [SerializeField] private Animator anim;
    // Flag to check if the enemy is dead
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        // Find the player game object
        player = GameObject.FindGameObjectWithTag("Player");

        // Get the Enemy_Info component
        ei = GetComponent<Enemy_Info>();

        // Get the Animator component
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the enemy is dead, do not proceed
        if(ei.health <= 0)
        {
            return;
        }

        // Calculate the distance between the enemy and the player
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // If the player is within the enemy's range, manage shooting
        if (distance < enemyRange)
        {
            // Increase the timer by the time passed since the last frame
            timer += Time.deltaTime;

            // If the timer exceeds the cooldown, shoot and reset the timer
            if(timer > cooldown)
            {
                timer = 0;
                StartCoroutine(ShootWithAnimation());
            } 
        }
    }
    
    // Function to instantiate a bullet and play the bullet sound
    void shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
        AudioManager.instance.Play("EnemyProjectile");
    }
    
    // Coroutine to manage shooting with animation
    IEnumerator ShootWithAnimation()
    {
        // Start the attack animation
        anim.SetBool("Attack", true);

        // Wait for the time in the animation when the bullet should be fired
        yield return new WaitForSeconds(0.5f);

        // Shoot the bullet
        shoot();

        // Wait for the remaining time of the animation
        yield return new WaitForSeconds(0.5f);

        // Stop the attack animation
        anim.SetBool("Attack", false);
    }
}