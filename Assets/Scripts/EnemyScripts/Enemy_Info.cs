using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Info : MonoBehaviour
{
    [SerializeField]
    public float health;
    [SerializeField]
    public float DropChance;

    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Spell")
        {
            health -= collision.gameObject.GetComponent<Spell_Info>().damage;
        }
    }
}
