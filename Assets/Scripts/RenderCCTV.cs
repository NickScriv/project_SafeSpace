using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCCTV : MonoBehaviour
{
    bool trig = false;
    public Camera[] _securityCameras;
    int i = 0;
    void Start()
    {
        foreach (var s in _securityCameras)
        {
            s.enabled = false;
        }
        
    }

    private void Update()
    {
        if(trig)
        {
           
            if (i >= _securityCameras.Length)
            {
                i = 0;
                return;
            }
            _securityCameras[i].Render();
            i++;
        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            trig = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            trig = false;
        }
    }



}
