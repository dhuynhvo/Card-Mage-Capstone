// Authors: Grant Davis & Robert Bothne
// Difficulty scaling and projectile height by Robert Bothne, Mechanics by Grant Davis
// CS 426 Senior Project: Card Mage
// EnemyBulletScript.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
     public GameObject player; // Reference to the player object.
    private Rigidbody rb; // The Rigidbody component of the bullet.
    public float force; // The force that will be applied to the bullet.
    private float timer; // Timer for bullet life duration.
    public float bulletDamage; // The amount of damage the bullet does when it hits the player.
    [SerializeField]
    private Level_Counter levels; // Reference to the level counter.

    // Start is called before the first frame update
    void Start()
    {
        //EXPLICIT EASY LEVEL 1
        if (levels.Level == 1)
        {
            bulletDamage = 1;
        }

        // Get the Rigidbody component.
        rb = GetComponent<Rigidbody>();

        // Find the player object.
        player = GameObject.FindGameObjectWithTag("Player");

        // Position the bullet at the same height as the player.
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);

        // Calculate the direction from the bullet to the player.
        Vector3 direction = player.transform.position - transform.position;

        // Apply velocity to the bullet so it moves towards the player.
        rb.velocity = direction.normalized * force;

        // Rotate the bullet to face the player.
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(90, 90, rot);
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the timer.
        timer += Time.deltaTime;

        // If the bullet has existed for more than 30 seconds, destroy it.
        if(timer > 30)
        {
            Debug.Log("timed out proj");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If the bullet collides with the player, calculate the damage and apply it.
        if(other.gameObject.CompareTag("Player"))
        {
            float result = (float)levels.Level * .5f;//IMPORTANT: multiplies bullet damage * levels * damage mult
            if (result < 1)
            {
                result = 1;
            }
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(bulletDamage * result);
            Destroy(gameObject);
        }
        // If the bullet collides with an object that is not the player, boss, enemy, ground, camera, money, spell, card, or boss room, destroy the bullet.
        else if (other.gameObject.tag != "Boss" && other.gameObject.tag != "Enemy" && other.gameObject.tag != "Ground" && other.gameObject.tag != "MainCamera" && other.gameObject.tag != "Money" && other.gameObject.tag != "Spell" &&
            other.gameObject.tag != "Card" && other.gameObject.tag != "BossRoom" && gameObject.activeSelf)
        {
            Debug.Log(other);
            Destroy(gameObject);
        }
    }
}
