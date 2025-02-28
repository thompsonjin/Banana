using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private bool hit;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hit)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }

    public void SetHit(bool h)
    {
        hit = h;
    }
}
