using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockInfusion : MonoBehaviour
{
    public PlayerHealth DefenceBuff;
    public GameObject Player;
    public float DefenceAmount;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        DefenceBuff = Player.GetComponent<PlayerHealth>();
        DefenceBuff.DefenceBuff = DefenceAmount;
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
    }
    private void OnDisable()
    {
        DefenceBuff.DefenceBuff = 1f;
    }
}
