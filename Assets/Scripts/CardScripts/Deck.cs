//Dan Huynhvo
//UNR
//CS 425

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> Cards;
    [SerializeField]
    public List<GameObject> Graveyard;
    [SerializeField]
    private Hand PlayerHand;
    [SerializeField]
    private bool DoneIndexing = false;
    [SerializeField]
    private int DeckLimit; //The max number - 1 accounting for 0
    [SerializeField]
    private int CardIndex = 1;
    [SerializeField]
    public int GraveIndex = 0;
    [SerializeField]
    public Premade_Decks[] DeckStash;
    [SerializeField]
    public Premade_Decks CurrentDeck;
    [SerializeField]
    public bool LoadingDeck = false;
    [SerializeField]
    private string CurrentDeckName = "Base";
    [SerializeField]
    private bool PushingDeck;


    private void Awake()
    {
        LoadDeck();
    }

    void Update()
    {
        PopDeck();
        CheckDeckZero();
    }

    public void Shuffle()
    {
        int index = 0;
        while (Cards[index] != null)
        {
            index++;
        }

        for (int i = 0; i < index; i++)
        {
            GameObject temp = Cards[i];
            int randomIndex = Random.Range(i, index);
            Cards[i] = Cards[randomIndex];
            Cards[randomIndex] = temp;
        }
    }

    public void CheckDeckZero()
    {
        if (Cards[0] == null && PushingDeck == false)
        {
            PushingDeck = true;
            PushCards();
        }
    }

    public void CheckEmptyDeck()
    {
        for(int i = 0; i < Cards.Count; i++)
        {
            if (Cards[i] != null)
            {
                return;
            }

        }
        GraveIndex = 0;
        DeckReload();
        PushCards();
        Shuffle();
    }

    public void LoadDeck(string nameOfDeck = "Base")
    {
        PlayerHand.EmptyHand();

        for (int i = 0; i < DeckStash.Count(); i++)
        {
            if(DeckStash[i].DeckName == nameOfDeck)
            {
                CurrentDeck = DeckStash[i];
                PlayerHand.BasicSpell = CurrentDeck.BasicSpell;
                CurrentDeckName = CurrentDeck.DeckName;
            }
        }

        for (int i = 0; i < DeckLimit; i++)
        {
            Cards[i] = CurrentDeck.cards[i];
            if (i >= DeckLimit - 1)
            {
                Cards[DeckLimit] = CurrentDeck.cards[DeckLimit];
            }
        }

        LoadingDeck = false;
    }

    public void PopDeck()
    {
        if (Cards[0] == null)
        { 

            while (Cards[CardIndex] != null && CardIndex <= DeckLimit && DoneIndexing != true)
            {
                CardIndex++;
                if (CardIndex >= DeckLimit)
                {
                    DoneIndexing = true;
                }
                else if (Cards[CardIndex] == null)
                {
                    DoneIndexing = true;
                }
            }

            if (DoneIndexing == true)
            {
                for (int i = 0; i < CardIndex; i++)
                {
                    Cards[i] = Cards[i + 1];
                }

                if (CardIndex == DeckLimit)
                {
                    Cards[CardIndex] = null;
                }

                DoneIndexing = false;
                CardIndex = 1;
            }

        }
    }

    public void GraveTheCard(GameObject card, ref int GravePosition)
    {
        Graveyard[GravePosition] = card;
        if (GravePosition == DeckLimit)
        {
            GravePosition = 0;
        }
        else
        {
            GravePosition++;
        }
    }

    public void ClearGrave()
    {
        for (int i = 0; i < Graveyard.Count; i++)
        {
            Destroy(Graveyard[i]);
        }
    }

    public void DeckReload()
    {
        List<GameObject> tempCards = new List<GameObject>();
        for (int i = 0; i <= DeckLimit; i++)
        {
            if(Graveyard[i] != null)
            {
                tempCards.Add(Resources.Load<GameObject>("Prefabs/Spells/" + Graveyard[i].GetComponent<Spell_Info>().SpellName));
            }
        }

        for (int i = 0; i < tempCards.Count; i++)
        {
            GameObject temp = tempCards[i];
            int randomIndex = Random.Range(i, tempCards.Count);
            tempCards[i] = tempCards[randomIndex];
            tempCards[randomIndex] = temp;
        }

        for (int i = 0; i < tempCards.Count; i++)
        {
            Cards[i] = tempCards[i];
        }

        for (int i = 0; i <= DeckLimit; i++)
        {
            Destroy(Graveyard[i], 1f);
            Graveyard[i] = null;
        }
    }

    public void PushCards()
    {
        List<GameObject> tempCards = new List<GameObject>();

        for(int i = 0; i < Cards.Count; i++)
        {
            if (Cards[i] != null)
            {
                tempCards.Append(Cards[i]);
            }
        }

        for (int i = 0; i < tempCards.Count; i++)
        {
            Debug.Log(i + "Testing");
            Cards[i] = tempCards[i];
        }

        tempCards.Clear();

        PushingDeck = false;
    }
}
