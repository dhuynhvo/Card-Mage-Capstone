// Abida Mim

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Singletonn : MonoBehaviour
{
    [SerializeField]
    private SteveMoneySO info;
    [SerializeField]
    private Stats_Info stats;
    [SerializeField]
    private Text MoneyText;
    [SerializeField]
    private Text SteveText;
    [SerializeField]
    private Text TimeText;
    [SerializeField]
    private Text EnemiesText;
    

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
            MoneyText.text = info.money.ToString();
            SteveText.text = info.SteveMoney.ToString();
            EnemiesText.text = stats.enemies.ToString();
            TimeText.text = stats.TimeSpent.ToString();
        }
    }
}
