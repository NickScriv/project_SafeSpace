using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatteryPickup : MonoBehaviour
{

    public GameObject battery;
    public GameObject flashlight;
    private TextMeshProUGUI interact;

    bool enter = false;

    private void Start()
    {
        interact = GameObject.Find("GameUI").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (GameManager.Instance.playerDead)
        {
            this.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && enter && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
        {
            Destroy(battery);
            flashlight.GetComponent<Flashlight_PRO>().PickUpBattery();
            flashlight.GetComponent<Flashlight_PRO>().batteryCount.text = flashlight.GetComponent<Flashlight_PRO>().batteries.ToString();
            FindObjectOfType<SoundManager>().Play("PickUp");
        }
    }

    private void OnDisable()
    {
        interact.SetText("");
    }

    /* void OnGUI()
     {
         if (enter && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
         {
             Rect label = new Rect((Screen.width - 210) / 2, Screen.height - 100, 210, 50);
             GUI.Label(label, "Press 'F' to pick up battery", GameManager.Instance.style);

         }
     }*/

    // Activate the Main function when Player enter the trigger area
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = true;
            interact.SetText("Press 'F' to pick up battery");
        }
    }

    // Deactivate the Main function when Player exit the trigger area
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = false;
            interact.SetText("");
        }
    }
}
