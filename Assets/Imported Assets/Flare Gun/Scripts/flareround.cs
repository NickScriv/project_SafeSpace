using UnityEngine;
using System.Collections;
using TMPro;

public class flareround : MonoBehaviour {
	public GameObject flaregun;
	public GameObject flareRound;
	flaregun flare;
    private TextMeshProUGUI interact;


	bool enter = false;

	// Use this for initialization
	void Start () 
	{
        interact = GameObject.Find("GameUI").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		flare = flaregun.GetComponent<flaregun>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.Instance.playerDead)
        {
            this.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && enter && flare.spareRounds < flare.maxSpareRounds && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
		{
            //GetComponent<AudioSource>().PlayOneShot(pickupSound);
           
            flare.spareRounds++;
			Destroy(flareRound);
            FindObjectOfType<SoundManager>().Play("PickUpFlare");
        }

	}

	/*void OnGUI()
	{
		if (enter && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
		{
			GUI.Label(new Rect(Screen.width / 2 - 75f, Screen.height - 100, 150, 30), "Press 'F' to pick up flare", GameManager.Instance.style);
		}
	}*/

	// Activate the Main function when Player enter the trigger area
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
            interact.SetText("Press 'F' to pick up flare");
            enter = true;
		}
	}

    private void OnDisable()
    {
        interact.SetText("");
    }

    // Deactivate the Main function when Player exit the trigger area
    void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
            interact.SetText("");
            enter = false;
		}
	}
}

