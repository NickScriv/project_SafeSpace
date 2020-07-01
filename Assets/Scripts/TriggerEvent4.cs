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
    public FirstPersonAIO firstPerson;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            exitWall.SetActive(false);
           
            FindObjectOfType<SoundManager>().StopAllAudioFade("forest");
            firstPerson.playerCanMove = false;
            firstPerson.enableCameraMovement = false;
       
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
        player.transform.position = SpawnToLocation.position;
        player.transform.rotation = SpawnToLocation.rotation;
        //cameraMain.transform.rotation = SpawnToLocation.rotation;
        firstPerson.playerCanMove = true;
        firstPerson.enableCameraMovement = true;
        firstPerson.originalRotation = SpawnToLocation.localRotation.eulerAngles;
        firstPerson.followAngles = Vector3.zero;
        firstPerson.targetAngles = SpawnToLocation.localRotation.eulerAngles;

      

        transitionScreen.GetComponent<Animator>().SetTrigger("FadeOut");
        FindObjectOfType<SoundManager>().Play("forest");
        Invoke("inNewArea", 3f);
    }

    void inNewArea()
    {
       

        transitionScreen.SetActive(false);
    }
}
