using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter_To_Boss : MonoBehaviour
{
    public GameObject BossRoomTeleport;
    public Point_To_Boss_Room Arrow;

    public void Start()
    {
        BossRoomTeleport = GameObject.Find("BossTeleport");
        Arrow = GameObject.Find("Player").transform.GetChild(5).GetComponent<Point_To_Boss_Room>();
        Arrow.Target = gameObject.transform.parent.gameObject;
        Arrow.FoundTarget = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Player")
        {
            other.transform.position = new Vector3(BossRoomTeleport.transform.position.x, 1f, BossRoomTeleport.transform.position.z);
            GameObject BossArrow = other.gameObject.transform.GetChild(5).gameObject;
            BossArrow.SetActive(false);
        }
    }
}
