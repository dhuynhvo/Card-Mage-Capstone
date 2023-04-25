using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HoleDetector : MonoBehaviour
{
    Camera myCamera;
    public float damage = 10f;
    public Transform respawnPoint;
    public float freezeTime = 0.5f;
    private void Start()
    {
        myCamera = Camera.main;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
        Time.timeScale = 1;
        myCamera.GetComponent<shaker>().enabled = true;
    }
}