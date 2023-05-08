// Worked on by Abida Mim

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    // allows an auto switch to new page after plot page timeline
public class PlotPage : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("InstPage", LoadSceneMode.Single);
    }
}
