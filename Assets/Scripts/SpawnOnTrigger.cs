using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnTrigger : MonoBehaviour
{
  public GameObject spawn;

  private void OnTriggerEnter(){
    spawn.SetActive(true);
  }

}
