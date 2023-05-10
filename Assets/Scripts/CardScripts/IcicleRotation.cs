//Worked on by Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleRotation : MonoBehaviour
{

    int angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle += 5;
        transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, angle));
        if(angle >= 360)
        {
            angle = 0;
        }
    }
}
