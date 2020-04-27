﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isDead = false;
    public GameObject objective;
    Objectives objectiveScript;
    public GameObject overlay1;
    public GameObject overlay2;
    public GameObject overlay3;
    public GameObject exitDoorTrigger;
    int numberOfCards = 0;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        numberOfCards = 0;
        currentHealth = maxHealth;
        objectiveScript = objective.GetComponent<Objectives>();
        Invoke("escapeNotify", 5f);
        
    }

    public void Restart()
    {
        GameManager.Instance.isPaused = false;
        GameManager.Instance.playerDead = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //FindObjectOfType<SoundManager>().Stop("Music");
        FindObjectOfType<SoundManager>().Play("Music");

    }

    public void BackToMenu()
    {
        GameManager.Instance.isPaused = false;
        GameManager.Instance.playerDead = false;
        Time.timeScale = 1;
        //FindObjectOfType<SoundManager>().Stop("Music");
        SceneManager.LoadScene(0);
    }

    void escapeNotify()
    {
        objectiveScript.popNotification("Find the exit");
        objectiveScript.setObjective("Find the exit");
    }

    public void collectedKey()
    {
        objectiveScript.popNotification("Key Collected");
        numberOfCards++;

        if (numberOfCards == 3)
        {
            //unlock door
            exitDoorTrigger.GetComponent<SpawnOffTriggerExit>().unlock();
            objectiveScript.objectiveCompleted(objectiveScript.getLastChild());
            StartCoroutine("escape");
        }

    }

    IEnumerator escape()
    {
        yield return new WaitForSeconds(8.7f);
        objectiveScript.setObjective("Escape!");
        objectiveScript.popNotification("Escape!");


    }

   


    void Update()
    {

      
      

        if (currentHealth <= maxHealth)
        {
            //Determines how fast the health regen is.
            currentHealth += Time.deltaTime * 5f;
        }


        // Determines each damage overlay.
        if (currentHealth >= 100)
        {
            overlay1.SetActive(false);
            overlay2.SetActive(false);
            overlay3.SetActive(false);
        }

        if (currentHealth < 75)
        {
            overlay1.SetActive(true);
            overlay2.SetActive(false);
            overlay3.SetActive(false);

        }

        if (currentHealth < 50)
        {
            overlay1.SetActive(false);
            overlay2.SetActive(true);
            overlay3.SetActive(false);

        }

        if (currentHealth < 25)
        {
            overlay1.SetActive(false);
            overlay2.SetActive(false);
            overlay3.SetActive(true);

        }



    }

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Dead();
        }

    }

    void Dead()
    {
        Debug.Log("Player Dead.");
        isDead = true;
    }
}
