using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public Transform leftBounds;
    public Transform rightBounds;
    public Transform bottomBounds;
    public Transform bottomBounds2;
    public Transform bottomBounds3;
    public Transform bottomBounds4;

    private bool isPlayer1Disabled = false;
    private bool isPlayer2Disabled = false;

    public GameObject background_1;
    public GameObject background_2;
    public GameObject background_3;
    public GameObject background_4;
    public GameObject background_5;
    public GameObject background_6;

    public AudioSource music_1;
    public AudioSource music_2;
    public AudioSource music_3;
    public AudioSource music_4;
    public AudioSource music_5;
    public AudioSource music_6;

    public GameObject chase_me_test;

    float time1 = 0;
    float time2 = 0;
    float time3 = 0;
    float time4 = 0;

    /*public Transform upperBounds;*/

    //the apporiximate time its going to take from our originial position to our target position.
    public float smoothDampTime = 0.15f;
    private Vector3 smoothDampVelocity = Vector3.zero;

    public Timer timer;
    public GameObject timer_canvas;
    private float camWidth, camHeight, levelMinX, levelMaxX, levelMinY, levelMaxY;
    // Start is called before the first frame update


    public time_master time_master;
    public Vector3 dest1;
    public Vector3 dest2;
    public Vector3 dest3;
    private bool isintimer=false;
    public int time_to_maze;
    private GameMaster gm;
    private bool isinmaze=false;
    //public Camera camera;
    void Start()
    {
        music_4.Stop();
        music_3.Stop();
        music_2.Stop();
        music_1.Stop();

        //1 player handler
        //means the user chose mario
        //need to uncomment it
        if (GameMaster.isMarioCheck() && !GameMaster.is2Players)
        {
            player1.gameObject.SetActive(true);
            player2.gameObject.SetActive(false);
        }
        //means the user chose boo
        else if (!GameMaster.isMarioCheck() && !GameMaster.is2Players)
        {
            player1.gameObject.SetActive(false);
            player2.gameObject.SetActive(true);
        }
        //means we're in two players game ,  i think here the login to make the input switch
        else
        {
            player1.gameObject.SetActive(true);
            player2.gameObject.SetActive(true);
        }


        if (!player1.gameObject.activeSelf)
        {
            isPlayer1Disabled = true;
        }
        if (!player2.gameObject.activeSelf)
        {
            isPlayer2Disabled = true;
        }
        //gives height
        camHeight = Camera.main.orthographicSize * 2;
        //gives the width
        camWidth = camHeight * Camera.main.aspect;

        float leftBoundsWidth = leftBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        float rightBoundsWidth = rightBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        /*float uperBoundsWidth = upperBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;*/
        float bottomBoundsWidth = bottomBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;

       /* float uperPLayerWidth = target.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;*/

 
        levelMinX = leftBounds.position.x + leftBoundsWidth + (camWidth / 2);
        levelMaxX = rightBounds.position.x - rightBoundsWidth - (camWidth / 2);
        levelMinY = bottomBounds.position.y + (camHeight / 2) - 0.5f;
        /*levelMaxY = upperBounds.position.y  - (camWidth / 2);*/

        
       /* levelMaxY = target.position.y + uperPLayerWidth + (camWidth / 2);*/
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        // if(gm.zoomIn){
        //     Camera.main.orthographicSize=4;
        //     camHeight = Camera.main.orthographicSize;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        //If someone die we need to update the bool (why? because here we loading the GameOver scene - only if 2 unactive)
        if (!player1.gameObject.activeSelf)
        {
            isPlayer1Disabled = true;
        }
        if (!player2.gameObject.activeSelf)
        {
            isPlayer2Disabled = true;
        }
        var currX = player1.position.x;
        var currX2 = player2.position.x;
        float midpointX = 0;
        float midpointY = 0;
        //midpoint is the center of the camera, we need to change the midpoint if we 1 player/2 players
        // we're 2 player GAME!
        if (!isPlayer1Disabled && !isPlayer2Disabled)
        {
            /*print("1");*/
            midpointX = (player1.position.x + player2.position.x) / 2f;
            midpointY = (player1.position.y + player2.position.y) / 2f;
            if ((currX >= 149 && currX <= 197) && (currX2 >= 149 && currX2 <= 197))
            {
                levelMinY = bottomBounds2.position.y + (camHeight / 2) - 0.5f;
            }
            else if (currX <= 275 && currX > 197 && player1.position.y >= 2 && currX2 <= 275 && currX2 > 197 && player2.position.y >= 2)
            {
                levelMinY = bottomBounds3.position.y + (camHeight / 2) - 0.5f;
            }
            else if ((currX > 275 && player1.position.y >= 2)&&(currX2 > 275 && player2.position.y >= 2))
            {
                levelMinY = bottomBounds4.position.y + (camHeight / 2) - 0.5f;
            }
        }
        //only PLAYER 1 IS Active (player 2 inactive)
        else if (!isPlayer1Disabled)
        {
           /* print("2");*/
            midpointX = player1.position.x ;
            midpointY = player1.position.y ;
            if ((currX >= 149 && currX <= 197))
            {
                levelMinY = bottomBounds2.position.y + (camHeight / 2) - 0.5f;
            }
            else if (currX <= 275 &&currX > 197 && player1.position.y >= 2)
            {
                levelMinY = bottomBounds3.position.y + (camHeight / 2) - 0.5f;
            }
            else if (currX > 275 && player1.position.y >= 2)
            {
                levelMinY = bottomBounds4.position.y + (camHeight / 2) - 0.5f;
            }
            
        }
        //only PLAYER 2 IS Active
        else if (!isPlayer2Disabled)
        {
            // /*print("3");*/
            // midpointX = player2.position.x;
            // midpointY = player2.position.y;
            // if ((currX2 >= 149 && currX2 <= 197))
            // {
            //     levelMinY = bottomBounds2.position.y + (camHeight / 2) - 0.5f;
            // }
            // else if (currX2 > 197 && player2.position.y >= 2)
            // {
            //     levelMinY = bottomBounds3.position.y + (camHeight / 2) - 0.5f;
            // }
            midpointX = player2.position.x ;
            midpointY = player2.position.y ;
            if ((currX2 >= 149 && currX2 <= 197))
            {
                levelMinY = bottomBounds2.position.y + (camHeight / 2) - 0.5f;
            }
            else if (currX2 <= 275 &&currX2 > 197 && player2.position.y >= 2)
            {
                levelMinY = bottomBounds3.position.y + (camHeight / 2) - 0.5f;
            }
            else if (currX2 > 275 && player2.position.y >= 2)
            {
                levelMinY = bottomBounds4.position.y + (camHeight / 2) - 0.5f;
            }
            
        }
        //means they both unactive so they're dead
        else
        {
            SceneManager.LoadScene("GameOver");
        }

        /*if (player1)*/



        float targetX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, midpointX));

            float targetY = Mathf.Max(levelMinY, Mathf.Min(50, midpointY));
            /*float targetY = Mathf.Max(Mathf.Min(levelMaxY, target.position.y),
                target.position.y + target.GetComponentInChildren<SpriteRenderer>().bounds.size.y/2);*/

            float x = Mathf.SmoothDamp(transform.position.x, targetX, ref smoothDampVelocity.x, smoothDampTime);
            float y = Mathf.SmoothDamp(transform.position.y, targetY, ref smoothDampVelocity.y, smoothDampTime);

            transform.position = new Vector3(x, y, transform.position.z);

            //moved here the update background and music and not per player
            update_scene(midpointX);



    }

        public void setBottom(Transform trans)
    {
        bottomBounds = trans;
    }

    //UNUSED, motivation for the camera movement 2 players
    // Follow Two Transforms with a Fixed-Orientation Camera
    public void FixedCameraFollowSmooth(Camera cam, Transform t1, Transform t2)
    {
        // How many units should we keep from the players
        float zoomFactor = 1f;
        float followTimeDelta = 0.8f;

        // Midpoint we're after
        Vector3 midpoint = (t1.position + t2.position) / 2f;

        // Distance between objects
        float distance = (t1.position - t2.position).magnitude;

        // Move camera a certain distance
        Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;

        // Adjust ortho size if we're using one of those
       /* if (cam.orthographic)*/
       /* {
            // The camera's forward vector is irrelevant, only this size will matter
            cam.orthographicSize = distance;
        }*/
        // You specified to use MoveTowards instead of Slerp
        cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

        // Snap when close enough to prevent annoying slerp behavior
        /*if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
            cam.transform.position = cameraDestination;*/
    }

    void update_scene(float x_pos)
    {
        //print("-1");
        float first = 0;
        float second = 75;
        float third = 149;
        float fourth = 223;
        float fifth = 275;
        float sixth = 330;
        /*x_pos = transform.localPosition.x;*/
        //print(x_pos);
        //print(second);
        //print(background_1.active);
        if (x_pos < second)
        {
            update_scene_number(1,x_pos);
        }
        if ((x_pos < third) && (x_pos >= second))
        {
            update_scene_number(2,x_pos);
        }
        if ((x_pos < fourth) && (x_pos >= third))
        { update_scene_number(3,x_pos); }
        if ((x_pos < fifth) && (x_pos >= fourth))
        { update_scene_number(4,x_pos); }
        if ((x_pos < sixth) && (x_pos >= fifth))
        { update_scene_number(5,x_pos); }
        if (x_pos >= sixth)
        {
            update_scene_number(6,x_pos);
        }
    }
    void update_scene_number(int num,float x_pos)
    {
        //print(num);
        if (num == 1)
        {
            if (!music_1.isPlaying) { music_1.Play(); }
            music_4.Stop();
            music_2.Stop();
            music_3.Stop();
            music_5.Stop();
            music_6.Stop();
            background_1.SetActive(true);

            //background_2.active=false;
            //background_3.active=false;
            //background_4.active=false;
        }
        if (num == 2)
        {
            background_2.SetActive(true);
            if (!music_2.isPlaying) { music_2.Play(); }
            music_4.Stop();
            music_1.Stop();
            music_3.Stop();
             music_5.Stop();
            music_6.Stop();

            //background_1.active=false;
            // background_3.active=false;
            //background_4.active=false;
        }
        if (num == 3)
        {
            if (!music_3.isPlaying) { music_3.Play(); }
            music_4.Stop();
            music_2.Stop();
            music_1.Stop();
            music_5.Stop();
            music_6.Stop();
            background_3.SetActive(true);

            // background_1.active=false;
            // background_2.active=false;
            // background_4.active=false;
        }
        if (num == 4)
        {
            if((! isintimer)&&(x_pos<=270)&&(x_pos>=230)){
                isintimer=true;
                timer_canvas.SetActive(true);
                timer.SetDuration(time_to_maze*100).Begin();
            time_master.next_point();
            //chase_me_test.GetComponent<Animator>().Play();
            }
            if(timer.IsPaused){
                times_up();
            }

            if (!music_4.isPlaying) { music_4.Play(); }
            music_1.Stop();
            music_2.Stop();
            music_3.Stop();
            music_5.Stop();
            music_6.Stop();
            //background_1.active=false;
            background_2.SetActive(false);
            background_3.SetActive(false);
            background_4.SetActive(true);
            background_5.SetActive(false);
            background_6.SetActive(false);

        }
        if (num == 5)
        {
            timer_canvas.SetActive(false);
            if(! isinmaze){
                print(camHeight);
                isinmaze=true;
                Camera.main.orthographicSize=4;
                camHeight = 4;
                //camWidth = camHeight * Camera.main.aspect;
            }
            if (!music_5.isPlaying) { music_5.Play(); }
            music_1.Stop();
            music_2.Stop();
            music_3.Stop();
            music_4.Stop();
            music_6.Stop();
            //background_1.active=false;
            background_2.SetActive(false);
            background_3.SetActive(false);
            background_4.SetActive(false);
            background_5.SetActive(true);
        }
        if (num == 6)
        {
            print("666");
            Camera.main.orthographicSize=8;
            camHeight = Camera.main.orthographicSize*2;
            if (!music_6.isPlaying) { music_6.Play(); }
            music_1.Stop();
            music_2.Stop();
            music_3.Stop();
            music_5.Stop();
            music_4.Stop();
            //background_1.active=false;
            background_2.SetActive(false);
            background_3.SetActive(false);
            background_4.SetActive(false);
            background_5.SetActive(false);
            background_6.SetActive(true);
        }
    }

    void times_up(){
        SceneManager.LoadScene("GameOver");
    }
}
