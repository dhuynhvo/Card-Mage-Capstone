//Author: Grant Davis
//CS 426 Senior Project: Card Mage
//EnemyProjectile.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject player;
    public float fireRange = 4f;
    public float projectileSpeed;
    public GameObject projectilePrefab;

    private float distanceToPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= fireRange) {
            FireProjectile();
        }
    }

    void FireProjectile() {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        projectile.GetComponent<Rigidbody>().velocity = directionToPlayer * projectileSpeed;
    }
}