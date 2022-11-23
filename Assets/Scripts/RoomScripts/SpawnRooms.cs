using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    public LayerMask roomFind;
    public LevelGeneration levelGen;


    public bool executin;
    public bool executin2;


    void Update()
    {/*
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, roomFind);
        if (levelGen.stopGen == true)
        {
          //  executin = true;
            //Debug.Log("game object" + gameObject.name + "has issue with" + roomDetection[0]);
        }
        Debug.Log("game object" + gameObject.name + "has issue with" + roomDetection);
        if (roomDetection == null && levelGen.stopGen == true)
        {
            int rand = Random.Range(0, levelGen.rooms.Length);
            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            executin2 = true;
            //Destroy(gameObject);
        }*/
    }

   }
