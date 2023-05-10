//Author: Grant Davis
//CS 426 Senior Project: Card Mage
//BossBullet.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public GameObject player; // Reference to the player object.
    private Rigidbody rb; // The Rigidbody component of the bullet.
    public float baseDamage; // The base damage of the bullet.
    private float bulletDamage; // The actual damage dealt by the bullet.
    public float force; // The force that will be applied to the bullet.
    private float timer; // Timer for bullet life duration.
    [SerializeField]
    private Level_Counter levels; // Reference to the level counter.

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component.
        rb = GetComponent<Rigidbody>();

        // Find the player object.
        player = GameObject.FindGameObjectWithTag("Player");

        // Instead of aiming the bullet directly at the player, it will fire in the direction the bullet is facing.
        Vector3 direction = transform.forward;
        rb.velocity = direction * force;

        // Position the bullet at the same height as the player.
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);

        // Calculate the rotation based on the bullet's forward direction.
        float rot = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(90, 90, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the timer.
        timer += Time.deltaTime;

        // If the bullet has existed for more than 10 seconds, destroy it.
        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If the bullet collides with the player, calculate the damage and apply it.
        if (other.gameObject.CompareTag("Player"))
        {
            float result = (float)levels.Level * .5f;
            if (result < 1)
            {
                result = 1;
            }
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(bulletDamage * result);
            Destroy(gameObject);
        }
        // If the bullet collides with an object that is not the player, boss, enemy, boss room, ground, or spell, destroy the bullet.
        else if (other.gameObject.tag != "Spell" && other.gameObject.tag != "Boss" && other.gameObject.tag != "Enemy" && other.gameObject.tag != "BossRoom"  && other.gameObject.tag != "Ground" && gameObject.activeSelf)
        {
            Destroy(gameObject);
        }
    }
}