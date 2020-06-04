using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public Transform[] locations;
    

    void Start()
    {
        if(locations.Length > 0)
        {
            Transform location = locations[Random.Range(0, locations.Length)];
            this.transform.position = location.position;
            this.transform.rotation = location.rotation;

      
        }

    }

}
