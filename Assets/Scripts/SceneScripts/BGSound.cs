using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Worked on by Abida
// used online sources for basis
        // keeps volume outside of scene
public class BGSound : MonoBehaviour
{

    private static BGSound playing = null;
    public static BGSound KeepPlaying
    {
        get { return playing; }
    }

    void Awake()
    {
        if (playing != null && playing != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            playing = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}