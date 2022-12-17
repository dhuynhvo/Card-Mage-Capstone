//Dan Huynhvo
//UNR
//CS 425

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
    }

    // Update is called once per frame
    void Update()
    {

        SpellMovement();

    }

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    private void SpellMovement()
    {
        rb.velocity = transform.up * info.speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag != "Spell" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Ground" && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            //StartCoroutine(LateStart());
        }
    }
}