using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{
    /*static bool player2choose = true;*/
    private GameMaster gm;
    public float jumpVelocity;
    public float bounceVelocity;
    public Vector2 velocity;
    public float gravity;

    public LayerMask wallMask;
    public LayerMask floorMask;


    private bool walk, walk_left, walk_right, jump;

    public enum PlayerState
    {
        jumping,
        idle,
        walking,
        bouncing
    }

    private PlayerState playerState = PlayerState.idle;
    private bool grounded = false;
    private bool bounce = false;

    private float PlayersDist;
    private float pivot;
    private bool inside_timer;
    public GameObject left_wall;



    public GameObject maze_start_floor;
    public GameObject maze_start_floor2;

    // Start is called before the first frame update
    void Start()
    {
        print("player2");
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPosPlayerTwo;
   
    }

    // Update is called once per frame
    void Update()
    {
        //we put boo first keys if he is the only player, or two player and he is the first one
        if ((!GameMaster.isMarioCheck() && !GameMaster.is2Players) || (GameMaster.is2Players)&& !GameMaster.marioFirst)
        {
            CheckPlayer1Input();
        }
        //means we're 2 players and boo second keys
        else 
        {
            CheckPlayer2Input();
        }

        UpdatePlayerPosition();
        UpdateAnimationStates();
        /*update_scene();*/
    }

   
    void UpdatePlayerPosition()
    {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.localScale;



        //update distance beween players
        GameObject player1 = GameObject.FindGameObjectWithTag("Player");
        if (player1)
        {
            pivot = this.gameObject.transform.position.x - player1.transform.position.x;
        }
        
        // stuck behavior if the players are too far (camera)!

        //means that player2 is to the right of player 1
        if (pivot > 0)
        {
            pos.x = pos.x - 0.001f;
        }
        if (pivot < 0)
        {
            pos.x = pos.x + 0.001f;
        }
        
        if (player1 && player1.activeSelf)
        {
            PlayersDist = Mathf.Abs(this.gameObject.transform.position.x - player1.transform.position.x);
        }

        if (PlayersDist >= 28)
        {
            transform.localPosition = pos;
            return;
        }
        if (pivot > 0)
        {
            pos.x = pos.x + 0.001f;
        }
        if (pivot < 0)
        {
            pos.x = pos.x - 0.001f;
        }



        

        if (walk)
        {
            if (walk_left)
            {
                pos.x -= velocity.x * Time.deltaTime;
                scale.x = -1;
            }

            if (walk_right)
            {
                pos.x += velocity.x * Time.deltaTime;
                scale.x = 1;
            }

            pos = CheckWallRays(pos, scale.x);
        }

        if (jump && playerState != PlayerState.jumping)
        {
            playerState = PlayerState.jumping;
            velocity = new Vector2(velocity.x, jumpVelocity);
        }

        if (playerState == PlayerState.jumping)
        {
            pos.y += velocity.y * Time.deltaTime;
            velocity.y -= gravity * Time.deltaTime;
        }

        if (bounce && playerState != PlayerState.bouncing)
        {
            playerState = PlayerState.bouncing;
            velocity = new Vector2(velocity.x, bounceVelocity);
        }

        if (playerState == PlayerState.bouncing)
        {
            pos.y += velocity.y * Time.deltaTime;
            velocity.y -= gravity * Time.deltaTime;
        }

        if (velocity.y <= 0)
        {
            pos = CheckFloorRays(pos);
        }


        if (velocity.y >= 0)
        {
            pos = CheckCeilingRays(pos);
        }


        transform.localPosition = pos;
        transform.localScale = scale;
        if((pos.x>3+left_wall.transform.position.x)&&(! inside_timer)){
           active_wall(true,left_wall);
        }

        if((pos.x>3+maze_start_floor.transform.position.x)&&(! inside_timer)){
            //print("kk");
           active_wall(false,maze_start_floor);
           active_wall(true,maze_start_floor2);
        }
        
    }
    void active_wall(bool active, GameObject wall){
        wall.SetActive(active);
    }

    void UpdateAnimationStates()
    {
        if (grounded && !walk && !bounce)
        {
            GetComponent<Animator>().SetBool("isFlying", false);
            GetComponent<Animator>().SetBool("Idle", true);
            GetComponent<Animator>().SetBool("isHappy", false);
        }
        if (grounded && walk)
        {
            GetComponent<Animator>().SetBool("isFlying", true);
            GetComponent<Animator>().SetBool("Idle", false);
            GetComponent<Animator>().SetBool("isHappy", false);
        }

        if (playerState == PlayerState.jumping)
        {
            GetComponent<Animator>().SetBool("isFlying", true);
            GetComponent<Animator>().SetBool("Idle", false);
            GetComponent<Animator>().SetBool("isHappy", true);

        }
    }

    /*   private void OnTriggerEnter2D(Collider2D collision)
       {
           // if we one player enters the checkpoint, we want theuy both get the new checkpoint
           if (collision.CompareTag("Enemy"))
           {

                   transform.gameObject.SetActive(false);
                   *//*SceneManager.LoadScene("GameOver");*//*

           }

       }*/

    void CheckPlayer1Input()
    {
        bool input_left = Input.GetKey(KeyCode.LeftArrow);
        bool input_right = Input.GetKey(KeyCode.RightArrow);
        bool input_space = Input.GetKeyDown(KeyCode.Space);

        walk = input_left || input_right;

        walk_left = input_left && !input_right;
        walk_right = input_right && !input_left;
        jump = input_space;
    }

    void CheckPlayer2Input()
    {
        bool input_left = Input.GetKey(KeyCode.A);
        bool input_right = Input.GetKey(KeyCode.D);
        bool input_space = Input.GetKeyDown(KeyCode.Tab);

        walk = input_left || input_right;

        walk_left = input_left && !input_right;
        walk_right = input_right && !input_left;
        jump = input_space;
    }

    Vector3 CheckWallRays(Vector3 pos, float direction)
    {
        Vector2 originTop = new Vector2(pos.x + direction * .4f, pos.y + 1f - 0.2f);
        Vector2 originMiddle = new Vector2(pos.x + direction * .4f, pos.y);
        Vector2 originBottom = new Vector2(pos.x + direction * .4f, pos.y - 1f + 0.2f);

        RaycastHit2D wallTop = Physics2D.Raycast(originTop, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallMiddle = Physics2D.Raycast(originMiddle, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallBottom = Physics2D.Raycast(originBottom, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);

        if (wallTop.collider != null || wallMiddle.collider != null || wallBottom.collider != null)
        {
            pos.x -= velocity.x * Time.deltaTime * direction;

            /*  RaycastHit2D hitRay = wallTop;

              if (wallTop)
              {
                  hitRay = wallTop;
              }
              else if (wallMiddle)
              {
                  hitRay = wallMiddle;
              }
              else if (wallBottom)
              {
                  hitRay = wallBottom;
              }

              if (hitRay.collider.tag == "Enemy")
              {
                  Application.LoadLevel("GameOver");
              }*/

        }
        return pos;

    }

    Vector3 CheckFloorRays(Vector3 pos)
    {
        Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y - 1f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y - 1.001f);
        Vector2 originRight = new Vector2(pos.x + 0.5f - 0.2f, pos.y - 1f);

        RaycastHit2D floorLeft = Physics2D.Raycast(originLeft, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D floorMiddle = Physics2D.Raycast(originMiddle, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D floorRight = Physics2D.Raycast(originRight, Vector2.down, velocity.y * Time.deltaTime, floorMask);

        if (floorLeft.collider != null || floorMiddle.collider != null || floorRight.collider != null)
        {
            RaycastHit2D hitRay = floorRight;

            if (floorLeft)
            {
                hitRay = floorLeft;
            }
            else if (floorMiddle)
            {
                hitRay = floorMiddle;
            }
            else if (floorRight)
            {
                hitRay = floorRight;
            }

            if (hitRay.collider.tag == "Enemy")
            {
                bounce = true;
                hitRay.collider.GetComponent<EnemyAI>().Crush();

            }

            playerState = PlayerState.idle;
            grounded = true;
            velocity.y = 0;

            //because the center is the pivot, we want to set the top of the bounds.
            // this is why we divide by 2, we add 1 because we set the player center position, we want his LEGS to be on the bounds.
            pos.y = hitRay.collider.bounds.center.y + hitRay.collider.bounds.size.y / 2 + 1;


        }
        else
        {
            if (playerState != PlayerState.jumping)
            {
                Fall();
            }
        }

        return pos;
    }

    Vector3 CheckCeilingRays(Vector3 pos)
    {
        Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y + 1f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y + 1f);
        Vector2 originRight = new Vector2(pos.x + 0.5f - 0.2f, pos.y + 1f);

        RaycastHit2D ceilLeft = Physics2D.Raycast(originLeft, Vector2.up, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D ceilMiddle = Physics2D.Raycast(originMiddle, Vector2.up, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D ceilRight = Physics2D.Raycast(originRight, Vector2.up, velocity.y * Time.deltaTime, floorMask);

        if (ceilLeft.collider != null || ceilMiddle.collider != null || ceilRight.collider != null)
        {
            RaycastHit2D hitRay = ceilLeft;
            if (ceilLeft)
            {
                hitRay = ceilLeft;
            }
            else if (ceilMiddle)
            {
                hitRay = ceilMiddle;
            }
            else if (ceilRight)
            {
                hitRay = ceilRight;
            }

            if (hitRay.collider.tag == "QuestionBlock")
            {
                hitRay.collider.GetComponent<QuestionBlock>().QuestionBlockBounce();
            }
            if (hitRay.collider.tag == "Enemy")
            {
                transform.gameObject.SetActive(false);
                /*SceneManager.LoadScene("GameOver");*/
            }

            pos.y = hitRay.collider.bounds.center.y - hitRay.collider.bounds.size.y / 2 - 1;

            Fall();
        }
        return pos;
    }



    void Fall()
    {
        velocity.y = 0;

        playerState = PlayerState.jumping;
        bounce = false;
        grounded = false;

    }
}
