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
    public GameObject LeaderboardScreen;


    public void ToLeaderboard()
    {

        CardMageTitle.SetActive(false);
        Buttons.SetActive(false);


        LeaderboardScreen.SetActive(true);
    }

    public void FromLeaderboard()
    {
        Buttons.SetActive(true);
        CardMageTitle.SetActive(true);


        LeaderboardScreen.SetActive(false);
    }
}
