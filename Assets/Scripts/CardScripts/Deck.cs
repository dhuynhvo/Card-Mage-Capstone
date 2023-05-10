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
    private int DeckLimit; //The max number - 1 accounting for 0
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
    [SerializeField]
    public bool InitDeck;
    [SerializeField]
    private Premade_Decks StartDeck;


    private void Awake()
    {
        StartDeck = Resources.Load<Premade_Decks>("Player_Card_Pool");
        LoadDeck(StartDeck.DeckName);
    }

    private void Start()
    {
        PushCards();
        Shuffle();
        InitDeck = true;
    }

    void Update()
    {
        CheckDeckZero();
    }

    public void Shuffle()   //shuffles the deck
    {
        int index = 0;
        while (Cards[index] != null && index < Cards.Count - 1)
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

    public void CheckDeckZero() //used in some functions
    {
        if (Cards[0] == null && PushingDeck == false)
        {
            PushingDeck = true;
            PushCards();
        }
    }

    public void CheckEmptyDeck()    //used in some functions
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

    public void LoadDeck(string nameOfDeck = "Base")    // loads deck when called
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
        
        for (int i = 0; i < Cards.Count; i++)
        {
            if (Cards[i] != null)
            {
                tempCards.Add(Cards[i]);
                Cards[i] = null;
            }
        }

        for (int i = 0; i < tempCards.Count; i++)
        {
            Cards[i] = tempCards[i];
        }

        tempCards.Clear();

        PushingDeck = false;
    }
}
