using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Listener : MonoBehaviour
{
    public Enemy_Info info;
    void Start()
    {
        GameObject listener = GameObject.Find("Listener");
        listener.gameObject.GetComponent<BossDeathEvents>().info = info;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
