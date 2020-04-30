using UnityEngine;
using System.Collections;

public class flareround : MonoBehaviour {
	public GameObject flaregun;
	public GameObject flareRound;
	flaregun flare;


	bool enter = false;

	// Use this for initialization
	void Start () 
	{
		flare =flaregun.GetComponent<flaregun>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.F) && enter && flare.spareRounds < flare.maxSpareRounds && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
		{
            //GetComponent<AudioSource>().PlayOneShot(pickupSound);
           
            flare.spareRounds++;
			Destroy(flareRound);
            FindObjectOfType<SoundManager>().Play("PickUpFlare");
        }

	}

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

