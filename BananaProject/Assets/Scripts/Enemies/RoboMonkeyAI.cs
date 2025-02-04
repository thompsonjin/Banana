using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboMonkeyAI : MonoBehaviour
{
    [Header("References")]
    private GameObject player;
    [SerializeField] private Rigidbody2D rb;

    [Header("Movement")]
    private Vector2 moveDir;
    [SerializeField] private float speed;
    public bool hit;
    private int hitTimer = 10;


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
            hitTimer--;

            if(hitTimer <= 0)
            {
               hit = false;     
            }
        }
    }

    private void FixedUpdate()
   {   
      if(!hit)
      {
        rb.velocity = new Vector2(-moveDir.x * speed, rb.velocity.y);
      }         
   }

   public void setHit(bool b)
   {
     hit = b;
     hitTimer = 10;
   }
}
