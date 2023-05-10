// Author: Grant Davis
// CS 426 Senior Project: Card Mage
// GhostEnemy.cs
// Specific for the ghost enemy that will walk through all walls.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : MonoBehaviour
{
    // Serialized fields allow you to set these variables in the Unity editor
    // Define the detection range, speed, idle time and player's transform
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float idleTimeMin = 2.0f;
    [SerializeField] private float idleTimeMax = 5.0f;
    [SerializeField] private Transform player;

    // Define references for the animator and sprite renderer
    private Animator anim;
    private SpriteRenderer sprite;

    // Define state variables for idling
    private bool isIdle;
    private float idleTime;
    private Vector3 idlePosition;
    // Define a boolean to determine if the ghost is facing right
    private bool facingRight = true;
    public Ghost_Mechanics ghostMechanics;

    // Keep track of initial Y position
    private float initialY;

    // This function is called when the script is first loaded
    void Start()
    {
        // Find the player's transform using the tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Get the animator and sprite renderer components
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        // Initialize idle state
        isIdle = false;
        // Store initial Y position
        idleTime = Random.Range(idleTimeMin, idleTimeMax);
        // Store initial Y position
        initialY = transform.position.y;
    }

    // If the player is in range, move towards the player
    void Update()
    {
        // If the player is in range, move towards the player
        if (IsPlayerInRange(detectionRange))
        {
            // Move the ghost enemy while keeping the Y position constant
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 34; // Set Y direction to 0
            transform.position += direction * moveSpeed * Time.deltaTime;
            // Keep Y position constant
            transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
            isIdle = false;
        }
        // If the player is not in range, idle
        else
        {
            if (!isIdle)
            {
                idlePosition = GetRandomPoint(transform.position, 5.0f);
                isIdle = true;
                idleTime = Random.Range(idleTimeMin, idleTimeMax);
            }
            else
            {
                idleTime -= Time.deltaTime;
                // Exit idle state when idle time is up
                if (idleTime <= 0)
                {
                    isIdle = false;
                }
            }
        }

        // Set walk animation state
        if (Vector3.Distance(transform.position, player.position) > 0.1f)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        // Flip enemy model based on movement direction
        if (player.position.x < transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (player.position.x > transform.position.x && facingRight)
        {
            Flip();
        }
    }

    // This function checks if the player is within a specified range
    private bool IsPlayerInRange(float range)
    {
        if (player == null)
        {
            return false;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= range;
    }

    //This function flips the direction the enemy is facing.
    private void Flip()
    {
        facingRight = !facingRight;
        sprite.flipX = !sprite.flipX;
    }

    // This function returns a random point within a certain distance from an origin point.
    private Vector3 GetRandomPoint(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        return randomDirection;
    }
}
