//Worked on by Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Reset_Drops_And_Cards : MonoBehaviour
{
    public Premade_Decks Drops;
    public Premade_Decks CardPool;
    public List<GameObject> StandardDrops;
    public List<string> StandardCardPool;

    void Start()
    {
        Drops.cards.Clear();
        CardPool.CardNames.Clear();

        for(int i = 0; i < StandardDrops.Count; i++)
        {
            Drops.cards.Add(StandardDrops[i]);
        }

        for (int i = 0; i < StandardCardPool.Count; i++)
        {
            CardPool.CardNames.Add(StandardCardPool[i]);
        }
    }

}
