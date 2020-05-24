using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class flarebullet : MonoBehaviour {
			

	private Light flarelight;
	private AudioSource flaresound;
	private ParticleSystemRenderer smokepParSystem;
	private bool myCoroutine;
	private float smooth = 2.4f;
	public 	float flareTimer = 9;
	public AudioClip flareBurningSound;
    public LayerMask mask;
    bool isGrounded = false;
    private Collider[] overlapResults = new Collider[1750];


    // Use this for initialization
    void Start ()
    {
        
            StartCoroutine("flareLightoff");
		
		GetComponent<AudioSource>().PlayOneShot(flareBurningSound);
		flarelight = GetComponent<Light>();
		flaresound = GetComponent<AudioSource>();
		smokepParSystem = GetComponent<ParticleSystemRenderer>();

		
		Destroy(gameObject,flareTimer + 1f);
		
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            return;
        }

        if (GameManager.Instance.playerDead || GameManager.Instance.isEnd)
        {
            Destroy(gameObject);
        }

        if (!GameManager.Instance.playerDead)
        {
            int num = Physics.OverlapSphereNonAlloc(transform.position, 20, overlapResults);
            int i = 0;

            while (i < num)
            {
                if (overlapResults[i].gameObject.CompareTag("Bug") && overlapResults[i].gameObject != null)
                {
                    GameObject bug = overlapResults[i].gameObject;
                    SmallAI bugAI = bug.GetComponent<SmallAI>();
                    if(bugAI.getState() != "kill" && bugAI.getState() != "shouting" && bugAI.getState() != "shout" && bugAI.getState() != "runAway" && bugAI.getState() != "runAway2")
                    {
                        bugAI.setState("runAway2");
                        bugAI.FlareBullet = gameObject;
                    }
                    
                   
                }
                i++;
            }
        }

        if (myCoroutine == true)
			
		{
			flarelight.intensity = Random.Range(2f,6.0f);
			
		}else
			
		{
			flarelight.intensity =  Mathf.Lerp(flarelight.intensity,0f,Time.deltaTime * smooth);
			flarelight.range =  Mathf.Lerp(flarelight.range,0f,Time.deltaTime * smooth);			
			flaresound.volume = Mathf.Lerp(flaresound.volume,0f,Time.deltaTime * smooth);
			smokepParSystem.maxParticleSize = Mathf.Lerp(smokepParSystem.maxParticleSize,0f,Time.deltaTime * 5);


		}

			
	}

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.transform.gameObject.name);
        if(collision.gameObject.CompareTag( "Boss") && !isGrounded)
        {
            collision.gameObject.GetComponent<EnemyAI>().hitByFlare();
        }
    }

    IEnumerator flareLightoff()
	{
		myCoroutine = true;
		yield return new WaitForSeconds(flareTimer);
		myCoroutine = false;

	}

    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            return;
        }
        //Debug.Log("Flare is Gounded: " + isGrounded);
        if (Physics.Raycast(transform.position, Vector3.down,  1f, mask))
        {
            //Debug.Log("Flare is Gounded");
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
