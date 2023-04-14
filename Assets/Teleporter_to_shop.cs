using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter_to_shop : MonoBehaviour
{
    public GameObject returnPoint;
    public GameObject ShopTeleport;
    public void Start()
    {
        
    }
    void FixedUpdate()
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
            //instance.transform.parent = transform;
            StartCoroutine(waiter(1.5f));
            Destroy(ShopTeleport);
            Destroy(gameObject);
        }
    }
    IEnumerator waiter(float waitTime)
    {
        float counter = 0;

        while (counter < waitTime)
        {
            //Increment Timer until counter >= waitTime
            counter += Time.deltaTime;
            //Debug.Log("We have waited for: " + counter + " seconds");
            //Wait for a frame so that Unity doesn't freeze
            yield return null;
        }
    }
}
