using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s;
        int number;
        switch (name)
        {
            case "footsteps_ground":
                number = UnityEngine.Random.Range(0, 5);
                s = sounds[number];
                s.source.Play();
                break;
            case "footsteps_rocks":
                number = UnityEngine.Random.Range(5, 9);
                s = sounds[number];
                s.source.Play();
                break;
            case "jump_quotes":
                number = UnityEngine.Random.Range(11, 13);
                s = sounds[number];
                s.source.Play();
                break;
            case "jump_quotes_orange":
                number = UnityEngine.Random.Range(13, 15);
                s = sounds[number];
                s.source.Play();
                break;
            default:
                s = Array.Find(sounds, sound => sound.name == name);
                if (s == null)
                    return;
                s.source.Play();
                break;
        }
    }

    public void Stop(string name)
    {
        Sound s;
        s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

}
