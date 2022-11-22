using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public string UpKey, DownKey, LeftKey, RightKey;
    public float PlayerSpeed;
    private char FacingWhat;
    Rigidbody rb;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

    }


    void FixedUpdate()
    {
        if(Input.GetKey(UpKey))
        {
            //transform.position += Vector3.forward.normalized * PlayerSpeed * Time.deltaTime;
            //rb.AddForce(0, 0, PlayerSpeed);
            rb.MovePosition(transform.position + Vector3.forward * Time.deltaTime * PlayerSpeed);
            FacingWhat = 'u';
        }
        if (Input.GetKey(DownKey))
        {
            //transform.position += Vector3.back.normalized * PlayerSpeed * Time.deltaTime;
            //rb.AddForce(0, 0, -PlayerSpeed);
            rb.MovePosition(transform.position + Vector3.back * Time.deltaTime * PlayerSpeed);
            FacingWhat = 'd';
        }
        if (Input.GetKey(LeftKey))
        {
            //transform.position += Vector3.left.normalized * PlayerSpeed * Time.deltaTime;
            //rb.AddForce(-PlayerSpeed, 0, 0);
            rb.MovePosition(transform.position + Vector3.left * Time.deltaTime * PlayerSpeed);
            FacingWhat = 'l';
        }
        if (Input.GetKey(RightKey))
        {
            //transform.position += Vector3.right.normalized * PlayerSpeed * Time.deltaTime;
            //rb.AddForce(PlayerSpeed, 0, 0);
            rb.MovePosition(transform.position + Vector3.right * Time.deltaTime * PlayerSpeed);
            FacingWhat = 'r';
        }
    }
}
