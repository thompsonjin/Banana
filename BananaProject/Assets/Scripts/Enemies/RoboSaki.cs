using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class RoboSaki : BaseEnemy
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform turnCheck;
    [SerializeField] private LayerMask groundLayer;
    private GameObject player;
    private PlayerController p_Con;
    public Animator anim;

    [Header("Movement")]
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;
    private Vector2 moveDir;
    private bool isFacingRight;
    private bool turn;

    [Header("Combat")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerLayer;
    private bool inRange;
    private const float PLAYER_HIT_TIME = .8f;
    private float playerHitTimer;
    public GameObject soundWave;
    private bool soundSpawn;
    private float reloadTimer;
    private const float RELOAD = 2;

    // Start is called before the first frame update
    void Awake()
    {
        
        player = GameObject.Find("Player");
        p_Con = player.GetComponent<PlayerController>();
        SetPatrol(true);    
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

            //check if the player is in range to hit
            if (HitRange())
            {
                inRange = true;
            }
            else
            {
                inRange = false;
            }
        }

        //give a grace period then attempt to damage the player if they are still within range if not reset
        if (inRange)
        {
            anim.SetBool("Shoot", true);
            if(!soundSpawn && reloadTimer <= 0)
            {
                playerHitTimer = PLAYER_HIT_TIME;
                Instantiate(soundWave, attackPoint);
                reloadTimer = RELOAD;
                soundSpawn = true;
            }
        }

        playerHitTimer -= Time.deltaTime;

        if(playerHitTimer <= 0)
        {
            anim.SetBool("Shoot", false);
            if (soundSpawn)
            {
                Destroy(attackPoint.GetChild(0).gameObject);
                soundSpawn = false;
            }
            reloadTimer -= Time.deltaTime;
        }



        //track how long the enemy is stunned by the hit 
        if (hit)
        {
            SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
            sprite.color = Color.red;

            hitTimer -= Time.deltaTime;

            if (hitTimer <= 0)
            {
                hit = false;
            }
        }
        else
        {
            SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
            sprite.color = Color.white;
        }

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
    private bool HitRange()
    {
        Collider2D playerHit = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (playerHit != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wall")
        {
            turn = !turn;
        }
    }
}
