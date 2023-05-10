//Worked on by Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Track_Level : MonoBehaviour
{
    public Leaderboard_Data Leaderboard;
    public GameObject EntryWindow;
    public Level_Counter CurrentLevel;
    public GameObject Screens;
    public bool HasPressed;
    public AudioClip victory;
    public AudioClip Error;

    string PlayerName;
    string SavedName;
    public Text InputText;

    public void EntryScreen()
    {
        EntryWindow.SetActive(true);
        Screens.SetActive(false);
        Time.timeScale = 0f;
    }

    public void OnSub() // updates the data object for the leaderboard
    {
        PlayerName = InputText.text;
        if(PlayerName != "" && HasPressed == false)
        {
            
            HasPressed = true;
            Leader_Data info = new Leader_Data(CurrentLevel.Level, PlayerName);
            Debug.Log(info.level + info.player);
            Leaderboard.data.Add(info);
            BubbleSort();
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = victory;
            audio.Play();
        }
    }

    public void GoToCredits()
    {
        if(HasPressed == true)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("CreditsPage");
        }

        else
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = Error;
            audio.Play();
        }
    }

    public void BubbleSort()
    {
        for (int i = 0; i < Leaderboard.data.Count - 1; i++)
        {
            for (int j = 0; j < Leaderboard.data.Count - 1; j++)
            {
                if (Leaderboard.data[j].level < Leaderboard.data[j + 1].level)
                {
                    Leader_Data temp = Leaderboard.data[j];
                    Leaderboard.data[j] = Leaderboard.data[j + 1];
                    Leaderboard.data[j + 1] = temp;
                }
            }
        }
    }
}
