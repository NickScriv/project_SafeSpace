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

  

    public void popNotification(string text)
    {
        FindObjectOfType<SoundManager>().Play("Objective");
        notification.transform.GetChild(0).GetComponent<Text>().text = text;
        notification.SetActive(true);
        Invoke("FadeOut", 6f);

       
        
    }
    public void setObjective(string text)
    {
        GameObject item = Instantiate<GameObject>(itemPrefab);
        item.transform.GetChild(0).GetComponent<Text>().text = "- " + text;
        item.transform.SetParent(transform, false);
    }

    public void objectiveCompleted(int index)
    {
        GameObject Go = transform.GetChild(transform.childCount - 1).gameObject;
        Color temp = Go.transform.GetChild(index).GetComponent<Text>().color;
        temp.a = .35f;
        Go.transform.GetChild(index).GetComponent<Text>().color = temp;
    }

    public int getLastChild()
    {
       GameObject Go = transform.GetChild(transform.childCount - 1).gameObject;
        Debug.Log(Go.transform.childCount);
        return Go.transform.childCount - 1;
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
