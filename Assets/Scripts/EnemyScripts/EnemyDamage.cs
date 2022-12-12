using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damage = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject player;
        player = GameObject.FindWithTag("Player");
        if(player == null){
            Debug.LogError("Player is not found!");
        }
        else{
            playerHealth = player.GetComponent<PlayerHealth>();
        }
      
        //playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.tag == "Player")
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
