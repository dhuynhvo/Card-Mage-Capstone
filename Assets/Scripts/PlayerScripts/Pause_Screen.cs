using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Screen : MonoBehaviour
{
    [SerializeField]
    string PauseButton;
    [SerializeField]
    GameObject PauseScreen;
    bool paused = false;

    void Update()
    {
        if(Input.GetButtonDown(PauseButton))
        {
            if(!paused)
            {
                PauseScreen.SetActive(true);
                Time.timeScale = 0f;
                paused = !paused;
            }

            else if(paused)
            {
                PauseScreen.SetActive(false);
                Time.timeScale = 1f;
                paused = !paused;
            }
        }

    }

    public void ResumeGame()
    {
        if(paused)
        {
            PauseScreen.SetActive(false);
            Time.timeScale = 1f;
            paused = !paused;
        }
    }
}
