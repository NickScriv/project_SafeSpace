using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    public string scene;
    void OnTriggerEnter(Collider coll){
 if(coll.tag=="Player"){
	 
	 SceneManager.LoadScene(scene);
 }
}
    public void quit(){
      Application.Quit();
    }
}
