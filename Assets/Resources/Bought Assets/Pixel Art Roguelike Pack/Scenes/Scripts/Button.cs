using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{   
    [SerializeField] private GameObject Door = null;
    public bool PlayerCanPressButton = true;
    public Animator animator = null;

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Player" && PlayerCanPressButton)
        {
            Door.GetComponent<Door>();
            Door door = Door.GetComponent<Door>();
            door.ChangeDoorState = true;
            
             PlayerCanPressButton = false;
             animator.Play("buttonPressed");

        }
    }
    private void OnTriggerExit2D(Collider2D collisionExit){
        PlayerCanPressButton = true;
        animator.Play("buttonReleased");

    }



}