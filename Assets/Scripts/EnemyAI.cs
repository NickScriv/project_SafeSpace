using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public AudioClip [] screams;
    public AudioClip[] footsteps;
    NavMeshAgent agent;
    Transform player;
    Animator anim;
    public AudioClip footSounds;
    public AudioSource sound;
    public AudioSource soundFoot;
    string state = "idle";
    public Transform vision;
    float waitSearch = 0f;
    float chaseTime = 0f;
    bool highAlert = false;
    float searchRadius = 20f;
    public GameObject deathcam;
    public Transform camPos;
    public Camera mainCamera;
    Rigidbody BossRb;
    Rigidbody PlayerRb;
    float dizzyTime = 15f;

    //TODO: Change tags of walls to "Barrier" in the final version of the maps

    // Start is called before the first frame update
    void Start()
    {
       
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = 1.2f;
        agent.updateRotation = true;
        BossRb = GetComponent<Rigidbody>();
        PlayerRb = player.GetComponent<Rigidbody>();



    }



    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isEnd)
        {
            agent.enabled = false;
            gameObject.SetActive(false);
        }

        //Debug.Log(waitSearch);

        // Debug.Log(searchRadius);
        //Debug.Log(state);
        Debug.DrawLine(vision.position, player.transform.position, Color.green);
        anim.SetFloat("velocity", agent.velocity.magnitude);
        // Debug.Log(agent.velocity.magnitude);


        if (state != "kill" && state != "DoNothing" && GameManager.Instance.playerDead)
        {
            state = "DoNothing";
            agent.isStopped = true;
            agent.ResetPath();
        }

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Dizzy"))
        {
            
            Vector3 p = this.transform.position;
            p.y += 0.198122f;
            this.transform.position = p;
        }


        if ( (state == "idle" || state == "search"))
        {

            Collider[] hitColliders = Physics.OverlapSphere(transform.position + new Vector3(0, 1f, 0), 1.5f);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.tag == "Barrier" && hitColliders[i].gameObject != null)
                {

                    agent.ResetPath();
                    Debug.Log("SOmewhere else");
                    state = "idle";
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
                    Debug.Log("WWWWWWWWWWWWWWWWWOOOOOOOOOOOOOOOOOOWWWWWWWWWWWWWWWW");
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
                waitSearch = 5f;
            }


        }

        if (state == "search")//How long he stops for and looks around
        {
            
            

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

        if (state == "shout")
        {
            agent.ResetPath();
            anim.SetTrigger("scream");
            playScream(Random.Range(0, 4));
            state = "shouting";
 

        }

        if (state == "chase")
        {
            agent.speed = 3.5f;
            chaseTime -= Time.deltaTime;
            agent.destination = player.transform.position;
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance > 25f || chaseTime <= 0)
            {
                state = "hunt";
            }

            else if (distance <= 2.5f)//TODO: alter this number for refinment (or adjust camPos position) depending on how tall the player is and how far the boss can reach. Stopping distance too
            {
                RaycastHit hit;
                if (Physics.Linecast(vision.position, player.transform.position, out hit))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                       
                        agent.isStopped = true;
                        agent.ResetPath();
                        GetComponent<Rigidbody>().freezeRotation = true;
                        BossRb.velocity = Vector3.zero;
                        BossRb.angularVelocity = Vector3.zero;
                        PlayerRb.isKinematic = true; 
                        transform.LookAt(player.transform.position);
                        state = "kill";
                        player.GetComponent<FirstPersonAIO>()._crouchModifiers.crouchKey = KeyCode.None;
                        player.GetComponent<FirstPersonAIO>().stopCrouching();
                        player.GetComponent<FirstPersonAIO>().playerCanMove = false;
                        player.GetComponent<FirstPersonAIO>().enabled = false;
                        PlayerRb.velocity = Vector3.zero;
                        PlayerRb.angularVelocity = Vector3.zero;
                        PlayerRb.isKinematic = true;
                        GameManager.Instance.playerDead = true;
                        // deathcam.SetActive(true);
                        //deathcam.transform.position = Camera.main.transform.position;
                        //deathcam.transform.rotation = Camera.main.transform.rotation;
                        //Camera.main.gameObject.SetActive(false);
                        anim.SetTrigger("AttackPlayer");
                        anim.speed = .8f;
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
            Quaternion lookOnLook = Quaternion.LookRotation(camPos.transform.position - player.transform.position);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, lookOnLook, 5f * Time.deltaTime);

        }

        if (state == "stay")
        {

            //Do Nothing
        }

        if (state == "shouting")
        {

            //Do Nothing
        }

    
        


        


    }


    void reset()
    {
       
        Time.timeScale = 0;
        GameManager.Instance.fadeIn();
        FindObjectOfType<SoundManager>().Play("BloodHit");
    }

    public void sight()
    {
        RaycastHit hit;

        if(Physics.Linecast(vision.position, player.transform.position, out hit))
        {
            //Debug.Log("Hit " + hit.collider.gameObject.name);

            if(hit.collider.gameObject.tag == "Player")
            {
            

                if(state == "search" || state == "walk")
                {
                    state = "shout";
                    
               }
            }
        }
    }

    public void footstep(int num)
    {
      
       soundFoot.PlayOneShot(footsteps[Random.Range(0,5)]);
        
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

    public void hitByFlare()
    {
        anim.SetTrigger("hit");
        StopCoroutine("endHit");
        StartCoroutine("endHit", 15f);
        agent.ResetPath();
        agent.isStopped = true;
        BossRb.velocity = Vector3.zero;
        BossRb.angularVelocity = Vector3.zero;
        state = "stay";
    }

    public IEnumerator endHit(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("execute end hit");
        agent.isStopped = false;
        state = "idle";
        anim.SetTrigger("backToIdle");
    }

    public string getState()
    {
        return state;
    }

  


    /* void OnDrawGizmosSelected()
     {
         // Draw a yellow sphere at the transform's position
         Gizmos.color = Color.yellow;
         Gizmos.DrawSphere(transform.position + new Vector3(0, 1f, 0), 1.5f);
     }*/
}
