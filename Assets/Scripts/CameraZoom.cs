using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    public Camera PlayerCamera;
    public int normal = 60;
    public int zoom = 50;
    FirstPersonAIO firstPerson;
    public float smooth = 5f;

    
    void Start()
    {
        firstPerson = GetComponent<FirstPersonAIO>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1) && firstPerson.playerCanMove)
        {
            PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, zoom, Time.deltaTime * smooth);
        }
        else
        {
            PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, normal, Time.deltaTime * smooth);
        }

     
    }
}
