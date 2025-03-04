using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarambeLaserBehaviour : MonoBehaviour
{
    bool canMove = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }

        if (collision.gameObject.tag == "Wall")
        {
            canMove = false;
            Debug.Log("stop");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
           
    }

    public void Fire(Vector3 scale, Vector3 pos)
    {
        if (canMove)
        {
            transform.localScale -= scale;
            transform.position -= pos;
        }     
    }
}
