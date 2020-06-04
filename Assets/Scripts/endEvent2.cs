using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endEvent2 : MonoBehaviour
{
    public GameObject hinge;
    public GameObject wall;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            wall.SetActive(true);
            if(hinge.GetComponent<OpenableDoor>().open)
            {
                hinge.GetComponent<OpenableDoor>().openDoor(true);
            }
          
            hinge.GetComponent<BoxCollider>().enabled = false;


        }
        
    }
}
