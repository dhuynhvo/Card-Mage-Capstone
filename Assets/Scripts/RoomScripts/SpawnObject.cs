using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;
    void Start()
    {
        int rand = Random.Range(0, objects.Length);
        if (objects[rand].name == "Tile")
        {
            GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.Euler(90, 0, 0));
            instance.transform.parent = transform;
        }
        if (objects[rand].name == "bigTile")
        {
            GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.Euler(90, 0, 0));
            instance.transform.parent = transform;
        }
        else
        {
            GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }
        
        //child                      parent
    }
}
