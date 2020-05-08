using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOffTriggerExit : MonoBehaviour
{
  public GameObject spawn;
   public GameObject objective;
   Objectives objectiveScript;
   bool enter = false;
    bool first = true;

    private void Start()
    {
        objectiveScript = objective.GetComponent<Objectives>();
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.name == "Player" && first)
         {
             first = false;
             objectiveScript.objectiveCompleted(0);
             objectiveScript.popNotification("Find all 3 keys");
             objectiveScript.setObjective("Find all 3 keys");
             spawn.SetActive(false);
             enter = true;
         }
     


    }

    public void unlock()
    {
        GetComponentInParent<OpenableDoorExit>().enabled = true;
        this.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            enter = false;
            
        }
    }

    void OnGUI()
    {
        if (enter && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
        {
         
            Rect label = new Rect((Screen.width - 210) / 2, Screen.height - 100, 210, 50);
            GUI.Label(label, "This door requires 3 keys to unlock", GameManager.Instance.style);
        }
      
    }



}
