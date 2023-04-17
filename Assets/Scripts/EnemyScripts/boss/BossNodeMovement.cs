using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNodeMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform[] nodes;
    public GameObject bulletPrefab;
    public GameObject bulletSingle;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 7f;
    public float bulletStormCooldown = 1f;
    public float stopTimeAtNode = 3f; // The duration the boss will stop at each node
    public int bulletStormsPerNode = 3; // The number of bullet storms at each node
    public float playerDetectionRange = 4f; // The range at which the boss detects the player
    private int currentNode = 0;
    private float timeSinceLastShot;
    private bool isMoving = true; // Indicates if the boss is moving or stopped
    private int bulletStormCounter = 0; // Counts the number of bullet storms at the current node
    private Animator anim;
    private bool isEngaged = false; // Indicates if the boss has engaged in a fight

    void Start()
    {
        nodes = new Transform[5];
        nodes[0] = GameObject.Find("node1").transform;
        nodes[1] = GameObject.Find("node2").transform;
        nodes[2] = GameObject.Find("node3").transform;
        nodes[3] = GameObject.Find("node4").transform;
        nodes[4] = GameObject.Find("node5").transform;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        bulletSpawnPoint.SetParent(transform);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
        if (distanceToPlayer <= playerDetectionRange)
        {
            isEngaged = true;
        }

        if (isEngaged)
        {
            if (isMoving)
            {
                anim.SetBool("Walk", isMoving);
                Vector3 direction = nodes[currentNode].position - transform.position;
                Flip(direction.x);
                transform.position = Vector3.MoveTowards(transform.position, nodes[currentNode].position, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.1f)
                {
                    isMoving = false;
                    anim.SetBool("Walk", isMoving);
                    StartCoroutine(ShootBulletStormsAtNode());
                }
            }
        }
    }

    private void Flip(float direction)
    {
        if (direction > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    IEnumerator ShootBulletStormsAtNode() {
        // Rotate the boss to face the player
        FacePlayer();
            // Shoot bullet storms multiple times
            //StartCoroutine(ShootWithAnimation());
            //shoot();
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
        // shoot();
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
    private void FacePlayer()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        float direction = playerPosition.x - transform.position.x;

        if (direction > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    void shoot()
    {
        Instantiate(bulletSingle, transform.position, Quaternion.identity);
        AudioManager.instance.Play("BossBigProjectile");
    }
}
