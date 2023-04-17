using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
// Implemented by Robert Bothne based on  ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL  by blackthornprod
public class SpawnTile : MonoBehaviour
{
    public GameObject[] objects;
    void Start()
    {
        int rotations = 0;
        int rand = Random.Range(0, objects.Length);
        int random = Random.Range(0, 3);
        if (random == 0)
        {
            rotations = 0;
        }
        else if (random == 1)
        {
            rotations = 90;
        }
        else if (random == 2)
        {
            rotations = 180;
        }
        else if (random == 3)
        {
            rotations = 270;
        }
        {
            GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.Euler(new Vector3(0,rotations, 0)));
            instance.transform.parent = transform;
        }

        //child                      parent
    }
}
