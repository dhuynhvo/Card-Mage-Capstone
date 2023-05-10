//Worked on by Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaze_Infusion_Behavior : MonoBehaviour
{
    public Play_Card AttackBuff;
    public GameObject Player;
    public float BuffAmount;

    void Start()    //buffs the players attack
    {
        Player = GameObject.Find("Player");
        AttackBuff = Player.GetComponent<Play_Card>();
        AttackBuff.AttackBuff = BuffAmount;
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private void Update()
    {
        transform.position = Player.transform.position;
    }

    private void OnDisable()
    {
        AttackBuff.AttackBuff = 1f;
    }
}
