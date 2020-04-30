using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{

    public GameObject GameWinPanel;
    public GameObject playerCam;
    GameObject player;
    public GameObject jumpCam;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.playerDead = true;
            GameManager.Instance.isPaused = true;
            GameManager.Instance.isEnd = true;
            GameWinPanel.SetActive(true);
            Invoke("text1", 4f);
            //TODO: Disable music
            FindObjectOfType<SoundManager>().StopFade("Music");
            FindObjectOfType<SoundManager>().StopFade("ChaseMusic");
            player.GetComponent<FirstPersonAIO>().enabled = false;
            Rigidbody PlayerRb = player.GetComponent<Rigidbody>();
            PlayerRb.velocity = Vector3.zero;
            PlayerRb.angularVelocity = Vector3.zero;
            PlayerRb.isKinematic = true;
        }
    }

    public void text1()
    {
        GameWinPanel.GetComponent<Animator>().SetTrigger("Text1");
        Invoke("text2", 4f);
    }

    public void text2()
    {
        GameWinPanel.GetComponent<Animator>().SetTrigger("Text2");
        Invoke("text2FadeOut", 4f);
    }

    public void text2FadeOut()
    {
        GameWinPanel.GetComponent<Animator>().SetTrigger("Text2FadeOut");
        Invoke("jumpscare", 4f);
    }

    public void jumpscare()
    {
        //Playscream
        FindObjectOfType<SoundManager>().Play("JumpScare");
        jumpCam.SetActive(true);
        playerCam.SetActive(false);
        GameWinPanel.GetComponent<Animator>().SetTrigger("JumpScare");
        Invoke("endScare", 3f);

    }

    public void endScare()
    {

        GameManager.Instance.isEnd = false;
        GameManager.Instance.playerDead = false;
        GameManager.Instance.isPaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }

}
