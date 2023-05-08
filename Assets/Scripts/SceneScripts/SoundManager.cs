// Worked on by Abida Mim

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Based on previously existing code

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider; // serializedfield private variable

    // Start is called before the first frame update
    void Start()
    {
            // if volume already exists
        if (!PlayerPrefs.HasKey("musicVol"))
        {
            PlayerPrefs.SetFloat("musicVol", 1);
        }
        else
        {
            Load();
        }
    }
    
        // saves volume changes
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

        // gets existing volume
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVol");
    }

        // saves volume across multiple scenes
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVol", volumeSlider.value);
    }
}
