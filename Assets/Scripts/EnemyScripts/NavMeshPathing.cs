// Author: Grant Davis
// CS 426 Senior Project: Card Mage
// NavMeshPathing.cs
// Used for nearly all enemies to navigate using navigation mesh.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavMeshPathing : MonoBehaviour
{
    public NavMeshAgent enemy;
    GameObject player;

    // Enemy behavior variables
    public float enemyDistance = 5.0f;
    public float idleTimeMin = 2.0f;
    public float idleTimeMax = 5.0f;
    public float baseSpeed;
    private float idleTime;
    private bool isIdle;
    private Vector3 idlePosition;

    // Animator and sprite variables
    [SerializeField]
    private Animator anim;
    SpriteRenderer sprite;

    // Direction control variable
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");

        isIdle = false;
        idleTime = Random.Range(idleTimeMin, idleTimeMax);
        baseSpeed = enemy.speed;
        
        anim = transform.GetChild(0).GetComponent<Animator>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < enemyDistance)
        {
            // Follow the player
            if(enemy.isOnNavMesh)
            {
                enemy.SetDestination(player.transform.position);
            }
            isIdle = false;
        }
        else
        {
            if (!isIdle)
            {
                // Start idle movement
                idlePosition = GetRandomPoint(transform.position, 5.0f, NavMesh.AllAreas);
                if(enemy.isOnNavMesh)
                {
                    enemy.SetDestination(idlePosition);
                }
                isIdle = true;
                idleTime = Random.Range(idleTimeMin, idleTimeMax);
            }
            else
            {
                // Continue idle movement
                idleTime -= Time.deltaTime;
                if (idleTime <= 0)
                {
                    isIdle = false;
                }
            }
        }
        // Set walk animation state
        if (enemy.velocity.magnitude > 0.1f)
        {
            if(anim != null)
            {
                anim.SetBool("Walk", true);
            }
            
        }
        else
        {
            if(anim != null)
            {
                anim.SetBool("Walk", false);
            }
        }
            
        // Flip enemy model based on movement direction

        if (enemy.isOnNavMesh == true && enemy.remainingDistance > enemy.stoppingDistance)
        {
            if (enemy.destination.x > transform.position.x && !facingRight)
            {
                Flip();
            }
            else if (enemy.destination.x < transform.position.x && facingRight)
            {
                Flip();
            }
        }
    }

    // This function flips the direction the enemy is facing.
    private void Flip()
    {
        // Toggle the 'facingRight' flag.
        facingRight = !facingRight;

        // Flip the sprite in the X-axis.
        sprite.flipX = !sprite.flipX;

        // Create a new vector with the same values as the current localScale.
        Vector3 newScale = transform.localScale;

        // Uncommenting the following line would also flip the local scale of the transform
        // newScale.x *= -1;

        // Assign the new scale to the transform's localScale.
        transform.localScale = newScale;
    }

    // This function is used to visually display the enemy's detection range in the Unity editor.
    private void OnDrawGizmos()
    {
        // Draw a wireframe sphere at the position of the enemy, with a radius equal to the enemy's detection range.
        Gizmos.DrawWireSphere(transform.position, enemyDistance);
    }

    // This function returns a random point within a certain distance from an origin point, constrained by a navigation mesh.
    private Vector3 GetRandomPoint(Vector3 origin, float distance, int areaMask)
    {
        // Generate a random direction.
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        // Add the origin to the random direction to get a point within the desired range.
        randomDirection += origin;

        // Create a variable to store the result of the NavMesh sampling.
        NavMeshHit navHit;
        // Sample the NavMesh at the randomly generated point, with the specified mask.
        NavMesh.SamplePosition(randomDirection, out navHit, distance, areaMask);

        // Return the position of the sampled point.
        return navHit.position;
    }

    // This function sets a specific game object as the priority target for the enemy.
    public void SetPriorityTarget(Transform target)
    {
        // Set the player as the priority target.
        player = target.gameObject;
    }

    // This coroutine function resets the enemy's speed after a certain duration.
    public IEnumerator ResetSpeedForEnemy(float b, NavMeshAgent agent, float ActiveDuration)
    {
        // Wait for the specified duration minus 0.1 seconds.
        yield return new WaitForSeconds(ActiveDuration - .1f);

        // Reset the speed of the enemy.
        enemy.speed = b;
    }
}