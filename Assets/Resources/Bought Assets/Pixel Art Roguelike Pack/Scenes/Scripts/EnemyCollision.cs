using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public bool hit = false;
    [SerializeField] private GameObject EnemyBase = null;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            Enemy Enemy = EnemyBase.GetComponent<Enemy>();
            Enemy.hitEnemy = true;

        }



    }
}
