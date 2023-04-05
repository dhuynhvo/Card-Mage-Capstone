using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.TestTools;

public class BossLock : MonoBehaviour
{
    [SerializeField]
    public Camera myCamera;
    //[SerializeField]
    //public GameObject Boss;
    bool inBossRoom = false;
    bool inBossRoom2 = false;
    public GameObject tile;
    public GameObject other;
    public GameObject BossHPBar;
    // Start is called before the first frame update
    void Start()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        //if player is on a gameObject bossroom, we zoom camera out and spawn doors.
        if (other.gameObject.tag == "BossRoom")
        {
            Debug.Log("HELLEELLELEL");
            BossHPBar.SetActive(true);
            inBossRoom = true;
            LockTheRoom();
            
        }
    }
    
    private void LockTheRoom()
    {
        foreach (var doorway in GameObject.FindGameObjectsWithTag("Doorway"))
        {
            if (doorway.name == "doorway")
            {
                 GameObject.Instantiate(tile, doorway.transform.position, Quaternion.identity);
            }
        }
    }
    private void HandleZoom()
    {
        if (inBossRoom == true)
        {
            GameObject other = GameObject.FindGameObjectWithTag("BossRoom");
            //Debug.Log("ZOOMING");

            float moveRate = .3f;
            myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, other.transform.position + new Vector3(0, 35, -7.5f), Time.deltaTime * moveRate);
            float cameraZoom = 4.5f;
            float cameraZoomDifference = cameraZoom - myCamera.orthographicSize;
            float cameraZoomSpeed = .7f;
            myCamera.orthographicSize += cameraZoomDifference * cameraZoomSpeed *Time.deltaTime;
            //this.transform.position = other.transform.position + new Vector3(0, 35, 0);
            if (myCamera.orthographicSize > 3.8f)
            {
                inBossRoom = false;
                inBossRoom2 = true;
            }
        }
    }
    private void HandleHold()
    {
        if (inBossRoom2 == true)
        {
            //Debug.Log("HOLDING ON BOSS");
            float moveRate = 1f;

            other = GameObject.FindGameObjectWithTag("Player");
            myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, other.transform.position  + new Vector3(0, 35, 0), Time.deltaTime * moveRate);

        }

    }
    // Update is called once per frame
    void Update()
    {
        HandleZoom();
        HandleHold();
    }
}
