using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class RoboProposcis : BaseEnemy
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private GameObject player;

    [Header("Movement")]
    private Vector2 moveDir;
    private bool isFacingRight;

    [Header("Combat")]
    [SerializeField] private LayerMask playerLayer;
    private const float PLAYER_HIT_TIME = 3f;
    private float playerHitTimer;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawn;

    [Header("Sound")]
    [SerializeField] private AudioSource blast;

    // Start is called before the first frame update
    void Awake()
    {
        playerHitTimer = PLAYER_HIT_TIME;
        player = GameObject.Find("Player");
        patrol = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!patrol)
        {
            moveDir = this.transform.position - player.transform.position;
            moveDir.Normalize();

            playerHitTimer -= Time.deltaTime;

            if(playerHitTimer <= 0)
            {
                blast.Play();
                Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
                playerHitTimer = PLAYER_HIT_TIME;
            }
        }

        //track how long the enemy is stunned by the hit 
        if (hit)
        {
            hitTimer -= Time.deltaTime;

            if (hitTimer <= 0)
            {
                hit = false;
            }
        }

        if (IsGrounded() && !hit)
        {
            rb.velocity = Vector3.zero;
        }

        Flip();
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

    private bool IsGrounded()
    {
        //Use groundCheck transform to check whether or not the enemy is touching the gorund
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
