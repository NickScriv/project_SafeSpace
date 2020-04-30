using System.Collections;
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
    public Transform player;
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
    // public Text stateText;
    public Transform [] runAwayPos;
    public bool inFlashlightZone = false;
    float attackRange = 1.3f;
    bool runAway = false;
    bool runAway2 = false;


    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
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

        if (GameManager.Instance.isEnd)
        {
            
            agent.enabled = false;
            gameObject.SetActive(false);
            this.enabled = false;
        }
          

        if (state != "kill" && state != "DoNothing" && GameManager.Instance.playerDead)
        {
            state = "DoNothing";
            agent.isStopped = true;
            agent.ResetPath();
        }


        Debug.Log(state);
        //stateText.text = state;
        Debug.DrawLine(vision.position, player.transform.position, Color.green);
        anim.SetFloat("velocity", agent.velocity.magnitude);

        if (!GameManager.Instance.playerDead && state != "kill" && state != "shouting" && state != "kill" && state != "shout" && state != "runAway")
        {
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
        }

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
            return;
        }

        if (state == "walk")
        {
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                state = "search";
                waitSearch = 4f;
                return;
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
                    return;
                }
            }

            if (waitSearch > 0f)
            {
                waitSearch -= Time.deltaTime;
            }
            else
            {
                state = "idle";
                return;
            }
        }

        //TODO: Implement a shout state
        if (state == "shout")
        {
            if (state != "runAway" && state != "runAway2")
            {
                agent.isStopped = true;
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
            else if (distance <= attackRange)
            {
                RaycastHit hit;
                if (Physics.Linecast(vision.position, player.transform.position, out hit))
                {
                    agent.isStopped = true;
                    agent.ResetPath();
                    state = "Attacking";
                    countdown = 0f;


                }

             }
          

           
            sight();


        }

        if (state == "Attacking")
        {
           
            if (player.GetComponent<PlayerInfo>().currentHealth <= 0)
            {

               
                state = "kill";
                GetComponent<Rigidbody>().freezeRotation = true;
                BugRb.velocity = Vector3.zero;
                BugRb.angularVelocity = Vector3.zero;
                transform.LookAt(player.transform.position);
                player.GetComponent<FirstPersonAIO>().enabled = false;
                PlayerRb.velocity = Vector3.zero;
                PlayerRb.angularVelocity = Vector3.zero;
                GameManager.Instance.playerDead = true;
                anim.SetTrigger("AttackPlayer");
                anim.speed = 0.6f;
                return;
            }


            RotateTowards(player);
            countdown -= Time.deltaTime;
            Debug.Log(countdown);
            if (countdown <= 0)
            {
                countdown = 1f;
                anim.SetTrigger("AttackPlayer");
            }
            if (Vector3.Distance(player.transform.position, transform.position) > attackRange)
            {
                state = "chase";
                countdown = 0f;
                //Invoke("changeState",2);
            }
        }

        if (state == "runAway")
        {
            agent.speed = 3f;
            Vector3 runTo = transform.position + ((transform.position - player.position) * 2);
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < range)
            {
               
                if (flashlight.GetComponent<Flashlight_PRO>().is_enabled != true || !inFlashlightZone)
                {
                    Debug.Log("runnnnnnnnnnnnnnnnnnn");
                    state = "chase";
                    runAway = false;
                    return;


                }
                else
                {
                    //agent.Move(player.transform.forward * Time.deltaTime);
                    if (!runAway)
                    {
                        RunAway(null);
                        runAway = true;
                    }

                }
            }

            if (distance >= range)
            {
                state = "idle";
                searchRadius = 22f;
                highAlert = false;
                runAway = false;



            }
            
        }

    

        if (state == "runAway2")
        {
            agent.speed = 3f;
            if (FlareBullet != null)
            {
                float distance = Vector3.Distance(transform.position, FlareBullet.position);
                if (distance < range)
                {
                    if(!runAway2)
                    {
                        RunAway(FlareBullet);
                        runAway2 = true;
                    }
                    

                }

                if (distance >= range)
                {
                    state = "idle";
                    runAway2 = false;
                    searchRadius = 22f;
                    highAlert = false;


                }
            }
            else if (FlareBullet == null)
            {
                runAway2 = false;
                state = "idle";
                searchRadius = 20f;
                highAlert = false;
                return;
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
            Invoke("reset", 2.5f);
            
        }




    }

    void reset()
    {
        Time.timeScale = 0;
        GameManager.Instance.fadeInBug();
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

        FindObjectOfType<SoundManager>().Play("BugHit");
    }

    public void dealDamage()
    {
     
        player.GetComponent<PlayerInfo>().ApplyDamage(26);
    }

    void changeState()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > attackRange)
        {
            state = "chase";
            countdown = 0f;
        }
    }
    
    public string getState()
    {
        return state;
    }

    public void RunAway(Transform flare)
    {
        float furthestDistanceSoFar = 0;
        Vector3 runPosition = Vector3.zero;

        foreach (Transform point in runAwayPos)
        {
            float currentCheckDistance;
            if(flare == null)
            {
                currentCheckDistance = Vector3.Distance(player.position, point.position);
            }
            else
            {
                currentCheckDistance = Vector3.Distance(flare.position, point.position);
            }
            
            if (currentCheckDistance > furthestDistanceSoFar)
            {
                furthestDistanceSoFar = currentCheckDistance;
                runPosition = point.position;
            }
        }
        //Set the right destination for the furthest spot

        agent.SetDestination(runPosition);
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