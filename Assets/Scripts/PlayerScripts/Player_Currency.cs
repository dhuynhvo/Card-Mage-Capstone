//Dan Huynhvo
//UNR
//CS 425

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Currency : MonoBehaviour
{
    public int money;
    public SteveMoneySO Steves;

    public void SteveReset()
    {
        if(Steves != null)
        {
            Steves.money = 0;
            Steves.SteveMoney = 0;
        }

    }
}
