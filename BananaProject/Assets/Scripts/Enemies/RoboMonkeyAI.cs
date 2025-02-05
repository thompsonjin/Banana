using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboMonkeyAI : MonoBehaviour
{
    [Header("References")]
    private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform turnCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement")]
    private Vector2 moveDir;
    [SerializeField] private float speed;
    private bool isFacingRight;
    private bool patrol;
    private bool turn;

    [Header("Combat")]
    private bool hit;
    [SerializeField] private float hitTimer;


    // Start is called before the first frame update
    void Awake()
    {
      player = GameObject.Find("Player");
      patrol = true;
    }

    // Update is called once per frame
    void Update()
    {
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
        moveDir = this.transform.position - player.transform.position;
        moveDir.Normalize();
      }
      
        
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
}
