using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DectectCrouch : MonoBehaviour
{
    public FirstPersonAIO playerScript;
    public OpenableVent ventScript;

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player") && ventScript.open)
        {
            playerScript.above = true;
            //Debug.Log(other.gameObject.name);

        }
    }


  
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript.above = false;

        }
    }

    private void OnDisable()
    {
        playerScript.above = false;
    }
}
