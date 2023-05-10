//Worked on by Dan Huynhvo
//CS426

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Listener : MonoBehaviour
{
    public Enemy_Info info;
    void Start()    // Only finds a reference to the boss listener game object during runtime
    {
        GameObject listener = GameObject.Find("Listener");
        listener.gameObject.GetComponent<BossDeathEvents>().info = info;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
