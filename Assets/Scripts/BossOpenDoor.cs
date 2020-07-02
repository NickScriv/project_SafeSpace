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
      
        
        if ( other.gameObject.CompareTag( "door") && other.gameObject.name ==  "hinge")
        {
           

            GameObject hinge = other.gameObject;
            

          
            if(hinge != null && !hinge.GetComponent<OpenableDoor>().open)
            {
               
                hinge.GetComponent<OpenableDoor>().openDoor(true);
            }
          
        }
        
    }

  
}
