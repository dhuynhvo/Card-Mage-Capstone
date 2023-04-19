//Author: Grant Davis
//CS 426 Senior Project: Card Mage
//EnemyDamage.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damage = 1;
    public float damageDelay = 1f; // delay time in seconds
    [SerializeField]
    private Enemy_Mechanics EM;
    public bool canDamage = true;
    [SerializeField]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player;
        EM = gameObject.GetComponent<Enemy_Mechanics>();
        player = GameObject.FindWithTag("Player");
        if(player == null){
            Debug.LogError("Player is not found!");
        }
        else{
            playerHealth = player.GetComponent<PlayerHealth>();
        }
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision){

        if(collision.gameObject.tag == "Player" && EM.NotDead && canDamage)
        {
            StartCoroutine(DoDamage());
        }
    }

    public IEnumerator DoDamage()
{
    canDamage = false;
    anim.SetBool("Attack", true); // Set the attack animation state
    playerHealth.TakeDamage(damage);

    //Disable collision
    Collider col = GetComponent<Collider>();
    if (col != null)
    {
        col.isTrigger = true; // Set collider as trigger to allow player to walk through
    }

    yield return new WaitForSeconds(damageDelay);

    // Re-enable collision
    if (col != null)
    {
        col.isTrigger = false; // Reset collider to its original state
    }

    anim.SetBool("Attack", false); // Reset the attack animation state
    canDamage = true;
    }
}