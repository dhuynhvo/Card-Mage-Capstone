using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creativity_Sprites : MonoBehaviour
{
    public GameObject[] Sprites;

    public void SpriteOn(float counter)
    {
        switch (counter)
        {
            case 0:
                Sprites[0].SetActive(false);
                Sprites[1].SetActive(false);
                Sprites[2].SetActive(false);
                Sprites[3].SetActive(false);
                Sprites[4].SetActive(false);
                break;

            case 1:
                Sprites[0].SetActive(true);
                Sprites[1].SetActive(false);
                Sprites[2].SetActive(false);
                Sprites[3].SetActive(false);
                Sprites[4].SetActive(false);
                break;

            case 2:
                Sprites[0].SetActive(true);
                Sprites[1].SetActive(true);
                Sprites[2].SetActive(false);
                Sprites[3].SetActive(false);
                Sprites[4].SetActive(false);
                break;

            case 3:
                Sprites[0].SetActive(true);
                Sprites[1].SetActive(true);
                Sprites[2].SetActive(true);
                Sprites[3].SetActive(false);
                Sprites[4].SetActive(false);
                break;

            case 4:
                Sprites[0].SetActive(true);
                Sprites[1].SetActive(true);
                Sprites[2].SetActive(true);
                Sprites[3].SetActive(true);
                Sprites[4].SetActive(false);
                break;

            case 5:
                Sprites[0].SetActive(true);
                Sprites[1].SetActive(true);
                Sprites[2].SetActive(true);
                Sprites[3].SetActive(true);
                Sprites[4].SetActive(true);
                break;
        }
    }

}
