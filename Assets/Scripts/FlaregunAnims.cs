using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaregunAnims : MonoBehaviour
{
    Animator anim;
    public GameObject flareGun;
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
        if (Input.GetMouseButtonDown(0) && hasBullet)
        {
           
        }

        //Reload Animations 
        /*if (Input.GetKeyDown("r"))
        {
            anim.SetTrigger("Reload");
        }*/


    }
    public void shootAnim()
    {
        anim.SetTrigger("Shoot");
    }

    public void endDrawing()
    {
        flareGun.GetComponent<flaregun>().endDrawing();
    }

    public void endFiring()
    {
        flareGun.GetComponent<flaregun>().endFiring();
    }

    public void reload()
    {
        anim.SetTrigger("Reload");
    }


}
