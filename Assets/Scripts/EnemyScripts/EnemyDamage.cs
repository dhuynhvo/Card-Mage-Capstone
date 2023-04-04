//Author: Grant Davis
//Enemy Damage
//Multiple tutorials were used during development of this code

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
    private bool canDamage = true;

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

    IEnumerator DoDamage()
    {
        canDamage = false;
        playerHealth.TakeDamage(damage);
        yield return new WaitForSeconds(damageDelay);
        canDamage = true;
    }
}