using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureGamma : MonoBehaviour
{
   
    public ContrastBrightnessGammaCorrection gammaScript;
    public GameObject gammaCanvas;
    public GameObject menuCanvas;

   

  

    public void adjustGamma(float val)
    {
        gammaScript.changeGamma(val);
    }

 

}
