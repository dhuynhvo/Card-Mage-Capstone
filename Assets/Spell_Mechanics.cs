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
        //rb.AddForce(Vector3.forward * 50);
        //transform.position += new Vector3(0, 0, info.speed);
        //Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.velocity = new Vector3(0, 0, 1) * info.speed;
    }
}
