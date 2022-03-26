using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // store the last position the player collided with!

    private static GameMaster instance;
    public Vector2 lastCheckPointPosPlayerOne;
    public Vector2 lastCheckPointPosPlayerTwo;
    public static bool is2Players = false;
    private static bool isMario = true;
    public static bool marioFirst = true;
    public bool zoomIn=false;
    
    //awake happens before start event!!
    private void Awake()
    {
            if (instance == null)
            {
            instance = this;
            DontDestroyOnLoad(instance);
            }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public static bool isMarioCheck()
    {
        return isMario;
    }

    public static void setMarioCheck(bool update)
    {
        isMario = update;
    }
}
