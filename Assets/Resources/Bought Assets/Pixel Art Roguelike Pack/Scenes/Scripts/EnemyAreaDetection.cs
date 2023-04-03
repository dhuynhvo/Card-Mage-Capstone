using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAreaDetection : MonoBehaviour
{
    public bool PlayerOnArea = false;

    void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            PlayerOnArea = true;
        }



    }
    void OnTriggerExit2D(Collider2D collisionExit)
    {
        if (collisionExit.gameObject.tag == "Player")
        {
            PlayerOnArea = false;
        }
    }


}
