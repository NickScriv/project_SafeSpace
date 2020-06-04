//Make an empty GameObject and call it "Door"
//Drag and drop your Door model into Scene and rename it to "Body"
//Make sure that the "Door" Object is at the side of the "Body" object (The place where a Door Hinge should be)
//Move the "Body" Object inside "Door"
//Add a Collider (preferably SphereCollider) to "Door" object and make it much bigger then the "Body" model, mark it as Trigger
//Assign this script to a "Door" Object (the one with a Trigger Collider)
//Make sure the main Character is tagged "Player"
//Upon walking into trigger area press "F" to open / close the door

using UnityEngine;

public class UnlockableDoor : MonoBehaviour
{

    // Smoothly open a door
    public float doorOpenAngle = 90.0f; //Set either positive or negative number to open the door inwards or outwards
    public float openSpeed = 2.0f; //Increasing this value will make the door open faster
    public AudioSource source;
    public AudioClip clip;

    bool open = false;
    bool enter = false;

    float defaultRotationAngle;
    float currentRotationAngle;
    float openTime = 0;

    void Start()
    {
        defaultRotationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;
    }

    // Main function
    void Update()
    {
        if(openTime < 1)
        {
            openTime += Time.deltaTime * openSpeed;
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? doorOpenAngle : 0), openTime), transform.localEulerAngles.z);

        if (Input.GetKeyDown(KeyCode.F) && enter && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
        {
            open = !open;
            currentRotationAngle = transform.localEulerAngles.y;
            openTime = 0;
	    source.PlayOneShot(clip);
        }
    }

    // Display a simple info message when player is inside the trigger area
    void OnGUI()
    {
        if (enter && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
        {
            Rect label = new Rect((Screen.width - 210) / 2, Screen.height - 100, 210, 50);
            GUI.Label(label, "Press 'F' to interact", GameManager.Instance.style);
    
        }
    }

    // Activate the Main function when Player enter the trigger area
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key1"))
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
