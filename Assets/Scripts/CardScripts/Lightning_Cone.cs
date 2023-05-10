//Worked on by Dan Huynhvo

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

    void Start()    //Teleports lightning bolt
    {
        Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.position = rayCast.GetPoint(35);
        Debug.Log(transform.position);
    }
}
