using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Powers")]
    [SerializeField] private bool hasShadowKick = false;
    [SerializeField] private bool hasCharge = false;
    [SerializeField] bool hasGroundPound = false;
    [SerializeField] private bool hasBananaGun = false;
    [SerializeField] private bool hasBananaShield = false;


    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject cameraFollowTarget;
    private CameraFollowTarget camFT;
    [SerializeField]private Animator anim;

    [Header("Health")]
    [SerializeField] private int maxHealth;
    private int health;
    //BANANA SHIELD
    private bool banananaShieldActive = false;
    [SerializeField] private GameObject shieldVisual;
    //HEALTH UI
    [SerializeField] private Image[] displayHealth = new Image[3];

    [Header("Resources")]
    //BANANA UI
    [SerializeField] private List<Image> displayBananas = new List<Image>();
    [SerializeField] private GameObject bananacoinUIprefab;
    [SerializeField] private GameObject emptyBananacoinPrefab;
    [SerializeField] private Transform bananaUIParent;
    [SerializeField] private Slider chargeBar;
    [SerializeField] private Slider powerSlider;
    private float bananaRegenTimer;
    [SerializeField] private float bananaRegen;

    [Header("Movement")]
    [SerializeField] private float slowSpeed;
    [SerializeField] private float normalSpeed;
    private float currentSpeed;
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
    public bool boop;
    private float boopTimer;
    private const float MAX_BOOP_TIME = .5f;
    public bool sakiBoost;
    public GameObject sakiBoostIndicator;
    public bool swim;

    [Header("Combat main stats")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    private int comboCount;
    float reload;
    public float reloadTime;

    [Header("Kick")]
    //Kick
    private float kickPower;
    [SerializeField] private float kickPowerMax;
    bool kickPowered;
    //Shadow Kick
    [SerializeField] private float shadowKickKnockback;
    private float shadowKickPower;
    private bool isChargingShadowKick;
    private bool isShadowKicking = false;
    [SerializeField] private float baseChargeTime = 1f;
    [SerializeField] private float maxChargeTime = 3f;
    [SerializeField] private float baseKickSpeed = 40f;
    [SerializeField] private float maxKickSpeed = 80f;
    [SerializeField] private float baseKickDistance = 8f;
    [SerializeField] private float maxKickDistance = 16f;


    [Header("Charge")]
    //Charge
    [SerializeField] private float chargeDuration = 6f;
    [SerializeField] private float boostSpeed = 24f;
    private float chargeTimer;
    private bool aura;
    public SpriteRenderer sprite;
    private bool canCharge;
    public CinemachineVirtualCamera v_Cam;
    private float focus = 15;

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
    [SerializeField] private Transform check;

    [Header("Weapon")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private bool isGunPulled = false;
    [SerializeField] private float fireRate = 0.5f;
    private float nextFireTime = 0f;
    [SerializeField] private SpriteRenderer weaponSprite;
    [SerializeField] private float gunDuration = 10f;
    [SerializeField] private float gunTimer = 0f;

    [Header("Checkpoint")]
    public GameObject[] checkpoints;

    void Awake()
    {
        maxBananas = BananaManager.MaxBananas;
        bananaCount = maxBananas;
        InitializeBananaUI();

        health = maxHealth;
        bananaCount = maxBananas;
        currentSpeed = normalSpeed;

        camFT = cameraFollowTarget.GetComponent<CameraFollowTarget>();
        weaponSprite.enabled = false;

       // anim = this.gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        transform.position = checkpoints[CheckpointManager.checkpointNum].transform.position;
    }

    void Update()
    {
      //Get the players left and right input to calculate the force that needs to be applied
      horizontal = Input.GetAxisRaw("Horizontal");
      vertical = Input.GetAxisRaw("Vertical");

        Debug.Log(horizontal);

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
        anim.SetBool("Climbing", false);
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

      if(!IsGrounded() && Input.GetKeyDown(KeyCode.Space) && sakiBoost)
      {
          //if the player is allowed to jump apply jump power to the player's velocity
          rb.velocity = new Vector2(rb.velocity.x, jumpPower * 2);

          jumpBufferCounter = 0f;
          sakiBoost = false;
          sakiBoostIndicator.SetActive(false);
      }

        if (!IsGrounded())
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

      //Climb
      if (vines && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Space))
        {
            isClimbing = true;
            rb.gravityScale = 0;
            anim.SetBool("Climbing", true);
        }


        //Punch
        reload -= Time.deltaTime;
        if(reload <= 0)
        {
            reload = 0;
        }

        if (reload <= 0 && Input.GetKeyDown(KeyCode.J))
        {
            BasicAttack();
            anim.SetBool("Punch", true);
            reload = reloadTime;
        }
        else
        {
            anim.SetBool("Punch", false);
        }

        //Regular Kick
        if (Input.GetKeyDown(KeyCode.K))
        {
            BasicKick();
            anim.SetBool("Kick", true);
        }
        else
        {
            anim.SetBool("Kick", false);
        }

        //Shadow kick charging
        if (Input.GetKey(KeyCode.K) && hasShadowKick)
        {
            if (bananaCount > 0)
            {
                isChargingShadowKick = true;
                shadowKickPower += .01f + Time.deltaTime;

                if (shadowKickPower >= maxChargeTime)
                {
                    shadowKickPower = maxChargeTime;
                    powerSlider.value = 1f;
                }
                else if (shadowKickPower >= baseChargeTime)
                {
                    powerSlider.value = 0.5f;
                }
                else
                {
                    powerSlider.value = (shadowKickPower / baseChargeTime) * 0.5f;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.K) && isChargingShadowKick)
        {

            ShadowKick();
            UseBanana(1);
            isChargingShadowKick = false;
            shadowKickPower = 0;
            powerSlider.value = 0;
        }

        //Ground Pound
        if (Input.GetKeyDown(KeyCode.J) && Input.GetKey(KeyCode.S) && !IsGrounded() && hasGroundPound)
        {
            if (bananaCount > 0)
            {
                groundPound = true;
                anim.SetBool("Ground Pound", true);
                SetAura(true);
                UseBanana(1);
            }
        }
        //Charge
        if (hasCharge)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (bananaCount >= 3)
                {
                    UseBanana(3);
                    canCharge = true;
                    SetAura(true);
                    currentSpeed = boostSpeed;
                    chargeTimer = chargeDuration;
                    chargeBar.value = 1f;
                }
            }

            if (Input.GetKey(KeyCode.L) && canCharge)
            {
                anim.SetBool("Charge", true);
                focus += .1f;

                if(focus >= 28)
                {
                    focus = 28;
                }
                chargeTimer -= Time.deltaTime;
                chargeBar.value = chargeTimer / chargeDuration;
                v_Cam.m_Lens.OrthographicSize = focus;

                if (chargeTimer <= 0)
                {
                    if (bananaCount == 0)
                    {
                        currentSpeed = normalSpeed;
                        chargeBar.value = 0;
                        SetAura(false);
                        canCharge = false;
                        anim.SetBool("Charge", false);
                    }
                    else
                    {
                        UseBanana(1);
                        chargeTimer = chargeDuration;
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.L))
            {
                currentSpeed = normalSpeed;
                chargeBar.value = 0;
                SetAura(false);
                canCharge = false;
                anim.SetBool("Charge", false);
            }

            if (!canCharge)
            {
                focus -= .1f;
                if (focus <= 15)
                {
                    focus = 15;
                }
                v_Cam.m_Lens.OrthographicSize = focus;
            }
        }
        

        //Shooting Banana Gun

        if (Input.GetKeyDown(KeyCode.I) && hasBananaGun && !isGunPulled) 
        {
            if (bananaCount >= 5)
            {
                isGunPulled = true;
                anim.SetBool("Gun", true);
                weaponSprite.enabled = true;
                UseBanana(5);
                gunTimer = gunDuration;
            }
        }

        if (isGunPulled)
        {
            gunTimer -= Time.deltaTime;
            if (gunTimer < 0)
            {
                anim.SetBool("Gun", false);
                isGunPulled = false;
                weaponSprite.enabled = false;
            }
            else if (Input.GetKey(KeyCode.J))
            {
                Shoot();
            }
        }

        //Banana shield handling
        if (hasBananaShield && bananaCount >= maxBananas && !banananaShieldActive)
        {
            banananaShieldActive = true;
            shieldVisual.SetActive(true);
            Debug.Log("Banana Shield ON");
        }
        

        //METHOD TO CHECK FOR THROWABLE OBJECTS
        CheckPickupAndThrow();

        //HEAL
        if (Input.GetKeyDown(KeyCode.F) && bananaCount >= 4)
        {
            UseBanana(4);
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


        if (boop)
        {
            boopTimer -= Time.deltaTime;

            if (boopTimer <= 0)
            {
                boopTimer = MAX_BOOP_TIME;
                boop = false;
            }
        }

        Flip();

        //Height check in map to kill the player if it goes below a specified height
        if (transform.position.y <= -500)

        {
            for (int i = health - 1; i >= 0; i--)
            {
                displayHealth[i].enabled = false;
            }

            health = 0;
            Debug.Log("You Are Dead");
            Die();
        }

        //DEV TOOL GOD MODE
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (SettingsManager.Instance != null)
            {
                SettingsManager.Instance.ToggleGodMode();
                Debug.Log("God Mode: " + (SettingsManager.Instance.GodModeEnabled ? "Enabled" : "Disabled"));
            }
        }
    }

   //MOVEMENT FUNCTIONS
   private void FixedUpdate()
   {
        if (!boop && !isShadowKicking)
        {
            if (groundPound)
            {
                //apply a force directly down and lock horizontal movement
                rb.velocity = new Vector2(rb.velocity.x, -50);
                if (IsGrounded())
                {
                    recoverTime += Time.deltaTime;
                    if (recoverTime > .5f)
                    {
                        SetAura(false);
                        recoverTime = 0;
                        groundPound = false;
                    }
                    anim.SetBool("Ground Pound", false);
                }
            }
            else if (isClimbing)
            {
                //apply the product of horizontal and speed to the players current velocity
                rb.velocity = new Vector2(horizontal * currentSpeed, vertical * currentSpeed);
            }
            else
            {
                Vector2 move = new Vector2(horizontal * currentSpeed, rb.velocity.y);
                //apply the product of horizontal and speed to the players current velocity
                rb.velocity = move;

                if (move.x != 0)
                {
                    anim.SetBool("Walking", true);
                }
                else
                {
                    anim.SetBool("Walking", false);
                }
            }
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
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);

            camFT.CallTurn();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vines")
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
            anim.SetBool("Climbing", false);
        }
    }

    //COMBAT FUNCTIONS
    //deal damage and knockback on each attack with the damage doubled every three hits
    private void BasicAttack()
    {
        comboCount++;

        if (comboCount == 3)
        {
            foreach (Collider2D col in HitRange())
            {
                if (HitRange() != null)
                {
                    BaseEnemy e_Ai = col.gameObject.GetComponent<BaseEnemy>();
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
                if (HitRange() != null)
                {
                    BaseEnemy e_Ai = col.gameObject.GetComponent<BaseEnemy>();
                    EnemyHealth e_Health = col.gameObject.GetComponent<EnemyHealth>();
                    Rigidbody2D e_Rigid = col.gameObject.GetComponent<Rigidbody2D>();

                    if (e_Ai != null)
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
            if (col.TryGetComponent<BaseEnemy>(out BaseEnemy e_Ai))
            {

                Rigidbody2D e_Rigid = col.gameObject.GetComponent<Rigidbody2D>();

                e_Ai.SetHit();
                e_Ai.SetPatrol(false);
                col.GetComponent<EnemyHealth>().Damage(2);

                Vector2 forceDir = this.transform.position - col.transform.position;
                forceDir.Normalize();
                Vector2 kickForce = new Vector2(-forceDir.x * kickKnockback * (Mathf.Abs(rb.velocity.x / 10) + 1), 5);
                e_Rigid.AddForce(kickForce, ForceMode2D.Impulse);
            }
        }
    }

    //Shadow Kick mechanic
    private void ShadowKick()
    {
        if (shadowKickPower < baseChargeTime) return;  
        //Determine the speed and distance based on charge level
        float kickDistance;
        float kickSpeed;
        if (shadowKickPower >= maxChargeTime)
        {
            kickDistance = maxKickDistance;
            kickSpeed = maxKickSpeed;
        }
        else
        {
            kickDistance = baseKickDistance;
            kickSpeed = baseKickSpeed;
        }

        //Used to give omnidirectionality
        Vector2 moveDirection = new Vector2(horizontal, vertical);

        //Check to see if the kick is going to be used with "facing" value or omnidirectionality
        if (moveDirection == Vector2.zero)
        {
            moveDirection = isFacingRight ? Vector2.right : Vector2.left;
        }
        else
        {
            moveDirection.Normalize();
        }

        isShadowKicking = true;
        anim.SetBool("Shadow Lunge", true);

        rb.velocity = moveDirection * kickSpeed;

        //Courotine handles the kick movement
        StartCoroutine(HandleShadowKickMovement(kickDistance));
    }

    private IEnumerator HandleShadowKickMovement(float kickDistance)
    {
        Vector2 startPos = transform.position;
        float distanceTraveled = 0f;

        //Used to keep track of enemies and not hit them multiple times
        HashSet<Collider2D> hitEnemies = new HashSet<Collider2D>();

        while (distanceTraveled < kickDistance)
        {
            //Check for enemies using the same method as other attacks
            foreach (Collider2D col in HitRange())
            {
                if (hitEnemies.Contains(col)) continue;

                if (col.TryGetComponent<BaseEnemy>(out BaseEnemy e_Ai))
                {
                    //Add enemy to hit list
                    hitEnemies.Add(col);

                    e_Ai.SetHit();
                    e_Ai.SetPatrol(false);
                    col.GetComponent<EnemyHealth>().Damage(4);

                    //Apply knockback to enemy
                    Vector2 kickForce = rb.velocity.normalized * shadowKickKnockback;
                    col.GetComponent<Rigidbody2D>().AddForce(kickForce, ForceMode2D.Impulse);
                }
            }

            //Update distance traveled
            distanceTraveled = Vector2.Distance(startPos, transform.position);

            yield return null;
        }

        rb.velocity = Vector2.zero;

        isShadowKicking = false;
        anim.SetBool("Shadow Lunge", false);
    }

    //Method to enable and disable Shadow Kick
    public void EnableShadowKick()
    {
        hasShadowKick = true;
    }

    //Method to enable and disable Ground Pound
    public void EnableGroundPound()
    {
        hasGroundPound = true;
    }

    //Method to enable and disable Banana Shield
    public void EnableBananaShield()
    {
        hasBananaShield = true;
    }

    //Method to enable and disable Charge
    public void EnableCharge()
    {
        hasCharge = true;
    }

    //Shoot method for Banana gun
    private void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            Instantiate(bulletprefab, firePoint.position, firePoint.rotation);
            nextFireTime = Time.time + fireRate;
        }

    }

    //Method to activate the banana Gun
    public void EnableBananaGun()
    {
        hasBananaGun = true;
    }

    //Throw Object Mechanic
    private void CheckPickupAndThrow()
    {
        //Throw
        if (isCarrying && Input.GetMouseButtonDown(0) || isCarrying && Input.GetKeyDown(KeyCode.J))
        {
            ThrowObject();
        }
        //Pickup
        else if (!isCarrying && Input.GetKeyDown(KeyCode.J))
        {
            TryPickupObject();
        }
    }

    private void TryPickupObject()
    {
        //Check for throwable objects in range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(check.position, pickupRange, throwableLayer);

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
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        return enemiesHit;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(check.position, pickupRange);

    }

    public void SetAura(bool a)
    {
        if (a)
        {
            aura = true;
        }
        else
        {
            aura = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" && aura)
        {
            BaseEnemy e_Ai = col.gameObject.GetComponent<BaseEnemy>();
            //EnemyHealth e_Health = col.gameObject.GetComponent<EnemyHealth>();
            Rigidbody2D e_Rigid = col.gameObject.GetComponent<Rigidbody2D>();

            e_Ai.SetHit();
            e_Ai.SetPatrol(false);

           if( col.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth h))
           {
                if (groundPound)
                {
                    h.Damage(5);
                }
                else
                {
                    h.Damage(4);
                    recoverTime = 0;
                }
           }
            

            //find the orientation of the hit enemy relative to the player
            Vector2 forceDir = this.transform.position - col.gameObject.transform.position;
            forceDir.Normalize();

            //using given direction change values to appropriate force
            Vector2 chargeForce = new Vector2(-forceDir.x * (chargeKnockback * (Mathf.Abs(rb.velocity.x / 10) + 1)), 2);

            e_Rigid.AddForce(chargeForce, ForceMode2D.Impulse);
        }
    }

    public bool GetGroundPound()
    {
        return groundPound;
    }


    //HEALTH MANAGMENT
    public void TakeDamage()
    {
        if (SettingsManager.Instance != null && SettingsManager.Instance.GodModeEnabled)
        {
            Debug.Log("God Mode: Damage prevented");
            return;
        }

        //Check if banana shield is active and able to use
        if (hasBananaShield && banananaShieldActive)
        {
            banananaShieldActive = false;
            shieldVisual.SetActive(false);
            UseBanana(2);
            Debug.Log("BLOCKED BY JAMES");
            return;
        }

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
            Die();
        }
    }

    public void GainHealth(int h)
    {
        health += h;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }

        displayHealth[health - 1].enabled = true;
    }

    //Death management logic
    public void Die()
    {
        GameManager.Instance.OnPLayerDeath();
        gameObject.SetActive(false);

    }

   //BANANA MANAGMENT
   private void UseBanana(int b)
   {
        //Infinite bananas if gode mode is active DEV TOOL
        if (SettingsManager.Instance != null && SettingsManager.Instance.GodModeEnabled)
        {
            for (int i = 0; i < b; i++)
            {
                displayBananas[bananaCount - 1 - i].enabled = false;
            }

            StartCoroutine(RefillBananaUI(b));
            return;
        }


        bananaCount -= b;

        for (int i = 0; i < b; i++)
        {
            displayBananas[bananaCount + i].enabled = false;
        }
    }

    public void GiveBanana(int b)
    {
        bananaCount += b;

        if (bananaCount > maxBananas)
        {
            bananaCount = maxBananas;
        }
        else
        {
            displayBananas[bananaCount - 1].enabled = true;
        }
    }

    //DEV TOOL used to refill bananas
    private IEnumerator RefillBananaUI(int count)
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < count; i++)
        {
            if (bananaCount - 1 - i >= 0 && bananaCount - 1 - i < displayBananas.Count)
                displayBananas[bananaCount - 1 - i].enabled = true;
        }
    }

    //Method to manage bananas
    private void InitializeBananaUI()
    {
        if (bananacoinUIprefab == null || emptyBananacoinPrefab == null || bananaUIParent == null)
        {
            Debug.LogWarning("Missing UI references in PlayerController");
            return;
        }

        foreach (Image img in displayBananas)
        {
            if (img != null)
                Destroy(img.gameObject);
        }
        displayBananas.Clear();

        float spacing = 40f;

        //Create UI elements for current max bananas
        for (int i = 0; i < maxBananas; i++)
        {
            //Empty banana slots
            GameObject emptySlot = Instantiate(emptyBananacoinPrefab, bananaUIParent);
            RectTransform emptyRect = emptySlot.GetComponent<RectTransform>();
            emptyRect.anchoredPosition = new Vector2(i * spacing, 0);
            emptySlot.GetComponent<Image>().raycastTarget = false;

            //Banana coins
            GameObject filledCoin = Instantiate(bananacoinUIprefab, bananaUIParent);
            RectTransform filledRect = filledCoin.GetComponent<RectTransform>();
            filledRect.anchoredPosition = new Vector2(i * spacing, 0);
            Image coinImage = filledCoin.GetComponent<Image>();
            coinImage.raycastTarget = false;

            displayBananas.Add(coinImage);

            coinImage.enabled = true;
        }
    }

    //Increase banana capacity when picking up the coin
    public void IncreaseMaxBananas()
    {
        BananaManager.IncreaseMaxBananas();
        maxBananas = BananaManager.MaxBananas;

        float spacing = 40f;

        GameObject emptySlot = Instantiate(emptyBananacoinPrefab, bananaUIParent);
        RectTransform emptyRect = emptySlot.GetComponent<RectTransform>();
        emptyRect.anchoredPosition = new Vector2((maxBananas - 1) * spacing, 0);
        emptySlot.GetComponent<Image>().raycastTarget = false;

        GameObject filledCoin = Instantiate(bananacoinUIprefab, bananaUIParent);
        RectTransform filledRect = filledCoin.GetComponent<RectTransform>();
        filledRect.anchoredPosition = new Vector2((maxBananas - 1) * spacing, 0);
        Image coinImage = filledCoin.GetComponent<Image>();
        coinImage.raycastTarget = false;

        displayBananas.Add(coinImage);
        displayBananas[maxBananas - 1].enabled = false;

        GiveBanana(1);
    }
}