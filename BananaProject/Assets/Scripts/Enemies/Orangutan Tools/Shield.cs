using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{
    float hitTime;
    float hitTimeMax = 2;
    public Animator anim;
    public RoboOrangutan robo;

    private void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController.isShadowKicking)
            {
                playerController.CancelShadowKick();
            }

            if (hitTime <= 0)
            {
                anim.SetBool("Attack", true);
                playerController.TakeDamage();
                hitTime = hitTimeMax;
                robo.chaseSpeed = 0;
            }
        }
    }

    private void Update()
    {
        if(hitTime < 0)
        {
            anim.SetBool("Attack", false);
            robo.chaseSpeed = 3;
            
        }
        else
        {
            hitTime -= Time.deltaTime;
        }
    }
}
