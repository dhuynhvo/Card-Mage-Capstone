using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_Level_To_1 : MonoBehaviour
{
    public Level_Counter levels;
    void Start()
    {
        levels.Level = 1;
    }

    public void LevelTo1()
    {
        levels.Level = 1;
    }

}
