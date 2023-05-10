//Worked on by Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneCube_Behavior : MonoBehaviour
{
    public Transform target;
    public Vector3 targetNoZ;
    public bool setToBoss;
    public Rigidbody rb;
    public float rotateSpeed = 200f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //if(target != null && target.gameObject.tag == "Enemy")
        //{
        //    // Get Angle in Radians
        //    float AngleRad = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.z - transform.position.z);
        //    // Get Angle in Degrees
        //    float AngleDeg = (180 / Mathf.PI) * AngleRad;
        //    // Rotate Object
        //    if(target.transform.position.x >= transform.position.x)
        //    {
        //        this.transform.rotation = Quaternion.Euler(90, 0, AngleDeg);
        //    }

        //    else
        //    {
        //        this.transform.rotation = Quaternion.Euler(90, 0, -AngleDeg);
        //    }
            
        //}

        if (target != null) //follows target
        {
            var speed = GetComponent<Spell_Info>().speed;
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
}
