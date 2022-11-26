using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawn_Rotate : MonoBehaviour
{
    [SerializeField]
    private Transform PlayerAvatar;
    [SerializeField]
    private Rigidbody rb;

    public Vector2 turn;
    public float sensitivity = .5f;
    public Vector3 deltaMove;
    public float speed = 1;

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
        /*Vector3 worldPos = position;
        Quaternion q = Quaternion.AngleAxis(angle, axis);
        Vector3 dif = worldPos - point;
        dif = q * dif;
        worldPos = point + dif;
        position = worldPos;*/

        float radius = 1f; // distance from pivot point to sprite
        Vector3 spritePivot = new Vector3(PlayerAvatar.position.x, PlayerAvatar.position.y, PlayerAvatar.position.z); //location of pivot point
        Vector3 mouseToPoint = new Vector3(Input.mousePosition.x - 1000, 0, Input.mousePosition.y - 600); //If anyone could explain why this works that would be great actually
        //GameObject EnemyToCreate = Instantiate(Resources.Load("test point") as GameObject, mouseToPoint, Quaternion.identity);
        //Debug.Log(Input.mousePosition.x + "     " + Input.mousePosition.y);
        mouseToPoint.Normalize();
        mouseToPoint *= radius;
        transform.position = spritePivot + mouseToPoint;

        ///////////////////////////////////////////////////////////
        ///

        /*Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(90f, 0f, -rotationZ - 90);

        if (rotationZ < -90 || rotationZ > 90)
        {



            if (transform.eulerAngles.y == 0)
            {


                transform.localRotation = Quaternion.Euler(90, 0, rotationZ + 90);


            }
            else if (transform.eulerAngles.y == 180)
            {


                transform.localRotation = Quaternion.Euler(90, 180, rotationZ + 90);
            }
        }*/

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
