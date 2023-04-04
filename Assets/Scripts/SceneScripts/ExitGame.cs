using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Worked on by Grant and Abida

public class ExitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

           // exits game when user presses "esc"
           // commented out line causes error on other's pcs
       
        // exits game with button press
    public void Exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false; // closes playig screen
        Debug.Log("Game has exited");
    }
}
