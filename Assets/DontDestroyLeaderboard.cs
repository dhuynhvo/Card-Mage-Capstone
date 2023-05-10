//Worked on by Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyLeaderboard : MonoBehaviour
{
    public Leaderboard_Data data;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
