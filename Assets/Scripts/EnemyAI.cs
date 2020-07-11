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
    public GameObject terrain;
    public LayerMask hitLayerMask;
    CapsuleCollider capsule;
    public AudioClip[] gruntSounds;
    public AudioSource gruntSource;
    FirstPersonAIO firstPerson;
    bool above = false;
    public AudioClip monsterHurt;
    




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
        capsule = player.GetComponent<CapsuleCollider>();
        StartCoroutine(playGruntSound());
        firstPerson = player.GetComponent<FirstPersonAIO>();
        agent.updateRotation = true;
   
        previousPos = transform.position;
    }



    // Update is called once per frame
    void Update()
    {
      
        //speed.text = state;
       anim.SetFloat("velocity", agent.velocity.magnitude);

         if (agent.velocity.magnitude > 0.05f)
        {
            anim.SetBool("isWalking", true);
      
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

        
     

       




        if (state != "kill" && state != "DoNothing" && GameManager.Instance.playerDead)
        {
            state = "DoNothing";
            if (agent.isActiveAndEnabled)
            {


                agent.isStopped = true;
                agent.ResetPath();
            }
        }

    

        if ( (state == "idle" || state == "search"))
        {

            Collider[] hitColliders = Physics.OverlapSphere(transform.position + new Vector3(0, 1f, 0), 1.5f);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.CompareTag("Barrier") && hitColliders[i].gameObject != null)
                {

                    agent.ResetPath();
                 
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
                NavMesh.SamplePosition(player.transform.position + randomPos, out navHit, 3f, NavMesh.AllAreas);
                searchRadius += 0.3f;

                if (searchRadius > 3f)
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
  
            StartCoroutine(CameraShake(1.8f, .05f));
            gruntSource.Stop();
            anim.SetTrigger("scream");
            playScream(Random.Range(0, 4));
            state = "shouting";
 

        }

        if (state == "chase")
        {
            agent.speed = 3.5f;
            chaseTime -= Time.deltaTime;
            /*Vector3 Pos = player.transform.position;
            Pos.y -= ((firstPerson.capsule.height / 2) + 0.2f);*/
            NavMeshHit hitNav;
            if (NavMesh.SamplePosition(player.transform.position, out hitNav, 3f, NavMesh.AllAreas))
            {
                
                agent.SetDestination(hitNav.position);
            }
         
        
          
            //agent.destination = player.transform.position;
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance > 25f || chaseTime <= 0)
            {
               // Debug.Log("stop chasing");
                state = "hunt";
            }

            else if (distance <= 2.5f && state != "stay" && !firstPerson.above)
            {
              
                RaycastHit hit;
                if (Physics.Linecast(vision.position, player.transform.position, out hit, hitLayerMask))
                {
                   
                    if (hit.collider.gameObject.CompareTag("Player") )
                    {
                        above = firstPerson.above;
                        //firstPerson._crouchModifiers.useCrouch = false;
                        firstPerson.enableCameraMovement = false;
                        firstPerson.playerCanMove = false;
                        //firstPerson.enabled = false;
                        GameManager.Instance.killedBy = "mutant";
                        GameManager.Instance.playerDead = true;
                        agent.isStopped = true;
                        agent.ResetPath();
                        GetComponent<Rigidbody>().freezeRotation = true;
                        BossRb.velocity = Vector3.zero;
                        BossRb.angularVelocity = Vector3.zero;
                        transform.LookAt(player.transform.position);
                        state = "kill";
                       
                        PlayerRb.velocity = Vector3.zero;
                        PlayerRb.angularVelocity = Vector3.zero;
                        PlayerRb.useGravity = false;
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
                searchRadius = 1f;
                sight();
            }
        }

        if (state == "kill")
        {
            if(!above)
            {
                stopCrouching();
            }
           
            Quaternion lookOnLook = Quaternion.LookRotation(camPos.transform.position - player.transform.position);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, lookOnLook, 5f * Time.deltaTime);

        }

      /*  if (state == "stay")
        {

            //Do Nothing
        }

        if (state == "shouting")
        {

            //Do Nothing
        }*/


    



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
       
        //Time.timeScale = 0;
        GameManager.Instance.fadeIn();
       
    }

    public void sight()
    {
        RaycastHit hit;

        if(Physics.Linecast(vision.position, player.transform.position, out hit, layerSightMask))
        {
           
            if(hit.collider.gameObject.CompareTag("Player"))
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
        if (!terrain.activeInHierarchy)
        {
            
            soundFoot.PlayOneShot(footsteps[Random.Range(0, 5)]);
        }
            
        
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
        playHurtSound();
        yield return new WaitForSeconds(time);
        if(gruntSource.isPlaying)
        {
            gruntSource.Stop();
        }
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

    public void playHurtSound()
    {
        if(sound.isPlaying || gruntSource.isPlaying)
        {
            Invoke("playHurtSound", 1f);
        }
        else
        {
            gruntSource.PlayOneShot(monsterHurt);
        }
    }

    public string getState()
    {
        return state;
    }

    public void setState(string stateSet)
    {
        state = stateSet;
    }

    void stopCrouching()
    {
   
       capsule.height = Mathf.MoveTowards(capsule.height, 2, 5f * Time.deltaTime);

    }

    IEnumerator playGruntSound()
    {
        yield return new WaitForSeconds(Random.Range(7f, 15f));
        int i = Random.Range(0, gruntSounds.Length);
        if (!sound.isPlaying && (state == "walk" || state == "idle" || state == "search"))
        {
            //Debug.Log("play monster sound");
            gruntSource.PlayOneShot(gruntSounds[i]);
           
        }

        yield return new WaitForSeconds(gruntSounds[i].length);
        StartCoroutine(playGruntSound());


    }

    IEnumerator CameraShake(float duration, float mag)
    {
        Vector3 originalPos = mainCamera.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * mag;
            float y = Random.Range(-1f, 1f) * mag;

            mainCamera.transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;

            yield return null;

        }
    }


  
}
