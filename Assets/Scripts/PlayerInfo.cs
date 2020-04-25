using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isDead = false;

    public GameObject overlay1;
    public GameObject overlay2;
    public GameObject overlay3;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        //Made for debugging. 

        /*if (Input.GetKeyDown("l"))
        {
            ApplyDamage(25);
        }*/

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
