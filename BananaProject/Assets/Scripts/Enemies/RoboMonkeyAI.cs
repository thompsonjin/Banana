using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboMonkeyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform turnCheck;
    [SerializeField] private LayerMask groundLayer;
    private GameObject player;
    private PlayerController p_Con;

    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector2 moveDir;
    private bool isFacingRight;
    private bool patrol;
    private bool turn;

    [Header("Combat")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerLayer;
    private bool inRange;
    private const float PLAYER_HIT_TIME = 1;
    private float playerHitTimer;
    private bool hit;
    private float hitTimer;


    // Start is called before the first frame update
    void Awake()
    {
      playerHitTimer = PLAYER_HIT_TIME;
      player = GameObject.Find("Player");
      p_Con = player.GetComponent<PlayerController>();
      patrol = true;
    }

    // Update is called once per frame
    void Update()
    {
      //if the player hasnt been spotted patrol the platform as normal
      if(patrol)
      {
        if(ShouldTurn())
        {
          turn = !turn;

          if(turn)
          {
            moveDir = new Vector2(1,0);
          }
          else
          {
            moveDir = new Vector2(-1,0);
          }
        }
      }
      else
      {
        //otherwise track the players location
        moveDir = this.transform.position - player.transform.position;
        moveDir.Normalize();

        //check if the player is in range to hit
        if(HitRange())
        {
          inRange = true;
        }
      }
      
      //give a grace period then attempt to damage the player if they are still within range if not reset
      if(inRange)
      {
        playerHitTimer -= Time.deltaTime;

        if(playerHitTimer <= 0)
        {
          if(HitRange())
          {
            p_Con.TakeDamage();
            playerHitTimer = PLAYER_HIT_TIME;
          }
          else
          {
            playerHitTimer = PLAYER_HIT_TIME;
            inRange = false;
          }
        }
        
      }

      //track how long the enemy is stunned by the hit 
      if(hit)
      {
        hitTimer -= Time.deltaTime;

        if(hitTimer <= 0)
        {
          hit = false;     
        }
      }

      Flip();
    }


   //MOVEMENT FUNCTIONS 
   private void FixedUpdate()
   {   
      if(!hit && IsGrounded())
      {
        rb.velocity = new Vector2(-moveDir.x * speed, rb.velocity.y);
      }         
   }

   public void SetHit()
   {
     hit = !hit;
     hitTimer = .3f;
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
      if(isFacingRight && moveDir.x < 0f || !isFacingRight && moveDir.x > 0f)
      {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
      }
   }

   public void SetPatrol(bool b)
   {
     patrol = b;
   }
   
   //COMBAT FUNCTIONS
   private bool HitRange()
   {
      Collider2D playerHit =  Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

      if(playerHit != null)
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
}
