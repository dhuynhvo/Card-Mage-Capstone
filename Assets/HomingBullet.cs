using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 7f;
    public float rotateSpeed = 200f;
    public float lifeTime = 5f;

    private Transform player;
    private Rigidbody rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroyBulletAfterTime());
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            float rotateAmount = Vector3.Cross(direction, transform.forward).y;

            rb.angularVelocity = new Vector3(0f, -rotateAmount * rotateSpeed, 0f);
            rb.velocity = transform.forward * speed;
        }
    }

    IEnumerator DestroyBulletAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}