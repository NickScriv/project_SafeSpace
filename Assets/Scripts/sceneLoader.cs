
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    public void loadScene(string sceneName){
        FindObjectOfType<SoundManager>().Play("ButtonClick");
        SceneManager.LoadScene(sceneName);
        FindObjectOfType<SoundManager>().Stop("MenuMusic");
    }

    public void quit(){
        FindObjectOfType<SoundManager>().Play("ButtonClick");
        Application.Quit();
    }
}
