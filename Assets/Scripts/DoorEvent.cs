using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    public GameObject invisibleWall;
    public GameObject monster;
    public GameObject exitWall;
    bool enter = false;
    public GameObject scarePanel;
    public LayerMask scareMask;
    public LayerMask scareMaskTurnCheck;
    public GameObject closeTrigger;
    public GameObject camPos;
    FirstPersonAIO playerScript;
    public AudioClip scareSound;
    AudioSource source;
    public Camera mainCamera;
    private TextMeshProUGUI interact;
    bool scared = false;


    private void Start()
    {
        scared = false;
        source = GetComponent<AudioSource>();
        interact = GameObject.Find("GameUI").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!this.enabled)
            return;

        if(other.gameObject.CompareTag("Player"))
        {
            
            exitWall.SetActive(false);
            if (GameManager.Instance.eventNumber == 2)
            {
                interact.SetText("Won't budge");
                enter = true;
            }
            else if(GameManager.Instance.eventNumber == 3)
            {
                interact.SetText("Still won't budge");
                invisibleWall.SetActive(true);
                RaycastHit hitScare;
                Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if (Physics.SphereCast(ray, 0.4f, out hitScare, 5f, scareMaskTurnCheck))
                {
                 
        
                    Quaternion lookOnLook = Quaternion.LookRotation(camPos.transform.position - other.transform.position);
                    mainCamera.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, lookOnLook, 5f * Time.deltaTime);
                    Invoke("enableMonster", 5f * Time.deltaTime);
                 
                }
                else
                {
                    enableMonster();
                }
                

            }
          
        }
    }

    public void enableMonster()
    {
        monster.SetActive(true);
        enter = true;
       // playerScript.enableCameraMovement = true;
    }

    private void Update()
    {
        if(enter && GameManager.Instance.eventNumber == 3)
        {
            
            RaycastHit hitScare;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (!scared && Physics.SphereCast(ray, 0.37f, out hitScare, 5f, scareMask))
            {


                scared = true;  
                source.PlayOneShot(scareSound);
                enter = false;
                interact.SetText("");
                Invoke("scare", 0.75f);
                
              

            }
         
        }
    }

    void scare()
    {
        scarePanel.SetActive(true);
        invisibleWall.SetActive(false);
        monster.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        Invoke("endScare", 1f);



    }

    void endScare()
    {
        scarePanel.SetActive(false);
        closeTrigger.SetActive(true);
        this.enabled = false;

    }

    private void OnDisable()
    {
        interact.SetText("");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag( "Player"))
        {
            if(GameManager.Instance.eventNumber == 2)
            {
                enter = false;
                closeTrigger.SetActive(true);
                GetComponent<BoxCollider>().enabled = false;
                this.enabled = false;
            }
            interact.SetText("");
           
        }
    }

    /*void OnGUI()
    {
        if (enter && !GameManager.Instance.isPaused && !GameManager.Instance.playerDead)
        {
            if (GameManager.Instance.eventNumber == 2)
            {
                Rect label = new Rect((Screen.width - 210) / 2, Screen.height - 100, 210, 50);
                GUI.Label(label, "Won't budge", GameManager.Instance.style);

            }
            else if(GameManager.Instance.eventNumber == 3)
            {
                Rect label = new Rect((Screen.width - 210) / 2, Screen.height - 100, 210, 50);
                GUI.Label(label, "Still won't budge", GameManager.Instance.style);
            }

        }
    }*/
}
