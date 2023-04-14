using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsScreens : MonoBehaviour
{
    [SerializeField]
    public GameObject Screen1;
    [SerializeField]
    public GameObject Screen2;
    [SerializeField]
    public GameObject Screen3;



    public void ChangeScreens1to2()
    {
        Screen1.SetActive(false);
        Screen2.SetActive(true);
    }

    public void ChangeScreens2to3()
    {
        Screen2.SetActive(false);
        Screen3.SetActive(true);
    }
}
