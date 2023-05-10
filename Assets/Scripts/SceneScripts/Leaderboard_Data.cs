//Worked on by Dan Huynhvo
//CS426

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Leaderboard", menuName = "ScriptableObjects/Leaderboard_Data", order = 6)]
public class Leaderboard_Data : ScriptableObject    // scriptable object for handling leaderboard data
{
    public List<Leader_Data> data = new List<Leader_Data>();
}

[System.Serializable]
public class Leader_Data
{
    public int level;
    public string player;

    public Leader_Data(int LevelData, string NameData)
    {
        player = NameData;
        level = LevelData;
    }
}
