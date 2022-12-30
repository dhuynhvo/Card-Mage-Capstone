//Dan Huynhvo
//UNR
//CS 425

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class Deck_Debugging_Tool : MonoBehaviour
{

    public Text[] CardCounts;
    public Deck DeckHolder, GraveHolder;
    public Hand HandHolder;
    public Play_Card QueueHolder;
    public int DeckCount, HandCount, QueueCount, GraveCount, TotalCount, StarterAmount;

    // This is a debugging tool for making sure that all cards stay in the game world and dont disappear
    // In this case we start with 20 cards in the deck, so Deck + Queue + Hand + Grave should == 20;
    void Update()
    {
        DeckCount = DeckHolder.Cards.Count(x => x != null);
        HandCount = HandHolder.CardsInHand.Count(x => x != null);
        QueueCount = QueueHolder.CardQueue.Count(x => x != null);
        GraveCount = GraveHolder.Graveyard.Count(x => x != null);

        CardCounts[0].text = "Deck: " + DeckCount.ToString();
        CardCounts[1].text = "Hand: " + HandCount.ToString();
        CardCounts[2].text = "Queue: " + QueueCount.ToString();
        CardCounts[3].text = "Grave: " + GraveCount.ToString();
        TotalCount = DeckCount + HandCount + QueueCount + GraveCount;
        CardCounts[4].text = "Total: " + TotalCount;

        if (DeckCount + HandCount + QueueCount + GraveCount == StarterAmount)
        {
            Debug.Log("Works Fine.... Currently: ");
        }

        else
        {
            Debug.Log("ERROR: ");
        }
    }
}
