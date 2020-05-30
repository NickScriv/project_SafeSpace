using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeleportMannaquin : MonoBehaviour
{
    public Transform player;
    bool lookingAtPlayer = false;
    NavMeshAgent agent;
   
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
      
        
    }
    // Update is called once per frame
    void Update()
    {

      

        Vector3 dir = (transform.position - player.transform.position).normalized;
        float dot = Vector3.Dot(dir, player.transform.forward);
      
   
        if(dot > 0.09f)
        {
            
            CancelInvoke();
            lookingAtPlayer = true;
           
            
        }
        else
        {
           
            if(lookingAtPlayer)
            {
                teleportObject();
                Invoke("teleportObject", 1f);
                Invoke("teleportObject", 3.5f);
                Invoke("teleportObject", 7f);
                Invoke("teleportObject", 11f);
                lookingAtPlayer = false;
            }
            
        }






    }

    void teleportObject()
    {
       
        NavMeshHit navHit;
        if(NavMesh.SamplePosition(player.position - (1.3f * player.forward), out navHit, 1.3f, NavMesh.AllAreas))
        {
            
           
           
            agent.Warp(navHit.position);
       
            transform.LookAt(player.transform);
          
     
        }
      
      
    }

    
}
