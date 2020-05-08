using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOpenDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "door1" || other.gameObject.tag == "door")
        {

            GameObject door = other.gameObject;
            GameObject hinge = door.transform.parent.gameObject;


            if(!hinge.GetComponent<OpenableDoor>().open)
            {
                //Debug.Log("Open Door");
                hinge.GetComponent<OpenableDoor>().openDoor();
            }
          
        }
    }
}
