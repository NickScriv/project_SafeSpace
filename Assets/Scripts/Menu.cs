using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public Canvas main;
    public Canvas instructions;
    public Canvas credits;
    public Canvas gammaCorrectionPanel;
    Animator cameraAnim;
    public SetGamma gammaScript;
    public ContrastBrightnessGammaCorrection gammaCorrectionScript;



    private void Awake()
    {
        
        credits.gameObject.SetActive(true);
        credits.gameObject.SetActive(false);
     


    }

    void Start()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.nextScene = 3;
        }

       

       cameraAnim = GetComponent<Animator>();

        Time.fixedDeltaTime = 0.02f;
        main.gameObject.SetActive(true);

         if(PlayerPrefs.HasKey("GammaValue"))
          {

              gammaCorrectionPanel.gameObject.SetActive(false);
              gammaCorrectionScript.enabled = false;
              gammaScript.enabled = true;
          }
          else
          {
              gammaCorrectionPanel.gameObject.SetActive(true);
              main.gameObject.SetActive(false);
              gammaCorrectionScript.enabled = true;
              gammaScript.enabled = false;

          }

        if (!gammaCorrectionPanel.gameObject.activeInHierarchy)
        {
            FindObjectOfType<SoundManager>().Play("MenuMusic");
        }
    }

    public void OnGammeConfirm()
    {
        FindObjectOfType<SoundManager>().Play("ButtonClick");
        PlayerPrefs.SetFloat("GammaValue", gammaCorrectionScript.getGammaValue());
        main.gameObject.SetActive(true);
        gammaCorrectionPanel.gameObject.SetActive(false);
        gammaCorrectionScript.enabled = false;
        gammaScript.enabled = true;
        FindObjectOfType<SoundManager>().Play("MenuMusic");
    }

    public void play()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.killedBy = "nothing";
            Debug.Log(GameManager.Instance.killedBy);
        }
      
        FindObjectOfType<SoundManager>().Play("ButtonClick");
        FindObjectOfType<SoundManager>().Stop("MenuMusic");
        SceneManager.LoadScene(2);
       
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
