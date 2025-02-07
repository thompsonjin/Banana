using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    private float health;
    
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private float horizontal;
    //Grace period where jump input is still registered after falling off a platform
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;
    //Grace period where jump input is still registered before touching a platform
    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;
    public bool isFacingRight;

    [Header("Combat")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    private int comboCount;
    [SerializeField] private float punchKnockback;
    [SerializeField] private float uppercutKnockback;
    [SerializeField] private float kickKnockback;

    
   void Awake()
   {
      health = maxHealth;
   } 

   void Update()
   {
      //Get the players left and right input to calculate the force that needs to be applied
      horizontal = Input.GetAxisRaw("Horizontal");

      //Manage coyote and jump buffer timers to give the player some leeway with jump inputs
      if(IsGrounded())
      {
       coyoteTimeCounter = coyoteTime;
      }
      else
      {
       coyoteTimeCounter -= Time.deltaTime;
      }

      if(Input.GetKeyDown(KeyCode.Space))
      {
        jumpBufferCounter = jumpBufferTime;
      }
      else
      {
        jumpBufferCounter -= Time.deltaTime;
      }

      if(jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
      {
         //if the player is allowed to jump apply jump power to the player's velocity
         rb.velocity = new Vector2(rb.velocity.x, jumpPower);

         jumpBufferCounter = 0f;
      }

      //jump height is variable based on how long the player holds space
      if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
      {
         rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

         coyoteTimeCounter = 0f;
      }


      //Punch
      if(Input.GetMouseButtonDown(0))
      {
         BasicAttack();
      }

      //Kick
      if(Input.GetMouseButtonDown(1))
      {
         KickAttack();
      }

      Flip();
   }

   //MOVEMENT FUNCTIONS
   private void FixedUpdate()
   {
      //apply the product of horizontal and speed to the players current velocity
      rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
   }

   private bool IsGrounded()
   {
      //Use groundCheck transform to check whether or not the player is touching the gorund
      return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
   }

   private void Flip()
   {
        //Rotate the character along the y axis based on the last horizontal input of the player
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
   }

   //COMBAT FUNCTIONS
   //deal damage and knockback on each attack with the damage doubled every three hits
   private void BasicAttack()
   {
      comboCount++;

      if(comboCount == 3)
      {
         

         foreach(Collider2D col in HitRange())
         {
            RoboMonkeyAI e_Ai = col.gameObject.GetComponent<RoboMonkeyAI>();
            EnemyHealth e_Health = col.gameObject.GetComponent<EnemyHealth>();
            Rigidbody2D e_Rigid = col.gameObject.GetComponent<Rigidbody2D>();

            e_Ai.SetHit();
            e_Health.Damage(4);

            //find the orientation of the hit enemy relative to the player
            Vector2 forceDir = this.transform.position - col.gameObject.transform.position;
            forceDir.Normalize();

            //using given direction change values to appropriate force
            Vector2 uppercutForce = new Vector2(-forceDir.x * (uppercutKnockback * (Mathf.Abs(rb.velocity.x / 10) + 1)), 20);

            e_Rigid.AddForce(uppercutForce, ForceMode2D.Impulse);
         }  

         comboCount = 0;  
      }
      else
      {
         foreach(Collider2D col in HitRange())
         {
            RoboMonkeyAI e_Ai = col.gameObject.GetComponent<RoboMonkeyAI>();
            EnemyHealth e_Health = col.gameObject.GetComponent<EnemyHealth>();
            Rigidbody2D e_Rigid = col.gameObject.GetComponent<Rigidbody2D>();

            e_Ai.SetHit();
            e_Health.Damage(2);

            //find the orientation of the hit enemy relative to the player
            Vector2 forceDir = this.transform.position - col.gameObject.transform.position;
            forceDir.Normalize();

            //using given direction change values to appropriate force
            Vector2 punchForce = new Vector2(-forceDir.x * (punchKnockback * (Mathf.Abs(rb.velocity.x / 10) + 1)), 5);

            e_Rigid.AddForce(punchForce, ForceMode2D.Impulse);
         }    
      }      
   }

   private void KickAttack()
   {
      foreach(Collider2D col in HitRange())
         {
            RoboMonkeyAI e_Ai = col.gameObject.GetComponent<RoboMonkeyAI>();
            EnemyHealth e_Health = col.gameObject.GetComponent<EnemyHealth>();
            Rigidbody2D e_Rigid = col.gameObject.GetComponent<Rigidbody2D>();

            e_Ai.SetHit();
            e_Health.Damage(6);

            //find the orientation of the hit enemy relative to the player
            Vector2 forceDir = this.transform.position - col.gameObject.transform.position;
            forceDir.Normalize();

            //using given direction change values to appropriate force
            Vector2 kickForce = new Vector2(-forceDir.x * (kickKnockback * (Mathf.Abs(rb.velocity.x / 10) + 1)), 5);

            e_Rigid.AddForce(kickForce, ForceMode2D.Impulse);
         }  
   }

   //Find all the enemies within the monkey's effective damage range
   private Collider2D[] HitRange()
   {
      Collider2D[] enemiesHit =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

      return enemiesHit;       
   }

   void OnDrawGizmosSelected()
   {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
   }

   
   //HEALTH FUNCTIONS
   public void TakeDamage()
   {
      health--;

      if(health <= 0)
      {
         Debug.Log("You Are Dead");
      }
      else
      {
         Debug.Log("DAMAGE");
      }
   }

   public void GainHealth(int h)
   {
      health += h;

      if(health >= maxHealth)
      {
         health = maxHealth;
      }
   }
}
