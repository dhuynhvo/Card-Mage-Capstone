using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    public Transform[] startingPositions;
    public void Start() 
    {
        transform.position = gameObject.transform.parent.parent.position;
    }
}
