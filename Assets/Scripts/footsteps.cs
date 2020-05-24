using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{

    bool isPlaying;
    bool isJumping;
    bool Grounded;
    [SerializeField]
    AudioClip[] footstepConcrete;
    [SerializeField]
    AudioClip[] footstepWood;
    [SerializeField]
    AudioClip[] footstepGrass;

    [SerializeField]
    int currentClip;
    [SerializeField]
    int preClip;
    [SerializeField]
    AudioSource movementSource;
    [SerializeField]
    float waitTime;
    FirstPersonAIO firstPersonScript;
    Rigidbody playerRB;
    public Transform foot;
    PlayerEnemyInteraction detection;

 

    void Start()
    {
        /*footstep = Resources.LoadAll<AudioClip>("Sounds/Movement/Footsteps/Dirtstep");
        footstepWood = Resources.LoadAll<AudioClip>("Sounds/Movement/Footsteps/Woodstep");
        footstepWater = Resources.LoadAll<AudioClip>("Sounds/Movement/Footsteps/Waterstep");
        jumpDirt = Resources.LoadAll<AudioClip>("Sounds/Movement/Jump/Dirtjump");
        jumpWood = Resources.LoadAll<AudioClip>("Sounds/Movement/Jump/Woodjump");*/
        firstPersonScript = GetComponent<FirstPersonAIO>();
        playerRB = GetComponent<Rigidbody>();
        detection = GetComponent<PlayerEnemyInteraction>();
    }

    void Update()
    {
        if (GameManager.Instance.isPaused || GameManager.Instance.playerDead || GameManager.Instance.isEnd)
        {
            return;
        }

      
        RaycastHit hit;
        Ray landingRay = new Ray(foot.position, -transform.up);
     
        if (firstPersonScript.isSprinting)
        {
            waitTime = 0.35f;//Random.Range (0.2f, 0.22f);
        }
        else
        {
            waitTime = 0.6f;//Random.Range (0.35f, 0.38f);
        }

        if (Input.GetAxis("Horizontal") > 0.1f && isPlaying == false)
        {
            StartCoroutine("Footsteps");
        }
        if (Input.GetAxis("Horizontal") < -0.1f && isPlaying == false)
        {
            StartCoroutine("Footsteps");
        }
        if (Input.GetAxis("Vertical") > 0.1f && isPlaying == false)
        {
            StartCoroutine("Footsteps");
        }
        if (Input.GetAxis("Vertical") < -0.1f && isPlaying == false)
        {
            StartCoroutine("Footsteps");
        }
      

    }

    IEnumerator Footsteps()
    {
       
        RaycastHit hit;
        Ray landingRay = new Ray(foot.position, -transform.up);

        if (Physics.Raycast(foot.position, Vector3.down, out hit, .5f))
        {
            
            isPlaying = true;
        
          
            if (hit.collider.CompareTag( "wood"))
            {
                if (currentClip == preClip)
                {
                    currentClip = (int)Random.Range(0f, footstepWood.Length);
                }

                movementSource.clip = footstepWood[currentClip];
                movementSource.volume = Random.Range(0.8f, 0.9f);
                if ((Mathf.Abs(Input.GetAxis("Vertical")) == 1 || Mathf.Abs(Input.GetAxis("Horizontal")) == 1) && firstPersonScript.enabled && !firstPersonScript.isCrouching && firstPersonScript.playerCanMove && !playerRB.isKinematic)
                {

                    movementSource.PlayOneShot(movementSource.clip);
                    
                }
               

                //				movementSource[sourceNumber()].pitch = Random.Range(0.9f,1.1f);
                //				movementSource [sourceNumber ()].PlayOneShot (footstepWood[currentClip],Random.Range(0.9f,1.1f));
            }
            else if (hit.collider.CompareTag("grass"))
            {
                if (currentClip == preClip)
                {
                    currentClip = (int)Random.Range(0f, footstepGrass.Length);
                }

                movementSource.clip = footstepGrass[currentClip];
                movementSource.volume = Random.Range(0.8f, 0.9f);
                if ((Mathf.Abs(Input.GetAxis("Vertical")) == 1 || Mathf.Abs(Input.GetAxis("Horizontal")) == 1) && firstPersonScript.enabled && !firstPersonScript.isCrouching && firstPersonScript.playerCanMove && !playerRB.isKinematic)
                {

                    movementSource.PlayOneShot(movementSource.clip);
                }
                //				movementSource[sourceNumber()].pitch = Random.Range(0.9f,1.1f);
                //				movementSource [sourceNumber ()].PlayOneShot (footstepWater[currentClip],Random.Range(0.9f,1.1f));
            }
            else
            {
                if (currentClip == preClip)
                {
                    currentClip = (int)Random.Range(0f, footstepConcrete.Length);
                }
                movementSource.clip = footstepConcrete[currentClip];
                movementSource.volume = Random.Range(0.8f, 0.9f);
                if ((Mathf.Abs(Input.GetAxis("Vertical")) == 1 || Mathf.Abs(Input.GetAxis("Horizontal")) == 1) && firstPersonScript.enabled && !firstPersonScript.isCrouching && firstPersonScript.playerCanMove && !playerRB.isKinematic)
                {

                    movementSource.PlayOneShot(movementSource.clip);
                    detection.playerSound("foot");
                }
                //				movementSource[sourceNumber()].pitch = Random.Range(0.9f,1.1f);
                //				movementSource [sourceNumber ()].PlayOneShot (footstepDirt[currentClip],Random.Range(0.9f,1.1f));
            }
            yield return new WaitForSeconds(waitTime);
            preClip = currentClip;
            isPlaying = false;
        }
    }


}