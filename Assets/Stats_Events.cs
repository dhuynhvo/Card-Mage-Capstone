using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats_Events : MonoBehaviour
{

    public Stats_Info info;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.OnEnemyDeath += IncrementEnemies;
        info.enemies = 0;
        info.TimeSpent = 0;
    }

    void Update()
    {
        IncrementTime();

    }

    // Update is called once per frame

    public void IncrementEnemies(int ID)
    {
        info.enemies++;
    }

    public void IncrementTime()
    {
        info.TimeSpent += Time.deltaTime;
    }
}
