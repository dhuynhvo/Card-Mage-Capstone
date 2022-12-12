using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 0f;
    [SerializeField] private float maxHealth = 100f;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    
    void Awake(){
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    public void TakeDamage(float mod)
    {
        health -= mod;
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void UpdateHealth(float mod){
        health += mod;
        
        if(health > maxHealth){
            health = maxHealth;
        }
        else if(health <= 0f){
            health = 0f;
        }
    }
}