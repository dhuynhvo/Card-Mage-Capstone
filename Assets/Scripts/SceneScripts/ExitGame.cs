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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            //UnityEditor.EditorApplication.isPlaying = false; // closes playing screen
            Debug.Log("Game exited with esc press");
        }
    }
       
        // exits game with button press
    public void Exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false; // closes playig screen
        Debug.Log("Game has exited");
    }
}
