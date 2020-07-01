//Make an empty GameObject and call it "Door"
//Drag and drop your Door model into Scene and rename it to "Body"
//Make sure that the "Door" Object is at the side of the "Body" object (The place where a Door Hinge should be)
//Move the "Body" Object inside "Door"
//Add a Collider (preferably SphereCollider) to "Door" object and make it much bigger then the "Body" model, mark it as Trigger
//Assign this script to a "Door" Object (the one with a Trigger Collider)
//Make sure the main Character is tagged "Player"
//Upon walking into trigger area press "F" to open / close the door

using TMPro;
using UnityEngine;

public class OpenableVent : MonoBehaviour
{

    // Smoothly open a door
    public float doorOpenAngle = 90.0f; //Set either positive or negative number to open the door inwards or outwards
    public float openSpeed = 2.0f; //Increasing this value will make the door open faster
     AudioSource source;
    public AudioClip clip;

    bool open = false;
    bool enter = false;

    float defaultRotationAngle;
    float currentRotationAngle;
    float openTime = 0;
    Vector3 closedSize;
    public Vector3 openedSize;
    Vector3 closedCenter;
    public Vector3 openedCenter;
    BoxCollider coll;
    private TextMeshProUGUI interact;
    bool playerUnder = false;
    FirstPersonAIO firstPersonScript;

    void Start()
    {
        playerUnder = false;
        interact = GameObject.Find("GameUI").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        coll = GetComponent<BoxCollider>();
        defaultRotationAngle = transform.localEulerAngles.x;
        currentRotationAngle = transform.localEulerAngles.x;
        closedSize = coll.size;
        closedCenter = coll.center;
        source = GetComponent<AudioSource>();
        firstPersonScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonAIO>();
    }

    // Main function
    void Update()
    {
        if (GameManager.Instance.playerDead)
        {
            this.enabled = false;
        }

        if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeed;
        }
        transform.localEulerAngles = new Vector3(Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? doorOpenAngle : 0), openTime), transform.localEulerAngles.y, transform.localEulerAngles.z);

        if (Input.GetKeyDown(KeyCode.F) && enter && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
        {
            open = !open;
           
            currentRotationAngle = transform.localEulerAngles.x;
            openTime = 0;
            source.PlayOneShot(clip);

            if (open)
            {
                interact.SetText("Press 'F' to close vent");
                coll.size = openedSize;
                coll.center = openedCenter;
            }
            else
            {
                interact.SetText("Press 'F' to open vent");
                coll.size = closedSize;
                coll.center = closedCenter;
            }
        }

       
    }

    // Display a simple info message when player is inside the trigger area
   /* void OnGUI()
    {
        if (enter && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
        {
            if (open)
            {
                Rect label = new Rect((Screen.width - 210) / 2, Screen.height - 100, 210, 50);
                GUI.Label(label, "Press 'F' to close vent", GameManager.Instance.style);
            }
            else
            {
                Rect label = new Rect((Screen.width - 210) / 2, Screen.height - 100, 210, 50);
                GUI.Label(label, "Press 'F' to open vent", GameManager.Instance.style);
            }


        }
    }*/

    // Activate the Main function when Player enter the trigger area
    void OnTriggerEnter(Collider other)
    {
        if (!this.enabled)
            return;

        if (other.CompareTag("Player"))
        {
            if (open)
            {
                interact.SetText("Press 'F' to close vent");

            }
            else
            {
                interact.SetText("Press 'F' to open vent");
            }
            enter = true;
        }
    }

    // Deactivate the Main function when Player exit the trigger area
    void OnTriggerExit(Collider other)
    {
        if (!this.enabled)
            return;

        if (other.CompareTag("Player"))
        {
            interact.SetText("");
            enter = false;
        }
    }

    private void OnDisable()
    {
        interact.SetText("");
    }
}
