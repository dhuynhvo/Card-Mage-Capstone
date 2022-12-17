using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Worked on by Abida
// recommended by youtube video to implement

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager
    {
        get;
        private set;
    }

    public UnitHealth playerHealth = new UnitHealth(100, 100);
    // Start is called before the first frame update
    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }


}
