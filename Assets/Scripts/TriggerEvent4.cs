using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent4 : MonoBehaviour
{
    public GameObject terrain;
    public GameObject shed;
    public GameObject player;
    public GameObject DirLight;
    public GameObject transitionScreen;
    public Transform SpawnToLocation;
    public GameObject exitWall;
    public Camera cameraMain;
    public Material sky;
    public GameObject battery;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            exitWall.SetActive(false);
           
            FindObjectOfType<SoundManager>().StopAllAudioFade("forest");
            player.GetComponent<Rigidbody>().isKinematic = true;
            player.GetComponent<FirstPersonAIO>().playerCanMove = false;
            player.GetComponent<FirstPersonAIO>().enableCameraMovement = false;
            //Cursor.lockState = CursorLockMode.None;
       
            // player.GetComponent<FirstPersonAIO>().enabled = false;
            terrain.SetActive(true);
            shed.SetActive(true);
            transitionScreen.SetActive(true);
            Invoke("teleportPlayer", 3f);
            RenderSettings.skybox = sky;

        }
    }

    void teleportPlayer()
    {
        
        DirLight.SetActive(true);
        battery.SetActive(false);
        player.GetComponent<FirstPersonAIO>().playerCanMove = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<FirstPersonAIO>().enableCameraMovement = true;
        player.GetComponent<FirstPersonAIO>().originalRotation = SpawnToLocation.localRotation.eulerAngles;
        player.GetComponent<FirstPersonAIO>().followAngles = Vector3.zero;
        player.GetComponent<FirstPersonAIO>().targetAngles = SpawnToLocation.localRotation.eulerAngles;
        player.transform.position = SpawnToLocation.position;
        player.transform.rotation = SpawnToLocation.rotation;
        cameraMain.transform.rotation = SpawnToLocation.rotation;

        transitionScreen.GetComponent<Animator>().SetTrigger("FadeOut");
        FindObjectOfType<SoundManager>().Play("forest");
        Invoke("inNewArea", 3f);
    }

    void inNewArea()
    {
       

        transitionScreen.SetActive(false);
    }
}
