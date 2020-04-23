using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class Flashlight_PRO : MonoBehaviour
{
	[Space(10)]
	[SerializeField()] GameObject Lights; // all light effects and spotlight
	[SerializeField()] AudioSource switch_sound; // audio of the switcher
	[SerializeField()] ParticleSystem dust_particles; // dust particles



	private Light spotlight;
	private Material ambient_light_material;
	private Color ambient_mat_color;
	public bool is_enabled = false;
    public Transform shootRay;
    //Dictionary<string, GameObject> currentHits;

   


    void Update()
    {
	if(Input.GetKeyDown("e"))
	Switch();

       /* RaycastHit[] hits =  Physics.SphereCastAll(shootRay.position, 5, shootRay.right,  6f);
        foreach(RaycastHit hit in hits)
        {
            if(hit.transform.gameObject.tag == "Bug" && is_enabled)
            {
                if(currentHits.ContainsKey(hit.transform.gameObject.name))
                {

                }
                hit.transform.gameObject.GetComponent<SmallAI>().setState("runAway");
                Debug.Log("I hit the bug!");
            }
        }*/

	}






	// Use this for initialization
	void Start ()
	{
        //Dictionary<string, GameObject> currentHits = new Dictionary<string, GameObject>();
        // cache components
        spotlight = Lights.transform.Find ("Spotlight").GetComponent<Light> ();
		ambient_light_material = Lights.transform.Find ("ambient").GetComponent<Renderer> ().material;
		ambient_mat_color = ambient_light_material.GetColor ("_TintColor");
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Bug" && is_enabled == true)
        {
            Debug.Log("hit Bug!");
            other.gameObject.GetComponent<SmallAI>().setState("runAway");
            other.gameObject.GetComponent<SmallAI>().inFlashlightZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bug")
        {
            other.gameObject.GetComponent<SmallAI>().inFlashlightZone = false;
        }
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
		is_enabled = !is_enabled;

		Lights.SetActive (is_enabled);

		if (switch_sound != null)
			switch_sound.Play ();
	}





	/// <summary>
	/// enables the particles.
	/// </summary>
	public void Enable_Particles(bool value)
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
	}



}
