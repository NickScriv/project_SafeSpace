using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SmallAI : MonoBehaviour
{
    public AudioClip[] screams;
    NavMeshAgent agent;
    Transform player;
    Animator anim;
    public AudioClip footSounds;
    AudioSource sound;
    string state = "idle";
    public Transform vision;
    float waitSearch = 0f;
    float chaseTime = 0f;
    bool highAlert = false;
    float searchRadius = 20f;
    public GameObject deathcam;
    public Camera mainCamera;
    public int Health;
    Rigidbody BugRb;
    Rigidbody PlayerRb;
    public Transform camPos;
    //TODO: Change tags of walls to "Barrier"

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


    }

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

        Debug.Log(searchRadius);
        Debug.Log(state);
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
                    Debug.Log("SOmewhere else");
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
            agent.ResetPath();
            anim.SetTrigger("scream");
            playScream(Random.Range(0, 4));
            state = "shouting";


        }

        if (state == "chase")
        {
            agent.speed = 3f;
            chaseTime -= Time.deltaTime;
            agent.destination = player.transform.position;
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance > 25f || chaseTime <= 0)
            {
                state = "hunt";
            }

            else if (distance <= 1.7f)
            {
                RaycastHit hit;
                if (Physics.Linecast(vision.position, player.transform.position, out hit))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        //TO DO: incorporate the Player health function
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
                            Health = Health - 1;
                            Debug.Log(Health);
                            if (Health != 0)
                            {
                                state = "chase";
                            }
                        }
                    }
                }
            }
            sight();


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
            
        }




    }

    private void LateUpdate()
    {
        /*if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }*/
    }

    void reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //TODO: Change this to transition to game over scene
    }

    public void sight()
    {
        RaycastHit hit;

        if (Physics.Linecast(vision.position, player.transform.position, out hit))
        {
            //Debug.Log("Hit " + hit.collider.gameObject.name);

            if (hit.collider.gameObject.tag == "Player")
            {
                /* if(state != "chase" && state != "kill")//TODO: change this when I add a shout state
                 {
                     scream.Play();

                 }*/

                if (state == "search" || state == "walk")
                {
                    state = "shout";

                }
            }
        }
    }

    public void footstep()
    {
        sound.clip = footSounds;
        sound.Play();
    }

    public void endShout()
    {
        chaseTime = 15f;
        state = "chase";
    }

    public void playScream(int num)
    {
        sound.clip = screams[num];
        sound.Play();
    }
}