using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboMonkeyAI : MonoBehaviour
{
    [Header("References")]
    private GameObject player;
    [SerializeField] private Rigidbody2D rb;
     [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement")]
    private Vector2 moveDir;
    [SerializeField] private float speed;
    private bool hit;
    [SerializeField] private float hitTimer;


    // Start is called before the first frame update
    void Awake()
    {
      player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
      moveDir = this.transform.position - player.transform.position;
      moveDir.Normalize();
        
      if(hit)
      {
        hitTimer -= Time.deltaTime;

        if(hitTimer <= 0)
        {
          hit = false;     
        }
      }
    }

    private void FixedUpdate()
   {   
      if(!hit && isGrounded())
      {
        rb.velocity = new Vector2(-moveDir.x * speed, rb.velocity.y);
      }         
   }

   public void setHit()
   {
     hit = !hit;
     hitTimer = .3f;
   }

   private bool isGrounded()
   {
      //Use groundCheck transform to check whether or not the enemy is touching the gorund
      return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
   }
}
