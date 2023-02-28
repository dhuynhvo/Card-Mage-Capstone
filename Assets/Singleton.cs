// Abida Mim

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Singletonn : MonoBehaviour
{
    [SerializeField]
    private Text MoneyText;
    [SerializeField]
    private Text SteveText;
    [SerializeField]
    private Player_Currency cash;

    public static Singletonn Instance
    {
        get;
        private set;
    }

    // checks if instance already exists, and deletes if it does
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        else
        {
            Instance = this;
            MoneyText.text = cash.money.ToString();
            SteveText.text = cash.Steves.SteveMoney.ToString();
        }
    }
}
