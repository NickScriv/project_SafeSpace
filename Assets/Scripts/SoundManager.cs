using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    private bool stoppedMenuMusic = false;
    bool keepFadingIn;
    bool keepFadingOut;

    public static SoundManager instance;



    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Invalid Audio name");
            return;
        }

        //if(!s.audioSource.isPlaying)
        s.audioSource.Play();
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Invalid Audio name");
            return;
        }

        //if(!s.audioSource.isPlaying)
        s.audioSource.PlayOneShot(s.audioClip);
    }

    public void PlayFade(string name)
    {
        /*Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Invalid Audio name");
            return;
        }*/


        Play(name);
        //StartCoroutine(FadeIn(s, .01f, s.volume));
    }

    public void StopAllAudio()
    {
        AudioSource [] audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in audioSources)
        {

            audioS.Stop();

        }
    }

    public void StopAllAudioFade(string name = "")
    {
        
        foreach (Sound audioS in sounds)
        {
            if(audioS != null && audioS.audioClip.name != name)
            {
             
                StartCoroutine(FadeOut(audioS,  1.75f));
            }
               

        }
    }

    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Invalid Audio name");
            return false;
        }
        return s.audioSource.isPlaying;

    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Invalid Audio name");
            return;
        }

        if (s.audioSource.isPlaying)
            s.audioSource.Stop();
    }

    public void StopFade(string name)
    {
        
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Invalid Audio name");
            return;
        }

        if (s.audioSource.isPlaying)
            StartCoroutine(FadeOut(s,  1.75f));
        
    }

    public IEnumerator FadeIn(Sound s, float speed, float maxVolume)
    {
  

        s.audioSource.volume = 0;
        float audioVol = s.audioSource.volume;
      
        s.audioSource.Play();
        while (s.audioSource.volume < maxVolume)
        {
           
            audioVol += speed;
            s.audioSource.volume = audioVol;
            yield return new WaitForSeconds(0.1f);
        }

    }

    public IEnumerator FadeOut(Sound s, float speed)
    {

        
        float audioVol = s.audioSource.volume;

        while (s.audioSource.volume > 0.01f)
        {

            audioVol -= speed * Time.deltaTime;
          
            s.audioSource.volume = audioVol;
            
            yield return new WaitForSecondsRealtime(0.01f);
        }
        s.audioSource.volume = 0;
        s.audioSource.Stop();
        s.audioSource.volume = s.volume;

    }

    private void Awake()
    {
        if (instance == null)
        {
         
            PlayerPrefs.DeleteAll();
          

        }
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
        foreach (Sound clip in sounds)
        {
            clip.audioSource = gameObject.AddComponent<AudioSource>();
            clip.audioSource.spatialBlend = 0.0f;
            clip.audioSource.clip = clip.audioClip;
            clip.audioSource.volume = clip.volume;
            clip.audioSource.pitch = clip.pitch;
            clip.audioSource.loop = clip.loop;
        }


    }

}
