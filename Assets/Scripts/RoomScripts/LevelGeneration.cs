using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;
//So uhh, Works by generating a path of traversal from top to bottom, with random left or right decisions. After this Path is made leftover nodes
// generate their own rooms, these dont neccesarily have to be connected. To add unique rooms, manage the prefabs and change spawn nodes and make clusters.
// Implemented by Robert Bothne based on  ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL  by blackthornprod

using UnityEngine.AI;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public Transform playerLoc;
    public GameObject[] spawnRooms;//index 0 = LeftTop, index 1 = RightToP, index 2 = spawnCenter
    public GameObject[] rooms;//index 0 = LR, index 1 = LRB, index 2 = LRT, index 3 = LRBT
    public GameObject[] bossRooms; //index 0 = BOSS
    public Transform[] poses;
    public NavMeshSurface surface; //surface for navmesh

    private int direction; //direction to move by offset
    public float moveAmount; //offset for spawning

    private float timeBetweenRoom;//time to spawn a room
    public float startTimeBetweenRoom = 0.5f;

    public float minX; //farthest left can gen
    public float maxX; //farthest right can gen
    public float minZ; //farthest down can gen
    public bool stopGen; //kill generator

    private int downCount = 0;// if going down twice, spawn a room with holes in every direction
    private int randStartingPos;
    public LayerMask room;
    public LayerMask Pose;
    private Collider[] poseDetection;
    bool quit = false;



    private void Start()
    {
        
        generatelevel();
        //build navmesh at runtime
        StartCoroutine(waiter(1.5f));
    }

    IEnumerator waiter(float waitTime){
        float counter = 0;

        while (counter < waitTime)
        {
            //Increment Timer until counter >= waitTime
            counter += Time.deltaTime;
            //Debug.Log("We have waited for: " + counter + " seconds");
            if (quit)
            {
                //Quit function
                yield break;
            }
            //Wait for a frame so that Unity doesn't freeze
            yield return null;
        }
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Tile"))
        {
            fooObj.transform.parent = GameObject.Find("Background").transform;
        }
        surface.BuildNavMesh();
    }

    void generatelevel()
    {
        randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        switch (randStartingPos)
        {
            case 0:
                Instantiate(spawnRooms[0], transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(spawnRooms[2], transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(spawnRooms[2], transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(spawnRooms[1], transform.position, Quaternion.identity);
                break;
        }
        playerLoc.transform.position = startingPositions[randStartingPos].position;
        //Instantiate(rooms[0], transform.position, Quaternion.identity);
        poseDetection = Physics.OverlapSphere(transform.position, 1, Pose);
        Destroy(poseDetection[0].gameObject);
        direction = Random.Range(1, 6);
    }
    // Update is called once per frame
    private void Update()
    {
        updategen();
    }

    void updategen(){
        if (timeBetweenRoom <= 0 && stopGen == false)
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
            if (transform.position.x < maxX)
            {
                downCount = 0;
                Vector3 newPos = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos;
                //spawn a random room type
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                poseDetection = Physics.OverlapSphere(transform.position, 1, Pose);
                if (poseDetection != null && poseDetection.Length > 0)
                {
                    if (poseDetection[0] != null)
                    {
                        Debug.Log("Pose to Destroy" + poseDetection[0].name);
                        Destroy(poseDetection[0].gameObject);
                    }
                }

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
        else if (direction == 3 || direction == 4)//move stampLEFT
        {
            if (transform.position.x > minX)
            {
                downCount = 0;
                Vector3 newPos = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos;
                direction = Random.Range(3, 6);
                //spawn a random room type
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                poseDetection = Physics.OverlapSphere(transform.position, 1, Pose);
                if (poseDetection != null && poseDetection.Length > 0)
                {
                    if (poseDetection[0] != null)
                    {
                        Debug.Log("Pose to Destroy" + poseDetection[0].name);
                        Destroy(poseDetection[0].gameObject);
                    }
                }

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

                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3) // if there isnt a bottom opening                                                                                                           
                {
                    if (downCount >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();// destroy a room
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                        poseDetection = Physics.OverlapSphere(transform.position, 1, Pose);
                        if (poseDetection != null && poseDetection.Length > 0)
                        {
                            if (poseDetection[0] != null)
                            {
                                Debug.Log("Pose to Destroy" + poseDetection);
                                Debug.Log("Pose to Destroy" + poseDetection[0].name);
                                Destroy(poseDetection[0].gameObject);
                            }
                        }
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();// destroy a room

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                        poseDetection = Physics.OverlapSphere(transform.position, 1, Pose);
                        if (poseDetection != null && poseDetection.Length > 0)
                        {
                            if (poseDetection[0] != null)
                            {
                                Debug.Log("Pose to Destroy" + poseDetection[0].name);
                                Destroy(poseDetection[0].gameObject);
                            }

                        }

                    }

                }
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveAmount);
                transform.position = newPos;
                direction = Random.Range(1, 6);

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                poseDetection = Physics.OverlapSphere(transform.position, 1, Pose);
                if (poseDetection != null && poseDetection.Length > 0)
                {
                    if (poseDetection[0] != null)
                    {
                        Debug.Log("Pose to Destroy" + poseDetection[0].name);
                        Destroy(poseDetection[0].gameObject);
                    }
                }
            }
            else //reached bottom
            {
                stopGen = true;
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                roomDetection.GetComponent<RoomType>().RoomDestruction();// destroy a room
                int randBoss = Random.Range(0, bossRooms.Length);
                Instantiate(bossRooms[randBoss], transform.position, Quaternion.identity); //make a boss room with a top hole in case path is obstructed horizontal.
                foreach (Transform Pose in poses) //fill out the rest of the map
                {
                    if (Pose != null)
                    {
                        int rand = Random.Range(0, rooms.Length);
                        transform.position = Pose.position;
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                        Destroy(Pose.gameObject);
                    }
                }
            }
        }

    }
}
