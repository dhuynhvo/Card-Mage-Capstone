using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Implemented by Robert Bothne based on  ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL  by blackthornprod
public class RoomType : MonoBehaviour
{
    public int type;
    public void RoomDestruction()
    {
        Destroy(gameObject);
    }
}
