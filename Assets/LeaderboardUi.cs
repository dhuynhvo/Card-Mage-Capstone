// Worked on by Abida Mim and Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardUi : MonoBehaviour
{

    [SerializeField]
    public GameObject CardMageTitle;
    [SerializeField]
    public GameObject Buttons;
    [SerializeField]
    public Change_Leaderboard Changer;
    [SerializeField]
    public GameObject LeaderboardScreen;


    public void ToLeaderboard()
    {

        CardMageTitle.SetActive(false);
        Buttons.SetActive(false);


        LeaderboardScreen.SetActive(true);

        Changer.ChangeScores();
    }

    public void FromLeaderboard()
    {
        Buttons.SetActive(true);
        CardMageTitle.SetActive(true);


        LeaderboardScreen.SetActive(false);
    }
}
