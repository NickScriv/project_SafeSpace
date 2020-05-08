using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    RaycastHit hit;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
           
            Debug.DrawLine(transform.position, hit.point, Color.cyan);
            if(hit.collider.gameObject.tag == "stair")
            {
                Debug.Log("ssssssssssssssss");
               // GetComponent<Rigidbody>().AddForce(-transform.up * 10);
            }
        }
    }
}
