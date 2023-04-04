using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNodeMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform[] nodes;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 7f;
    public float bulletStormCooldown = 1f;
    public float stopTimeAtNode = 3f; // The duration the boss will stop at each node
    public int bulletStormsPerNode = 3; // The number of bullet storms at each node
    public float playerDetectionRange = 7f; // The range at which the boss detects the player
    private int currentNode = 0;
    private float timeSinceLastShot;
    private bool isMoving = true; // Indicates if the boss is moving or stopped
    private int bulletStormCounter = 0; // Counts the number of bullet storms at the current node

    void Start() {
        // Find the positions of the nodes
        nodes = new Transform[5];
        nodes[0] = GameObject.Find("node1").transform;
        nodes[1] = GameObject.Find("node2").transform;
        nodes[2] = GameObject.Find("node3").transform;
        nodes[3] = GameObject.Find("node4").transform;
        nodes[4] = GameObject.Find("node5").transform;

         // Get the player reference
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        bulletSpawnPoint.SetParent(transform);
    }

    void Update() {
    // Check if the player is within the detection range
    Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
    if (distanceToPlayer <= playerDetectionRange)
    {
        if (isMoving)
        {
            // Move towards the current node
            transform.position = Vector3.MoveTowards(transform.position, nodes[currentNode].position, speed * Time.deltaTime);

            // Check if we've reached the current node
            if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.1f) {
                // Stop moving and start shooting bullet storms
                isMoving = false;
                StartCoroutine(ShootBulletStormsAtNode());
            }
        }
    }

    // Update the player detection range based on the current node
    if (currentNode == 0) {
        playerDetectionRange = 5f;
    } else {
        playerDetectionRange = 15f;
    }
}

IEnumerator ShootBulletStormsAtNode() {
    // Shoot bullet storms multiple times
    for (bulletStormCounter = 0; bulletStormCounter < bulletStormsPerNode; bulletStormCounter++)
    {
        ShootBulletStorm();
        yield return new WaitForSeconds(bulletStormCooldown);
    }

    // Wait for a few seconds before moving to the next node
    yield return new WaitForSeconds(stopTimeAtNode);

    if (currentNode == 0) {
        // If we're at node 0, shuffle the order of the nodes randomly
        ShuffleNodes();
        // Set the current node to a random node other than node 0
        currentNode = UnityEngine.Random.Range(1, nodes.Length);
    } else {
        // Otherwise, return to node 0
        currentNode = 0;
    }

    // Start moving again
    isMoving = true;
}

void ShuffleNodes()
{
    // Shuffle the order of the nodes randomly, excluding the first node
    for (int i = 1; i < nodes.Length; i++)
    {
        int randomIndex = UnityEngine.Random.Range(i, nodes.Length);
        Transform temp = nodes[i];
        nodes[i] = nodes[randomIndex];
        nodes[randomIndex] = temp;
    }
}

    void ShootBulletStorm()
    {
        // Set the number of bullets and angle between them
        int numberOfBullets = 12;
        float angleBetweenBullets = 360f / numberOfBullets;

        for (int i = 0; i < numberOfBullets; i++)
        {
            // Calculate the rotation for the current bullet
            Quaternion bulletRotation = Quaternion.Euler(0f, angleBetweenBullets * i, 0f) * bulletSpawnPoint.rotation;

            // Instantiate a bullet and set its position and rotation
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletRotation);

            // Set the bullet's velocity
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            // Destroy the bullet after a certain time to prevent memory issues
            Destroy(bullet, 5f);
        }
    }
}