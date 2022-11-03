using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public List<GameObject> Cards;
    public List<GameObject> Graveyard;
    private bool DoneIndexing = false;
    public int DeckLimit = 9;
    int CardIndex = 1;
    int GraveIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Cards[0] == null)
        {
            
            while(Cards[CardIndex] != null && CardIndex < DeckLimit && DoneIndexing != true)
            {
                CardIndex++;
                if(CardIndex >= DeckLimit)
                {
                    DoneIndexing = true;
                }
                else if(Cards[CardIndex] == null)
                {
                    DoneIndexing = true;
                }
            }

            if(DoneIndexing == true)
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

        if (Input.GetMouseButtonDown(0) && Cards[0] != null)
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
        }
    }
}
