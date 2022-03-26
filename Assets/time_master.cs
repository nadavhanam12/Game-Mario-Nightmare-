using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time_master : MonoBehaviour
{
    public Vector3 dest1;
    public Vector3 dest2;
    public Vector3 dest3;
    public Vector3 dest4;

     public float speed;
    public Timer timer;
    public Player player;

     public Vector3 destination;
 
     void Start () {
         // Set the destination to be the object's position so it will not start off moving
         SetDestination (gameObject.transform.position);
         //GetComponent<Animator>().SetBool("isCrushed", true);
     }
     
     void Update () {
         // If the object is not at the target destination
         if (destination != gameObject.transform.position) {
             // Move towards the destination each frame until the object reaches it
             IncrementPosition ();
         }
     }
 
     void IncrementPosition ()
     {
         // Calculate the next position
         float delta = speed * Time.deltaTime;
         Vector3 currentPosition = gameObject.transform.position;
         Vector3 nextPosition = Vector3.MoveTowards (currentPosition, destination, delta);
 
         // Move the object to the next position
         gameObject.transform.position = nextPosition;
     }
 
     // Set the destination to cause the object to smoothly glide to the specified location
     public void SetDestination (Vector3 value) {
         destination = value;
     }
    public void next_point () {
        print(destination);
        if(destination==dest4){Destroy(gameObject);}
        if(destination==dest3){kill_time_master();}
        else if(destination==dest2){destination=dest3;}
        else if(destination==dest1){destination=dest2;}  
        else{destination=dest1;}  
        //print(destination); 
        }
    public void kill_time_master(){
        //print("kill_time_master");
        timer.kill();
        player.open_wall();
        destination=dest4;
        //Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //print("hhh");
        if((other.tag=="Player")||(other.tag=="Player 2")){
        next_point();
        }
    }
}
