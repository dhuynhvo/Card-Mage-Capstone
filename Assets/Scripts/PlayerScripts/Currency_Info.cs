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

    private void Start()
    {
        Destroy(gameObject, 20f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject player = collision.gameObject;
        if(player.tag == "Player")
        {
            if(gameObject.tag != "SteveMoney")
            {
                player.GetComponent<Player_Currency>().Steves.money += worth;
            }

            else if(gameObject.tag == "SteveMoney")
            {
                player.GetComponent<Player_Currency>().Steves.SteveMoney += worth;
            }
            Destroy(gameObject);
        }
    }
}
