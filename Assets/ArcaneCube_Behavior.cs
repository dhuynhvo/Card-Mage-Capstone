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
            float AngleRad = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.z - transform.position.z);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            if(target.transform.position.x >= transform.position.x)
            {
                this.transform.rotation = Quaternion.Euler(90, 0, AngleDeg);
            }

            else
            {
                this.transform.rotation = Quaternion.Euler(90, 0, -AngleDeg);
            }
            

            //Vector3 LookPos = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
            //gameObject.transform.LookAt(LookPos);


        }
    }
}
