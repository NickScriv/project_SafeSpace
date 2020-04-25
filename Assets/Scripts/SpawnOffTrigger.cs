using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOffTrigger : MonoBehaviour
{
    public GameObject spawn;

    private void OnTriggerEnter()
    {
        spawn.SetActive(false);
    }

}
