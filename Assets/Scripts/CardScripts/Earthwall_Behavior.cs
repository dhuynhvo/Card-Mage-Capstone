using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthwall_Behavior : MonoBehaviour
{
    public float distance;

    void Start()
    {
        transform.position = GameObject.Find("Spell Spawn").transform.position;
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z) + transform.up * distance;
    }
}
