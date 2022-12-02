using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Screen : MonoBehaviour
{
    [SerializeField]
    private string PauseButton;
    [SerializeField]
    private GameObject PauseScreen;
    [SerializeField]
    private GameObject ChangeDeckScreen;
    [SerializeField]
    private Deck PlayerDeck;
    private bool paused = false;

    void Update()
    {
        /*if(Input.GetKey(KeyCode.RightBracket))          //THIS PORTION OF CODE IS FOR GETTING SCREENSHOTS, IF YOU DON'T NEED IT COMMENT IT OuT
        {
            Time.timeScale = 0f;
        }

        else if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            Time.timeScale = 1f;
        }   */                                            //Ends here

        if(Input.GetButtonDown(PauseButton))
        {
            if(!paused)
            {
                PauseScreen.SetActive(true);
                Time.timeScale = 0f;
                paused = !paused;
            }

            else if(paused)
            {
                PauseScreen.SetActive(false);
                Time.timeScale = 1f;
                paused = !paused;
            }
        }

    }

    public void ResumeGame()
    {
        if(paused)
        {
            PauseScreen.SetActive(false);
            Time.timeScale = 1f;
            paused = !paused;
        }
    }

    public void ChangeDeckPauseScreen()
    {
        PauseScreen.SetActive(false);
        ChangeDeckScreen.SetActive(true);
    }

    public void BackToPause()
    {
        PauseScreen.SetActive(true);
        ChangeDeckScreen.SetActive(false);
    }

    public void ChangeDeck(string deckName)
    {
        PlayerDeck.LoadDeck(deckName);
    }


    //It's hardcoded because i'm very sleepy, if anyone finds this remind me to fix it please -Dan
    public void ChoseDeck1()
    {
        PlayerDeck.LoadDeck("LITERALLY JUST FIRE");
        BackToPause();
    }

    public void ChoseDeck2()
    {
        PlayerDeck.LoadDeck("TALLEST GLASS OF WATER");
        BackToPause();
    }

    public void ChoseDeck3()
    {
        PlayerDeck.LoadDeck("NOTHING LOL");
        BackToPause();
    }
}
