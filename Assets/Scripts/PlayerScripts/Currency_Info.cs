//Dan Huynhvo
//UNR
//CS 425

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Currency_Info : MonoBehaviour
{
    [SerializeField]
    private int worth;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform player;

    private void Start()
    {
        Destroy(gameObject, 20f);
        player = GameObject.Find("Player").transform;
        speed = 5;
    }

    private void Update()
    {
        if(player != null)
        {
            var step = speed * Time.deltaTime; // calculate distance to move
            speed += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
    }

    private void OnTriggerEnter(Collider collision) //adds currency on touching gameobject
    {
        GameObject player = collision.gameObject;

        if(collision.gameObject.tag == "Untagged" || collision.gameObject.tag == "Untagged")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        if(player.tag == "Player")
        {
            if(gameObject.tag != "SteveMoney")
            {
                GameEvents.current.MoneyGained(worth, false);
                player.GetComponent<Player_Currency>().Steves.money += worth;
            }

            else if(gameObject.tag == "SteveMoney")
            {
                GameEvents.current.MoneyGained(worth, true);
                player.GetComponent<Player_Currency>().Steves.SteveMoney += worth;
            }
            Destroy(gameObject);
        }
    }
}
