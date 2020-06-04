using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwitch : MonoBehaviour
{
    public int currentItem = 0;
    public int maxItems = 2;
    Animator animator;
    public GameObject flashlight;
    public GameObject flareArms;
    public GameObject flareGun;

    // Start is called before the first frame update
    void Awake()
    {
        //Auto starts the character with the flashlight & the animation of him pulling it out
        transform.GetChild(0).gameObject.GetComponent<ItemPlayer>().pickedUp = true;
        SelectItem(0);
    }

 
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPaused || GameManager.Instance.playerDead)
            return;

        //Updating item choosen with scroll wheel
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && !flareArms.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Glock18_Hide") && !flareArms.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Glock18_Draw") && !flareArms.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Glock18_Fire") && !flareGun.GetComponent<flaregun>().reloading && !flareGun.GetComponent<flaregun>().firing)
        {
            if (currentItem + 1 <= maxItems)
                currentItem++;
            else
                currentItem = 0;
        
            SelectItem(currentItem);
        }

         //Commented out due to there being a glitch - Won't show flare arm for some reason.
        /*else if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentItem - 1 <= maxItems)
                currentItem--;
            else
                currentItem = maxItems;

            SelectItem(currentItem);
        }*/


        //Barrier to not go above or under
        if (currentItem == maxItems + 1)
            currentItem = 0;

        if (currentItem == -1)
            currentItem = maxItems;


        //Updating item with alpha numbers (Not numpad)
        if (Input.GetKeyDown(KeyCode.Alpha1) && !flareArms.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Glock18_Hide") && !flareArms.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Glock18_Draw") && !flareArms.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Glock18_Fire") && !flareGun.GetComponent<flaregun>().reloading && !flareGun.GetComponent<flaregun>().firing)
        {
            if (currentItem + 1 <= maxItems)
                currentItem++;
            else
                currentItem = 0;
       
        
            currentItem = 0;
            SelectItem(currentItem);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !flareArms.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Glock18_Hide") && !flareArms.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Glock18_Draw") && !flareArms.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Glock18_Fire") && !flareGun.GetComponent<flaregun>().reloading && !flareGun.GetComponent<flaregun>().firing)
            {
                if (currentItem + 1 <= maxItems)
                    currentItem++;
                else
                    currentItem = 0;
       
        
            currentItem = 1;
            SelectItem(currentItem);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && !flareArms.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Glock18_Hide") && !flareArms.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Glock18_Draw") && !flareArms.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Glock18_Fire") && !flareGun.GetComponent<flaregun>().reloading && !flareGun.GetComponent<flaregun>().firing)
        {
                    if (currentItem + 1 <= maxItems)
                        currentItem++;
                    else
                        currentItem = 0;
       
        
            currentItem = 2;
            SelectItem(currentItem);
        }


    }

    public void SelectItem(int index)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            //Check if item is picked up & activate it
            if (i == index && transform.GetChild(i).gameObject.GetComponent<ItemPlayer>().pickedUp == true)
            {
               
                transform.GetChild(i).gameObject.SetActive(true);
               // transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                if (transform.GetChild(i).gameObject.name == "FlashlightArms" && flashlight.GetComponent<Flashlight_PRO>().is_enabled)
                {
                    flashlight.GetComponent<Flashlight_PRO>().Switch();

                }
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
