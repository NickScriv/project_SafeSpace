using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour {
public AudioSource source;
public AudioClip clip;

void OnTriggerEnter(Collider coll){
 if(coll.CompareTag("Player"))
        {
          
	 source.PlayOneShot(clip);
	 GetComponent<Collider>().enabled = false;
 }
}
}
