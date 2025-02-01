using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Movement")]
    private float horizontal;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    //Grace period where jump input is still registered after falling off a platform
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;
    //Grace period where jump input is still registered before touching a platform
    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;
    public bool isFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
       horizontal = Input.GetAxisRaw("Horizontal");

       //Jump
       if(isGrounded())
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

       if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
       {
          rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

          coyoteTimeCounter = 0f;
       }

       Flip();
    }

    private void FixedUpdate()
    {
        //apply the product of horizontal and speed to the players current velocity
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool isGrounded()
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


}
