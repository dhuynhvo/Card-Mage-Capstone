// Dan Hyunhvo and Abida Mim

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Increments enemy deaths and time
public class Stats_Events : MonoBehaviour
{
        // connects to scriptable object
    public Stats_Info info;

           // sets enemy death and time to 0
    void Start()
    {
        GameEvents.current.OnEnemyDeath += IncrementEnemies;
        GameEvents.current.MoneyIncrement += IncrementMoney;
        info.enemies = 0;
        info.TimeSpent = 0;
    }

    void Update()
    {
        IncrementTime();
    }

        // adds when enemy is killed
    public void IncrementEnemies(int ID)
    {
        info.enemies++;
    }

        // time goes up
    public void IncrementTime()
    {
        info.TimeSpent += Time.deltaTime;
    }

    public void IncrementMoney(int value, bool isSteve)
    {
        if(isSteve)
        {
            info.Steves++;
        }

        else
        {
            info.Currency += value;
        };
    }
}
