//Worked on by Dan Huynhvo
//CS426

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Point_To_Boss_Room : MonoBehaviour
{
    public GameObject Target;
    public bool FoundTarget;
    public Transform PlayerAvatar;
    public float radius;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(FoundTarget == true)
        {
            transform.LookAt(Target.transform);
        }
    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
