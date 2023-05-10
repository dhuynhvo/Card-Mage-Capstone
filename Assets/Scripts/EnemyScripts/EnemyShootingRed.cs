// Author: Robert and Grant Davis
// used for all ranged enemies other than boss. Changes from EnemyShooting.cs by Robert bothne (difficulty scale, several bullets fired, single shot mode, fire rate, first strike timer and initial attack)
// CS 426 Senior Project: Card Mage
// EnemyShootingRed.cs
// code originally implemented in EnemyShooting.cs, reimplemented here instead.

using System.Collections;
using UnityEngine;

// This class manages the shooting behavior of a specific type of enemy (Red)
public class EnemyShootingRed : MonoBehaviour
{
    // Level counter reference to factor in difficulty scaling
    [SerializeField]
    private Level_Counter levels;
    // Boolean to decide if enemy should fire only a single shot per attack
    public bool singleShot = false;
    // Prefab for the bullet to be shot
    public GameObject bullet;
    // Position from where the bullet will be shot
    public Transform bulletPos;
    // Timer to manage shooting cooldown
    private float timer;
    // Cooldown time between each shot
    public float cooldown;
    // Bool to manage first strike
    private bool firstStrike;
    // Time till first attack
    public float initialAttack;
    // Range within which the enemy can shoot
    public float enemyRange;
    // Additional duration for shooting based on level
    float additionalDuration;
    // Reference to the player game object
    private GameObject player;
    // Reference to the Enemy_Info component
    private Enemy_Info ei;
    // Reference to the Animator component
    [SerializeField] private Animator anim;
    // The duration for shooting
    public float shootDuration = 5f;
    // The time between each shot
    public float firerate = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        // Find the player game object
        player = GameObject.FindGameObjectWithTag("Player");

        // Get the Enemy_Info component
        ei = GetComponent<Enemy_Info>();

        // Get the Animator component
        anim = GetComponent<Animator>();

        // Calculate additional shooting duration based on level
        additionalDuration = (float)levels.Level / 7;
    }

    // Update is called once per frame
    void Update()
    {
        // If the enemy is dead, do not proceed
        if (ei.health <= 0)
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

            // If the timer exceeds the time for the first attack, shoot and reset the timer
            if (timer > initialAttack && firstStrike == true)
            {
                firstStrike = false;
                timer = 0;
                StartCoroutine(ShootWithAnimation());
            }

            // If the timer exceeds the cooldown, shoot and reset the timer
            if (timer > cooldown && firstStrike == false)
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

        // Initialize timers
        float durationTimer = 0;
        float fireTimer = 0;

        // Check if the enemy should fire multiple shots
        if (singleShot != true)
        {
            // Keep shooting until the duration is reached
            while (durationTimer < shootDuration + additionalDuration)
            {
                // If the time since the last shot exceeds the fire rate, shoot
                if (fireTimer > firerate)
                {
                    // Check if the enemy is alive before shooting
                    if (ei.health > 0)
                    {
                        shoot();
                    }
                    fireTimer = 0;
                }
                // Update timers
                durationTimer += Time.deltaTime;
                fireTimer += Time.deltaTime;
                yield return null; // return control to the Unity engine for this frame and wait for the next frame before continuing
            }
        }
        else
        {
            // If single shot, check if the enemy is alive before shooting
            if (ei.health > 0)
            {
                shoot();
            }
        }
        // Stop the attack animation
        anim.SetBool("Attack", false);
    }
}