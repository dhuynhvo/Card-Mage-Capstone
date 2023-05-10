//Worked on by Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove_From_Drops : MonoBehaviour
{
    public Premade_Decks Drops;
    public Premade_Decks CardPool;

    public void Start()
    {
        for(int i = 0; i < CardPool.CardNames.Count; i++)
        {
            RemoveDrops(CardPool.CardNames[i]);
        }
    }

    public void RemoveDrops(string drop)
    {
        for(int i = 0; i < Drops.cards.Count; i++)
        {
            if (Drops.cards[i].name == drop + " Card" || Drops.cards[i].name == drop)
            {
                Drops.cards.RemoveAt(i);
            }
        }
    }
}
