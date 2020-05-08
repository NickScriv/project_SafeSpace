using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
    public GameObject hinge;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hinge.GetComponent<OpenableDoor>().enter = true;
        }
    }

    // Deactivate the Main function when Player exit the trigger area
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hinge.GetComponent<OpenableDoor>().enter = false;
        }
    }
}
