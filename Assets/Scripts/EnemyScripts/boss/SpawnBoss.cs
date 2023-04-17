using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
// Implemented by Robert Bothne based on  ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL  by blackthornprod
public class SpawnBoss : MonoBehaviour
{
    [SerializeField]
    public GameObject Boss;
    void Start()
    {

        //if (objects[rand].name == "Tile" || objects[rand].name == "HTBL" || objects[rand].name == "HTBR" || objects[rand].name == "HTTL" || objects[rand].name == "HTTR")
        Boss.transform.parent = transform;


        //child                      parent
    }
}
