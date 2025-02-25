using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject cameraFollowTarget;
    private CameraFollowTarget camFT;

    [Header("Health")]
    [SerializeField] private int maxHealth;
    private int health;
    //HEALTH UI
    [SerializeField] private Image[] displayHealth = new Image[3];

    [Header("Resources")]
    //BANANA UI
    [SerializeField] private Image[] displayBananas = new Image[5];
    [SerializeField] private Slider chargeBar;
    [SerializeField] private Slider powerSlider;
    private float bananaRegenTimer;
    [SerializeField] private float bananaRegen;

    [Header("Movement")]
    [SerializeField] private float normalSpeed;
    private float currentSpeed;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float jumpPower;
    private float horizontal;
    private float vertical;
    private bool vines;
    private bool isClimbing;
    //Grace period where jump input is still registered after falling off a platform
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;
    //Grace period where jump input is still registered before touching a platform
    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;
    public bool isFacingRight;

    [Header("Combat main stats")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    private int comboCount;

    [Header("Kick")]
    //Kick
    private float kickPower;
    [SerializeField] private float kickPowerMax;
    bool kickPowered;
    //Shadow Kick
    [SerializeField] private float shadowKickKnockback;
    //How far the player will go with the shadow kick
    [SerializeField] private float shadowKickDistance;
    [SerializeField] private bool hasShadowKick = false;
    private float shadowKickPower;
    //Seconds needed to charge the kick
    [SerializeField] private float shadowKickMaxCharge;
    private bool isChargingShadowKick;
    [SerializeField] private float shadowKickChargeRate = 1f;

    [Header("Charge")]
    //Charge
    private float chargePower;
    [SerializeField]private float chargePowerMax;
    private float chargePowerTimer;
    private bool aura;
    public SpriteRenderer sprite;
    private bool canCharge;
    //GROUND POUND
    private bool groundPound;
    private float recoverTime;

    [Header("Knockback stats")]
    //KNOCKBACK VALUES
    [SerializeField] private float punchKnockback;
    [SerializeField] private float uppercutKnockback;
    [SerializeField] private float kickKnockback;
    [SerializeField] private float chargeKnockback;
    [SerializeField] private int maxBananas;
    private int bananaCount;

    [Header("Throwing Mechanics")]
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float pickupRange = 1.5f;
    [SerializeField] private Transform carryPoint;
    [SerializeField] private LayerMask throwableLayer;
    private ThrowableObject carriedObject;
    private bool isCarrying = false;


    void Awake()
   {
      health = maxHealth;
      bananaCount = maxBananas;
      currentSpeed = normalSpeed;

      chargePowerTimer = 0;
      camFT = cameraFollowTarget.GetComponent<CameraFollowTarget>();
   } 

   void Update()
   {
      //Get the players left and right input to calculate the force that needs to be applied
      horizontal = Input.GetAxisRaw("Horizontal");
      vertical = Input.GetAxisRaw("Vertical");

      //Manage coyote and jump buffer timers to give the player some leeway with jump inputs
      if (IsGrounded() || isClimbing)
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

        isClimbing = false;
        rb.gravityScale = 7;
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

      //Climb
      if(vines && Input.GetKeyDown(KeyCode.W))
      {
         isClimbing = true;
         rb.gravityScale = 0;
      }


      //Punch
      if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Y))
      {
         BasicAttack();
      }

        //Regular Kick
        if (Input.GetKeyDown(KeyCode.B) && IsGrounded())
        {
            BasicKick();
        }

        // Shadow kick charging
        if (Input.GetMouseButton(1) && hasShadowKick && IsGrounded() || Input.GetKey(KeyCode.B) && hasShadowKick && IsGrounded())
        {
            if (bananaCount > 0)
            {
                isChargingShadowKick = true;
                shadowKickPower += Time.deltaTime * shadowKickChargeRate;
                powerSlider.value = shadowKickPower / shadowKickMaxCharge;

                if (shadowKickPower >= shadowKickMaxCharge)
                {
                    shadowKickPower = shadowKickMaxCharge;
                }
            }
        }

        if (Input.GetMouseButtonUp(1) && isChargingShadowKick || Input.GetKeyUp(KeyCode.B) && isChargingShadowKick)
        {
            ShadowKick();
            UseBanana(1);
            isChargingShadowKick = false;
            shadowKickPower = 0;
            powerSlider.value = 0;
        }

        //Ground Pound
        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.S) && !IsGrounded() || Input.GetKeyDown(KeyCode.Y) && Input.GetKey(KeyCode.S) && !IsGrounded())
        {
            if (bananaCount > 0)
            {
                groundPound = true;
                SetAura(true);
                UseBanana(1);
            }      
        }

      //Charge


        if (Input.GetKeyDown(KeyCode.X))
        {
            if(bananaCount >= 3)
            {
                UseBanana(3);
                canCharge = true;
            }
        }

        if (Input.GetKey(KeyCode.X) && canCharge)
        {
            ChargeAttack();      
        }

        if(Input.GetKeyUp(KeyCode.X))
        {
            chargePower = 0;
            chargePowerTimer = 0;
            currentSpeed = normalSpeed;
            chargeBar.value = 0;
            SetAura(false);
            canCharge = false;
        }

        //METHOD TO CHECK FOR THROWABLE OBJECTS
        CheckPickupAndThrow();

        //TEMPORARY HEAL BUTTON
        if (Input.GetKeyDown(KeyCode.F))
      {
         GainHealth(1);
      }

        //BANANA REGENERATION
        if (!canCharge)
        {
            bananaRegenTimer += Time.deltaTime;
            if (bananaRegenTimer > bananaRegen)
            {
                if (bananaCount < maxBananas)
                {
                    GiveBanana(1);
                }

                bananaRegenTimer = 0;
            }
        }  

      Flip();
   }

   //MOVEMENT FUNCTIONS
   private void FixedUpdate()
   {
        if(groundPound)
        {
            //apply a force directly down and lock horizontal movement
            rb.velocity = new Vector2(rb.velocity.x, -50);
            if(IsGrounded())
            {
                recoverTime += Time.deltaTime;
                if(recoverTime > .5f)
                {
                    SetAura(false);
                    recoverTime = 0;
                    groundPound = false;
                }          
            }
        }
        else if(isClimbing)
        {
            //apply the product of horizontal and speed to the players current velocity
            rb.velocity = new Vector2(horizontal * currentSpeed, vertical * currentSpeed);
        }
        else
        {
            //apply the product of horizontal and speed to the players current velocity
            rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);
        }  
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

            camFT.CallTurn();
        }
   }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Vines")
        {           
            vines = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vines")
        {
            rb.gravityScale = 7;
            vines = false;

            isClimbing = false;
        }
    }

    //COMBAT FUNCTIONS
    //deal damage and knockback on each attack with the damage doubled every three hits
    private void BasicAttack()
   {
      comboCount++;

      if(comboCount == 3)
      {
        foreach (Collider2D col in HitRange())
        {
            if(HitRange() != null)
            {
                RoboMonkeyAI e_Ai = col.gameObject.GetComponent<RoboMonkeyAI>();
                EnemyHealth e_Health = col.gameObject.GetComponent<EnemyHealth>();
                Rigidbody2D e_Rigid = col.gameObject.GetComponent<Rigidbody2D>();

                
                if (e_Ai != null)
                {
                    e_Ai.SetHit();
                    e_Ai.SetPatrol(false);
                    e_Health.Damage(4);
                    //find the orientation of the hit enemy relative to the player
                    Vector2 forceDir = this.transform.position - col.gameObject.transform.position;
                    forceDir.Normalize();

                    //using given direction change values to appropriate force
                    Vector2 uppercutForce = new Vector2(-forceDir.x * (uppercutKnockback * (Mathf.Abs(rb.velocity.x / 10) + 1)), 20);

                    e_Rigid.AddForce(uppercutForce, ForceMode2D.Impulse);

                }

            }       
           
        }

        comboCount = 0;       
      }
      else
      {
        foreach (Collider2D col in HitRange())
        {
            if(HitRange() != null)
            {
                RoboMonkeyAI e_Ai = col.gameObject.GetComponent<RoboMonkeyAI>();
                EnemyHealth e_Health = col.gameObject.GetComponent<EnemyHealth>();
                Rigidbody2D e_Rigid = col.gameObject.GetComponent<Rigidbody2D>();

                if(e_Ai != null)
                {
                    e_Ai.SetHit();
                    e_Ai.SetPatrol(false);
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
        
      }      
   }


    //Basic kick call
   private void BasicKick()
   {
        foreach (Collider2D col in HitRange())
        {
            if (col.TryGetComponent<RoboMonkeyAI>(out RoboMonkeyAI e_Ai))
            {
                e_Ai.SetHit();
                e_Ai.SetPatrol(false);
                col.GetComponent<EnemyHealth>().Damage(2);

                Vector2 forceDir = this.transform.position - col.transform.position;
                forceDir.Normalize();
                Vector2 kickForce = new Vector2(-forceDir.x * kickKnockback * (Mathf.Abs(rb.velocity.x / 10) + 1), 5);
                col.GetComponent<Rigidbody2D>().AddForce(kickForce, ForceMode2D.Impulse);
            }
        }
    }

    //Shadow Kick mechanic
    private void ShadowKick()
    {
        if (shadowKickPower <= 0) return;

        //Calculate charge ratio and movement distance
        float chargeRatio = shadowKickPower / shadowKickMaxCharge;
        float moveDistance = shadowKickDistance * chargeRatio;
        Vector2 moveDirection = isFacingRight ? Vector2.right : Vector2.left;

        Vector2 currentPos = transform.position;
        Vector2 movement = moveDirection * moveDistance;
        Vector2 intendedPos = currentPos + movement;

        //Debug purposes
        Debug.Log($"Starting ShadowKick - Current: {currentPos}, Intended: {intendedPos}, Distance: {moveDistance}");

        //Create box for enemy detection
        Vector2 boxSize = new Vector2(0.5f, 1f);
        Vector2 boxCenter = currentPos + (moveDirection * (moveDistance / 2));

        //Debug purposes
        Debug.DrawLine(currentPos, intendedPos, Color.red, 1f);

        //Check for enemies in the path
        Collider2D hitCollider = Physics2D.OverlapBox(boxCenter, new Vector2(moveDistance, 1f), 0f, enemyLayer);

        if (hitCollider != null)
        {
            Debug.Log($"Hit enemy: {hitCollider.name} at position: {hitCollider.transform.position}");

            //Calculate distance to enemy
            float distanceToEnemy = Vector2.Distance(currentPos, hitCollider.transform.position);
            Vector2 finalPos = currentPos + (moveDirection * (distanceToEnemy - 1f));

            Debug.Log($"Moving to position before enemy: {finalPos}");

            //Handle enemy interaction
            if (hitCollider.TryGetComponent<RoboMonkeyAI>(out RoboMonkeyAI e_Ai))
            {
                e_Ai.SetHit();
                e_Ai.SetPatrol(false);
                hitCollider.GetComponent<EnemyHealth>().Damage(4);

                Vector2 kickForce = moveDirection * shadowKickKnockback * chargeRatio;
                hitCollider.GetComponent<Rigidbody2D>().AddForce(kickForce, ForceMode2D.Impulse);
            }

            //Move player
            transform.position = new Vector3(finalPos.x, finalPos.y, transform.position.z);
        }
        else
        {
            //Debug purposes
            Debug.Log("No enemy hit, moving full distance");
            transform.position = new Vector3(intendedPos.x, intendedPos.y, transform.position.z);
        }

        //Reset velocity
        rb.velocity = Vector2.zero;
    }

    //Method to enable and disable Shadow Kick
    public void EnableShadowKick()
    {
        hasShadowKick = true;
    }

    public void ChargeAttack()
    { //Hold to charge dash
        if (chargePower < chargePowerMax)
        {
            currentSpeed = 0;
            chargePower += Time.deltaTime;
            chargePowerTimer += Time.deltaTime;
            chargeBar.value = chargePowerTimer / chargePowerMax;
        }
        else
        {
            if (chargePower >= chargePowerMax)
            {
                //Activate when fully charged
                SetAura(true);
                currentSpeed = chargeSpeed;
                chargePowerTimer -= Time.deltaTime;
                chargeBar.value = chargePowerTimer / chargePowerMax;

                if (chargePowerTimer <= 0)
                {
                    //if holding for longer than the allowed time use more banana shards to extend if no shards cancel ability
                    if (chargePowerTimer <= -1)
                    {
                        if (bananaCount > 0)
                        {
                            UseBanana(1);
                            chargePowerTimer = 0;
                        }
                        else
                        {
                            currentSpeed = normalSpeed;
                            SetAura(false);
                        }
                    }
                }
            }
        }
    }

    //Throw Object Mechanic
    private void CheckPickupAndThrow()
    {
        //Throw
        if (isCarrying && Input.GetMouseButtonDown(0) || isCarrying && Input.GetKeyDown(KeyCode.Y))
        {
            ThrowObject();
        }
        //Pickup
        else if (!isCarrying && Input.GetKeyDown(KeyCode.E))
        {
            TryPickupObject();
        }
    }

    private void TryPickupObject()
    {
        //Check for throwable objects in range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRange, throwableLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<ThrowableObject>(out ThrowableObject throwable))
            {
                //Pick up the object
                carriedObject = throwable;
                carriedObject.OnPickup();

                //Parent to carry point
                throwable.transform.parent = carryPoint;
                throwable.transform.localPosition = Vector3.zero;

                isCarrying = true;
                break;
            }
        }
    }

    private void ThrowObject()
    {
        if (carriedObject != null)
        {
            carriedObject.transform.parent = null;

            //Calculate throw direction based on player facing
            Vector2 throwDirection = isFacingRight ? Vector2.right : Vector2.left;

            //Throw the object
            carriedObject.Throw(throwDirection, throwForce);

            carriedObject = null;
            isCarrying = false;
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

    public void SetAura(bool a)
    {
        if (a)
        {
            aura = true;
            sprite.color = Color.blue;
        }
        else
        {
            aura = false;
            sprite.color = Color.gray;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy" && aura)
        {
            RoboMonkeyAI e_Ai = col.gameObject.GetComponent<RoboMonkeyAI>();
            EnemyHealth e_Health = col.gameObject.GetComponent<EnemyHealth>();
            Rigidbody2D e_Rigid = col.gameObject.GetComponent<Rigidbody2D>();

            e_Ai.SetHit();
            e_Ai.SetPatrol(false);
            if (groundPound)
            {
                e_Health.Damage(5);
            }
            else
            {
                e_Health.Damage(4);
                recoverTime = 0;
            }
            
            //find the orientation of the hit enemy relative to the player
            Vector2 forceDir = this.transform.position - col.gameObject.transform.position;
            forceDir.Normalize();

            //using given direction change values to appropriate force
            Vector2 chargeForce = new Vector2(-forceDir.x * (chargeKnockback * (Mathf.Abs(rb.velocity.x / 10) + 1)), 2);

            e_Rigid.AddForce(chargeForce, ForceMode2D.Impulse);
        }
    }

    //HEALTH MANAGMENT
    public void TakeDamage()
   {
        if (!aura)
        {
            health--;

            if (health > 0)
            {
                Debug.Log("DAMAGE");

                displayHealth[health].enabled = false;
            }
            else if (health == 0)
            {
                Debug.Log("You Are Dead");

                displayHealth[health].enabled = false;
            }
            else
            {
                Debug.Log("You Are Dead");
            }
        }       
   }

   public void GainHealth(int h)
   {
      health += h;

      if(health >= maxHealth)
      {
         health = maxHealth;
      }

      displayHealth[health - 1].enabled = true;
   }

   //BANANA MANAGMENT
   private void UseBanana(int b)
   {
        bananaCount -= b;

        for (int i = 0; i < b; i++)
        {
            displayBananas[bananaCount + i].enabled = false;
        }     
   }

   public void GiveBanana(int b)
   {
      bananaCount += b;

      if(bananaCount > maxBananas)
      {
         bananaCount = maxBananas;
      }
      else
      {
         displayBananas[bananaCount -  1].enabled = true;
      }    
   }
}
