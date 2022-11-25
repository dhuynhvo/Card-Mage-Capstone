using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawn_Rotate : MonoBehaviour
{

    public Transform PlayerAvatar;
    public Vector2 spriteLocation;
    public float spriteAngle;
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

        float radius =  3f; // distance from pivot point to sprite
        Vector3 spritePivot = new Vector3(PlayerAvatar.position.x, PlayerAvatar.position.y + 2 ,PlayerAvatar.position.z); //location of pivot point
        Vector3 mouseToPoint = spritePivot + new Vector3(Input.mousePosition.x, 0 ,Input.mousePosition.y);
        //Debug.Log(Input.mousePosition.x + "     " + Input.mousePosition.y);
        mouseToPoint.Normalize();
        spriteAngle = Mathf.Atan2(Input.mousePosition.x, Input.mousePosition.y);
        mouseToPoint *= radius;
        transform.position = spritePivot + mouseToPoint;
    }
}
