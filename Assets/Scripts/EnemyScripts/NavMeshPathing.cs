using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPathing : MonoBehaviour
{
    public NavMeshAgent enemy;
    GameObject player;

    public float enemyDistance = 1.0f;
    public float idleTimeMin = 2.0f;
    public float idleTimeMax = 5.0f;
    public float baseSpeed;
    private float idleTime;
    private bool isIdle;
    private Vector3 idlePosition;
    [SerializeField]
    private Animator anim;
    
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
    
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < enemyDistance)
        {
            // Follow the player
            enemy.SetDestination(player.transform.position);
            isIdle = false;
        }
        else
        {
            if (!isIdle)
            {
                // Start idle movement
                idlePosition = GetRandomPoint(transform.position, 5.0f, NavMesh.AllAreas);
                enemy.SetDestination(idlePosition);
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
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
            
        // Flip enemy model based on movement direction
        if (enemy.remainingDistance > enemy.stoppingDistance)
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

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyDistance);
    }

    private Vector3 GetRandomPoint(Vector3 origin, float distance, int areaMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, areaMask);
        return navHit.position;
    }

    public void SetPriorityTarget(Transform target)
    {
        // Set the player as the priority target
        player = target.gameObject;
    }

    public IEnumerator ResetSpeedForEnemy(float b, NavMeshAgent agent, float ActiveDuration)
    {
        yield return new WaitForSeconds(ActiveDuration - .1f);
        enemy.speed = b;
    }
}