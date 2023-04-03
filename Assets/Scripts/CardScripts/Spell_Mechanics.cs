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
    public Animator anim;

    void Start()
    {
        info = gameObject.GetComponent<Spell_Info>();
        rb = gameObject.GetComponent<Rigidbody>();
        DeactivateOnTime();
    }

    // Update is called once per frame
    void Update()
    {

        SpellMovement();

    }

    private IEnumerator LateStart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //anim.SetBool("hit", false);
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
            //anim.SetBool("hit", true);
            StartCoroutine(LateStart(info.ActiveDuration));
            //gameObject.SetActive(false);

            //Destroy(gameObject);
        }
        else if (collision.gameObject.tag != "Spell" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Ground" && collision.gameObject.tag != "MainCamera" && collision.gameObject.tag != "Money" &&
            collision.gameObject.tag != "Card" && gameObject.activeSelf)
        {
            SpawnAOE();
            //anim.SetBool("hit", true);
            StartCoroutine(LateStart(info.ActiveDuration));
            //gameObject.SetActive(false);

            //Destroy(gameObject);

        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SpawnAOE();
            //anim.SetBool("hit", true);
            StartCoroutine(LateStart(info.ActiveDuration));
            //gameObject.SetActive(false);

            //Destroy(gameObject);
        }
        else if (collision.gameObject.tag != "Spell" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Ground" && collision.gameObject.tag != "MainCamera" && collision.gameObject.tag != "Money" &&
            collision.gameObject.tag != "Card" && gameObject.activeSelf)
        {
            SpawnAOE();
            //anim.SetBool("hit", true);
            StartCoroutine(LateStart(info.ActiveDuration));
            //gameObject.SetActive(false);

            //Destroy(gameObject);

        }
    }

    private void DeactivateOnTime()
    {
        if (info.DeactivateOnHit == false)
        {
            StartCoroutine(LateStart(info.ActiveDuration));
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