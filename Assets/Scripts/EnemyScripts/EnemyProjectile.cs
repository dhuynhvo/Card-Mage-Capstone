//Author: Grant Davis
//CS 426 Senior Project: Card Mage
//EnemyProjectile.cs
// The following script is used to manage the projectiles of enemies within the game 'Card Mage'.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject player; // Reference to the player object.
    public float fireRange = 4f; // The range at which the enemy will start firing projectiles.
    public float projectileSpeed; // The speed at which the projectile moves.
    public GameObject projectilePrefab; // The prefab of the projectile that will be instantiated when firing.

    private float distanceToPlayer; // To store the current distance from the enemy to the player.
    
    // Start is called before the first frame update
    void Start()
    {
        // As of now, no operations are carried out when the script starts.
    }

    // Update is called once per frame
    void Update() {
        // Calculate the distance between the enemy and the player.
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // If the player is within the enemy's firing range, fire a projectile.
        if (distanceToPlayer <= fireRange) {
            FireProjectile();
        }
    }

    void FireProjectile() {
        // Instantiate a new projectile at the enemy's position.
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Calculate the direction from the enemy to the player.
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        // Set the velocity of the projectile to make it move towards the player.
        projectile.GetComponent<Rigidbody>().velocity = directionToPlayer * projectileSpeed;
    }
}
