using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaregunAnims : MonoBehaviour
{
    public Animator anim;
    public GameObject flareGun;
    public bool hasBullet = true;
    public GameObject crossHair;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
       //crossHair.SetActive(false);

    }

    private void OnEnable()
    {
       
        crossHair.SetActive(true);
    }

    private void OnDisable()
    {
        if(crossHair != null)
         crossHair.SetActive(false);
    }
    // Update is called once per frame

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
