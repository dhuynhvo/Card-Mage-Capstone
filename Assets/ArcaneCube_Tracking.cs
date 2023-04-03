using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneCube_Tracking : MonoBehaviour
{
    public ArcaneCube_Behavior cube;

    void OnTriggerEnter(Collider collision)
    {
        if(cube.target == null && collision.gameObject.tag == "Enemy")
        {
            cube.target = collision.gameObject.transform;
        }
    }
}
