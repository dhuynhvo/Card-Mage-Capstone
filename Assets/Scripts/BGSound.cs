using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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