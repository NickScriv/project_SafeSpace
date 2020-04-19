using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public Canvas main;
    public Canvas instructions;
    public Canvas credits;
    Animator cameraAnim;
    AudioSource audio;
    public AudioClip press;

    void Start()
    {
        cameraAnim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    public void play()
    {
        SceneManager.LoadScene(1);
        audio.PlayOneShot(press);
    }

    public void Instructions()
    {

        main.gameObject.SetActive(false);
        cameraAnim.SetTrigger("Instructions");
        audio.PlayOneShot(press);

    }

    public void ActivateInstructions()
    {
        instructions.gameObject.SetActive(true);

    }

    public void backToMenuFromInstructions()
    {
        instructions.gameObject.SetActive(false);
        cameraAnim.SetTrigger("InstructToMain");
        audio.PlayOneShot(press);

    }

    public void ActivateMain()
    {
        main.gameObject.SetActive(true);
    }

    public void Credits()
    {
        main.gameObject.SetActive(false);
        cameraAnim.SetTrigger("Credits");
        audio.PlayOneShot(press);

    }

    public void ActivateCredits()
    {
        credits.gameObject.SetActive(true);
    }

    public void backToMenuFromCredits()
    {
        credits.gameObject.SetActive(false);
        cameraAnim.SetTrigger("CreditsToMain");
        audio.PlayOneShot(press);

    }


    public void exit()
    {
        audio.PlayOneShot(press);
        Application.Quit();
    }

    void Update()
    {
        
    }
}
