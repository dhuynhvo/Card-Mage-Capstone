using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning_Cone : MonoBehaviour
{
    public Player_Movement playerMove;
    public Rigidbody playerRB;
    float speed;
    float dashSpeed;

    void Start()
    {
        speed = 2.5f;
        dashSpeed = 3f;
    }

    void OnEnable()
    {
        playerMove = GameObject.Find("Player").GetComponent<Player_Movement>();
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();
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
