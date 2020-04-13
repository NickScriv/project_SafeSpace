using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform dest;

    public Vector3 PickPosition;
    public Vector3 PickRotation;

    //WIP atm
    void OnPickup()
    {
        gameObject.SetActive(false);
    }
}
