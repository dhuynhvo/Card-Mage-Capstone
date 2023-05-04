using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HoleDetector : MonoBehaviour
{
    Camera myCamera;
    public float basedamage = 10f;
    private float damage;
    public Transform respawnPoint;
    public float freezeTime = 0.5f;
    [SerializeField]
    private Level_Counter levels;
    public GameObject player;
    private void Start()
    {
        myCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damage = basedamage + levels.Level * 2;
            if (levels.Level == 1)
            {
                damage = 5;
            }
            // Apply damage to player
            other.GetComponent<PlayerHealth>().TakeDamage(damage);

            // Move player to respawn point
            other.transform.position = new Vector3(respawnPoint.transform.position.x, other.transform.position.y, respawnPoint.transform.position.z);
            myCamera.GetComponent<shaker>().enabled = false;
           // Time.timeScale = 0f;
            StartCoroutine(Pause(freezeTime));
            
        }
    }
    IEnumerator Pause(float p)
    {
        
        Time.timeScale = 0.1f;
        float pauseEndTime = Time.realtimeSinceStartup + 1;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        if (player.GetComponent<PlayerHealth>().health > 0)
        {
            Time.timeScale = 1;
            myCamera.GetComponent<shaker>().enabled = true;
        }

    }
}