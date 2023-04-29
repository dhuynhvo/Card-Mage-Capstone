using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardUi : MonoBehaviour
{
    [SerializeField]
    public GameObject PlayButton;
    [SerializeField]
    public GameObject OptionsButton;
    [SerializeField]
    public GameObject CreditsButton;
    [SerializeField]
    public GameObject ExitButton;
    [SerializeField]
    public GameObject CardMageTitle;
    [SerializeField]
    public GameObject LeaderboardButton;

    [SerializeField]
    public GameObject LeaderboardScreen;


    public void ToLeaderboard()
    {
        PlayButton.SetActive(false);
        OptionsButton.SetActive(false);
        CreditsButton.SetActive(false);
        ExitButton.SetActive(false);
        CardMageTitle.SetActive(false);
        LeaderboardButton.SetActive(false);

        LeaderboardScreen.SetActive(true);
    }

    public void FromLeaderboard()
    {
        PlayButton.SetActive(true);
        OptionsButton.SetActive(true);
        CreditsButton.SetActive(true);
        ExitButton.SetActive(true);
        CardMageTitle.SetActive(true);
        LeaderboardButton.SetActive(true);

        LeaderboardScreen.SetActive(false);
    }
}
