using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private Rigidbody rb;
    public float force;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Use the bullet's forward direction for velocity instead of player direction
        Vector3 direction = transform.forward;
        rb.velocity = direction * force;

        // No rotation calculation is needed since we are not aiming at the player
        float rot = Mathf.Atan2(-direction.y, -direction.x)* Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(90, 90, rot + 0);
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
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(5);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag != "Spell" && other.gameObject.tag != "Boss" && other.gameObject.tag != "Enemy" && other.gameObject.tag != "Ground" && gameObject.activeSelf)
        {
            Destroy(gameObject);
        }
    }
}
