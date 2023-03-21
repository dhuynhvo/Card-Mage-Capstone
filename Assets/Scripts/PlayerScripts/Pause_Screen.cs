//Dan Huynhvo, Abida Mim
//UNR
//CS 425

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
    private GameObject DeckBuilderScreen;
    [SerializeField]
    private GameObject StatsScreen;
    [SerializeField]
    private Deck PlayerDeck;
    [SerializeField]
    private Premade_Decks[] stash;
    private bool paused = false;

    private void Start()
    {
        for(int i = 0; i < stash.Length; i++)
        {
            stash[i] = gameObject.GetComponent<Deck>().DeckStash[i];
        }
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.RightBracket))          //THIS PORTION OF CODE IS FOR GETTING SCREENSHOTS, IF YOU DON'T NEED IT COMMENT IT OuT
        {
            Time.timeScale = 0f;
        }

        else if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            Time.timeScale = 1f;
        }                                            //Ends here

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
                ChangeDeckScreen.SetActive(false);
                DeckBuilderScreen.SetActive(false);
                StatsScreen.SetActive(false);
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
    public void ResumeGameFromStats()
    {
        if (paused)
        {
            StatsScreen.SetActive(false);
            Time.timeScale = 1f;
            paused = !paused;
        }
    }

    public void ChangeDeckPauseScreen()
    {
        PauseScreen.SetActive(false);
        ChangeDeckScreen.SetActive(true);
    }

    public void ChangeStatsScreen()
    {
        PauseScreen.SetActive(false);
        StatsScreen.SetActive(true);
    }

    public void DeckBuilderPauseScreen()
    {
        PauseScreen.SetActive(false);
        ChangeDeckScreen.SetActive(false);
        DeckBuilderScreen.SetActive(true);
    }

    public void BackToPause()
    {
        PauseScreen.SetActive(true);
        ChangeDeckScreen.SetActive(false);
        DeckBuilderScreen.SetActive(false);
    }

    public void StatsToPause()
    {
        PauseScreen.SetActive(true);
        StatsScreen.SetActive(false);
    }

    public void ChangeDeckToResume()
    {
        PauseScreen.SetActive(false);
        ChangeDeckScreen.SetActive(false);
    }

    public void ChangeDeck(string deckName)
    {
        PlayerDeck.LoadDeck(deckName);
    }


    //It's hardcoded because i'm very sleepy, if anyone finds this remind me to fix it please -Dan
    public void ChoseDeck1()
    {
        PlayerDeck.ClearGrave();
        PlayerDeck.LoadDeck(stash[0].DeckName);
        BackToPause();
    }

    public void ChoseDeck2()
    {
        PlayerDeck.ClearGrave();
        PlayerDeck.LoadDeck(stash[1].DeckName);
        BackToPause();
    }

    public void ChoseDeck3()
    {
        PlayerDeck.ClearGrave();
        PlayerDeck.LoadDeck(stash[2].DeckName);
        BackToPause();
    }

    public void ChoseDeck4()
    {
        PlayerDeck.ClearGrave();
        PlayerDeck.LoadDeck(stash[3].DeckName);
        BackToPause();
    }

    public void SaveDeck()
    {
        int index = 0;
        for (int i = 0; i < 20; i++)
        {
            if (DeckBuilderScreen.transform.GetChild(0).transform.GetChild(i).transform.childCount > 0)
            {
                //gameObject.GetComponent<Deck>().DeckStash[3].cards.Add(DeckBuilderScreen.transform.GetChild(0).transform.GetChild(i).transform.GetChild(0).GetComponent<Connected_Spell>().spell);
                gameObject.GetComponent<Deck>().DeckStash[3].cards[i] = DeckBuilderScreen.transform.GetChild(0).transform.GetChild(i).transform.GetChild(0).GetComponent<Connected_Spell>().spell;
            }

            else
            {
                gameObject.GetComponent<Deck>().DeckStash[3].cards[i] = null;
            }
            
        }

        for (int i = 0; i < 20; i++)
        {
            if (gameObject.GetComponent<Deck>().DeckStash[3].cards[i] == null && i != 19)
            {
                index = i + 1;
                if (index < 20)
                {
                    while (index < 20 && gameObject.GetComponent<Deck>().DeckStash[3].cards[index] == null)
                    {
                        index++;
                    }
                }
                    
                if (index < 20)
                {
                    gameObject.GetComponent<Deck>().DeckStash[3].cards[i] = gameObject.GetComponent<Deck>().DeckStash[3].cards[index];
                    gameObject.GetComponent<Deck>().DeckStash[3].cards[index] = null;
                }
                else
                {
                    index = 19;
                }

            }
        }

        //if (DeckBuilderScreen.transform.GetChild(0).transform.GetChild(20).transform.childCount > 0)
        //{
        //    gameObject.GetComponent<Deck>().DeckStash[3].BasicSpell = DeckBuilderScreen.transform.GetChild(0).transform.GetChild(20).transform.GetChild(0).GetComponent<Connected_Spell>().spell;
        //}
            
    }
}
