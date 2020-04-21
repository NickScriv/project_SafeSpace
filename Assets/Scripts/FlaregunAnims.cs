using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaregunAnims : MonoBehaviour
{
    Animator anim;
    public bool hasBullet = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //Shooting animations - WIP to make it not animate when gun has no rounds.
        if (Input.GetMouseButtonDown(0) && hasBullet == true)
        {
            anim.SetTrigger("Shoot");
        }

        //Reload Animations 
        if (Input.GetKeyDown("r"))
        {
            anim.SetTrigger("Reload");
        }


    }
}
