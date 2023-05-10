//Worked on by Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_Leaderboard : MonoBehaviour
{
    public List<Text> Scores;
    public Leaderboard_Data Data;
    public int MaxScores;

    public void Start()
    {
        ChangeScores();
    }

    public void ChangeScores()
    {
        if (Data.data.Count > 0)
        {
            for (int i = 0; i < Data.data.Count; i++)
            {
                if (i < MaxScores)
                {
                    Scores[i].text = (i + 1) + ". " + Data.data[i].player + ": " + Data.data[i].level;
                }
            }
        }

        if(Data.data.Count < MaxScores && Data.data.Count > 0)
        {
            int index = Data.data.Count;
            while(index < MaxScores)
            {
                Scores[index].text = (index + 1).ToString() + ". ";
                index++;
            }
        }
    }
}
