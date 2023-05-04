using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleSS : MonoBehaviour
{
    private Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
       myCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScreenShakeToggle()
    {
        if (myCamera.GetComponent<shaker>().enableds == true)
        {
            myCamera.GetComponent<shaker>().enableds = false;
        }
        else if (myCamera.GetComponent<shaker>().enableds == false)
        {
            myCamera.GetComponent<shaker>().enableds = true;
        }
    }

}
