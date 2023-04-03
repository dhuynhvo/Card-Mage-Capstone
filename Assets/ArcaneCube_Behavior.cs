using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneCube_Behavior : MonoBehaviour
{
    public Transform target;
    public Vector3 targetNoZ;
    public Rigidbody rb;
    public float rotateSpeed = 200f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(target != null)
        {

            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            this.transform.rotation = Quaternion.Euler(90, 0, AngleDeg - 90);
        }
    }
}
