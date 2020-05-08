using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairForce : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("FFFOOORCCCEEEE");
            other.gameObject.GetComponent<Rigidbody>().AddForce(0, -50000, 0);
        }
    }
}
