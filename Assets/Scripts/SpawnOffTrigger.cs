using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOffTrigger : MonoBehaviour
{
    public GameObject spawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
            spawn.SetActive(false);
    }
    

}
