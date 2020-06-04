 using UnityEngine;
 using System.Collections;
 
 public class OpenableDrawer : MonoBehaviour {
 
     //use this script for doors that require pressing the use button
     //set up the use button in edit >> project settings >> input
     //the door has to have a trigger
 
     private bool player_inrange = false; //is player in range of the door
     private float state_closed; //the value of y axis position when the door is closed
     private float state_open; //the value of y axis position when the door is open
     private bool wanted_open = false; //if the door wants to be open or closed
     private float current_y; //y axis position of the door, updated every frame
     public float speed; //how fast the door moves
     
 
     void Start()
     {
         state_closed = gameObject.transform.position.y;
         state_open = state_closed + 3f;
     }
 
     void OnTriggerEnter(Collider other)
     {
         if (other.name == "Player")
         {
             player_inrange = true;
             CancelInvoke("IdleCloseDoor");
        
         }
     }
 
     void OnTriggerExit(Collider other)
     {
         if (other.name == "Player")
         {
             player_inrange = false;
          
             Invoke("IdleCloseDoor", 5f);
         }
     }
 
 
 
     void Update()
     {
         current_y = gameObject.transform.position.y;
         if (Input.GetButtonDown("Use") && player_inrange == true) //if player is in range and presses e(default)
         {
             wanted_open = !wanted_open;
             CancelInvoke("IdleCloseDoor");
             
         }
 
         //if they want to be open, but aren't (it should stop when it reaches state_open)
         if (wanted_open == true && current_y < state_open)
         {
             gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);
         }
 
         //if they want to be closed, but aren't (it should stop when it reaches state_closed)
         if (wanted_open == false && current_y > state_closed) 
         {
             gameObject.transform.Translate(Vector3.down * Time.deltaTime * speed);
         }
     }
 
 
     //and this makes the door want to close when the player wanders away and the door stays open for too long
         void IdleCloseDoor()
     {
         wanted_open = false;
     }
 }
