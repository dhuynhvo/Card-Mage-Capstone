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
    private Text TimeText;

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
            Instance = this;

            TimeText.text = stats.TimeSpent.ToString("#.##");

        }
    }
}
