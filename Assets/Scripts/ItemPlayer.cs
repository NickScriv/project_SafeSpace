using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlayer : MonoBehaviour
{
    public bool pickedUp = false;
    public bool isSelected = true;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (pickedUp == true & isSelected == false)
        {
            anim.SetBool("Drawn", false);
        }
        else
        {
            anim.SetBool("Drawn", true);
        }
    }
}
