using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Mechanics : MonoBehaviour
{
    Spell_Info info;
    Rigidbody rb;

    void Start()
    {
        info = gameObject.GetComponent<Spell_Info>();
        rb = gameObject.GetComponent<Rigidbody>();
        Destroy(gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {

        SpellMovement();

    }

    private void SpellMovement()
    {
        rb.velocity = transform.up * info.speed;
    }
}