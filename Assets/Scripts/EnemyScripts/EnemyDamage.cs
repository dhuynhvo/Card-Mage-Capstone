//Author: Grant Davis
//CS 426 Senior Project: Card Mage
//EnemyDamage.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the damage enemies can inflict on the player
public class EnemyDamage : MonoBehaviour
{
    // Reference to the PlayerHealth component of the player
    public PlayerHealth playerHealth;
    // Base damage an enemy can inflict
    public int baseDamage = 1;
    // Delay in seconds between each damage inflicted
    public float damageDelay = 1f;
    // Reference to the Enemy_Mechanics component of the enemy
    [SerializeField] private Enemy_Mechanics EM;
    // Reference to the Ghost_Mechanics component of the enemy (if it's a ghost)
    private Ghost_Mechanics GM;
    // Flag to determine if the enemy can inflict damage
    public bool canDamage = true;
    // Reference to the Animator component of the enemy
    [SerializeField] private Animator anim;
    // Reference to the Level_Counter component
    [SerializeField] private Level_Counter levelCounter;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Enemy_Mechanics component
        EM = gameObject.GetComponent<Enemy_Mechanics>();
        // Find the player game object
        GameObject player = GameObject.FindWithTag("Player");
        // If player not found, log an error
        if(player == null)
        {
            Debug.LogError("Player is not found!");
        }
        // If player found, get the PlayerHealth component
        else
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        // Get the Animator component
        anim = GetComponent<Animator>();
    }

    // This function is called when a collision is detected
    private void OnCollisionEnter(Collision collision)
    {
        // If the collision is with the player and the enemy is not dead, initiate damage
        if(collision.gameObject.tag == "Player" && EM.NotDead && canDamage)
        {
            StartCoroutine(DoDamage());
        }
        // If the collision is with the player and the ghost enemy is not dead, initiate damage
        if(collision.gameObject.tag == "Player" && GM.NotDead && canDamage)
        {
            StartCoroutine(DoDamage());
        }
    }

    // Coroutine to manage damage infliction
    public IEnumerator DoDamage()
    {
        // Set the canDamage flag to false
        canDamage = false;

        // Start the attack animation
        anim.SetBool("Attack", true);

        // Calculate the damage based on the current level
        int damage = baseDamage * levelCounter.Level;

        // Inflict damage on the player
        playerHealth.TakeDamage(damage);

        // Disable the enemy's collider to allow the player to walk through the enemy
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }

        // Wait for the damage delay
        yield return new WaitForSeconds(damageDelay);

        // Re-enable the collider
        if (col != null)
        {
            col.isTrigger = false;
        }

        // Stop the attack animation
        anim.SetBool("Attack", false);

        // Reset the canDamage flag to true
        canDamage = true;
    }
}