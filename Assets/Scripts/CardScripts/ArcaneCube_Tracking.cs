//Worked on by Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneCube_Tracking : MonoBehaviour
{
    public ArcaneCube_Behavior cube;



    void OnTriggerEnter(Collider collision) //finds target for arcane cube
    {
        if(cube.target == null && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss"))
        {
            cube.target = collision.gameObject.transform;
        }
    }
}
