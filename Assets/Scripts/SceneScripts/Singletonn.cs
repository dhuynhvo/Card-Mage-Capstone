// Abida Mim

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    // creates a singleton that displays the serialized values
public class Singletonn : MonoBehaviour
{
        // connects to the Scriptable Objects
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
    
        // creates basis
    public static Singletonn Instance
    {
        get;
        private set;
    }

    // checks if instance already exists, and deletes if it does
    private void Update()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

            // otherwise sets serialized field values to be printed
        else
        {
            Instance = this;

            MoneyText.text = info.money.ToString();
            SteveText.text = info.SteveMoney.ToString();

            EnemiesText.text = stats.enemies.ToString();
            TimeText.text = stats.TimeSpent.ToString("#.## seconds");

        }
    }
}
