using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairForce : MonoBehaviour
{
   private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            
            other.gameObject.GetComponent<Rigidbody>().AddForce(0, -200f, 0);
        }
    }
}
