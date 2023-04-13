using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter_To_Boss : MonoBehaviour
{
    public GameObject BossRoomTeleport;

    public void Start()
    {
        BossRoomTeleport = GameObject.Find("BossTeleport");
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Player")
        {
            other.transform.position = new Vector3(BossRoomTeleport.transform.position.x, 1f, BossRoomTeleport.transform.position.z);
        }
    }
}
