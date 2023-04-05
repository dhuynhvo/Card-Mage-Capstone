using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthwall_Behavior : MonoBehaviour
{
    void Start()
    {
        transform.position = GameObject.Find("Spell Spawn").transform.position;
        transform.position = new Vector3(transform.position.x, 1, transform.position.z) + transform.up;
    }
}
