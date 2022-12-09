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

    public event Action<int> OnEnemyDeath;
    public event Action<int, GameObject> OnShopBuy;
    public event Action<int> NearDroppedCard;
    public void DropCard_E(int ID)
    {
        if(OnEnemyDeath != null)
        {
            OnEnemyDeath(ID);
        }
    }
    public void DropCard_S(int ID, GameObject player)
    {
        if (OnShopBuy != null)
        {
            OnShopBuy(ID, player);
        }
    }
    public void PickUpCard_E(int ID)
    {
        if(NearDroppedCard != null)
        {
            NearDroppedCard(ID);
        }
    }
}
