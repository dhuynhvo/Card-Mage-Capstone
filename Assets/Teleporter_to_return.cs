using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter_to_return : MonoBehaviour
{
    public GameObject returnPoint;
    public void Start()
    {
        returnPoint = GameObject.Find("ReturnPoint");
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = new Vector3(returnPoint.transform.position.x, 1f, returnPoint.transform.position.z);
            Destroy(returnPoint);
            Destroy(gameObject);
        }
    }
}
