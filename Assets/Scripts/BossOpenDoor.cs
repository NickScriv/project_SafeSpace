using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOpenDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<EnemyAI>().sight();
        }
        else if (other.gameObject.name == "door1" || other.gameObject.CompareTag( "door"))
        {

            GameObject door = other.gameObject;
            GameObject hinge = door.transform.parent.gameObject;


            if(!hinge.GetComponent<OpenableDoor>().open)
            {
               
                hinge.GetComponent<OpenableDoor>().openDoor();
            }
          
        }
        
    }

  
}
