using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutentEvent4 : MonoBehaviour
{
    public GameObject player;
    public GameObject scareScreen;
    public GameObject terrain;
    public GameObject shed;
    public GameObject DirLight;
    public Transform playerSpawnBack;
    public GameObject hingeToSecondArea;
    public GameObject closeTrigger;
    public GameObject shedTrigger2;
    public Camera mainCamera;
    public GameObject battery;




    public void EndEvent4()
    {
        if(hingeToSecondArea.GetComponent<OpenableDoor>().open)
        {
            hingeToSecondArea.GetComponent<OpenableDoor>().openDoor(true);
        }

        hingeToSecondArea.GetComponent<BoxCollider>().enabled = false;
        scareScreen.SetActive(true);
        DirLight.SetActive(false);
        FindObjectOfType<SoundManager>().StopAllAudio();
        //gameObject.SetActive(false);
        shedTrigger2.SetActive(false);
        FindObjectOfType<SoundManager>().Play("ScareEvent4");
        battery.SetActive(true);
    
        player.GetComponent<FirstPersonAIO>().playerCanMove = true;
        player.GetComponent<FirstPersonAIO>().enableCameraMovement = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<FirstPersonAIO>().originalRotation = playerSpawnBack.localRotation.eulerAngles;
        player.GetComponent<FirstPersonAIO>().followAngles = Vector3.zero;
        player.GetComponent<FirstPersonAIO>().targetAngles = playerSpawnBack.localRotation.eulerAngles;
        player.transform.position = playerSpawnBack.position;
        player.transform.rotation = playerSpawnBack.rotation;
        mainCamera.transform.rotation = playerSpawnBack.rotation;



        Invoke("fadeOut", 1f);
        RenderSettings.skybox = null;

    }

    public void fadeOut()
    {
        scareScreen.GetComponent<Animator>().SetTrigger("FadeOut");
        Invoke("endScare", 3f);
    }

    public void endScare()
    {
        scareScreen.SetActive(false);


       
        FindObjectOfType<SoundManager>().Play("Music");
        shed.SetActive(false);
        terrain.SetActive(false);
        closeTrigger.SetActive(true);


    }
}
