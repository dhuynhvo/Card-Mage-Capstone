//Author: Grant Davis
//CS 426 Senior Project: Card Mage
//BossBullet.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rb;
    public float bulletDamage;
    public float force;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
    rb = GetComponent<Rigidbody>();
    player = GameObject.FindGameObjectWithTag("Player");
    // Use the bullet's forward direction for velocity instead of player direction
    Vector3 direction = transform.forward;
    rb.velocity = direction * force;
    transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);

    // Calculate the rotation based on the bullet's forward direction
    float rot = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(90, 90, rot+ 90);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag != "Spell" && other.gameObject.tag != "Boss" && other.gameObject.tag != "Enemy" && other.gameObject.tag != "BossRoom"  && other.gameObject.tag != "Ground" && gameObject.activeSelf)
        {
            Destroy(gameObject);
        }
    }
}
