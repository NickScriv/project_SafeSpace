using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RespawnMenu : MonoBehaviour
{
    GameObject player;
    GameObject arms;
    GameObject respawn;

    FirstPersonAIO script;
    PlayerInfo script2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        arms = GameObject.FindGameObjectWithTag("Arms");
        script = player.GetComponent<FirstPersonAIO>();
        script2 = player.GetComponent<PlayerInfo>();


    }


    // Update is called once per frame
    void Update()
    {
        if (script2.isDead == true)
            Restart();
    }

    void Restart()
    {
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
