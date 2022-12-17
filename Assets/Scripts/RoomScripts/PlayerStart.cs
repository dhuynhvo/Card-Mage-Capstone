using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Implemented by Robert Bothne based on  ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL  by blackthornprod
public class PlayerStart : MonoBehaviour
{
    public Transform[] startingPositions;
    public void Start() 
    {
        transform.position = gameObject.transform.parent.parent.position;
    }
}
