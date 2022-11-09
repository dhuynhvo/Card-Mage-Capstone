using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public List<GameObject> Cards;
    public List<GameObject> Graveyard;
    public Hand PlayerHand;
    private bool DoneIndexing = false;
    public int DeckLimit = 9; //The max number - 1 accounting for 0
    public int CardIndex = 1;
    public int GraveIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < DeckLimit; i++)
        {
            Cards[i] = Resources.Load("Prefabs/FireCube") as GameObject;
            i++;
            if(i <= DeckLimit)
            {
                Cards[i] = Resources.Load("Prefabs/Glass of Water") as GameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PopDeck();

        /*if (Input.GetMouseButtonDown(0) && Cards[0] != null)
        {
            Debug.Log(Cards[0].ToString() + " has been played");
            Graveyard[GraveIndex] = Cards[0];
            GraveIndex++;
            Cards[0] = null;
        }

        else if (Input.GetMouseButton(0) && Cards[0] == null)
        {
            Debug.Log("Empty Deck!!!");
            for(int i = 0; i < GraveIndex; i++)
            {
                Cards[i] = Graveyard[i];
            }
            
            for (int i = 0; i < GraveIndex; i++)
            {
                Graveyard[i] = null;
            }
            GraveIndex = 0;
            Debug.Log("Deck Reload");
        }*/
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
            Cards[i] = Graveyard[i];
        }

        for (int i = 0; i <= DeckLimit; i++)
        {
            Graveyard[i] = null;
        }
    }
}
