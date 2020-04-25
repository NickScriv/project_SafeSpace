using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int health = 100;
    public bool isDead = false;
    public GameObject objective;
    Objectives objectiveScript;

    private void Start()
    {
        objectiveScript = objective.GetComponent<Objectives>();
        Invoke("escapeNotify", 5f);
        
    }

    void escapeNotify()
    {
        objectiveScript.popNotification("Find the exit");
        objectiveScript.setObjective("Find the exit");
    }

    public void collectedKey(bool lastKey)
    {
        objectiveScript.popNotification("Key Collected");


        if (lastKey)
        {
            objectiveScript.objectiveCompleted(1);
            StartCoroutine("escape");
        }

    }

    IEnumerator escape()
    {
        yield return new WaitForSeconds(7f);
        objectiveScript.setObjective("Escape!");
        objectiveScript.popNotification("Escape!");


    }

    void Update()
    {
      
       /* if (Input.GetKeyDown(KeyCode.B))
        {
            //ApplyDamage(30);
            objectiveScript.collectedKey(2);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //ApplyDamage(30);
            objectiveScript.collectedKey(0);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            //ApplyDamage(30);
            objectiveScript.collectedKey(1);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            //ApplyDamage(30);
            objectiveScript.collectedKey(3);
        }*/
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
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
