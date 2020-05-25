using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoss : MonoBehaviour
{
     GameObject prevBoss;
    public GameObject newBoss;
    // Start is called before the first frame update
 

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            prevBoss = GameObject.FindGameObjectWithTag("Boss");
            if (prevBoss.GetComponent<EnemyAI>().getState() != "chase" && prevBoss.GetComponent<EnemyAI>().getState() != "shouting" && prevBoss.GetComponent<EnemyAI>().getState() != "shouting")
            {
                
                prevBoss.SetActive(false);
                newBoss.SetActive(true);
            }

            this.gameObject.SetActive(false);
        }
    }
}
