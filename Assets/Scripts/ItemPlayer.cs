using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlayer : MonoBehaviour
{
    public bool pickedUp = false;
    
    //Possible way to create a system to lock scroll for items you don't have. 
    public bool isLocked = true;

    void Update()
    {
        if (pickedUp == true)
        {
            isLocked = false;
        }
    }
}
