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
    private Level_Counter levels;

    [SerializeField]
    private Text MoneyText;
    [SerializeField]
    private Text SteveText;
    [SerializeField]
    private Text TimeSeconds;
    [SerializeField]
    private Text EnemiesText;
    [SerializeField]
    private Text LevelsText;

    // creates basis
    public static Singletonn Instance
    {
        get;
        private set;
    }

    public void Start()
    {
        levels.Level = 1;
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

            LevelsText.text = levels.Level.ToString();

            MoneyText.text = info.money.ToString();
            //SteveText.text = info.SteveMoney.ToString();

            EnemiesText.text = stats.enemies.ToString();

            float dividedMinutes = 0;
            float dividedSeconds = 0;

            dividedMinutes = Mathf.Floor(stats.TimeSpent / 60f);
            dividedSeconds = Mathf.Floor(stats.TimeSpent % 60f);

            TimeSeconds.text = string.Format("{0:00}:{1:00}", dividedMinutes, dividedSeconds);

        }
    }
}
