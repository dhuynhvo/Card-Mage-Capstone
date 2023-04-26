using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : MonoBehaviour
{
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float idleTimeMin = 2.0f;
    [SerializeField] private float idleTimeMax = 5.0f;
    [SerializeField] private Transform player;

    private Animator anim;
    private SpriteRenderer sprite;

    private bool isIdle;
    private float idleTime;
    private Vector3 idlePosition;
    private bool facingRight = true;
    public Ghost_Mechanics ghostMechanics;

    private float initialY;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        isIdle = false;
        idleTime = Random.Range(idleTimeMin, idleTimeMax);

        initialY = transform.position.y;
    }

    void Update()
    {
        if (IsPlayerInRange(detectionRange))
        {
            // Move the ghost enemy while keeping the Y position constant
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 34; // Set Y direction to 0
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
            isIdle = false;
        }
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

    private bool IsPlayerInRange(float range)
    {
        if (player == null)
        {
            return false;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= range;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        sprite.flipX = !sprite.flipX;
    }

    private Vector3 GetRandomPoint(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        return randomDirection;
    }
}
