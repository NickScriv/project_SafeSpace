using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    private bool stoppedMenuMusic = false;

    public static SoundManager instance;


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("Invalid Audio name");
            return;
        }
        s.audioSource.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Invalid Audio name");
            return;
        }
        s.audioSource.Stop();
    }


    private void Start()
    {
        stoppedMenuMusic = false;
        
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
        foreach(Sound clip in sounds)
        {
            clip.audioSource = gameObject.AddComponent<AudioSource>();
            clip.audioSource.clip = clip.audioClip;
            clip.audioSource.volume = clip.volume;
            clip.audioSource.pitch = clip.pitch;
            clip.audioSource.loop = clip.loop;
        }

       
    }

 


}
