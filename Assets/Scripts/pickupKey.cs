//Make an empty GameObject and call it "Door"
//Drag and drop your Door model into Scene and rename it to "Body"
//Make sure that the "Door" Object is at the side of the "Body" object (The place where a Door Hinge should be)
//Move the "Body" Object inside "Door"
//Add a Collider (preferably SphereCollider) to "Door" object and make it much bigger then the "Body" model, mark it as Trigger
//Assign this script to a "Door" Object (the one with a Trigger Collider)
//Make sure the main Character is tagged "Player"
//Upon walking into trigger area press "F" to open / close the door

using UnityEngine;

public class pickupKey : MonoBehaviour
{

    // pick up keycard

    int numberOfCards = 0;
    public GameObject item;
    public GameObject itemPlayer;
    public PlayerInfo playerInfo;

    bool enter = false;


    // Main function
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.F) && enter && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
      {
        
        itemPlayer.GetComponent<ItemPlayer>().pickedUp = true;
        item.SetActive(false);
        numberOfCards++;
        playerInfo.collectedKey();
        //FindObjectOfType<SoundManager>().Play("PickUpKey");
        FindObjectOfType<SoundManager>().Play("PickUp");

        }
    }

    // Display a simple info message when player is inside the trigger area
    void OnGUI()
    {
        if (enter && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 150, 30), "Press 'F' to pick up");
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
