// Worked on by Abida

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// using scriptable objects and connecting them to ui elements, 
// the currency values are displayed

public class Display_Currency : MonoBehaviour
{
    [SerializeField]
    private Text MoneyText;
    [SerializeField]
    private Text SteveText;
    [SerializeField]
    private Player_Currency cash;

    void FixedUpdate()
    {
        DisplayMoney();
    }

    private void DisplayMoney()
    {
        MoneyText.text = cash.Steves.money.ToString();
        SteveText.text = cash.Steves.SteveMoney.ToString();
    }
}
