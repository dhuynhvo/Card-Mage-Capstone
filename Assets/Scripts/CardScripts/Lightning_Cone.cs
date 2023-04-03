using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning_Cone : MonoBehaviour
{
    public Player_Movement playerMove;
    public Rigidbody playerRB;
    public Transform SpellSpawn;
    float speed;
    float dashSpeed;

    void Start()
    {
        speed = 15f;
        dashSpeed = 1.5f;
    }

    void Update()
    {
        if(SpellSpawn != null)
        {
            transform.position = SpellSpawn.position + transform.up;
            transform.rotation = SpellSpawn.rotation;
        }
    }

    void OnEnable()
    {
        playerMove = GameObject.Find("Player").GetComponent<Player_Movement>();
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();
        SpellSpawn = GameObject.Find("Player").transform.GetChild(3);
        playerRB.constraints = RigidbodyConstraints.FreezeAll;
        playerMove.PlayerSpeed = 0;
        playerMove.DashSpeed = 0;
    }

    void OnDisable()
    {
        playerRB.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        playerMove.PlayerSpeed = speed;
        playerMove.DashSpeed = dashSpeed;
    }
}
