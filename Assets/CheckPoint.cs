using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
  private GameMaster gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if we one player enters the checkpoint, we want theuy both get the new checkpoint
        if (collision.CompareTag("Player") || collision.CompareTag("Player 2"))
        {
            print(transform.position);
            gm.lastCheckPointPosPlayerOne = transform.position;
            gm.lastCheckPointPosPlayerTwo = new Vector3(transform.position.x, transform.position.y);
            if(transform.position.x>270){
                gm.zoomIn=true;
            }
            
        }

    }
}
