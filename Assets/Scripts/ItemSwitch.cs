using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwitch : MonoBehaviour
{
    public int currentItem = 0;
    public int maxItems = 2;
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        SelectItem(0);
    }

    // Update is called once per frame
    void Update()
    {

        //Updating item choosen with scroll wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentItem + 1 <= maxItems)
                currentItem++;
            else
                currentItem = 0;

            SelectItem(currentItem);
        }
        /*else if(Input.GetAxis("Mouse ScrollWheel") < 0)
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentItem = 0;
            SelectItem(currentItem);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentItem = 1;
            SelectItem(currentItem);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentItem = 2;
            SelectItem(currentItem);
        }


    }

    void SelectItem(int index)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            //Check if item is picked up & activate it
            if (i == index && transform.GetChild(i).gameObject.GetComponent<ItemPlayer>().pickedUp == true)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).gameObject.GetComponent<ItemPlayer>().isSelected = true;
            }


            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
                transform.GetChild(i).gameObject.GetComponent<ItemPlayer>().isSelected = false;
            }
        }
    }
}
