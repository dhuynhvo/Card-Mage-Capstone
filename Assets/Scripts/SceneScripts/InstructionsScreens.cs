// Worked on by Abida
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // ui elements are toggled on and off in order to progress
    // the pages of instructions
public class InstructionsScreens : MonoBehaviour
{
    [SerializeField]
    public GameObject Screen1;
    [SerializeField]
    public GameObject Screen2;
    [SerializeField]
    public GameObject Screen3;
    [SerializeField]
    public GameObject Screen4;
    [SerializeField]
    public GameObject Screen5;
    [SerializeField]
    public GameObject Screen6;
    [SerializeField]
    public GameObject Screen7;

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

    public void ChangeScreens3to4()
    {
        Screen3.SetActive(false);
        Screen4.SetActive(true);
    }

    public void ChangeScreens4to5()
    {
        Screen4.SetActive(false);
        Screen5.SetActive(true);
    }

    public void ChangeScreens5to6()
    {
        Screen5.SetActive(false);
        Screen6.SetActive(true);
    }

    public void ChangeScreens6to7()
    {
        Screen6.SetActive(false);
        Screen7.SetActive(true);
    }
}
