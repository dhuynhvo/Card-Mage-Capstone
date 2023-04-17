using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcane_Bolt_Behavior : MonoBehaviour
{

    int SpawnNumber = 4;
    public GameObject bolt;
    int offset = -30;

    void Start()
    {
        for(int i = 0; i < SpawnNumber; i++)
        {
            Instantiate(bolt, transform.position, Quaternion.Euler(90, 0, -transform.localEulerAngles.y + offset));
            offset += 15;
        }
    }

}
