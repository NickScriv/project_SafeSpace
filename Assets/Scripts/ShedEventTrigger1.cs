using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShedEventTrigger1 : MonoBehaviour
{
    public GameObject manequin;
    public GameObject trigger2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag( "Player"))
        {
            manequin.GetComponent<TeleportMannaquin>().enabled = true;
            trigger2.SetActive(true);
            gameObject.SetActive(false);
            
        }




    }
}
