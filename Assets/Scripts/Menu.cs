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

    private void Awake()
    {
        credits.gameObject.SetActive(true);
        credits.gameObject.SetActive(false);
    }

    void Start()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.nextScene = 2;
        }
       
        cameraAnim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        Time.fixedDeltaTime = 1/80f;
    }

    public void play()
    {
        FindObjectOfType<SoundManager>().Play("ButtonClick");
        SceneManager.LoadScene(1);
       
    }

    public void Instructions()
    {
        FindObjectOfType<SoundManager>().Play("ButtonClick");
        main.gameObject.SetActive(false);
        cameraAnim.SetTrigger("Instructions");
        

    }

    public void ActivateInstructions()
    {
        instructions.gameObject.SetActive(true);

    }

    public void backToMenuFromInstructions()
    {
        FindObjectOfType<SoundManager>().Play("ButtonClick");
        instructions.gameObject.SetActive(false);
        cameraAnim.SetTrigger("InstructToMain");
       

    }

    public void ActivateMain()
    {
        main.gameObject.SetActive(true);
    }

    public void Credits()
    {
        FindObjectOfType<SoundManager>().Play("ButtonClick");
        main.gameObject.SetActive(false);
        cameraAnim.SetTrigger("Credits");
    

    }

    public void ActivateCredits()
    {
        credits.gameObject.SetActive(true);
    }

    public void backToMenuFromCredits()
    {
        FindObjectOfType<SoundManager>().Play("ButtonClick");
        credits.gameObject.SetActive(false);
        cameraAnim.SetTrigger("CreditsToMain");
      

    }


    public void exit()
    {
        FindObjectOfType<SoundManager>().Play("ButtonClick");
        Application.Quit();
    }

 
}
