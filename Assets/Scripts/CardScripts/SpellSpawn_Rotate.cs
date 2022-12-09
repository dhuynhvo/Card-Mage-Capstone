using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawn_Rotate : MonoBehaviour
{
    [SerializeField]
    private Transform PlayerAvatar;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float OffsetX;
    [SerializeField]
    private float OffsetY;
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
        if(Input.GetMouseButton(0))       //THIS IS A DEBUGGING TOOL, IF THE CURSOR IS BUGGED TURN THIS BACK ON, AND PUT THE NUMBERS INTO THE OFFSET
        {
            Debug.Log(Input.mousePosition.x + "     " + Input.mousePosition.y);
        }
        
    }

    void rotater()
    {
        Vector3 spritePivot = new Vector3(PlayerAvatar.position.x, PlayerAvatar.position.y, PlayerAvatar.position.z); //location of pivot point
        Vector3 mouseToPoint = new Vector3(Input.mousePosition.x - OffsetX, 0, Input.mousePosition.y - OffsetY); //If anyone could explain why this works that would be great actually
        //Vector3 mouseToPoint = new Vector3(Input.mousePosition.x - 1000, 0, Input.mousePosition.y - 600);
        //Debug.Log(Input.mousePosition.x + "     " + Input.mousePosition.y);
        mouseToPoint.Normalize();
        mouseToPoint *= radius;
        transform.position = spritePivot + mouseToPoint;

        ///////////////////////////////////////////////////////////

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(PlayerAvatar.transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, angle + 90));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

}
