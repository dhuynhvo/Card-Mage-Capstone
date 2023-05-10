//Worked on by Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats_Data", menuName = "ScriptableObjects/Stats_Info", order = 1)]
public class NumEnemies : ScriptableObject  //data object for stats
{

    [SerializeField]
    public int enemies = 0;
    [SerializeField]
    public string TimeSpent;
    [SerializeField]
    public string Currency;

}
