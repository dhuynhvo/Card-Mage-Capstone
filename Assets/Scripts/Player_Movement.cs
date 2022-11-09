using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public string UpKey, DownKey, LeftKey, RightKey;
    public float PlayerSpeed;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
    }


    void FixedUpdate()
    {
        if(Input.GetKey(UpKey))
        {
            transform.position += Vector3.forward.normalized * PlayerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(DownKey))
        {
            transform.position += Vector3.back.normalized * PlayerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(LeftKey))
        {
            transform.position += Vector3.left.normalized * PlayerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(RightKey))
        {
            transform.position += Vector3.right.normalized * PlayerSpeed * Time.deltaTime;
        }
    }
}
