using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter_to_return : MonoBehaviour
{
    public GameObject returnPoint;
    public void Start()
    {
      //  returnPoint = GameObject.Find("ReturnPoint");
    }

    private void OnCollisionEnter(Collision other)
    {
        returnPoint = GameObject.Find("ReturnPoint(Clone)");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = new Vector3(returnPoint.transform.position.x, 1f, returnPoint.transform.position.z);
            StartCoroutine(waiter(1.5f));
            Destroy(returnPoint);
            
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
