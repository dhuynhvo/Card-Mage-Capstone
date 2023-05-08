// Worked on by Abida Mim

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Singleton : MonoBehaviour
{
    [SerializeField]
    private Text MoneyText;
    [SerializeField]
    private Text SteveText;
    [SerializeField]
    private Player_Currency cash;

    public static Singleton Instance
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
