using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    public bool enemyKill;
    public bool instant;

    private float swimTimer;
    public float maxSwimTime;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!instant)
            {
                swimTimer = maxSwimTime;     
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage();
            }
           
        }

        if (collision.gameObject.tag == "Enemy" && enemyKill)
        {
            
            if(collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth component))
            {
                collision.gameObject.GetComponent<EnemyHealth>().Damage(10);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!instant)
            {
                collision.gameObject.GetComponent<PlayerController>().swim = true;
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3.5f;
                swimTimer -= Time.deltaTime;

                if (swimTimer <= 0)
                {
                    collision.gameObject.GetComponent<PlayerController>().TakeDamage();
                    swimTimer = maxSwimTime;
                }
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!instant)
            {
                collision.gameObject.GetComponent<PlayerController>().swim = false;
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 7;
            }          
        }
    }

}
