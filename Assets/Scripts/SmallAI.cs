﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SmallAI : MonoBehaviour
{
    public AudioClip[] screams;
    public AudioClip hit;
    NavMeshAgent agent;
    Transform player;
    Animator anim;
    public AudioClip[] footSounds;
    AudioSource sound;
    string state = "idle";
    public Transform vision;
    float waitSearch = 0f;
    float chaseTime = 0f;
    bool highAlert = false;
    float searchRadius = 20f;
    public GameObject deathcam;
    public GameObject flashlight;
    //public GameObject FlareGun;
    Transform FlareBullet;
    public Camera mainCamera;
   int Health = 1000;
    Rigidbody BugRb;
    Rigidbody PlayerRb;
    public Transform camPos;
    int multiplier;
    public float range;
    float countdown = 0f;
    public Text stateText;
    public bool inFlashlightZone = false;


    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sound = GetComponent<AudioSource>();
        BugRb = GetComponent<Rigidbody>();
        PlayerRb = player.GetComponent<Rigidbody>();
        agent.speed = 1.2f;
        agent.updateRotation = true;
        multiplier = 1;
        //range = 10;

    }

 


   void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {

            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

   /* void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {

            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }*/
    
    // Update is called once per frame
    void Update()
    {
        /* if(GetComponent<Rigidbody>().velocity.magnitude <= 0)
         {
             agent.updateRotation = false;

         }
         else
         {
             agent.updateRotation = true;

         }*/


        Debug.Log(Health);
        stateText.text = state;
        Debug.DrawLine(vision.position, player.transform.position, Color.green);
        anim.SetFloat("velocity", agent.velocity.magnitude);

        if (state == "idle")
        {
            Vector3 randomPos = Random.insideUnitSphere * searchRadius;
            NavMeshHit navHit;
            NavMesh.SamplePosition(transform.position + randomPos, out navHit, 20f, NavMesh.AllAreas);

            if (highAlert)
            {
                NavMesh.SamplePosition(player.transform.position + randomPos, out navHit, 20f, NavMesh.AllAreas);
                searchRadius += 2.5f;

                if (searchRadius > 20f)
                {
                    highAlert = false;
                    agent.speed = 1.2f;
                }
            }
            agent.SetDestination(navHit.position);
            state = "walk";
        }

        if (state == "walk")
        {
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                state = "search";
                waitSearch = 4f;
            }


        }

        if (state == "search")//How long he stops for and looks around
        {
            RaycastHit hit;
            if (Physics.Raycast(vision.position, vision.forward, out hit, 3.5f))
            {
                if (hit.collider.gameObject.tag == "Barrier")
                {
                    //Debug.Log("SOmewhere else");
                    state = "idle";
                }
            }

            if (waitSearch > 0f)
            {
                waitSearch -= Time.deltaTime;
            }
            else
            {
                state = "idle";
            }
        }

        //TODO: Implement a shout state
        if (state == "shout")
        {
            if (state != "runAway" && state != "runAway2")
            {
                agent.ResetPath();
                anim.SetTrigger("scream");
                playScream(Random.Range(0, 4));
                state = "shouting";
             }


        }

        if (state == "chase")
        {
            agent.speed = 3f;
            chaseTime -= Time.deltaTime;
            agent.destination = player.transform.position;
            float distance = Vector3.Distance(player.transform.position, transform.position);
            
            if (distance > 10f || chaseTime <= 0)
            {
                state = "hunt";
            }

            else if (distance <= 1f)
            {
                RaycastHit hit;
                if (Physics.Linecast(vision.position, player.transform.position, out hit))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        //TODO: incorporate the Player health function
                        if (Health == 0)
                        {
                            agent.isStopped = true;
                            agent.ResetPath();
                            GetComponent<Rigidbody>().freezeRotation = true;
                            BugRb.velocity = Vector3.zero;
                            BugRb.angularVelocity = Vector3.zero;
                            transform.LookAt(player.transform.position);
                            state = "kill";
                            player.GetComponent<FirstPersonAIO>().enabled = false;
                            PlayerRb.velocity = Vector3.zero;
                            PlayerRb.angularVelocity = Vector3.zero;
                            //deathcam.SetActive(true);
                            //deathcam.transform.position = Camera.main.transform.position;
                            //deathcam.transform.rotation = Camera.main.transform.rotation;
                            //Camera.main.gameObject.SetActive(false);
                            anim.SetTrigger("AttackPlayer");
                            anim.speed = 0.8f;
                        }
                        else if(Health != 0)
                        {
                            agent.isStopped = true;
                            agent.ResetPath();
                            //Debug.Log(Health);
                            if (state != "runAway" && state != "runAway2")
                            {
                                    state = "Attacking";
                                
                            }
                            
                        }
                    }

                }
            }

           /* if (flashlight.GetComponent<Flashlight_PRO>().is_enabled == true)
            {
                state = "runAway";
            }*/

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.tag == "FlareBullet" && hitColliders[i].gameObject != null)
                {
                    FlareBullet= hitColliders[i].gameObject.transform;
                    state = "runAway2";
                    break;
                }
                i++;
            }
            sight();


        }
        if (state == "Attacking")
        {
            if (Health <= 0)
            {

                agent.isStopped = true;
                agent.ResetPath();
                GetComponent<Rigidbody>().freezeRotation = true;
                BugRb.velocity = Vector3.zero;
                BugRb.angularVelocity = Vector3.zero;
                transform.LookAt(player.transform.position);
                state = "kill";
                player.GetComponent<FirstPersonAIO>().enabled = false;
                PlayerRb.velocity = Vector3.zero;
                PlayerRb.angularVelocity = Vector3.zero;
                //deathcam.SetActive(true);
                //deathcam.transform.position = Camera.main.transform.position;
                //deathcam.transform.rotation = Camera.main.transform.rotation;
                //Camera.main.gameObject.SetActive(false);
                anim.SetTrigger("AttackPlayer");
                anim.speed = 0.8f;
            }

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.tag == "FlareBullet" && hitColliders[i].gameObject != null)
                {
                    FlareBullet = hitColliders[i].gameObject.transform;
                    state = "runAway2";
                    break;
                }
                i++;
            }

            agent.isStopped = true;
            agent.ResetPath();
            RotateTowards(player);
            countdown -= Time.deltaTime;
            //Debug.Log(countdown);
            if (countdown <= 0)
            {
                countdown = 2f;
                anim.SetTrigger("AttackPlayer");
            }
            if (Vector3.Distance(player.transform.position, transform.position) > .8f)
            {
                state = "chase";
                countdown = 0f;
                //Invoke("changeState",2);
            }
        }

        if (state == "runAway")
        {
            Vector3 runTo = transform.position + ((transform.position - player.position) * multiplier);
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < range)
            {
                if (flashlight.GetComponent<Flashlight_PRO>().is_enabled != true || !inFlashlightZone)
                {
                    
                        state = "chase";
                    
                }
                agent.SetDestination(runTo);
            }

            if (distance > range) state = "search";
            
        }

        if (state == "runAway2")
        {
            if (FlareBullet != null)
            {
                Vector3 runTo = transform.position + ((transform.position - FlareBullet.position) * multiplier);
                float distance = Vector3.Distance(transform.position, FlareBullet.position);
                if (distance < range)
                {

                    //if (FlareBullet != true)
                   // {
                       // state = "chase";
                   // }
                    agent.SetDestination(runTo);
                }

                if (distance > range) state = "search";
            }
            else if (FlareBullet == null)
            {
                state = "search";
            }

        }

        if (state == "hunt")
        {
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)//How long he stops for and looks around
            {
                state = "search";
                waitSearch = 5f;
                highAlert = true;
                searchRadius = 5f;
                sight();
            }
        }

        if (state == "kill")
        {

            //deathcam.transform.position = Vector3.Slerp(deathcam.transform.position, camPos.position, 20f * Time.deltaTime);
            //deathcam.transform.rotation = Quaternion.Slerp(deathcam.transform.rotation, camPos.rotation, 20f * Time.deltaTime);
            Quaternion lookOnLook = Quaternion.LookRotation(camPos.transform.position - player.transform.position);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, lookOnLook, 5f * Time.deltaTime);
            Invoke("reset", 2f);
            
        }




    }

    void reset()//TODO: change this to checkpoint in final build!
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void sight()
    {

        RaycastHit hit;

        if (Physics.Linecast(vision.position, player.transform.position, out hit))
        {
            //Debug.Log("Hit " + hit.collider.gameObject.name);

            if (hit.collider.gameObject.tag == "Player")
            {
                

                if (state == "search" || state == "walk")
                {
                    if (state != "runAway" && state != "runAway2")
                    {
                        state = "shout";
                    }

                }
            }
        }
    }

    public void footsteps()
    {
       // sound.clip = footSounds[Random.Range(0, 4)];
        sound.PlayOneShot(footSounds[Random.Range(0, 4)]);
    }

    public void endShout()
    {
        chaseTime = 15f;
        if (state != "runAway" && state != "runAway2")
        {
            state = "chase";
        }
        //state = "chase";
    }

    public void playScream(int num)
    {
        sound.clip = screams[num];
        sound.PlayOneShot(sound.clip);

        //sound.Play();
    }

    public void playHit()
    {

        sound.PlayOneShot(hit);

        //sound.Play();
    }

    public void dealDamage()
    {
        Debug.Log(Health);
        Health = Health - 100;
    }

    void changeState()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > .8f)
        {
            state = "chase";
            countdown = 0f;
        }
    }

    public void setState(string newState)
    {
        state = newState;
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }



}