using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour {
public AudioSource source;
public AudioClip clip;

void OnTriggerEnter(Collider coll){
 if(coll.tag=="Player"){
            Debug.Log("Play");
	 source.PlayOneShot(clip);
	 GetComponent<Collider>().enabled = false;
 }
}
}
