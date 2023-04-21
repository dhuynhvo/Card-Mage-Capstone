using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlotPage : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("InstPage", LoadSceneMode.Single);
    }
}
