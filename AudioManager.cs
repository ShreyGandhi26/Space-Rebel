using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    //private static Dictionary<Sound, float> soundTimerDictionary;
    private bool canPlay;
    float currentTime;

    // public static void intialize()
    // {
    //     soundTimerDictionary = new Dictionary<Sound, float>();
    //     //soundTimerDictionary[]
    // }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.Looping;
            s.source.spatialBlend = s.spatialBlend;
        }
    }

    private void Start()
    {
        playSound("Theme");
    }

    public void playSound(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.Play();
    }
    public void StopSound(String name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.Stop();
    }
}
