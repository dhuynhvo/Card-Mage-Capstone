//Dan Huynhvo
//UNR
//CS 425

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public List<GameObject> CardsInHand;
    public GameObject BasicSpell;
    public Deck PlayerDeck;
    public bool HandIsFull;
    public bool HandIsEmpty;
    [SerializeField]
    private int MaxHandLength = 4;

    // Start is called before the first frame update
    void Awake()
    {
        CardsInHand = new List<GameObject>(new GameObject[MaxHandLength]);
    }

    // Update is called once per frame
    void Update()
    {
        FillHand();
    }

    public void FillHand()
    {
        //if (PlayerDeck.Cards[0] != null && !HandFullCheck())
        if (!HandFullCheck()) //unsure why the behavior works without the Player.Deck.Cards != null
        {
            for (int i = 0; i < MaxHandLength; i++)
            {
                if (CardsInHand[i] == null)
                {
                    CardsInHand[i] = PlayerDeck.Cards[0];
                    PlayerDeck.Cards[0] = null;
                }

            }

        }
    }

    public void EmptyHand()
    {
            for (int i = 0; i < MaxHandLength; i++)
            {
                if (CardsInHand[i] != null)
                {
                    CardsInHand[i] = null;
                }
            }
    }

    public bool HandFullCheck()
    {

        for(int i = 0; i < MaxHandLength; i++)
        {
            
            if(CardsInHand[i] == null)
            {
                HandIsFull = false;
                return false;
            }
        }

        HandIsFull = true;
        return true;
    }

    public bool HandEmptyCheck()
    {
        for (int i = 0; i < MaxHandLength; i++)
        {
            if (CardsInHand[i] != null)
            {
                HandIsEmpty = false;
                return false;
            }
        }

        HandIsEmpty = true;
        return true;
    }
}
