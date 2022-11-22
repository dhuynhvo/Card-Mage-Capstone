using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms; //index 0 = LR, index 1 = LRB, index 2 = LRT, index 3 = LRBT

    private int direction; //direction to move by offset
    public float moveAmount; //offset for spawning

    private float timeBetweenRoom;//time to spawn a room
    public float startTimeBetweenRoom = 0.25f; 

    public float minX; //farthest left can gen
    public float maxX; //farthest right can gen
    public float minZ; //farthest down can gen
    private bool stopGen; //kill generator

    private int downCount;// if going down twice, spawn a room with holes in every direction

    public LayerMask room;

    private void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0],transform.position, Quaternion.identity);

        direction = Random.Range(1,6);
    }

    // Update is called once per frame
    private void Update()
    {
        if (timeBetweenRoom <= 0 && stopGen==false)
        {
            Move();
            timeBetweenRoom = startTimeBetweenRoom;
        }
        else
        {
            timeBetweenRoom -= Time.deltaTime;
        }
    }
        
    private void Move()
    {
        if (direction == 1 || direction == 2)//move stamp RIGHT
        {
            if (transform.position.x < maxX){
                downCount = 0;
                Vector3 newPos = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos;
                //spawn a random room type
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        } 
        else if(direction == 3 || direction == 4)//move stampLEFT
        {
            if (transform.position.x >minX)
            {
                downCount = 0;
                Vector3 newPos = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos;
                direction = Random.Range(3, 6);
                //spawn a random room type
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
            }
            else
            { 
                direction = 5; 
            }          
        }
        
        else if (direction == 5)//move DOWN
        {
            downCount++;
            if (transform.position.z > minZ) //keep going, not at bottom
            {

                Collider[] roomDetection = Physics.OverlapSphere(transform.position, 1, room);
                foreach (Collider col in roomDetection)
                {
                    if (col.GetComponent<RoomType>().type != 1 && col.GetComponent<RoomType>().type != 3) // if there isnt a bottom opening
                    {
                        if (downCount >= 2)
                        {
                            col.GetComponent<RoomType>().RoomDestruction();// destroy a room
                            Instantiate(rooms[3], transform.position,Quaternion.identity);
                        }
                        else
                        {
                            col.GetComponent<RoomType>().RoomDestruction();// destroy a room

                            int randBottomRoom = Random.Range(1, 4);
                            if (randBottomRoom == 2)
                            {
                                randBottomRoom = 1;
                            }
                            Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                        }
                        
                    }
                }
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveAmount);
                transform.position = newPos;
                direction = Random.Range(1, 6);

                int rand = Random.Range(2,4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
            }
            else //reached bottom
            {
                stopGen = true;
                //stop level gen
            }
        }
    } 
}
