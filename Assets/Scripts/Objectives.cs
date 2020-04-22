using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectives : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject notification;
    public Animator anim;
    void Start()
    {
       // anim = GetComponent<Animator>();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  collectedKey(int index)
    {
        if (index != 0)
        { 
            GameObject Go = transform.GetChild(transform.childCount - 1).gameObject;
            Color temp = Go.transform.GetChild(0).GetComponent<Text>().color;
            temp.a = .35f;
            Go.transform.GetChild(0).GetComponent<Text>().color = temp;
        }

        if (index == 1)
        {
            popNotification("Find the second key");
        }
        else if (index == 2)
        {
            popNotification("Find the third key");
        }
        else if (index == 0)
        {
            popNotification("Find the first key");
            return;
        }


        

      


    }

    public void popNotification(string text)
    {
        FindObjectOfType<SoundManager>().Play("Objective");
        notification.transform.GetChild(0).GetComponent<Text>().text = text;
        notification.SetActive(true);

        GameObject item = Instantiate<GameObject>(itemPrefab);
        item.transform.GetChild(0).GetComponent<Text>().text = "- " + text;
        item.transform.SetParent(transform, false);
        Invoke("FadeOut", 6f);
    }

    public void FadeOut()
    {

        anim.SetTrigger("FadeOut");
        Invoke("diabaleNotification", 1.5f);

    }

    public void diabaleNotification()
    {
        Debug.Log("WEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
        notification.SetActive(false);

    }
}
