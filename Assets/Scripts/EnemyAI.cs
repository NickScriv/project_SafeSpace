using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EZCameraShake;
using UnityStandardAssets.Characters.ThirdPerson;

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
    float rotationSpeed = 2.7f;
    //public Text speed;
    Vector3 previousPos;
    Vector3 direction;
    Vector3 prevDirection;
    public LayerMask layerSightMask;



    // Start is called before the first frame update
    void Start()
    {
       
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = 1.2f;
        BossRb = GetComponent<Rigidbody>();
        PlayerRb = player.GetComponent<Rigidbody>();
        agent.updateRotation = false;

      

        previousPos = transform.position;
    }



    // Update is called once per frame
    void Update()
    {
        //speed.text = state;
       anim.SetFloat("velocity", agent.velocity.magnitude);

         if (agent.desiredVelocity.magnitude > 0.005f)
        {
            anim.SetBool("isWalking", true);
            RotateTowards(agent.steeringTarget);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
       


        if (GameManager.Instance.isEnd)
        {
            agent.enabled = false;
            gameObject.SetActive(false);
        }

        Debug.DrawLine(vision.position, player.transform.position, Color.green);
     

       




        if (state != "kill" && state != "DoNothing" && GameManager.Instance.playerDead)
        {
            state = "DoNothing";
            agent.isStopped = true;
            agent.ResetPath();
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
                    anim.SetBool("isWalking", true);
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
                    FindObjectOfType<SoundManager>().StopFade("ChaseMusic");                     
                    if(!FindObjectOfType<SoundManager>().isPlaying("Music"))
                    {
                        FindObjectOfType<SoundManager>().PlayFade("Music");
                    }
                    
                
                    highAlert = false;
                    agent.speed = 1.2f;
                }
            }
            agent.SetDestination(navHit.position);
            anim.SetBool("isWalking", true);
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
  
            StartCoroutine(CameraShaker.Instance.CameraShake(1.8f, .12f));

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

            else if (distance <= 2.5f && state != "stay")
            {
                Debug.Log("Distance check!");
                RaycastHit hit;
                if (Physics.Linecast(vision.position, player.transform.position, out hit))
                {

                    if (hit.collider.gameObject.tag == "Player")
                    {
                        Debug.Log("kill now");
                        GameManager.Instance.playerDead = true;
                        agent.isStopped = true;
                        agent.ResetPath();
                        GetComponent<Rigidbody>().freezeRotation = true;
                        BossRb.velocity = Vector3.zero;
                        BossRb.angularVelocity = Vector3.zero;
                        transform.LookAt(player.transform.position);
                        state = "kill";
                        player.GetComponent<FirstPersonAIO>().playerCanMove = false;
                        player.GetComponent<FirstPersonAIO>().enabled = false;
                        PlayerRb.velocity = Vector3.zero;
                        PlayerRb.angularVelocity = Vector3.zero;
                        PlayerRb.isKinematic = true;
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
            Debug.Log("In kill state");
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

    private void RotateTowards(Vector3 target)
    {
        if (GameManager.Instance.playerDead)
            return;
        prevDirection = direction;
        direction = (target - transform.position).normalized;
        if (direction != prevDirection)
            anim.SetBool("isWalking", true);
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
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

        if(Physics.Linecast(vision.position, player.transform.position, out hit, layerSightMask))
        {
            //Debug.Log("Hit " + hit.collider.gameObject.name);
            Debug.Log("Hit: " + hit.collider.gameObject.name);
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
        FindObjectOfType<SoundManager>().StopFade("Music");
        if(!FindObjectOfType<SoundManager>().isPlaying("ChaseMusic"))
        {
            FindObjectOfType<SoundManager>().PlayFade("ChaseMusic");
        }
        
    }

    public void playScream(int num)
    {
        
        sound.clip = screams[num];
        sound.Play();
    }

    public void hitByFlare()
    {
        state = "stay";
        anim.SetTrigger("hit");
        StopCoroutine("endHit");
        StartCoroutine("endHit", 15f);
        agent.ResetPath();
        agent.isStopped = true;
        BossRb.velocity = Vector3.zero;
        BossRb.angularVelocity = Vector3.zero;
        
    }

    public IEnumerator endHit(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("execute end hit");
        agent.isStopped = false;
        state = "idle";
        highAlert = true;
        searchRadius = 22;
        FindObjectOfType<SoundManager>().StopFade("ChaseMusic");
        if (!FindObjectOfType<SoundManager>().isPlaying("Music"))
        {
            FindObjectOfType<SoundManager>().PlayFade("Music");
        }
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
