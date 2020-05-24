using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyInteraction : MonoBehaviour
{

  

  

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Vision")
        {
         
            other.transform.parent.GetComponent<EnemyAI>().sight();
        }

        if (other.gameObject.name == "BugVision")
        {
            other.transform.parent.GetComponent<SmallAI>().sight();
        }
    }

    public void playerSound(string type)
    {
        float dist = 0f;

        if (type == "foot")
        {
            if (gameObject.GetComponent<FirstPersonAIO>().isSprinting)
            {
                dist = 10f;
            }
            else if(gameObject.GetComponent<FirstPersonAIO>().isCrouching)
            {
                dist = 1f;
            }
            else
            {
                dist = 4f;
            }
        }
        else if (type == "jump")
        {
            dist = 7f;

        }
        else if (type == "land")
        {

            dist = 10f;
        }


        if (dist > 0f)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, dist);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.CompareTag("Boss"))
                {
                   // Debug.Log("Hit Boss");
                    hitColliders[i].transform.GetComponent<EnemyAI>().sight();
                    return;
                }

                if (hitColliders[i].CompareTag("Bug"))
                {
                   // Debug.Log("Hit bug");
                    hitColliders[i].transform.GetComponent<SmallAI>().sight();
                    return;
                }
                i++;
            }
        }

    }   

   
}




