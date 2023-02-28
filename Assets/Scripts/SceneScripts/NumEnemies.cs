using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats_Data", menuName = "ScriptableObjects/StatsInfo", order = 1)]
public class NumEnemies : ScriptableObject
{

    [SerializeField]
    public string NumEnemies;
    [SerializeField]
    public string TimeSpent;
    [SerializeField]
    public string Currency;
}
