//Worked on by Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Rotation : MonoBehaviour
{
    public Vector3 rotationDirection;
    public float smoothTime;
    private float convertedTime = 200;
    private float smooth;

    // Use this for initialization
    void Start()
    {
        smoothTime = 1;
    }

    // Update is called once per frame
    void Update()   //Rotates the cards in the shop for visual effect
    {
        smooth = Time.deltaTime * smoothTime * convertedTime;
        transform.Rotate(rotationDirection * smooth);
        
    }
}
