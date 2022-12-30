//Dan Huynhvo
//UNR
//CS 425

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateCardSprites : MonoBehaviour
{
    [SerializeField]
    private Hand PlayerHand;
    [SerializeField]
    public Image[] CardSprites;
    [SerializeField]
    private Sprite DefaultCard;
    [SerializeField]
    private int MaxCardSprites;

    private void Start()
    {
    }
    void Update()
    {
        for(int i = 0; i < MaxCardSprites; i++)
        {
            if(PlayerHand.CardsInHand[i] != null)
            {
                CardSprites[i].sprite = PlayerHand.CardsInHand[i].GetComponent<Spell_Info>().CardSprite;
            }

            else
            {
                CardSprites[i].sprite = DefaultCard;
            }
        }
    }
}
