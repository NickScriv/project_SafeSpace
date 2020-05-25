using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Flashlight_PRO : MonoBehaviour
{
    [Space(10)]
    [SerializeField()] GameObject Lights; // all light effects and spotlight
    [SerializeField()] AudioSource switch_sound; // audio of the switcher
 


    
    private Light spotlight;
    private Material ambient_light_material;
    private Color ambient_mat_color;
    public bool is_enabled = false;
    public Transform shootRay;
    public Text batteryCount;
    public GameObject terrain;
    //public Text batteryLife;
    //Dictionary<string, GameObject> currentHits;

    public float maxEnergy;
    public float currentEnergy;
    public float usedEnergy;
    bool outOfBattery = true;
   

    public int batteries;
    float energy;
    public GameObject batteryImageGO;

    //Index 0 is 0 life
    //Index 20 is full life
    public Sprite[] batteryImages;
    

    // Use this for initialization
    void Start()
    {
        // cache components
        spotlight = Lights.transform.Find("Spotlight").GetComponent<Light>();
        ambient_light_material = Lights.transform.Find("ambient").GetComponent<Renderer>().material;
        ambient_mat_color = ambient_light_material.GetColor("_TintColor");

        currentEnergy = maxEnergy;
        maxEnergy = 50 * batteries;

        batteryCount.text = 0.ToString();
        batteryImageGO.GetComponent<Image>().sprite = batteryImages[0];


    }


    void Update()
    {
        if (GameManager.Instance.isPaused || GameManager.Instance.playerDead)
            return;

        maxEnergy = 50 * batteries;
		currentEnergy = maxEnergy;
       // batteryLife.text = usedEnergy.ToString();


        if (Input.GetKeyDown("e"))
			Switch();

		if (is_enabled && !terrain.activeInHierarchy)
        {

            
            if (currentEnergy <= 0)
            {
              
                outOfBattery = true;
                FindObjectOfType<SoundManager>().Play("LowBattery");
                Lights.SetActive(false);
                is_enabled = !is_enabled;
                batteries = 0;
                batteryCount.text = batteries.ToString();
            }
            else
            {
               
                Lights.SetActive(true);
             
                currentEnergy -= 0.3f * Time.deltaTime;
				usedEnergy += 0.3f * Time.deltaTime;

                energy = 50 - usedEnergy;

                if (energy >= 47.62f)
                {
                    
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[20];
                }
                else if (energy >= 45.24)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[19];
                }
                else if (energy >= 42.86)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[18];
                }
                else if (energy >= 40.48)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[17];
                }
                else if (energy >= 38.10)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[16];
                }
                else if (energy >= 35.72)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[15];
                }
                else if (energy >= 33.34)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[14];
                }
                else if (energy >= 30.96)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[13];
                }
                else if (energy >= 28.58)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[12];
                }
                else if (energy >= 26.2)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[11];
                }
                else if (energy >= 23.82)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[10];
                }
                else if (energy >= 21.44)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[9];
                }
                else if (energy >= 19.06)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[8];
                }
                else if (energy >= 16.68)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[7];
                }
                else if (energy >= 14.3)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[6];
                }
                else if (energy >= 11.92)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[5];
                }
                else if (energy >= 9.54)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[4];
                }
                else if (energy >= 7.16)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[3];
                }
                else if (energy >= 4.78)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[2];
                }
                else if (energy >= 2.4)
                {
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[1];
                }
                else
                {
                   
                    batteryImageGO.GetComponent<Image>().sprite = batteryImages[0];
                }


            }



            //Change_Intensivity((50 - usedEnergy));

            if (usedEnergy >= 50)
            {
                batteries -= 1;
                batteryCount.text = batteries.ToString();

                usedEnergy = 0;


            }





        }

    }

    public void PickUpBattery()
    {
        outOfBattery = false;
        if (currentEnergy <= 0)
        {
            batteryImageGO.GetComponent<Image>().sprite = batteryImages[20];
        }
        batteries++;
    }


	/// <summary>
	/// changes the intensivity of lights from 0 to 100.
	/// call this from other scripts.
	/// </summary>
	public void Change_Intensivity(float percentage)
	{
		percentage = Mathf.Clamp (percentage, 0, 100);


		spotlight.intensity = (8 * percentage) / 100;

		ambient_light_material.SetColor ("_TintColor", new Color(ambient_mat_color.r , ambient_mat_color.g , ambient_mat_color.b , percentage/2000));
	}


	/// <summary>
	/// switch current state  ON / OFF.
	/// call this from other scripts.
	/// </summary>
	public void Switch()
	{
        if(!outOfBattery)
        {
            is_enabled = !is_enabled;

            Lights.SetActive(is_enabled);

        }
		

		if (switch_sound != null)
			switch_sound.Play ();
	}

	/// <summary>
	/// enables the particles.
	/// </summary>
	/*public void Enable_Particles(bool value)
	{
		if(dust_particles != null)
		{
			if(value)
			{
				dust_particles.gameObject.SetActive(true);
				dust_particles.Play();
			}
			else
			{
				dust_particles.Stop();
				dust_particles.gameObject.SetActive(false);
			}
		}
	}*/



}
