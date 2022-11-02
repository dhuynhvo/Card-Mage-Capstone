using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    string UpKey, DownKey, LeftKey, RightKey;
    public float PlayerSpeed;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        UpKey = "w";
        DownKey = "s";
        LeftKey = "a";
        RightKey = "d";
        PlayerSpeed = 1;
    }


    void FixedUpdate()
    {
        if(Input.GetKey(UpKey))
        {
            transform.position += Vector3.forward * PlayerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(DownKey))
        {
            transform.position += Vector3.back * PlayerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(LeftKey))
        {
            transform.position += Vector3.left * PlayerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(RightKey))
        {
            transform.position += Vector3.right * PlayerSpeed * Time.deltaTime;
        }
    }
}
