using UnityEngine;

[ExecuteInEditMode]
public class ContrastBrightnessGammaCorrection : MonoBehaviour
{

  
    /*[SerializeField, Range(-0.55f, +0.4f)]*/ private float gammaAdjustment;
    Material material;
    public Shader Shader;

    private void OnEnable()
    {
        material = new Material(Shader);
        material.hideFlags = HideFlags.DontSave;
      
    }

    public float getGammaValue()
    {
        return gammaAdjustment;
    }

    public void changeGamma(float gammaValue)
    {
        gammaAdjustment = gammaValue;
    }

    // Called by camera to apply image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
     
        material.SetFloat("_GammaAdjustment", gammaAdjustment);

        Graphics.Blit(source, destination, material);
    }
}