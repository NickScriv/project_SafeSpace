using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class ShedEventTrigger2 : MonoBehaviour
{
    public Transform camPos1;
    public Transform camPos2;
    Transform camPos;
    Transform playerTrans;
    bool startRotate = false;
    public Transform mutantSpawn1;
    public Transform mutantSpawn2;
    public GameObject manequin;
    public GameObject mutant1;
    public GameObject mutant2;
    public Camera mainCamera;
    GameObject player;
    CapsuleCollider capsule;
    float colliderHeight;



    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag( "Player"))
        {
            mainCamera.GetComponent<AnalogGlitch>().enabled = true;
            
            FindObjectOfType<SoundManager>().Play("glitch");
            Invoke("StopGlitch", .8f);
            player = other.gameObject;
            capsule = player.GetComponent<CapsuleCollider>();
            colliderHeight = player.GetComponent<FirstPersonAIO>()._crouchModifiers.colliderHeight;
            playerTrans = player.transform;
            manequin.SetActive(false);
            stopCrouching();
            //Do Jump scare
            player.GetComponent<FirstPersonAIO>().enableCameraMovement = false;
            player.GetComponent<FirstPersonAIO>().playerCanMove = false;
            player.GetComponent<Rigidbody>().isKinematic = true;
            Vector3 dir = (camPos1.position - player.transform.position).normalized;
            float dot = Vector3.Dot(dir, player.transform.forward);
            if(dot < 0)
            {
                //camPos1
                mutant1.SetActive(true);
                mutant1.transform.position = mutantSpawn1.position;
                mutant1.transform.rotation = mutantSpawn1.rotation;
                mutant1.transform.LookAt(new Vector3(player.transform.position.x, mutant1.transform.position.y, player.transform.position.z));
 
                camPos = camPos1;
                startRotate = true;
               
            }
            else
            {
                //camPos2
                mutant2.SetActive(true);
                mutant2.transform.position = mutantSpawn2.position;
                mutant2.transform.rotation = mutantSpawn2.rotation;
                mutant2.transform.LookAt(new Vector3(player.transform.position.x, mutant2.transform.position.y, player.transform.position.z));
              
                camPos = camPos2;
                startRotate = true;
            }
            //Invoke("stopRotate", 6f * Time.deltaTime);
       
        }
    }

    void stopRotate()
    {
        startRotate = false;

    }

    void stopCrouching()
    {
        capsule.height = colliderHeight;
        //capsule.height = Mathf.MoveTowards(capsule.height, colliderHeight, 5f * Time.deltaTime);

    }



    private void Update()
    {

        if(startRotate)
        {
            
            Quaternion lookOnLook = Quaternion.LookRotation(camPos.transform.position - playerTrans.position);
            mainCamera.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, lookOnLook, 5f * Time.deltaTime);
        }
        
    }

    void StopGlitch()
    {
        mainCamera.GetComponent<AnalogGlitch>().enabled = false;
    }
}
