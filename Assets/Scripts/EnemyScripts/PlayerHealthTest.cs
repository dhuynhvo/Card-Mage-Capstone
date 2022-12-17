//Grant Davis
//Unused code that is reimplement in player script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthTest : MonoBehaviour
{ 
    public int health;
    public int maxHealth = 10;
    // Start is called before the first frame update
    void Start()
    {
        health  = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

