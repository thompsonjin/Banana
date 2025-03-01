using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    public bool enemyKill;

    private float swimTimer;
    public float maxSwimTime;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            swimTimer = maxSwimTime;
        }

        if (collision.gameObject.tag == "Enemy" && enemyKill)
        {
            collision.gameObject.GetComponent<EnemyHealth>().Damage(10);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            swimTimer -= Time.deltaTime;

            if(swimTimer <= 0)
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage();
                swimTimer = maxSwimTime;
            }
        }
    }

}
