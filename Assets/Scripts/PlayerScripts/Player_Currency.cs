using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Currency : MonoBehaviour
{
    public int money;

    private void Start()
    {
        if(money < 100)
        {
            money = 100;
        }
    }

}
