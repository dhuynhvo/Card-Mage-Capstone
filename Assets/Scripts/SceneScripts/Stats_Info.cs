using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats_Data", menuName = "ScriptableObjects/Stats_Infa", order = 2)]
public class Stats_Info : ScriptableObject
{

    [SerializeField]
    public int enemies = 0;
    [SerializeField]
    public string TimeSpent;
    [SerializeField]
    public string Currency;

}
