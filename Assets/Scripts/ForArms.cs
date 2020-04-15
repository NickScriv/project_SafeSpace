using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForArms : MonoBehaviour
{
    Animator animator;

    public GameObject player;
    FirstPersonAIO script;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        script = player.GetComponent<FirstPersonAIO>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", script.speed);

    }
}
