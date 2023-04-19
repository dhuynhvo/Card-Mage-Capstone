//Author: Grant Davis
//CS 426 Senior Project: Card Mage
//enemyai.cs

//AI Pathfinding script (NOT USED)
//Multiple tutorials were used during development of this code:

//Unused code used in early testing of game. 
//Discarded due to decision to implement a Navigation Mesh for enemy movement.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyai : MonoBehaviour
{
    public float speed = 3f;
    //[SerializeField] private float attackDamage = 10f;
    //[SerializeField] private float attackSpeed = 1f;
    private float canAttack;
    private Transform target;
    [SerializeField]
    private Enemy_Mechanics EM;

    private void Start()
    {
        EM = gameObject.GetComponent<Enemy_Mechanics>();
    }
    private void Update() {
        if (target != null && EM.NotDead){
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            target = other.transform;
        }
    }
    
    private void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Player"){
            target = null;
        }
    }
}