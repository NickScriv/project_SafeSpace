using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool playerDead;
    public bool isPaused;
    public bool isEnd = false;
     GameObject panel;
    GameObject panel2;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        isPaused = false;
        playerDead = false;
        isEnd = false;
        //panel = GameObject.FindGameObjectWithTag("GameUI").transform.GetChild(0).gameObject;
        //panel2 = GameObject.FindGameObjectWithTag("GameUI").transform.GetChild(1).gameObject;
        DontDestroyOnLoad(gameObject);
    }

    public void fadeIn()
    {
        FindObjectOfType<SoundManager>().Stop("Music");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if(panel == null)
        {
            GameObject Go = GameObject.Find("GameUI").gameObject;
            panel = FindObject(Go, "Died");
           
        }
        panel.SetActive(true);
    }

     GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    public void fadeInBug()
    {
        FindObjectOfType<SoundManager>().Stop("Music");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (panel2 == null)
        {
            GameObject Go = GameObject.Find("GameUI").gameObject;
            panel2 = FindObject(Go, "BugDied");

        }
        panel2.SetActive(true);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isPaused || playerDead);
    }
}
