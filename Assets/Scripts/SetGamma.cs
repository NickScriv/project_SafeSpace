using UnityEngine;


public class SetGamma : MonoBehaviour
{

    public RenderTexture myRenderTexture;
    public Camera mainCam;
    [SerializeField, Range(-0.55f, +0.4f)]
    private float gammaAdjustment;
    Material material;
    public Shader Shader;
   int width;
    int height;
 

    private void Start()
    {

        material = new Material(Shader);
        material.hideFlags = HideFlags.DontSave;
        material.enableInstancing = true;
    
        material.SetFloat("_GammaAdjustment", PlayerPrefs.GetFloat("GammaValue"));
        width = Screen.width;
        height = Screen.height;
        
      
   

    }





    // Called by camera to apply image effect
     void OnRenderImage(RenderTexture source, RenderTexture destination)
      {
       
        Graphics.Blit(source, destination, material);
      }

}