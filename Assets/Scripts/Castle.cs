using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    private GameMaster gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" || other.tag == "Player 2")
        {
            gm.lastCheckPointPosPlayerOne = new Vector2(4, 2);
            gm.lastCheckPointPosPlayerTwo = new Vector3(6, 2);
            Application.LoadLevel("Win");
        }
    }
}
