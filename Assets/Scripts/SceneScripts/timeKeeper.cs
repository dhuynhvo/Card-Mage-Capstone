// Abida Mim

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// creates a singleton that displays the serialized values
public class timeKeeper : MonoBehaviour
{
    [SerializeField]
    private Stats_Info stats;

    [SerializeField]
    private Text TimeSeconds;

    // creates basis
    public static timeKeeper Instance
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
            float dividedMinutes = 0;
            float dividedSeconds = 0;

            Instance = this;

            dividedMinutes = Mathf.Floor(stats.TimeSpent / 60f);
            dividedSeconds = Mathf.Floor(stats.TimeSpent % 60f);

            TimeSeconds.text = string.Format("{0:00}:{1:00}", dividedMinutes, dividedSeconds);

        }
    }
}
