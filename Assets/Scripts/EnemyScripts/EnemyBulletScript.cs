//Author: Grant Davis
//CS 426 Senior Project: Card Mage
//EnemyBulletScript

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rb;
    public float force;
    private float timer;
    public float bulletDamage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = direction.normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x)* Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(90, 90, rot + 0);
    }

    // Update is called once per frame
    void Update()
    {
       // transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y,timer);
        timer += Time.deltaTime;
        if(timer > 30)
        {
            Debug.Log("timed out proj");
            Destroy(gameObject);
            
        }
            
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag != "Boss" && other.gameObject.tag != "Enemy" && other.gameObject.tag != "Ground" && other.gameObject.tag != "MainCamera" && other.gameObject.tag != "Money" && other.gameObject.tag != "Spell" &&
            other.gameObject.tag != "Card" && other.gameObject.tag != "BossRoom" && gameObject.activeSelf)
        {
            Debug.Log(other);
            Destroy(gameObject);
        }
    }
}
