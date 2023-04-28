using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] sprites;
    [SerializeField]
    private Level_Counter levels;
    void Start()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            SetColorByLevel(sprites[i]);
        }
    }
    private void SetColorByLevel(GameObject spriteToChange)
    {
        Renderer bossRenderer = spriteToChange.GetComponent<Renderer>();
        bossRenderer.material.color = levels.colors[levels.Level];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
