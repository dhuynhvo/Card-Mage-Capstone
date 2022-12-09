using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyai : MonoBehaviour
{
    public float speed = 3f;
    private Transform target;

    private void Update() {
        if (target != null){
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
