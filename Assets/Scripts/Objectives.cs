using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectives : MonoBehaviour
{
    public GameObject itemPrefab;
    void Start()
    {
        for(int i = 0; i < 2; i++)
        {
            GameObject item = Instantiate<GameObject>(itemPrefab);
            item.transform.GetChild(0).GetComponent<Text>().text = "Escape!";
           // item.GetComponent<Text>().text = "Escape!";
            item.transform.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
