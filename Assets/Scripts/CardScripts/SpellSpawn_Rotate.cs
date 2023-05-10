//Dan Huynhvo
//UNR
//CS 425
//Outside Resource referenced for rotation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawn_Rotate : MonoBehaviour
{
    [SerializeField]
    private Transform PlayerAvatar;
    [SerializeField]
    public Vector2 turn;
    public float sensitivity = .5f;
    public Vector3 deltaMove;
    public float speed = 1;
    public float radius = 1.8f; // distance from pivot point to sprite

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rotater();    
    }

    void rotater()  //used youtube tutorial: logic for aiming spells
    {
        //float positionXRatio = Input.mousePosition.x / Screen.width;
        //float positionYRatio = Input.mousePosition.y / Screen.height;

        //Vector3 spritePivot = PlayerAvatar.position; //location of pivot point
        //Vector3 mouseToPoint = new Vector3(positionXRatio - .5f, 0, positionYRatio -.5f); //If anyone could explain why this works that would be great actually
        //mouseToPoint.Normalize();

        //transform.position = spritePivot + mouseToPoint * radius;

        ///////////////////////////////////////////////////////////

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 35f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(90, 0, angle - 90));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

}
