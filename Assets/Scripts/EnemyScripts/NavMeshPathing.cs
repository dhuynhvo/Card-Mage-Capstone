using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPathing : MonoBehaviour
{
    NavMeshAgent enemy;
    GameObject player;

    public float enemyDistance = 1.0f;
    public float idleTimeMin = 2.0f;
    public float idleTimeMax = 5.0f;
    private float idleTime;
    private bool isIdle;
    private Vector3 idlePosition;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        isIdle = false;
        idleTime = Random.Range(idleTimeMin, idleTimeMax);
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
}