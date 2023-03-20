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

    private IEnumerator LateStart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(false);
    }

    private void SpellMovement()
    {
        rb.velocity = transform.up * info.speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SpawnAOE();
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        else if(collision.gameObject.tag != "Spell" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Ground" && gameObject.activeSelf)
        {
            SpawnAOE();
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    private void SpawnAOE()
    {
        if (info.IsAOE == true)
        {
            GameObject aoe = Instantiate(info.AOE, transform.position, transform.rotation) as GameObject;
            Destroy(aoe, info.AOETime);
        }
    }
}