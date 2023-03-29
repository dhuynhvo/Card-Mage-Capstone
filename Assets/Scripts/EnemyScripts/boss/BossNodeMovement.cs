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
    public float bulletSpeed = 10f;
    public float bulletStormCooldown = 1f;
    private int currentNode = 0;
    private float timeSinceLastBulletStorm = 0f;
    public float minBulletStormInterval = 3f;
    public float maxBulletStormInterval = 6f;
    private float nextBulletStormTime;

    void Start()
    {
        // Find the positions of the nodes
        nodes = new Transform[5];
        nodes[0] = GameObject.Find("node1").transform;
        nodes[1] = GameObject.Find("node2").transform;
        nodes[2] = GameObject.Find("node3").transform;
        nodes[3] = GameObject.Find("node4").transform;
        nodes[4] = GameObject.Find("node5").transform;

        // Initialize the next bullet storm time
        nextBulletStormTime = Time.time + UnityEngine.Random.Range(minBulletStormInterval, maxBulletStormInterval);

        bulletSpawnPoint.SetParent(transform);
    }

    void Update()
    {
        // Move towards the current node
        transform.position = Vector3.MoveTowards(transform.position, nodes[currentNode].position, speed * Time.deltaTime);

        // Check if we've reached the current node
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.1f)
        {
            // Move to the next node
            currentNode = (currentNode + 1) % nodes.Length;

            // If we've reached the last node, shuffle the order of the nodes
            if (currentNode == 0)
            {
                ShuffleNodes();
            }
        }
        // Check if it's time for the next bullet storm
        if (Time.time >= nextBulletStormTime)
        {
            ShootBulletStorm();

            // Set the next bullet storm time
            nextBulletStormTime = Time.time + UnityEngine.Random.Range(minBulletStormInterval, maxBulletStormInterval);
        }
    }

    void ShuffleNodes()
    {
        // Shuffle the order of the nodes randomly
        for (int i = 0; i < nodes.Length; i++)
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