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
        if (other.gameObject.tag == "Player" && first)
        {
            first = false;
            objectiveScript.objectiveCompleted(0);
            objectiveScript.popNotification("Find all 3 keys");
            objectiveScript.setObjective("Find all 3 keys");
            spawn.SetActive(false);
            enter = true;
        }
        else if(other.gameObject.tag == "Player")
        {
            enter = true;
        }
        

    }

    public void unlock()//TODO: call this when player collects all keys
    {
        GetComponent<OpenableDoor>().enabled = true;
        this.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = false;
            
        }
    }

    void OnGUI()
    {
        if (enter)
        {
            
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 150, 50), "This door requires 3 keys to unlock.");
        }
      
    }



}
