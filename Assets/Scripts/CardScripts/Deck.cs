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

    private void Awake()
    {
        LoadDeck();
    }

    void Update()
    {
        PopDeck();
    }

    public void LoadDeck(string nameOfDeck = "Base")
    {
        PlayerHand.EmptyHand();

        for (int i = 0; i < 3; i++)
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

            while (Cards[CardIndex] != null && CardIndex < DeckLimit && DoneIndexing != true)
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

    public void DeckReload()
    {
        for (int i = 0; i <= DeckLimit; i++)
        {
            Cards[i] = Resources.Load<GameObject>("Prefabs/" + Graveyard[i].GetComponent<Spell_Info>().SpellName);
        }

        for (int i = 0; i <= DeckLimit; i++)
        {
            Destroy(Graveyard[i], 1f);
            Graveyard[i] = null;
        }
    }
}
