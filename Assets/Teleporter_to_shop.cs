using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter_to_shop : MonoBehaviour
{
    public GameObject returnPoint;
    public GameObject ShopTeleport;
    public void Start()
    {
        ShopTeleport = GameObject.Find("ShopTeleport");
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = new Vector3(ShopTeleport.transform.position.x, 1f, ShopTeleport.transform.position.z);
            GameObject instance = (GameObject)Instantiate(returnPoint, transform.position, Quaternion.identity);
            instance.transform.parent = transform;
            Destroy(ShopTeleport);
        }
    }
}
