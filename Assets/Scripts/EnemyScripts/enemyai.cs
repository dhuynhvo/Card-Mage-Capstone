using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyai : MonoBehaviour
{
    public float speed = 3f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
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

    /*private void OnCollisonStay(Collision other){
        if(other.gameObject.tag == "Player" && EM.NotDead)
        {
            if(attackSpeed<=canAttack){
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackSpeed);
                canAttack = 0f;
            }
            else{
                canAttack += Time.deltaTime;
            }
        }
    }*/
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
