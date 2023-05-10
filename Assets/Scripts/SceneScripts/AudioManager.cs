using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
//Implemented by Robert Bothne
//https://www.youtube.com/watch?v=dS9my809VJk tutorial

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    void Awake(){
        if (instance != null){
            Destroy(gameObject);
            return;
        } else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void Play(string sound){
        Sound s = Array.Find(sounds, item => item.name == sound);
        s.source.Play();
    }
    public void Stop(string sound){
        Sound s = Array.Find(sounds, item => item.name == sound);
        s.source.Stop();
    }
}