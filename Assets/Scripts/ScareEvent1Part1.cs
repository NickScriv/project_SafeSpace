using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareEvent1Part1 : MonoBehaviour
{
    public AudioSource source;
    public AudioClip [] clip;
    public GameObject spawnWall;
    public GameObject monster;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            int i =  Random.Range(0, clip.Length);
            source.PlayOneShot(clip[i]);
            spawnWall.SetActive(true);
            monster.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
