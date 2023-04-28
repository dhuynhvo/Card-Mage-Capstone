using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_Increments", menuName = "ScriptableObjects/Level_Counter", order = 6)]
public class Level_Counter : ScriptableObject
{
    public int Level = 1;
    public Color[] colors = new Color[5];

    // Example colors
    private Color color1 = Color.red;
    private Color color2 = Color.blue;
    private Color color3 = Color.green;
    private Color color4 = Color.yellow;
    private Color color5 = Color.magenta;

    private void Awake()
    {
        // Assigning example colors to the array
        colors[0] = color1;
        colors[1] = color2;
        colors[2] = color3;
        colors[3] = color4;
        colors[4] = color5;
    }
}