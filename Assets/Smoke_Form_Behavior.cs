using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke_Form_Behavior : MonoBehaviour
{
    public GameObject Player;
    public SphereCollider SmokeForm;
    public CapsuleCollider NormalForm;
    
    void Start()
    {
        int Smoke = LayerMask.NameToLayer("SmokeForm");
        Player = GameObject.Find("Player");
        Player.layer = Smoke;
        //SmokeForm = Player.GetComponent<SphereCollider>();
        //NormalForm = Player.GetComponent<CapsuleCollider>();
        //SmokeForm.enabled = true;
        //NormalForm.enabled = false;
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private void Update()
    {
        transform.position = Player.transform.position;
    }

    private void OnDisable()
    {
        int Playerr = LayerMask.NameToLayer("Player");
        //SmokeForm.enabled = false;
        //NormalForm.enabled = true;
        Player.layer = Playerr;
    }
}
