//Author: Grant Davis
//subAuthor: Robert Bothne - Added big single shot functionality
//CS 426 Senior Project
//BossNodeMovement.cs

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defining the BossNodeMovement class that inherits from MonoBehaviour
public class BossNodeMovement : MonoBehaviour
{
    // Defining variables related to boss movement
    public float speed = 5f;  // Speed of the boss
    public Transform[] nodes;  // Array to hold the nodes the boss will travel to
    private int currentNode = 0;  // The current node the boss is moving towards
    private bool isMoving = true;  // Flag indicating if the boss is moving

    // Defining variables related to boss attacks
    public GameObject bulletPrefab;  // Bullet prefab to instantiate when the boss attacks
    public GameObject bulletSingle;  // A single bullet prefab
    public Transform bulletSpawnPoint;  // The spawn point for bullets
    public float bulletSpeed = 7f;  // Speed of the bullets
    public float bulletStormCooldown = 1f;  // Cooldown time between bullet storms
    public float stopTimeAtNode = 3f;  // The time the boss stops at each node
    public float bulletStormsPerNode = 3f;  // The number of bullet storms at each node

    // Variables related to player detection and engagement
    public float playerDetectionRange = 4f;  // The range within which the boss detects the player
    private bool isEngaged = false;  // Flag indicating if the boss is engaged in a fight

    // Variable for controlling boss animations
    private Animator anim;  // Animator component for the boss

    // Variables for boss health and phase control
    private Enemy_Info enemyInfo;  // Component holding boss's health and other related info
    private bool isSecondPhase = false;  // Flag indicating if the boss is in the second phase of the fight

    // Variables for the first phase of the boss fight
    [SerializeField] float numberOfBullets = 4;  // Number of bullets in a bullet storm

    // Variables for the second phase of the boss fight
    public float numberOfBulletsPhase2 = 8f;  // Number of bullets in a bullet storm in the second phase
    public float damagePhase2 = 1.5f;  // Damage multiplier in the second phase
    public float bulletSpeedatHalfHP = 10f;  // Bullet speed when boss's HP is at half

    // More variables related to boss behavior
    [SerializeField] private Level_Counter levels;  // Component holding level-related information
    public Color goldTint = new Color(1f, 0.84f, 0f);  // Gold tint for the boss
    public float goldTintMultiplier = 0.1f;  // Amount of gold tint to apply per level

    [SerializeField] private float destroyBossDelay = 3f;  // Delay before destroying the boss
    private bool isDying = false;  // Flag indicating if the boss is dying

    // Variables related to boss death animation
    public float vanishUpwardSpeed = 3f;  // Speed at which the boss vanishes upwards
    public float vanishDuration = 3f;
    public GameObject explosionPrefab;
    public GameObject explosionPrefab2;

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
        enemyInfo.health += levels.Level * 13;
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

            if (!isDying)
            {
                // Set isDying to true to prevent the coroutine from being called repeatedly
                isDying = true;
                StartCoroutine(MoveToNode0AndDestroy());
            }
        }
    }

    IEnumerator MoveToNode0AndDestroy()
    {
        // Instantiate the explosion prefab at the boss's local position with a 90-degree rotation on the x-axis
        Vector3 explosionLocalPosition = new Vector3(0f, 0f, -1f);
        GameObject explosion = Instantiate(explosionPrefab, transform.TransformPoint(explosionLocalPosition), Quaternion.Euler(90f, 0f, 0f), transform);

        // Set the explosion as a child of the boss, so it follows the boss
        explosion.transform.SetParent(transform);
        
        isMoving = true;
        anim.SetBool("Walk", isMoving);

        // Move to node 0
        float node0Speed = speed * 0.5f;
        while (Vector3.Distance(transform.position, nodes[0].position) > 0.1f)
        {
            Vector3 direction = nodes[0].position - transform.position;
            Flip(direction.x);
            transform.position = Vector3.MoveTowards(transform.position, nodes[0].position, node0Speed * Time.deltaTime);
            yield return null;
        }

        // Stop walking animation when the boss reaches node 0
        anim.SetBool("Walk", false);

        

        // Move the boss up by 1 unit along the z-axis
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.5f);
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        anim.SetBool("Walk", true);
        while (distanceToTarget > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            yield return null;
        }

        anim.SetBool("Walk", false);

        // Wait for a few seconds to allow the explosion animation to play
        yield return new WaitForSeconds(destroyBossDelay);

        // Destroy the explosion prefab along with the boss
        Destroy(explosion);
        Destroy(gameObject);
    }

    // Change boss color to for every level to add freshness.
    private void SetBossColorByLevel(int level)
    {
        if (level >= 2)
        {
            Renderer bossRenderer = GetComponent<Renderer>();
            Color randomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
            bossRenderer.material.color = randomColor;
        }
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
    //unused code for flashing gold boss when under half hp
    IEnumerator FlashFullGold()
    {
        Renderer bossRenderer = GetComponent<Renderer>();
        Color originalColor = bossRenderer.material.color;

        bossRenderer.material.color = goldTint;
        yield return new WaitForSeconds(0.5f);

        bossRenderer.material.color = originalColor;
    }

    // This function handles shooting bullet storms at a node
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

    // This function handles shooting a single bullet with a delay
    IEnumerator ShootSingleBulletWithDelay()
    {
            for (int i = 0; i < levels.Level; i++)
        {
            shoot();
            yield return new WaitForSeconds(0.5f);
        }
    }

    // This function shuffles the order of the nodes randomly, excluding the first node
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

    // This function handles shooting a bullet storm
    void ShootBulletStorm()
    {
        if (IsDead()) return;
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

    // This function rotates the boss to face the player
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

    // This function shoots a single bullet
    void shoot()
    {
        if (IsDead()) return;
        GameObject bullet2 = Instantiate(bulletSingle, transform.position, Quaternion.identity);
        AudioManager.instance.Play("BossBigProjectile");

        // Destroy the bullet after a certain time to prevent memory issues
        Destroy(bullet2, 5f);
    }

    // The method that is called when the boss reaches 50% health, initiating the second phase of the fight
    private void EnterSecondPhase()
    {
        
        isSecondPhase = true;
        bulletSpeed *= damagePhase2;
        //StartCoroutine(FlashFullGold());

        // Double the number of bullet storms per node in the second phase
        bulletStormsPerNode = Mathf.RoundToInt(bulletStormsPerNode * 1.5f);

        // Increase the number of bullets per bullet storm in the second phase
        numberOfBullets = numberOfBulletsPhase2;

        // other stats
    }

    // This method checks if the boss is dead by checking if the boss's health is less than or equal to 0
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
    // This method checks if the boss is dead by checking if the boss's health is less than or equal to 0
    private bool IsDead()
    {
        return enemyInfo.health <= 0;
    }
}
