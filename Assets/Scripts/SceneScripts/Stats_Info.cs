// Worked on by Abida Mim and Dan Hyunhvo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // creates scriptable object for stats data
[CreateAssetMenu(fileName = "Stats_Data", menuName = "ScriptableObjects/Stats_Infa", order = 2)]
public class Stats_Info : ScriptableObject
{

    [SerializeField]
    public int enemies = 0;
    [SerializeField]
    public float TimeSpent;
    [SerializeField]
    public int Currency;
    [SerializeField]
    public int Steves;

}
