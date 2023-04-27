//Author: Grant Davis
//CS 426 Senior Project
//BossNodeMovement.cs

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNodeMovement : MonoBehaviour
{
    // Boss movement variables
    public float speed = 5f;
    public Transform[] nodes;
    private int currentNode = 0;
    private bool isMoving = true; // Indicates if the boss is moving or stopped
    // Bullet storm variables
    public GameObject bulletPrefab;
    public GameObject bulletSingle;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 7f;
    public float bulletStormCooldown = 1f;
    public float stopTimeAtNode = 3f; // The duration the boss will stop at each node
    public float bulletStormsPerNode = 3f; // The number of bullet storms at each node
    // Player detection and engagement variables
    public float playerDetectionRange = 4f; // The range at which the boss detects the player
    private bool isEngaged = false; // Indicates if the boss has engaged in a fight
    // Animation variable
    private Animator anim;
    // Boss health and phase control
    private Enemy_Info enemyInfo;
    private bool isSecondPhase = false;
    // Phase 1 variables
    [SerializeField] float numberOfBullets = 4;
    // Phase 2 variables
    public float numberOfBulletsPhase2 = 8f;
    public float damagePhase2 = 1.5f;
    public float bulletSpeedatHalfHP = 10f;
    [SerializeField] private Level_Counter levels;
    public Color goldTint = new Color(1f, 0.84f, 0f); // Gold color
    public float goldTintMultiplier = 0.1f; // The amount of gold tint to apply per level


    void Start()
    {
        // Initialize nodes array and find node game objects
        nodes = new Transform[5];
        nodes[0] = GameObject.Find("node1").transform;
        nodes[1] = GameObject.Find("node2").transform;
        nodes[2] = GameObject.Find("node3").transform;
        nodes[3] = GameObject.Find("node4").transform;
        nodes[4] = GameObject.Find("node5").transform;

        // Set bullet spawn point as a child of the boss
        bulletSpawnPoint.SetParent(transform);

        // Get the animator component
        anim = GetComponent<Animator>();

        // Get the enemy information component
        enemyInfo = GetComponent<Enemy_Info>();

        // Adjust boss variables based on the level
        AdjustBossDifficulty(levels.Level);
        SetBossColorByLevel(levels.Level);
    }

    void Update()
    {
        if (!IsDead())
            {
            // Check if the player is within the detection range
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
            if (distanceToPlayer <= playerDetectionRange)
            {
                isEngaged = true;
            }

            // If engaged, move between nodes and shoot bullet storms
            if (isEngaged)
            {
                if (isMoving)
                {
                    // Move the boss towards the current node
                    anim.SetBool("Walk", isMoving);
                    Vector3 direction = nodes[currentNode].position - transform.position;
                    Flip(direction.x);
                    transform.position = Vector3.MoveTowards(transform.position, nodes[currentNode].position, speed * Time.deltaTime);

                    // If the boss has reached the node, stop and shoot bullet storms
                    if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.1f)
                    {
                        isMoving = false;
                        anim.SetBool("Walk", isMoving);
                        StartCoroutine(ShootBulletStormsAtNode());
                    }
                }
            }

            // If the boss health is below 50%, enter the second phase
            if (!isSecondPhase && enemyInfo.health <= enemyInfo.maxHealth * 0.5f)
            {
                EnterSecondPhase();
            }
        }
        else
        {
            // Stop all animations and movement when the boss is dead
            anim.SetBool("Walk", false);
            isMoving = false;
        }
    }

    private void SetBossColorByLevel(int level)
    {
        Renderer bossRenderer = GetComponent<Renderer>();
        Color randomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        bossRenderer.material.color = randomColor;
    }

    // Flip the boss to face the correct direction based on movement
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
    IEnumerator FlashFullGold()
    {
        Renderer bossRenderer = GetComponent<Renderer>();
        Color originalColor = bossRenderer.material.color;

        bossRenderer.material.color = goldTint;
        yield return new WaitForSeconds(0.5f);

        bossRenderer.material.color = originalColor;
    }

    IEnumerator ShootBulletStormsAtNode()
    {
        // Rotate the boss to face the player
        FacePlayer();

        // Shoot single bullet and wait for a delay
        yield return StartCoroutine(ShootSingleBulletWithDelay());

        // Perform multiple bullet storms at the current node
        for (int bulletStormCounter = 0; bulletStormCounter < bulletStormsPerNode; bulletStormCounter++)
        {
            ShootBulletStorm();
            yield return new WaitForSeconds(bulletStormCooldown);
        }

        // Wait for a few seconds before moving to the next node
        yield return new WaitForSeconds(stopTimeAtNode);

        if (currentNode == 0)
        {
            // If at node 0, shuffle the order of the nodes randomly
            ShuffleNodes();

            // Set the current node to a random node other than node 0
            currentNode = UnityEngine.Random.Range(1, nodes.Length);
        }
        else
        {
            // Otherwise, return to node 0
            currentNode = 0;
        }

        // Start moving again
        isMoving = true;
    }

    IEnumerator ShootSingleBulletWithDelay()
    {
        shoot();
        yield return new WaitForSeconds(0.5f);
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
        GameObject bullet2 = Instantiate(bulletSingle, transform.position, Quaternion.identity);
        AudioManager.instance.Play("BossBigProjectile");

        // Destroy the bullet after a certain time to prevent memory issues
        Destroy(bullet2, 5f);
    }

    private void EnterSecondPhase()
    {
        
        isSecondPhase = true;
        bulletSpeed *= damagePhase2;
        StartCoroutine(FlashFullGold());

        // Double the number of bullet storms per node in the second phase
        bulletStormsPerNode = Mathf.RoundToInt(bulletStormsPerNode * 1.5f);

        // Increase the number of bullets per bullet storm in the second phase
        numberOfBullets = numberOfBulletsPhase2;

        // other stats
    }

    private void AdjustBossDifficulty(int level)
    {
        // Adjust the boss variables based on the level
        speed += 0.5f * (level - 1);
        bulletSpeed += 0.5f * (level - 1);
        bulletSpeedatHalfHP += 0.5f * (level - 1);
        bulletStormsPerNode += 0.5f * (level - 1);
        numberOfBullets += 1f * (level - 1);
        numberOfBulletsPhase2 += 1f * (level - 1);
    }

    private bool IsDead()
    {
        return enemyInfo.health <= 0;
    }

}
