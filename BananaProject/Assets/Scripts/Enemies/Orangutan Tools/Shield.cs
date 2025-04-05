using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private bool hit;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitTime <= 0)
        {
            anim.SetBool("Attack", true);
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }

    private void Update()
    {
        hit = h;
    }
}
