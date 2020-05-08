using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject item;
    public GameObject itemPlayer;

    ItemPlayer script;

    bool enter = false;

    void Start()
    {
        script = itemPlayer.GetComponent<ItemPlayer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && enter && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
        {
            item.SetActive(false);
            script.pickedUp = true;
            //source.PlayOneShot(clip);
            FindObjectOfType<SoundManager>().Play("PickUp");
        }
    }

    void OnGUI()
    {
        if (enter && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
        {
            Rect label = new Rect((Screen.width - 210) / 2, Screen.height - 100, 210, 50);
            GUI.Label(label, "Press 'F' to pick up flare gun", GameManager.Instance.style);
      
        }
    }

    // Activate the Main function when Player enter the trigger area
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = true;
        }
    }

    // Deactivate the Main function when Player exit the trigger area
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = false;
        }
    }
}
