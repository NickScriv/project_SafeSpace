using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    GameObject player;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.playerDead)
        {
            Debug.Log("wowo");
            if(isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void Resume()
    {
        //player.GetComponent<FirstPersonAIO>().lockAndHideCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        GameManager.Instance.isPaused = false;
    }

    public void  QuitGame()
    {
        GameManager.Instance.isPaused = false;
        GameManager.Instance.playerDead = false;
        Time.timeScale = 1f;
        FindObjectOfType<SoundManager>().Stop("Music");
        SceneManager.LoadScene(0);
        Debug.Log("quit game");
    }

    void PauseGame()
    {
        //player.GetComponent<FirstPersonAIO>().lockAndHideCursor = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        GameManager.Instance.isPaused = true;
    }
}
