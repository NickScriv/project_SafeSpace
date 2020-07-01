using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFramRate : MonoBehaviour
{
   
    public int target = 10;
   void Awake()
    {
        QualitySettings.vSyncCount = 1;  
        Application.targetFrameRate = 1000;
      
    
    }
    
 /*   private void Update()
    {
        if(target != Application.targetFrameRate)
        {
            Application.targetFrameRate = target;
        }
    }*/
}
