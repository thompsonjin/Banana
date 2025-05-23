using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class RoboChimp : BaseEnemy
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public Animator anim;

    [Header("Movement")]
    public bool isFacingRight;

    [Header("Combat")]
    private const float PLAYER_HIT_TIME = 2.5f;
    private float playerHitTimer;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private bool spray;

    // Start is called before the first frame update
    void Awake()
    {
        Flip();
        playerHitTimer = PLAYER_HIT_TIME;
        spray = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!spray)
        {
            anim.SetBool("Shoot", false);
            playerHitTimer -= Time.deltaTime;

            if (playerHitTimer <= 0)
            {
                Instantiate(projectile, projectileSpawn);
                spray = true;
            }

        }
        else
        {
            anim.SetBool("Shoot", true);
            playerHitTimer += Time.deltaTime;

            if (playerHitTimer >= PLAYER_HIT_TIME)
            {
                Destroy(projectileSpawn.transform.GetChild(0).gameObject);
                spray = false;
            }
        }

        if (IsGrounded() && !hit)
        {
            rb.velocity = Vector3.zero;
        }

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
    }

    private void Flip()
    {
        //Rotate the character along the y axis based on the last horizontal input of the player
        if (!isFacingRight)
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }
    private bool IsGrounded()
    {
        //Use groundCheck transform to check whether or not the enemy is touching the gorund
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
