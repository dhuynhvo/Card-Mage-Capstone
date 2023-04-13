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

    void rotater()
    {
        float positionXRatio = Input.mousePosition.x / Screen.width;
        float positionYRatio = Input.mousePosition.y / Screen.height;

        Vector3 spritePivot = PlayerAvatar.position; //location of pivot point
        Vector3 mouseToPoint = new Vector3(positionXRatio - .5f, 0, positionYRatio -.5f); //If anyone could explain why this works that would be great actually
        mouseToPoint.Normalize();

        transform.position = spritePivot + mouseToPoint * radius;

        ///////////////////////////////////////////////////////////

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(PlayerAvatar.transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, angle + 95));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

}
