using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPathing : MonoBehaviour
{
    NavMeshAgent enemy;
    GameObject player;
    //Transform that NPC has to follow
    //public Transform transformToFollow;
 
    //NavMesh Agent variable
    //NavMeshAgent agent;
 
    public float enemyDistance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }
 
    // Update is called once per frame
    void Update()
    {
        //enemy.SetDestination(player.transform.position);
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < enemyDistance)
        {
            Debug.Log("test1");
            //Follow the player
            enemy.SetDestination(player.transform.position);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyDistance);
    }
}