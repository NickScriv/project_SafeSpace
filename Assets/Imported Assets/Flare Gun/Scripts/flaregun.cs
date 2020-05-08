using UnityEngine;
using System.Collections;

public class flaregun : MonoBehaviour {
	
	public Rigidbody flareBullet;
	public Transform barrelEnd;
	public GameObject muzzleParticles;
	public AudioClip flareShotSound;
	public AudioClip noAmmoSound;	
	public AudioClip reloadSound;	
	public int bulletSpeed = 2000;
	public int maxSpareRounds = 5;
	public int spareRounds = 3;
	public int currentRound = 0;
	public GameObject anims;
    public float widthOffset = 1.66f;
    public float heightOffest = 1.41f;
    public bool firing = false;
    public bool reloading = false;

	


	// Use this for initialization
	void Start () 
	{
        widthOffset = 1.66f;
        heightOffest = 1.41f;
        firing = false;
        reloading = false;

    }
	
	// Update is called once per frame
	void Update () 
	{
        if (GameManager.Instance.playerDead || GameManager.Instance.isPaused)
            return;

        if (Input.GetButtonDown("Fire1") && !GetComponent<Animation>().isPlaying && Cursor.lockState == CursorLockMode.Locked && Cursor.visible == false &&  !firing && !reloading)
		{
			if(currentRound > 0)
            {
                firing = true;
                anims.GetComponent<FlaregunAnims>().shootAnim();
            
                Shoot();
			}
            else
            {
				GetComponent<AudioSource>().PlayOneShot(noAmmoSound);
				anims.GetComponent<FlaregunAnims>().hasBullet = false;

			}
		}

		if(Input.GetKeyDown(KeyCode.R) && !GetComponent<Animation>().isPlaying && spareRounds > 0 && !firing && !reloading)
		{
            if (spareRounds >= 1 && currentRound == 0)
            {
                reloading = true;
                anims.GetComponent<FlaregunAnims>().hasBullet = true;
                anims.GetComponent<FlaregunAnims>().reload();
                Invoke("Reload", 0.7f);
              
            }
			
		}
	
	}

    public void endFiring()
    {
        Debug.Log("end firing");
        firing = false;
    }

    public void endDrawing()
    {
        Debug.Log("end drawing");
        reloading = false;
    }


	
	void Shoot()
	{
		currentRound--;
		if(currentRound <= 0){
			currentRound = 0;
		}

        

            GetComponent<AudioSource>().PlayOneShot(flareShotSound);
		
			
			Rigidbody bulletInstance;			
			bulletInstance = Instantiate(flareBullet,barrelEnd.position,barrelEnd.rotation) as Rigidbody; //INSTANTIATING THE FLARE PROJECTILE
			
			
			bulletInstance.AddForce(barrelEnd.forward * bulletSpeed); //ADDING FORWARD FORCE TO THE FLARE PROJECTILE
			
			Instantiate(muzzleParticles, barrelEnd.position,barrelEnd.rotation);	//INSTANTIATING THE GUN'S MUZZLE SPARKS	
           
		
	}
	
	void Reload()
	{
		
		GetComponent<AudioSource>().PlayOneShot(reloadSound);
		
        spareRounds--;
		currentRound++;
		
		
	}

    private void OnGUI()
    {
        if (spareRounds >= 1 && currentRound == 0 && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
        {
            GameManager.Instance.style2.normal.textColor = Color.yellow;
            Rect label = new Rect((Screen.width - 210) / widthOffset, Screen.height / heightOffest, 210, 50);
            GUI.Label(label, "Reload", GameManager.Instance.style2);
        }

        if (spareRounds < 1 && currentRound == 0 && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
        {
            GameManager.Instance.style2.normal.textColor = Color.red;
             Rect label = new Rect((Screen.width - 210) / widthOffset, Screen.height/heightOffest, 210, 50);
             GUI.Label(label, "No Ammo", GameManager.Instance.style2);
        }
    }
}
