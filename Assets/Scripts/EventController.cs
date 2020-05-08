using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [Header("Event 1")]
    public GameObject EventTrigger1;
    public GameObject WallRun;

    [Header("Event 2 and 3")]
    public GameObject exitWall;
    public GameObject WallToAreaInCell;
    public GameObject hingeToCell;



    // [Header("Event 4")]





    // Start is called before the first frame update
    void Start()
    {
        

        switch(GameManager.Instance.eventNumber)
        {
            case 1:
                EventTrigger1.SetActive(true);
                WallRun.SetActive(true);
                exitWall.SetActive(false);
                WallToAreaInCell.SetActive(true);
                hingeToCell.GetComponent<OpenableDoor>().enabled = false;
                break;

            case 2:
                EventTrigger1.SetActive(false);
                WallRun.SetActive(false);
                exitWall.SetActive(true);
                WallToAreaInCell.SetActive(false);
                hingeToCell.GetComponent<OpenableDoor>().enabled = true;

                break;

            case 3:
                EventTrigger1.SetActive(false);
                WallRun.SetActive(false);
                exitWall.SetActive(true);
                WallToAreaInCell.SetActive(false);
                hingeToCell.GetComponent<OpenableDoor>().enabled = true;

                break;

            default:
                EventTrigger1.SetActive(false);
                WallRun.SetActive(false);
                exitWall.SetActive(false);
                WallToAreaInCell.SetActive(true);
                hingeToCell.GetComponent<OpenableDoor>().enabled = false;


                break;

        }
    }
}
