
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairPlayer : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           /* Vector3 slopeForce = Vector3.ProjectOnPlane(collision.transform.forward, Vector3.up);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-(Vector3.up * 10f));*/
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
               



            }

        }
    }




}