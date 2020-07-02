using UnityEngine;
using System.Collections;

public class FootStepScript : MonoBehaviour
{
    public float stepRate = 0.5f;
    public float stepCoolDown;
    public AudioClip [] footStepsConcrete;
    public AudioClip[] footStepsWood;
    public AudioClip[] footStepsGrass;
    public Transform standingFoot;
    public Transform crouchingFoot;
    public Vector3 foot;
    AudioSource audioSource;
    FirstPersonAIO firstPerson;
    public GameObject terrain;
    public LayerMask ventLayerMask;

    Rigidbody playerRB;
    public LayerMask crouchMask;
    public Transform curGrounded;

    public Vector3 groundNormal;
    private PlayerEnemyInteraction detection;
    CapsuleCollider capsule;
    public LayerMask groundLayer;
    Vector3 originalFeetPosition;
    // 0 - concrete
    // 1 - grass
    // 2 - wood
    public int floorMaterialIndex = 0;

    private void Start()
    {
        detection = GetComponent<PlayerEnemyInteraction>();
        capsule = GetComponent<CapsuleCollider>();
        groundNormal = Vector3.one;
        audioSource = GetComponent<AudioSource>();
        firstPerson = GetComponent<FirstPersonAIO>();
        playerRB = GetComponent<Rigidbody>();
     
    }

    // Update is called once per frame
    void Update()
    {

       
      

        if (firstPerson.isCrouching)
        {
            
            foot = crouchingFoot.position;
        }
        else
        {
            foot = standingFoot.position;
        }
 
        if (firstPerson.isSprinting && firstPerson.staminaInternal > 1)
        {
            stepRate = 0.35f;
        }
        else
        {
            stepRate = 0.55f;
        }


       // 
        if (firstPerson.IsGrounded)
        {
           
            stepCoolDown -= Time.deltaTime;
            if ((Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.95f || Mathf.Abs(Input.GetAxis("Vertical")) >= 0.95f) && stepCoolDown < 0f && playerRB.velocity.magnitude > 0.1f && !firstPerson.isCrouching)
            {
                
                audioSource.pitch = 1f + Random.Range(-0.2f, 0.2f);
                if (floorMaterialIndex == 2)
                {
                    audioSource.PlayOneShot(footStepsWood[0], Random.Range(0.8f, 0.9f));
                }
                else if(floorMaterialIndex == 1)
                {
                    audioSource.PlayOneShot(footStepsGrass[(int)Random.Range(0f, footStepsGrass.Length)], Random.Range(0.8f, 0.9f));
                }
                else
                {
                    detection.playerSound("foot");
                    audioSource.PlayOneShot(footStepsConcrete[(int)Random.Range(0f, footStepsConcrete.Length)], Random.Range(0.8f, 0.9f));
                }
                    
                stepCoolDown = stepRate;
            }
        }
       
    }

   

    
}