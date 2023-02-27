// Abida Mim

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
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
        }
    }
}
