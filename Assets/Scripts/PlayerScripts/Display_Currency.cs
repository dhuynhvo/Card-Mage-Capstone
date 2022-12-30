using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
        MoneyText.text = cash.money.ToString();
        SteveText.text = cash.Steves.SteveMoney.ToString();
    }
}
