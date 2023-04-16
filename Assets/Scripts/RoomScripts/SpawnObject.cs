using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
// Implemented by Robert Bothne based on  ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL  by blackthornprod
public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;
    void Start()
    {
        //int rand = Random.Range(0, objects.Length);
        //if (objects[rand].name == "Tile" || objects[rand].name == "HTBL" || objects[rand].name == "HTBR" || objects[rand].name == "HTTL" || objects[rand].name == "HTTR")
        //{
        //    GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
        //    instance.transform.parent = GameObject.Find("Background").transform;
        //}
        if (objects.Length > 0)
        {
            int rand = Random.Range(0, objects.Length);
            GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }
        else
        {
            Debug.Log("The objects array is empty.");
        }
        
        //child                      parent
    }
}
