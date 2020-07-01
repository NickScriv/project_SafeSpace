using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    public Image loadingBar;
    public RawImage background;
    public Texture[] backgroundImages;
    public Texture bugTexture;
    public Texture mutantTexture;
    public GUIStyle styleTest;
    public Transform spawnFlare;
    public Rigidbody flareBullet;
  
  
    public GameObject muzzleParticles;


    AsyncOperation gameLevel;
    // Start is called before the first frame update



    void Start()
    {
        flareBullet = Instantiate(flareBullet, spawnFlare.position, spawnFlare.rotation) as Rigidbody;
        Instantiate(muzzleParticles, spawnFlare.position, spawnFlare.rotation);
     

        GUIStyle styleTest = new GUIStyle();
   

        styleTest.normal.textColor = Color.white;
        styleTest.alignment = TextAnchor.MiddleCenter;

     
       

        if (GameManager.Instance == null)
        {

           
            
            background.texture = backgroundImages[Random.Range(0, backgroundImages.Length)];

        }
        else if (GameManager.Instance.killedBy == "mutant")
        {
            background.texture = mutantTexture;
        }
        else if (GameManager.Instance.killedBy == "bug")
        {
            background.texture = bugTexture;
        }
        else
        {
            background.texture = backgroundImages[Random.Range(0, backgroundImages.Length)];
        }
       
        Invoke("startLoading", 2f);
       
        
    }

    void startLoading()
    {
        
        StartCoroutine(LoadSyncOperation());
    }

    IEnumerator LoadSyncOperation()
    {
        if(GameManager.Instance == null)
        {
             gameLevel = SceneManager.LoadSceneAsync(3);
        }
        else
        {
             gameLevel = SceneManager.LoadSceneAsync(GameManager.Instance.nextScene);
        }
           

        while (gameLevel.progress < 1)
        {
            //loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, gameLevel.progress, 1f * Time.deltaTime);
            loadingBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
        
    }

    private void OnGUI()
    {
        Rect label = new Rect((Screen.width + 1000) / 2, Screen.height + 1000, 210, 50);
        GUI.Label(label, "Test", styleTest);
    }

}
