using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action OnEnemyDeath;
    public event Action OnShopBuy;
    public event Action NearDroppedCard;
    public void DropCard_E()
    {
        if(OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }
    }
    public void DropCard_S()
    {
        if (OnShopBuy != null)
        {
            OnShopBuy();
        }
    }
    public void PickUpCard_E()
    {
        if(NearDroppedCard != null)
        {
            NearDroppedCard();
        }
    }
}
