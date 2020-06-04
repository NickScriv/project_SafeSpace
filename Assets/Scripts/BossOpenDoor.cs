using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOpenDoor : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<EnemyAI>().sight();
        }
      
        
        if ( other.gameObject.CompareTag( "door"))
        {

            GameObject hinge = other.gameObject;
            

          
            if(!hinge.GetComponent<OpenableDoor>().open)
            {
               
                hinge.GetComponent<OpenableDoor>().openDoor(true);
            }
          
        }
        
    }

  
}
