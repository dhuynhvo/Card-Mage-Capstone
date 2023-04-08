using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock_Boots_Behavior : MonoBehaviour
{
    public Player_Movement SpeedBuff;
    public GameObject Player;
    public float SpeedAmount;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        SpeedBuff = Player.GetComponent<Player_Movement>();
        SpeedBuff.SpeedBuff = SpeedAmount;
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
    }
    private void OnDisable()
    {
        SpeedBuff.SpeedBuff = 1f;
    }
}
