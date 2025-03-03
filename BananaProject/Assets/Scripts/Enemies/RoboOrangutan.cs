using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboOrangutan : BaseEnemy
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform turnCheck;
    [SerializeField] private LayerMask groundLayer;
    private GameObject player;
    private PlayerController p_Con;

    [Header("Movement")]
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;
    private Vector2 moveDir;
    private bool isFacingRight;
    private bool turn;

    [Header("Combat")]
    private const float PLAYER_HIT_TIME = .8f;
    private float playerHitTimer;
    [SerializeField] private Shield shield;

    // Start is called before the first frame update
    void Awake()
    {
        playerHitTimer = PLAYER_HIT_TIME;
        player = GameObject.Find("Player");
        p_Con = player.GetComponent<PlayerController>();
        shield = transform.GetChild(3).gameObject.GetComponent<Shield>();
        patrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if the player hasnt been spotted patrol the platform as normal
        if (patrol)
        {
            if (ShouldTurn())
            {
                turn = !turn;

                if (turn)
                {
                    moveDir = new Vector2(1, 0);
                }
                else
                {
                    moveDir = new Vector2(-1, 0);
                }
            }
        }
        else
        {
            //otherwise track the players location
            moveDir = this.transform.position - player.transform.position;
            moveDir.Normalize();           
        }


        Attack();
        Flip();
    }


    //MOVEMENT FUNCTIONS 
    private void FixedUpdate()
    {
        if (!hit && IsGrounded() && !patrol)
        {
            rb.velocity = new Vector2(-moveDir.x * chaseSpeed, rb.velocity.y);
        }
        else if (!hit && IsGrounded() && patrol)
        {
            rb.velocity = new Vector2(-moveDir.x * patrolSpeed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        //Use groundCheck transform to check whether or not the enemy is touching the gorund
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool ShouldTurn()
    {
        //Use groundCheck transform to check whether or not the enemy is touching the gorund
        return !Physics2D.OverlapCircle(turnCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        //Rotate the character along the y axis based on the last horizontal input of the player
        if (isFacingRight && moveDir.x < 0f || !isFacingRight && moveDir.x > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }



    //COMBAT FUNCTIONS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Throwable")
        {
            turn = !turn;
        }
    }

    private void Attack()
    {
        playerHitTimer -= Time.deltaTime;

        if (playerHitTimer <= 0)
        {
            shield.SetHit(true);
            playerHitTimer = PLAYER_HIT_TIME;
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
